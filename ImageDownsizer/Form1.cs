using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ImageDownsizer
{
    public partial class Form1 : Form
    {
        public string? SelectedImage { get; private set; }
        public byte[] ImagePixelData { get; private set; }
        public byte[] DownsizedImagePixelData { get; private set; }
        public int ImageWidth { get; private set; }
        public int ImageHeight { get; private set; }
        public int ImageBytesPerPixel { get; private set; }
        public PixelFormat ImagePixelFormat { get; private set; }
        public Bitmap ResultBitmap { get; private set; }
        public int NewImageWidth { get; private set; }
        public int NewImageHeight { get; private set; }

        private string ResultTimeFormat = "mm':'ss'.'fffffff";

        public Form1()
        {
            InitializeComponent();

            n_dsFactor.Value = 0.5m;
            tb_dsFactor.Value = 5000;
        }

        private void tb_dsFactor_ValueChanged(object sender, EventArgs e)
        {
            n_dsFactor.Value = tb_dsFactor.Value / 100m;
        }

        private void n_dsFactor_ValueChanged(object sender, EventArgs e)
        {
            tb_dsFactor.Value = (int)(n_dsFactor.Value * 100);
        }

        private void b_selectImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Image files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp";

            ofd.Multiselect = false;
            ofd.CheckFileExists = true;
            ofd.FileOk += SelectImageFile_FileOk;

            ofd.ShowDialog();
        }

        private void SelectImageFile_FileOk(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            this.SelectedImage = ((OpenFileDialog?)sender)!.FileName;

            l_imagePath.Text = this.SelectedImage ?? "No image selected";
            pb_selectedImagePreview.ImageLocation = this.SelectedImage;

            b_downscale.Enabled = true;
            b_parallelDownscale.Enabled = true;
        }

        private void b_downscale_Click(object sender, EventArgs e)
        {
            if (this.SelectedImage is null || this.SelectedImage.Trim() == "")
            {
                return;
            }

            var fileInfo = new FileInfo(this.SelectedImage);
            var sfd = new SaveFileDialog();
            sfd.Filter = $"{fileInfo.Extension.ToUpper()[1..]} file|*{fileInfo.Extension}";
            sfd.AddExtension = true;
            sfd.CheckPathExists = true;
            sfd.DefaultExt = fileInfo.Extension;
            sfd.InitialDirectory = fileInfo.DirectoryName;
            sfd.FileName = Path.GetFileNameWithoutExtension(fileInfo.Name) + $"@{this.n_dsFactor.Value / 100m:F2}x{fileInfo.Extension}";

            sfd.OverwritePrompt = true;

            if (sfd.ShowDialog() is DialogResult.OK or DialogResult.Continue)
            {
                var savePath = sfd.FileName;

                var image = new Bitmap(this.SelectedImage);

                PrepareForDownsizing(image);

                var stopwatchTimer = Stopwatch.StartNew();

                for (int x = 0; x < this.NewImageWidth; x++)
                {
                    for (int y = 0; y < this.NewImageHeight; y++)
                    {
                        var srcX = (int)(x / (this.n_dsFactor.Value / 100m));
                        var srcY = (int)(y / (this.n_dsFactor.Value / 100m));
                        srcX = Math.Min(srcX, image.Width);
                        srcY = Math.Min(srcY, image.Height);

                        SetPixel(x, y, GetPixel(srcX, srcY));
                    }
                }

                stopwatchTimer.Stop();

                l_dsResults.Text = "It took " + stopwatchTimer.Elapsed.ToString(this.ResultTimeFormat);

                SaveImage(savePath);
            }
        }

        private void b_parallelDownscale_Click(object sender, EventArgs e)
        {
            if (this.SelectedImage is null || this.SelectedImage.Trim() == "")
            {
                return;
            }

            var fileInfo = new FileInfo(this.SelectedImage);
            var sfd = new SaveFileDialog();
            sfd.Filter = $"{fileInfo.Extension.ToUpper()[1..]} file|*{fileInfo.Extension}";
            sfd.AddExtension = true;
            sfd.CheckPathExists = true;
            sfd.DefaultExt = fileInfo.Extension;
            sfd.InitialDirectory = fileInfo.DirectoryName;
            sfd.FileName = Path.GetFileNameWithoutExtension(fileInfo.Name) + $"@{this.n_dsFactor.Value / 100m:F2}x{fileInfo.Extension}";

            sfd.OverwritePrompt = true;

            if (sfd.ShowDialog() is DialogResult.OK or DialogResult.Continue)
            {
                var savePath = sfd.FileName;

                var image = new Bitmap(this.SelectedImage);

                PrepareForDownsizing(image);

                var stopwatchTimer = Stopwatch.StartNew();


                for (int y = 0; y < this.NewImageHeight; y++)
                {
                    Parallel.For(0, this.NewImageWidth, (x) =>
                    {

                        var srcX = (int)(x / (this.n_dsFactor.Value / 100m));
                        var srcY = (int)(y / (this.n_dsFactor.Value / 100m));
                        srcX = Math.Min(srcX, this.ImageWidth);
                        srcY = Math.Min(srcY, this.ImageHeight);

                        SetPixel(x, y, GetPixel(srcX, srcY));
                    });
                }

                stopwatchTimer.Stop();

                l_pdsResults.Text = "It took " + stopwatchTimer.Elapsed.ToString(this.ResultTimeFormat);

                SaveImage(savePath);
            }
        }

        private void SaveImage(string savePath)
        {
            var result = new Bitmap(
                this.NewImageWidth,
                this.NewImageHeight,
                this.ImagePixelFormat
            );

            var resultData = result.LockBits(
                new Rectangle(0, 0, result.Width, result.Height),
                ImageLockMode.WriteOnly,
                this.ImagePixelFormat
            );

            Marshal.Copy(this.DownsizedImagePixelData, 0, resultData.Scan0, this.DownsizedImagePixelData.Length);
            result.UnlockBits(resultData);

            result.Save(savePath);
        }

        private void PrepareForDownsizing(Bitmap image)
        {
            var imageData = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly,
                image.PixelFormat
            );

            var stride = Math.Abs(imageData.Stride);

            int bytesPerPixel = stride / imageData.Width;

            var bytes = stride * imageData.Height;
            this.ImagePixelData = new byte[bytes];
            Marshal.Copy(imageData.Scan0, this.ImagePixelData, 0, bytes);
            image.UnlockBits(imageData);

            this.ImageWidth = image.Width;
            this.ImageHeight = image.Height;
            this.ImageBytesPerPixel = bytesPerPixel;
            this.ImagePixelFormat = image.PixelFormat;

            var newWidth = (int)(image.Width * (this.n_dsFactor.Value / 100m));
            var newHeight = (int)(image.Height * (this.n_dsFactor.Value / 100m));
            this.NewImageWidth = newWidth;
            this.NewImageHeight = newHeight;

            this.DownsizedImagePixelData = new byte[newWidth * newHeight * bytesPerPixel];
        }

        private byte[] GetPixel(int x, int y)
        {
            x = Math.Clamp(x, 0, this.ImageWidth);
            y = Math.Clamp(y, 0, this.ImageHeight);

            var offset = x * this.ImageBytesPerPixel + y * this.ImageBytesPerPixel * this.ImageWidth;

            return this.ImagePixelData[offset..(offset + this.ImageBytesPerPixel)];
        }

        private void SetPixel(int x, int y, byte[] data)
        {
            x = Math.Clamp(x, 0, this.NewImageWidth);
            y = Math.Clamp(y, 0, this.NewImageHeight);

            var offset = x * this.ImageBytesPerPixel + y * this.ImageBytesPerPixel * this.NewImageWidth;

            for (var i = 0; i < data.Length; i++)
            {
                this.DownsizedImagePixelData[offset + i] = data[i];
            }
        }
    }
}
