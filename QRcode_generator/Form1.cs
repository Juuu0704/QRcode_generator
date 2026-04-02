using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;

/* ----------------------------------------------------------a ajouter --------------------------------------------------------*/
using QRCoder;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting.Channels;

namespace QRcode_generator
{
    public partial class Form1 : Form
    {
        PrintDocument printDocument = new PrintDocument();
        PrintPreviewDialog previewDialog = new PrintPreviewDialog();
        PrintDialog printDialog1 = new PrintDialog();
        PrintDocument printDocument1 = new PrintDocument();

        public Form1()
        {
            InitializeComponent();
            printDocument.PrintPage += PrintDocument_PrintPage;
            previewDialog.Document = printDocument;
            printDocument1.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);

        }

        public static string GetTempFileName()
        {
            return System.IO.Path.GetTempFileName();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            /*---------------------penser à changer le nom de la VARIABLE qui va s'afficher--------------------------------------*/
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(textBox1.Text, QRCodeGenerator.ECCLevel.L, true);
            QRCode qrCode = new QRCode(qrCodeData);

            Bitmap qrCodeImage = qrCode.GetGraphic(5);
            QRpicture.Image = qrCodeImage;
        }
        public void ImageViewer(string path)
        {
            Process.Start("explorer.exe", path);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (QRpicture.Image != null)
            {
                int size = 200;

                int x = (e.PageBounds.Width - size) / 2;
                int y = (e.PageBounds.Height - size) / 2;

                e.Graphics.DrawImage(QRpicture.Image, x, y, size, size);

                //Dessine la bordure autour du QR Code
                Pen pen = new Pen(Color.Black, 2);     //couleur et épaisseur
                e.Graphics.DrawRectangle(pen, x, y, size, size);

                //Définit la police
                Font font = new Font("Arial", 12);
                //Mesurer la largeur du texte
                SizeF textSize = e.Graphics.MeasureString(textBox1.Text, font);
                //Calculer le X centré par rapport au QR Code
                float textX = x + (size - textSize.Width) / 2;
                //Rapprocher le texte du QR Code (5 au lieu de 10)
                float textY = y + size + 5;

                e.Graphics.DrawString(textBox1.Text, new Font("Arial",12), Brushes.Black, textX, textY );


            }
        }

        private void BtnAperçu_Click(object sender, EventArgs e)
        {
            if (QRpicture.Image == null)
            {
                MessageBox.Show("il faut générer un QR Code avant d'afficher l'aperçu !");
                return;
            }

            PrintDialog printDialog1 = new PrintDialog();
            printDialog1.Document = printDocument1;
            DialogResult result = printDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                printDocument1.Print();
            }


            previewDialog.Width = 800;
            previewDialog.Height = 600;

            previewDialog.ShowDialog();
        }
    }
}
