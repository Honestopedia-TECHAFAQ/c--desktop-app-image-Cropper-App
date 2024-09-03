using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageCropperApp
{
    public partial class ImageCard : UserControl
    {
        public event EventHandler CropButtonClicked;
        public PictureBox PictureBox { get; private set; }
        private Button btnCrop;
        private CheckBox checkBoxSelect;
        private string imagePath;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public ImageCard()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
            InitializeComponent();
        }

        public void SetImage(string path)
        {
            imagePath = path;
            PictureBox.Image = Image.FromFile(path);
        }

        public string GetImagePath()
        {
            return imagePath;
        }

        private void btnCrop_Click(object sender, EventArgs e)
        {
            CropButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        private void InitializeComponent()
        {
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.btnCrop = new System.Windows.Forms.Button();
            this.checkBoxSelect = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            this.SuspendLayout();

            // PictureBox
            this.PictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureBox.Location = new System.Drawing.Point(3, 3);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(160, 90);
            this.PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBox.TabIndex = 0;
            this.PictureBox.TabStop = false;

            // btnCrop
            this.btnCrop.Location = new System.Drawing.Point(3, 99);
            this.btnCrop.Name = "btnCrop";
            this.btnCrop.Size = new System.Drawing.Size(80, 23);
            this.btnCrop.TabIndex = 1;
            this.btnCrop.Text = "Crop";
            this.btnCrop.UseVisualStyleBackColor = true;
            this.btnCrop.Click += new System.EventHandler(this.btnCrop_Click);

            // checkBoxSelect
            this.checkBoxSelect.Location = new System.Drawing.Point(86, 99);
            this.checkBoxSelect.Name = "checkBoxSelect";
            this.checkBoxSelect.Size = new System.Drawing.Size(77, 24);
            this.checkBoxSelect.TabIndex = 2;
            this.checkBoxSelect.Text = "Select";
            this.checkBoxSelect.UseVisualStyleBackColor = true;

            // ImageCard
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxSelect);
            this.Controls.Add(this.btnCrop);
            this.Controls.Add(this.PictureBox);
            this.Name = "ImageCard";
            this.Size = new System.Drawing.Size(166, 125);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
