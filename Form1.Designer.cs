namespace FinalCode
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            shapeDataLabel = new Label();
            startButton = new Button();
            contoursLabel = new Label();
            bgrLabel = new Label();
            blurLabel = new Label();
            gaussianUp = new Button();
            gaussianDown = new Button();
            bgrSlider = new TrackBar();
            triAreaSlider = new TrackBar();
            triAreaLabel = new Label();
            rawFrame = new PictureBox();
            decoFrame = new PictureBox();
            blurFrame = new PictureBox();
            bgrFrame = new PictureBox();
            squareAreaLabel = new Label();
            squareAreaSlider = new TrackBar();
            borderAreaLabel = new Label();
            borderAreaSlider = new TrackBar();
            openCommsButton = new Button();
            arduinoDataLabel = new TextBox();
            comPortTextBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)bgrSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)triAreaSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)rawFrame).BeginInit();
            ((System.ComponentModel.ISupportInitialize)decoFrame).BeginInit();
            ((System.ComponentModel.ISupportInitialize)blurFrame).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bgrFrame).BeginInit();
            ((System.ComponentModel.ISupportInitialize)squareAreaSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)borderAreaSlider).BeginInit();
            SuspendLayout();
            // 
            // shapeDataLabel
            // 
            shapeDataLabel.AutoSize = true;
            shapeDataLabel.BackColor = SystemColors.ActiveCaptionText;
            shapeDataLabel.ForeColor = Color.Cyan;
            shapeDataLabel.Location = new Point(17, 534);
            shapeDataLabel.Margin = new Padding(0);
            shapeDataLabel.Name = "shapeDataLabel";
            shapeDataLabel.Size = new Size(200, 48);
            shapeDataLabel.TabIndex = 2;
            shapeDataLabel.Text = "Shape Data";
            // 
            // startButton
            // 
            startButton.Location = new Point(2886, 3062);
            startButton.Name = "startButton";
            startButton.Size = new Size(430, 136);
            startButton.TabIndex = 6;
            startButton.Text = "Press to begin shape accquisition";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // contoursLabel
            // 
            contoursLabel.AutoSize = true;
            contoursLabel.BackColor = SystemColors.ActiveCaptionText;
            contoursLabel.ForeColor = Color.Cyan;
            contoursLabel.Location = new Point(1584, 1572);
            contoursLabel.Margin = new Padding(0);
            contoursLabel.Name = "contoursLabel";
            contoursLabel.Size = new Size(164, 48);
            contoursLabel.TabIndex = 9;
            contoursLabel.Text = "Contours";
            // 
            // bgrLabel
            // 
            bgrLabel.AutoSize = true;
            bgrLabel.Location = new Point(1569, 1188);
            bgrLabel.Name = "bgrLabel";
            bgrLabel.Size = new Size(272, 48);
            bgrLabel.TabIndex = 12;
            bgrLabel.Text = "BGR filter value:";
            // 
            // blurLabel
            // 
            blurLabel.AutoSize = true;
            blurLabel.Location = new Point(790, 1188);
            blurLabel.Name = "blurLabel";
            blurLabel.Size = new Size(334, 48);
            blurLabel.TabIndex = 13;
            blurLabel.Text = "Gaussian blur value:";
            // 
            // gaussianUp
            // 
            gaussianUp.Location = new Point(777, 1254);
            gaussianUp.Margin = new Padding(4, 5, 4, 5);
            gaussianUp.Name = "gaussianUp";
            gaussianUp.Size = new Size(286, 166);
            gaussianUp.TabIndex = 16;
            gaussianUp.Text = "Increase Blur";
            gaussianUp.UseVisualStyleBackColor = true;
            gaussianUp.Click += gaussianUp_Click;
            // 
            // gaussianDown
            // 
            gaussianDown.Location = new Point(1071, 1254);
            gaussianDown.Margin = new Padding(4, 5, 4, 5);
            gaussianDown.Name = "gaussianDown";
            gaussianDown.Size = new Size(286, 166);
            gaussianDown.TabIndex = 17;
            gaussianDown.Text = "Decrease Blur";
            gaussianDown.UseVisualStyleBackColor = true;
            gaussianDown.Click += gaussianDown_Click;
            // 
            // bgrSlider
            // 
            bgrSlider.AutoSize = false;
            bgrSlider.LargeChange = 10;
            bgrSlider.Location = new Point(1569, 1271);
            bgrSlider.Maximum = 255;
            bgrSlider.Name = "bgrSlider";
            bgrSlider.Size = new Size(523, 114);
            bgrSlider.SmallChange = 2;
            bgrSlider.TabIndex = 14;
            bgrSlider.Scroll += bgrSlider_Scroll;
            // 
            // triAreaSlider
            // 
            triAreaSlider.Location = new Point(2293, 1249);
            triAreaSlider.Margin = new Padding(4, 5, 4, 5);
            triAreaSlider.Maximum = 10000;
            triAreaSlider.Minimum = 1100;
            triAreaSlider.Name = "triAreaSlider";
            triAreaSlider.Size = new Size(523, 136);
            triAreaSlider.TabIndex = 18;
            triAreaSlider.Value = 1100;
            triAreaSlider.Scroll += triAreaSlider_Scroll;
            // 
            // triAreaLabel
            // 
            triAreaLabel.AutoSize = true;
            triAreaLabel.Location = new Point(2293, 1167);
            triAreaLabel.Margin = new Padding(4, 0, 4, 0);
            triAreaLabel.Name = "triAreaLabel";
            triAreaLabel.Size = new Size(367, 48);
            triAreaLabel.TabIndex = 21;
            triAreaLabel.Text = "Triangle area modifier";
            // 
            // rawFrame
            // 
            rawFrame.Location = new Point(17, 23);
            rawFrame.Margin = new Padding(4, 5, 4, 5);
            rawFrame.Name = "rawFrame";
            rawFrame.Size = new Size(713, 506);
            rawFrame.TabIndex = 26;
            rawFrame.TabStop = false;
            // 
            // decoFrame
            // 
            decoFrame.Location = new Point(738, 23);
            decoFrame.Margin = new Padding(4, 5, 4, 5);
            decoFrame.Name = "decoFrame";
            decoFrame.Size = new Size(713, 506);
            decoFrame.TabIndex = 27;
            decoFrame.TabStop = false;
            // 
            // blurFrame
            // 
            blurFrame.Location = new Point(1472, 23);
            blurFrame.Margin = new Padding(4, 5, 4, 5);
            blurFrame.Name = "blurFrame";
            blurFrame.Size = new Size(713, 506);
            blurFrame.TabIndex = 28;
            blurFrame.TabStop = false;
            // 
            // bgrFrame
            // 
            bgrFrame.Location = new Point(2205, 23);
            bgrFrame.Margin = new Padding(4, 5, 4, 5);
            bgrFrame.Name = "bgrFrame";
            bgrFrame.Size = new Size(713, 506);
            bgrFrame.TabIndex = 29;
            bgrFrame.TabStop = false;
            // 
            // squareAreaLabel
            // 
            squareAreaLabel.AutoSize = true;
            squareAreaLabel.Location = new Point(2293, 1337);
            squareAreaLabel.Margin = new Padding(4, 0, 4, 0);
            squareAreaLabel.Name = "squareAreaLabel";
            squareAreaLabel.Size = new Size(352, 48);
            squareAreaLabel.TabIndex = 30;
            squareAreaLabel.Text = "Square area modifier";
            // 
            // squareAreaSlider
            // 
            squareAreaSlider.Location = new Point(2293, 1400);
            squareAreaSlider.Margin = new Padding(4, 5, 4, 5);
            squareAreaSlider.Maximum = 20000;
            squareAreaSlider.Minimum = 1000;
            squareAreaSlider.Name = "squareAreaSlider";
            squareAreaSlider.Size = new Size(523, 136);
            squareAreaSlider.TabIndex = 31;
            squareAreaSlider.Value = 1000;
            squareAreaSlider.Scroll += squareAreaSlider_Scroll;
            // 
            // borderAreaLabel
            // 
            borderAreaLabel.AutoSize = true;
            borderAreaLabel.Location = new Point(1569, 1378);
            borderAreaLabel.Margin = new Padding(4, 0, 4, 0);
            borderAreaLabel.Name = "borderAreaLabel";
            borderAreaLabel.Size = new Size(350, 48);
            borderAreaLabel.TabIndex = 32;
            borderAreaLabel.Text = "Border area modifier";
            // 
            // borderAreaSlider
            // 
            borderAreaSlider.Location = new Point(1569, 1431);
            borderAreaSlider.Margin = new Padding(4, 5, 4, 5);
            borderAreaSlider.Maximum = 500000;
            borderAreaSlider.Minimum = 5000;
            borderAreaSlider.Name = "borderAreaSlider";
            borderAreaSlider.Size = new Size(523, 136);
            borderAreaSlider.TabIndex = 33;
            borderAreaSlider.Value = 45000;
            borderAreaSlider.Scroll += borderAreaSlider_Scroll;
            // 
            // openCommsButton
            // 
            openCommsButton.Location = new Point(2293, 1522);
            openCommsButton.Margin = new Padding(4, 5, 4, 5);
            openCommsButton.Name = "openCommsButton";
            openCommsButton.Size = new Size(431, 124);
            openCommsButton.TabIndex = 34;
            openCommsButton.Text = "Press to open serial comms with Arduino";
            openCommsButton.UseVisualStyleBackColor = true;
            openCommsButton.Click += openCommsButton_Click;
            // 
            // arduinoDataLabel
            // 
            arduinoDataLabel.BackColor = SystemColors.WindowText;
            arduinoDataLabel.ForeColor = Color.FromArgb(128, 255, 255);
            arduinoDataLabel.Location = new Point(17, 1557);
            arduinoDataLabel.Margin = new Padding(4, 5, 4, 5);
            arduinoDataLabel.Name = "arduinoDataLabel";
            arduinoDataLabel.Size = new Size(414, 55);
            arduinoDataLabel.TabIndex = 36;
            arduinoDataLabel.Text = "Returned coordinate data";
            arduinoDataLabel.TextChanged += arduinoDataLabel_TextChanged;
            // 
            // comPortTextBox
            // 
            comPortTextBox.Location = new Point(0, 0);
            comPortTextBox.Name = "comPortTextBox";
            comPortTextBox.Size = new Size(100, 55);
            comPortTextBox.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(20F, 48F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2985, 1932);
            Controls.Add(comPortTextBox);
            Controls.Add(arduinoDataLabel);
            Controls.Add(openCommsButton);
            Controls.Add(borderAreaSlider);
            Controls.Add(borderAreaLabel);
            Controls.Add(squareAreaSlider);
            Controls.Add(squareAreaLabel);
            Controls.Add(bgrFrame);
            Controls.Add(blurFrame);
            Controls.Add(decoFrame);
            Controls.Add(rawFrame);
            Controls.Add(triAreaLabel);
            Controls.Add(triAreaSlider);
            Controls.Add(gaussianDown);
            Controls.Add(gaussianUp);
            Controls.Add(bgrSlider);
            Controls.Add(blurLabel);
            Controls.Add(bgrLabel);
            Controls.Add(contoursLabel);
            Controls.Add(startButton);
            Controls.Add(shapeDataLabel);
            Margin = new Padding(10, 12, 10, 12);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)bgrSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)triAreaSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)rawFrame).EndInit();
            ((System.ComponentModel.ISupportInitialize)decoFrame).EndInit();
            ((System.ComponentModel.ISupportInitialize)blurFrame).EndInit();
            ((System.ComponentModel.ISupportInitialize)bgrFrame).EndInit();
            ((System.ComponentModel.ISupportInitialize)squareAreaSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)borderAreaSlider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label shapeDataLabel;
        private Button startButton;
        private Label contoursLabel;
        private Label bgrLabel;
        private Label blurLabel;
        private Button gaussianUp;
        private Button gaussianDown;
        private TrackBar bgrSlider;
        private TrackBar triAreaSlider;
        private Label triAreaLabel;
        private PictureBox rawFrame;
        private PictureBox decoFrame;
        private PictureBox blurFrame;
        private PictureBox bgrFrame;
        private Label squareAreaLabel;
        private TrackBar squareAreaSlider;
        private Label borderAreaLabel;
        private TrackBar borderAreaSlider;
        private Button openCommsButton;
        private TextBox arduinoDataLabel;
        private TextBox comPortTextBox;
    }
}
