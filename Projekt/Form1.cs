using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Projekt
{
    public partial class Form1 : Form
    {
        [DllImport(@"C:\Users\knamy\Desktop\Asembler\JA_filtr\x64\Debug\filtrasm.dll")]
        static extern void MyProc1(IntPtr inputPtr, IntPtr outputPtr, int width, int height, int stride);

        [DllImport("C:\\Users\\knamy\\Desktop\\Asembler\\JA_filtr\\x64\\Debug\\filtrcpp.dll")]
        static extern void ApplyEmbossEastFilter(IntPtr bmpData, IntPtr outputSegmentPtr, int width, int height, int stride);

        public int SelectedThreadCount { get; private set; } = 1;
        private string selectedImagePath;
        private bool isCppSelected = true;
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);

            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(comboBox1.SelectedItem.ToString(), out int threadCount))
            {
                SelectedThreadCount = threadCount;
            }
            else
            {
                SelectedThreadCount = 1;
            }
        }

        private void buttonSelectImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Bitmap Files (*.bmp)|*.bmp|All Files (*.*)|*.*";
                openFileDialog.Title = "Select an Image File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedImagePath = openFileDialog.FileName;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedImagePath))
            {
                ProcessImage(selectedImagePath);
            }
            else
            {
                MessageBox.Show("Najpierw wybierz obraz!", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ProcessImage(string imagePath)
        {
            Bitmap bmp = new Bitmap(imagePath);
            pictureBox1.Image = bmp;

            Bitmap processedBmp = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format24bppRgb); // Zmiana na Format24bppRgb
            using (Graphics g = Graphics.FromImage(processedBmp))
            {
                g.DrawImage(bmp, 0, 0);
            }

            int width = bmp.Width;
            int height = bmp.Height;

            Rectangle rect = new Rectangle(0, 0, processedBmp.Width, processedBmp.Height);
            BitmapData bmpData = processedBmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb); // Format24bppRgb
            int threadCount = SelectedThreadCount;
            threadCount = Math.Max(1, Math.Min(threadCount, height));

            int segmentHeight = height / threadCount;
            int remainder = height % threadCount;
            int stride = bmpData.Stride;
            Bitmap copyBmp = (Bitmap)bmp.Clone();  // Kopia obrazu

            // Jeœli chcesz skopiowaæ równie¿ dane z oryginalnej bitmapy na inny format
            Bitmap bit2 = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(bit2))
            {
                g.DrawImage(bmp, 0, 0);
            }
            Rectangle rect2 = new Rectangle(0, 0, processedBmp.Width, processedBmp.Height);
            BitmapData bit2Data = bit2.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb); // Format24bppRgb
            Thread[] threads = new Thread[threadCount];
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();


            for (int i = 0; i < threadCount; i++)
            {
                int currentSegmentHeight = segmentHeight + (i < remainder ? 1 : 0);
                int segmentStart = i * segmentHeight + Math.Min(i, remainder);

                // Oblicz wskaŸniki dla segmentów
                IntPtr inputSegmentPtr = (IntPtr)((long)bmpData.Scan0 + (segmentStart * stride));
                IntPtr outputSegmentPtr = (IntPtr)((long)bit2Data.Scan0 + (segmentStart * stride));

                threads[i] = new Thread(() =>
                {
                    if (isCppSelected)
                    {
                        ApplyEmbossEastFilter(inputSegmentPtr, outputSegmentPtr, width, currentSegmentHeight, stride);
                    }
                    else
                    {
                        MyProc1(inputSegmentPtr, outputSegmentPtr, currentSegmentHeight, width, stride);
                    }
                });
                threads[i].Start();
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }
            stopwatch.Stop();
            processedBmp.UnlockBits(bmpData);
            bit2.UnlockBits(bit2Data);
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory; 
            string outputPath1 = Path.Combine(currentDirectory, "cepepe.bmp");
            string outputPath2 = Path.Combine(currentDirectory, "asembler.bmp");

            
            bit2.Save(outputPath2, ImageFormat.Bmp);
            bit2.Save(outputPath1, ImageFormat.Bmp);


            pictureBox2.Image = bit2;


            string timeFilePath = Path.Combine(currentDirectory, "czas.txt");

            using (StreamWriter writer = new StreamWriter(timeFilePath))
            {
                writer.WriteLine($"{stopwatch.ElapsedMilliseconds} ms");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            isCppSelected = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            isCppSelected = false;
        }
    }
}