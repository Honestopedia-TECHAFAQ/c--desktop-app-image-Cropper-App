using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace ImageCropperApp
{
    public partial class CropForm : Form
    {
        private Rectangle cropArea;
        private ImageCard imageCard;
        private bool isDragging = false;
        private float zoomFactor = 1.0f;

        public CropForm(ImageCard imageCard)
        {
            InitializeComponent();
            this.imageCard = imageCard;
            LoadImage();
        }

        private void LoadImage()
        {
            try
            {
                string imagePath = imageCard.GetImagePath();
                if (File.Exists(imagePath))
                {
                    using (var tempImage = Image.FromFile(imagePath))
                    {
                        // Dispose of any existing image to free memory
                        pictureBox.Image?.Dispose();
                        pictureBox.Image = new Bitmap(tempImage);
                        pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
                else
                {
                    MessageBox.Show("Image file not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading the image: " + ex.Message);
            }
        }

        private void btnCrop_Click(object sender, EventArgs e)
        {
            if (cropArea == Rectangle.Empty || cropArea.Width <= 0 || cropArea.Height <= 0)
            {
                MessageBox.Show("Please define a valid crop area.");
                return;
            }

            string imagePath = imageCard.GetImagePath();
            try
            {
                using (var originalImage = new Bitmap(imagePath))
                {
                    using (var resizedImage = ResizeImageIfNeeded(originalImage))
                    {
                        // Calculate the crop rectangle on the resized image
                        var adjustedCropArea = GetAdjustedCropArea(cropArea, resizedImage);

                        using (var croppedImage = CropImage(resizedImage, adjustedCropArea))
                        {
                            ShowCropPreview(croppedImage);

                            // Ask user for confirmation
                            DialogResult result = MessageBox.Show("Do you want to save the cropped image?", "Confirm", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                SaveCroppedImage(croppedImage);
                            }
                        }
                    }
                }

                MessageBox.Show("Image cropped and saved successfully!");
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show("Not enough memory to crop the image. Try a smaller image.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

            this.Close();
        }

        private Bitmap ResizeImageIfNeeded(Bitmap image)
        {
            const int maxWidth = 2000;
            const int maxHeight = 2000;

            if (image.Width > maxWidth || image.Height > maxHeight)
            {
                float ratioX = (float)maxWidth / image.Width;
                float ratioY = (float)maxHeight / image.Height;
                float ratio = Math.Min(ratioX, ratioY);

                int newWidth = (int)(image.Width * ratio);
                int newHeight = (int)(image.Height * ratio);

                zoomFactor = (float)newWidth / image.Width; // Update zoom factor

                return new Bitmap(image, new Size(newWidth, newHeight));
            }

            return new Bitmap(image);
        }

        private Rectangle GetAdjustedCropArea(Rectangle cropArea, Bitmap resizedImage)
        {
            // Get the size of the PictureBox image
            Size pictureBoxSize = pictureBox.ClientSize;
            Size imageSize = pictureBox.Image.Size;

            // Calculate scale factors
            float scaleX = (float)imageSize.Width / pictureBoxSize.Width;
            float scaleY = (float)imageSize.Height / pictureBoxSize.Height;

            // Adjust the crop rectangle
            int adjustedX = (int)(cropArea.X * scaleX);
            int adjustedY = (int)(cropArea.Y * scaleY);
            int adjustedWidth = (int)(cropArea.Width * scaleX);
            int adjustedHeight = (int)(cropArea.Height * scaleY);

            // Ensure crop area is within bounds
            if (adjustedX < 0) adjustedX = 0;
            if (adjustedY < 0) adjustedY = 0;
            if (adjustedX + adjustedWidth > resizedImage.Width) adjustedWidth = resizedImage.Width - adjustedX;
            if (adjustedY + adjustedHeight > resizedImage.Height) adjustedHeight = resizedImage.Height - adjustedY;

            return new Rectangle(adjustedX, adjustedY, adjustedWidth, adjustedHeight);
        }

        private Bitmap CropImage(Bitmap image, Rectangle cropArea)
        {
            // Ensure crop area is within image bounds
            if (cropArea.X < 0) cropArea.X = 0;
            if (cropArea.Y < 0) cropArea.Y = 0;
            if (cropArea.Right > image.Width) cropArea.Width = image.Width - cropArea.X;
            if (cropArea.Bottom > image.Height) cropArea.Height = image.Height - cropArea.Y;

            try
            {
                return image.Clone(cropArea, image.PixelFormat);
            }
            catch (OutOfMemoryException)
            {
                throw; // Allow the outer catch block to handle this exception
            }
        }

        private void ShowCropPreview(Bitmap croppedImage)
        {
            using (Form previewForm = new Form())
            {
                previewForm.Text = "Crop Preview";
                PictureBox previewPictureBox = new PictureBox
                {
                    Image = new Bitmap(croppedImage),
                    Dock = DockStyle.Fill,
                    SizeMode = PictureBoxSizeMode.Zoom
                };
                previewForm.Controls.Add(previewPictureBox);
                previewForm.ShowDialog();
            }
        }

        private void SaveCroppedImage(Bitmap croppedImage)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "JPEG Image|*.jpg";
                sfd.FileName = Path.GetFileNameWithoutExtension(imageCard.GetImagePath()) + "_cropped.jpg";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    croppedImage.Save(sfd.FileName, ImageFormat.Jpeg);
                }
            }
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                cropArea = new Rectangle(e.X, e.Y, 0, 0);
                pictureBox.Invalidate();
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                cropArea.Width = e.X - cropArea.X;
                cropArea.Height = e.Y - cropArea.Y;
                pictureBox.Invalidate();
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false;
                if (cropArea.Width < 0)
                {
                    cropArea.X += cropArea.Width;
                    cropArea.Width = -cropArea.Width;
                }
                if (cropArea.Height < 0)
                {
                    cropArea.Y += cropArea.Height;
                    cropArea.Height = -cropArea.Height;
                }
                pictureBox.Invalidate();
            }
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (cropArea != Rectangle.Empty)
            {
                e.Graphics.DrawRectangle(Pens.Red, cropArea);
            }
        }
    }
}
