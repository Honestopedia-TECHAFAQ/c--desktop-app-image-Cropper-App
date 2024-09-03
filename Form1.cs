using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ImageCropperApp
{
    public partial class Form1 : Form
    {
        private List<ImageCard> imageCards;
        private Rectangle cropArea;
        private Point startPoint;
        private ImageCard? currentImageCard;

        public Form1()
        {
            InitializeComponent();
            imageCards = new List<ImageCard>();
            cropArea = Rectangle.Empty;
            currentImageCard = null;
        }

        private void btnSelectImages_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = true;
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    imageCards.Clear();
                    flowLayoutPanel.Controls.Clear();

                    foreach (var path in ofd.FileNames)
                    {
                        var imageCard = new ImageCard();
                        imageCard.SetImage(path);
                        imageCard.CropButtonClicked += ImageCard_CropButtonClicked;
                        flowLayoutPanel.Controls.Add(imageCard);
                        imageCards.Add(imageCard);
                    }
                }
            }
        }

        private void ImageCard_CropButtonClicked(object sender, EventArgs e)
        {
            var card = sender as ImageCard;
            if (card == null) return;

            currentImageCard = card;

            // Trigger the crop functionality
            using (var cropForm = new CropForm(currentImageCard))
            {
                cropForm.ShowDialog();
            }
        }
    }
}
