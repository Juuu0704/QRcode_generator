using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

/* ----------------------------------------------------------à télécharger --------------------------------------------------------*/
using QRCoder;          //Projet -> Manage NuGet Packages -> Browse -> QRCoder -> Install

namespace QRcode_generator
{
    public partial class Form1 : Form
    {
        //Déclaration des objets nécessaires pour l'impression
        PrintDocument printDocument = new PrintDocument();                  //Document d'impression
        PrintPreviewDialog previewDialog = new PrintPreviewDialog();        //Boîte de dialogue pour l'aperçu avant impression
        PrintDialog printDialog1 = new PrintDialog();                       //Boîte de dialogue pour la sélection de l'imprimante
        PrintDocument printDocument1 = new PrintDocument();                 //Document d'impression pour la boîte de dialogue d'impression

        public Form1()
        {
            InitializeComponent();                                          // Initialise les composants visuels générés par le designer

            // Abonne l'événement PrintPage des deux documents au même gestionnaire
            // Cela définit ce qui sera dessiné sur la page lors de l'impression
            printDocument.PrintPage += PrintDocument_PrintPage;
            previewDialog.Document = printDocument;                         // Associe le document d'impression à la boîte de dialogue d'aperçu
            printDocument1.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);         // Associe le même gestionnaire d'événements au document utilisé pour la boîte de dialogue d'impression

        }

        // Clé secrète (à stocker ailleurs en prod)
        static string secretKey = "CLEE_VR@1M3NT_SECRETE";

        static string Sign(string data, string secretKey)                         //!!!!!!!!!!!!!!!!!!!!!!changer secretKey (dans un fichier pour plus de sécurité)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data))
                    throw new ArgumentException("Data invalide.");

                if (string.IsNullOrWhiteSpace(secretKey))                  //!!!!!!!!!!!!!!!!!!!!!!changer secretKey (dans un fichier pour plus de sécurité)
                    throw new ArgumentException("Clé secrète invalide.");

                var keyBytes = Encoding.UTF8.GetBytes(secretKey);          //!!!!!!!!!!!!!!!!!!!!!!changer secretKey (dans un fichier pour plus de sécurité)
                var dataBytes = Encoding.UTF8.GetBytes(data);

                using (var hmac = new HMACSHA256(keyBytes))
                {
                    var hash = hmac.ComputeHash(dataBytes);
                    return Convert.ToBase64String(hash);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la signature : {ex.Message}");
                return null; // ou throw;
            }
        }

        static string CreateCard(string cardid, string version, string secretKey)          //!!!!!!!!!!!!!!!!!!!!!!changer secretKey (dans un fichier pour plus de sécurité)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cardid))
                    throw new ArgumentException("Card ID invalide.");

                if (string.IsNullOrWhiteSpace(version))
                    throw new ArgumentException("Version invalide.");

                string payload = cardid + ";" + version;

                string signature = Sign(payload, secretKey);          //!!!!!!!!!!!!!!!!!!!!!!changer secretKey (dans un fichier pour plus de sécurité)

                if (signature == null)
                    throw new Exception("Échec de la signature.");

                return payload + ";" + signature;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la création de la carte : {ex.Message}");
                return null;
            }
        }

        public static string GetTempFileName()              //Méthode pour générer un nom de fichier temporaire
        {
            return System.IO.Path.GetTempFileName();        // Génère un nom de fichier temporaire unique et retourne son chemin
        }



/*---------------------------------------------------------------------------------VARIABLE A MODIFIER------------------------------------------------------------------------------------------------------------------------*/
        string variable = CreateCard("123456789", "1", secretKey);    //"123456789" = identifiant ou ISBN ; "1" = n° de version ; secretKey = clé de chiffrement
        



        
        //Gestionnaire d'événement pour le clic sur le bouton de génération du QR Code
        private void button1_Click(object sender, EventArgs e)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();            // Crée une instance du générateur de QR Code

            /*---------------------penser à changer le nom de la VARIABLE qui va s'afficher--------------------------------------*/
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(variable, QRCodeGenerator.ECCLevel.L, true);      // Génère les données du QR Code à partir du texte saisi dans textBox1, avec un niveau de correction d'erreur L (Low) et en utilisant le mode de rendu SVG
            QRCode qrCode = new QRCode(qrCodeData);                         // Crée une instance du QRCode à partir des données générées

            Bitmap qrCodeImage = qrCode.GetGraphic(5);                      // Génère une image Bitmap du QR Code avec une taille de module de 5 pixels
            QRpicture.Image = qrCodeImage;                                  // Affiche l'image du QR Code dans un contrôle PictureBox nommé QRpicture
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)       // Gestionnaire d'événement pour dessiner le contenu à imprimer sur la page
        {
            if (QRpicture.Image != null)                                    // Vérifie si une image de QR Code est présente dans le contrôle PictureBox
            {
                int size = 80;                                             // Taille souhaitée pour le QR Code à imprimer (80x80 pixels)

                int x = (e.PageBounds.Width - size) / 50;                    // Calcule la position X pour centrer le QR Code horizontalement sur la page
                int y = (e.PageBounds.Height - size) / 3;                   // Calcule la position Y pour centrer le QR Code verticalement sur la page

                e.Graphics.DrawImage(QRpicture.Image, x, y, size, size);    // Dessine l'image du QR Code à la position calculée avec la taille spécifiée

                //Dessine la bordure autour du QR Code
                Pen pen = new Pen(Color.Black, 2);     //couleur et épaisseur
                e.Graphics.DrawRectangle(pen, x, y, size, size);

                
                Font font = new Font("Arial", 9);                                      //Définit la police
                
                SizeF textSize = e.Graphics.MeasureString(textBox1.Text, font);         //Mesurer la largeur du texte
                
                float textX = x + (size - textSize.Width) / 50;                          //Calculer le X centré par rapport au QR Code
                
                float textY = y + size + 5;                                             //Rapprocher le texte du QR Code (5 au lieu de 10)

                string[] id = variable.Split(';');

                e.Graphics.DrawString(id[0], new Font("Arial",9), Brushes.Black, textX, textY );       //Dessine le texte centré sous le QR Code
            }
        }

        private void BtnAperçu_Click(object sender, EventArgs e)                // Gestionnaire d'événement pour le clic sur le bouton d'aperçu avant impression
        {
            if (QRpicture.Image == null)                                        // Vérifie si une image de QR Code est présente dans le contrôle PictureBox
            {
                MessageBox.Show("il faut générer un QR Code avant d'afficher l'aperçu !");          // Affiche un message d'erreur si aucun QR Code n'est généré
                return;                                                        // Sort de la méthode pour éviter d'afficher l'aperçu
            }

            PrintDialog printDialog1 = new PrintDialog();                       // Crée une nouvelle instance de la boîte de dialogue d'impression
            printDialog1.Document = printDocument1;                             // Associe le document d'impression à la boîte de dialogue d'impression
            printDocument.DefaultPageSettings = printDialog1.PrinterSettings.DefaultPageSettings;        // Définit les paramètres de page par défaut du document d'impression en fonction des paramètres de l'imprimante sélectionnée dans la boîte de dialogue d'impression
            //DialogResult result = printDialog1.ShowDialog();                    // Affiche la boîte de dialogue d'impression et stocke le résultat (OK ou Annuler)
            previewDialog.Width = 800;                                          // Définit la largeur de la boîte de dialogue d'aperçu
            previewDialog.Height = 600;                                         // Définit la hauteur de la boîte de dialogue d'aperçu

            previewDialog.ShowDialog();                                         // Affiche la boîte de dialogue d'aperçu avant impression
        }
    }
}
