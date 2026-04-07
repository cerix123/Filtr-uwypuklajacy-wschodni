namespace Projekt
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
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            comboBox1 = new ComboBox();
            button2 = new Button();
            buttonSelectImage = new Button();
            buttonCpp = new Button();
            buttonAsm = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(69, 66);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(193, 106);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(422, 66);
            pictureBox2.Margin = new Padding(3, 2, 3, 2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(190, 106);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "1", "2", "4", "8", "16", "32", "64" });
            comboBox1.Location = new Point(271, 203);
            comboBox1.Margin = new Padding(3, 2, 3, 2);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(133, 23);
            comboBox1.TabIndex = 2;
            comboBox1.Text = "Liczba Watkow";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // button2
            // 
            button2.Location = new Point(296, 240);
            button2.Margin = new Padding(3, 2, 3, 2);
            button2.Name = "button2";
            button2.Size = new Size(82, 22);
            button2.TabIndex = 3;
            button2.Text = "button1";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // buttonSelectImage
            // 
            buttonSelectImage.Location = new Point(10, 9);
            buttonSelectImage.Margin = new Padding(3, 2, 3, 2);
            buttonSelectImage.Name = "buttonSelectImage";
            buttonSelectImage.Size = new Size(105, 22);
            buttonSelectImage.TabIndex = 0;
            buttonSelectImage.Text = "Wybierz obraz";
            buttonSelectImage.UseVisualStyleBackColor = true;
            buttonSelectImage.Click += buttonSelectImage_Click;
            // 
            // buttonCpp
            // 
            buttonCpp.Location = new Point(226, 281);
            buttonCpp.Name = "buttonCpp";
            buttonCpp.Size = new Size(75, 23);
            buttonCpp.TabIndex = 4;
            buttonCpp.Text = "C++";
            buttonCpp.UseVisualStyleBackColor = true;
            buttonCpp.Click += button3_Click;
            // 
            // buttonAsm
            // 
            buttonAsm.Location = new Point(376, 281);
            buttonAsm.Name = "buttonAsm";
            buttonAsm.Size = new Size(75, 23);
            buttonAsm.TabIndex = 5;
            buttonAsm.Text = "asembler";
            buttonAsm.UseVisualStyleBackColor = true;
            buttonAsm.Click += button4_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 338);
            Controls.Add(buttonAsm);
            Controls.Add(buttonCpp);
            Controls.Add(buttonSelectImage);
            Controls.Add(button2);
            Controls.Add(comboBox1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        public PictureBox pictureBox1;
        public PictureBox pictureBox2;
        public ComboBox comboBox1;
        private Button button1;
        private Button button2;
        private Button buttonSelectImage;
        private Button buttonCpp;
        private Button buttonAsm;
    }
}