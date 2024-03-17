namespace ImageDownsizer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            b_selectImage = new Button();
            b_downscale = new Button();
            tb_dsFactor = new TrackBar();
            n_dsFactor = new NumericUpDown();
            label1 = new Label();
            b_parallelDownscale = new Button();
            l_dsResults = new Label();
            l_pdsResults = new Label();
            l_imagePath = new Label();
            pb_selectedImagePreview = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)tb_dsFactor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)n_dsFactor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pb_selectedImagePreview).BeginInit();
            SuspendLayout();
            // 
            // b_selectImage
            // 
            b_selectImage.Location = new Point(12, 12);
            b_selectImage.Name = "b_selectImage";
            b_selectImage.Size = new Size(180, 34);
            b_selectImage.TabIndex = 0;
            b_selectImage.Text = "Select Image";
            b_selectImage.UseVisualStyleBackColor = true;
            b_selectImage.Click += b_selectImage_Click;
            // 
            // b_downscale
            // 
            b_downscale.Enabled = false;
            b_downscale.Location = new Point(12, 207);
            b_downscale.Name = "b_downscale";
            b_downscale.Size = new Size(180, 34);
            b_downscale.TabIndex = 1;
            b_downscale.Text = "Downscale";
            b_downscale.UseVisualStyleBackColor = true;
            b_downscale.Click += b_downscale_Click;
            // 
            // tb_dsFactor
            // 
            tb_dsFactor.Location = new Point(12, 106);
            tb_dsFactor.Maximum = 9995;
            tb_dsFactor.Minimum = 5;
            tb_dsFactor.Name = "tb_dsFactor";
            tb_dsFactor.Size = new Size(864, 69);
            tb_dsFactor.TabIndex = 2;
            tb_dsFactor.TickStyle = TickStyle.None;
            tb_dsFactor.Value = 5;
            tb_dsFactor.ValueChanged += tb_dsFactor_ValueChanged;
            // 
            // n_dsFactor
            // 
            n_dsFactor.DecimalPlaces = 2;
            n_dsFactor.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            n_dsFactor.Location = new Point(12, 154);
            n_dsFactor.Maximum = new decimal(new int[] { 9995, 0, 0, 131072 });
            n_dsFactor.Minimum = new decimal(new int[] { 5, 0, 0, 131072 });
            n_dsFactor.Name = "n_dsFactor";
            n_dsFactor.Size = new Size(180, 31);
            n_dsFactor.TabIndex = 3;
            n_dsFactor.Value = new decimal(new int[] { 5, 0, 0, 131072 });
            n_dsFactor.ValueChanged += n_dsFactor_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 67);
            label1.Name = "label1";
            label1.Size = new Size(194, 25);
            label1.TabIndex = 4;
            label1.Text = "Downscaling factor (%)";
            // 
            // b_parallelDownscale
            // 
            b_parallelDownscale.Enabled = false;
            b_parallelDownscale.Location = new Point(222, 207);
            b_parallelDownscale.Name = "b_parallelDownscale";
            b_parallelDownscale.Size = new Size(180, 34);
            b_parallelDownscale.TabIndex = 5;
            b_parallelDownscale.Text = "Parallel Downscale";
            b_parallelDownscale.UseVisualStyleBackColor = true;
            b_parallelDownscale.Click += b_parallelDownscale_Click;
            // 
            // l_dsResults
            // 
            l_dsResults.AutoSize = true;
            l_dsResults.Location = new Point(12, 258);
            l_dsResults.Name = "l_dsResults";
            l_dsResults.Size = new Size(0, 25);
            l_dsResults.TabIndex = 6;
            // 
            // l_pdsResults
            // 
            l_pdsResults.AutoSize = true;
            l_pdsResults.Location = new Point(222, 258);
            l_pdsResults.Name = "l_pdsResults";
            l_pdsResults.Size = new Size(0, 25);
            l_pdsResults.TabIndex = 7;
            // 
            // l_imagePath
            // 
            l_imagePath.AutoSize = true;
            l_imagePath.Location = new Point(222, 17);
            l_imagePath.Name = "l_imagePath";
            l_imagePath.Size = new Size(159, 25);
            l_imagePath.TabIndex = 8;
            l_imagePath.Text = "No image selected";
            // 
            // pb_selectedImagePreview
            // 
            pb_selectedImagePreview.BackColor = Color.Black;
            pb_selectedImagePreview.Location = new Point(12, 286);
            pb_selectedImagePreview.Name = "pb_selectedImagePreview";
            pb_selectedImagePreview.Size = new Size(864, 486);
            pb_selectedImagePreview.SizeMode = PictureBoxSizeMode.Zoom;
            pb_selectedImagePreview.TabIndex = 9;
            pb_selectedImagePreview.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(899, 793);
            Controls.Add(pb_selectedImagePreview);
            Controls.Add(l_imagePath);
            Controls.Add(l_pdsResults);
            Controls.Add(l_dsResults);
            Controls.Add(b_parallelDownscale);
            Controls.Add(label1);
            Controls.Add(n_dsFactor);
            Controls.Add(tb_dsFactor);
            Controls.Add(b_downscale);
            Controls.Add(b_selectImage);
            MaximizeBox = false;
            MaximumSize = new Size(921, 849);
            MinimumSize = new Size(921, 309);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)tb_dsFactor).EndInit();
            ((System.ComponentModel.ISupportInitialize)n_dsFactor).EndInit();
            ((System.ComponentModel.ISupportInitialize)pb_selectedImagePreview).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button b_selectImage;
        private Button b_downscale;
        private TrackBar tb_dsFactor;
        private NumericUpDown n_dsFactor;
        private Label label1;
        private Button b_parallelDownscale;
        private Label l_dsResults;
        private Label l_pdsResults;
        private Label l_imagePath;
        private PictureBox pb_selectedImagePreview;
    }
}
