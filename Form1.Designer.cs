
namespace ImageCropperApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Button btnSelectImages;
        private System.Windows.Forms.Button btnCropImages;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSelectImages = new System.Windows.Forms.Button();
            this.btnCropImages = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // flowLayoutPanel
            this.flowLayoutPanel.AutoScroll = true;
            this.flowLayoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(760, 420);
            this.flowLayoutPanel.TabIndex = 0;

            // btnSelectImages
            this.btnSelectImages.Location = new System.Drawing.Point(12, 450);
            this.btnSelectImages.Name = "btnSelectImages";
            this.btnSelectImages.Size = new System.Drawing.Size(120, 40);
            this.btnSelectImages.TabIndex = 1;
            this.btnSelectImages.Text = "Select Images";
            this.btnSelectImages.UseVisualStyleBackColor = true;
            this.btnSelectImages.Click += new System.EventHandler(this.btnSelectImages_Click);

            // btnCropImages
            this.btnCropImages.Location = new System.Drawing.Point(652, 450);
            this.btnCropImages.Name = "btnCropImages";
            this.btnCropImages.Size = new System.Drawing.Size(120, 40);
            this.btnCropImages.TabIndex = 2;
            this.btnCropImages.Text = "Crop Images";
            this.btnCropImages.UseVisualStyleBackColor = true;
            this.btnCropImages.Click += new System.EventHandler(this.btnCropImages_Click);

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 501);
            this.Controls.Add(this.btnCropImages);
            this.Controls.Add(this.btnSelectImages);
            this.Controls.Add(this.flowLayoutPanel);
            this.Name = "Form1";
            this.Text = "Image Cropper";
            this.ResumeLayout(false);
        }

        private void btnCropImages_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
