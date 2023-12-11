using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System.IO.Ports;
using System.Text;

namespace FinalCode
{
    public partial class Form1 : Form
    {
        VideoCapture capture;
        Thread captureThread;
        Thread arduinoCommsThread;
        SerialPort arduinoSerial;
        bool enableCoordinateSending = false;
        Size blurRatio = new Size(1, 1);

        // Global Variables
        int value;
        int triangleMinArea = 0;
        int triangleMaxArea = 0;
        int squareMinArea = 0;
        int squareMaxArea = 0;
        int borderMinArea = 0;
        int borderMaxArea = 0;
        int blur = 3;
        int pixPerIn = 0;

        //Creates a class "Shape" to store information of each shape
        public class Shape
        {
            public int numb { get; set; }
            public int shapeType { get; set; }
            public double area { get; set; }
            public Point center { get; set; }
            public int drivingDist { get; set; }
            public int drivingAngle { get; set; }
        }
        List<Shape> shapeInfo = new List<Shape>();

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            capture = new VideoCapture(0);
            captureThread = new Thread(processImage);
            captureThread.Start();
        }

        private void openCommsButton_Click(object sender, EventArgs e)
        {
            arduinoSerial = new SerialPort();
            int comPort = 3;
            try
            {
                arduinoSerial.PortName = $"COM";
                arduinoSerial.BaudRate = 9600;
                arduinoSerial.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Initializing COM port");
            }
            arduinoCommsThread = new Thread(serialCommunication);
            arduinoCommsThread.Start();
        }
        private void startButton_Click(object sender, EventArgs e)
        {
            Invoke(new Action(() =>
            {
                arduinoDataLabel.Text = $"\nWaiting for data...\n";
                shapeDataLabel.Text = $"Waiting for shape data...\n";
            }));
            captureThread.Abort();
            capture.Stop();
            capture = new VideoCapture(0);
            captureThread = new Thread(processImage);
            captureThread.Start();
        }
        //shape detection, data computation, and updating the GUI.
        private void processImage()
        {
            while (true)
            {
                shapeInfo.Clear();

                Mat sourceMat = new Mat();
                sourceMat = capture.QueryFrame();

                capture.Pause();
                Mat countourMat = sourceMat.Clone();

                var blurredMat = new Mat();
                var binaryMat = new Mat();
                var decoratedMat = new Mat();
                CvInvoke.GaussianBlur(countourMat, blurredMat, new Size(blur, blur), 0);
                CvInvoke.CvtColor(blurredMat, blurredMat, typeof(Bgr), typeof(Gray));
                CvInvoke.Threshold(blurredMat, binaryMat, value, 255, Emgu.CV.CvEnum.ThresholdType.Binary);
                CvInvoke.CvtColor(binaryMat, decoratedMat, typeof(Gray), typeof(Bgr));
                resizePicBoxes(sourceMat, blurredMat, binaryMat, decoratedMat);
                processShapes(binaryMat, decoratedMat);
                displayShapeData();

                if (enableCoordinateSending)
                {
                    sendCoordinates();
                }
            }
        }

        private void serialCommunication()
        {
            while (true)
            {
                // block until \n character is received, extract command data
                string msg = arduinoSerial.ReadLine();
                // confirm the string has both < and > characters
                if (msg.IndexOf("<") == -1 || msg.IndexOf(">") == -1)
                {
                    continue;
                }
                // remove everything before (and including) the < character
                msg = msg.Substring(msg.IndexOf("<") + 1);
                // remove everything after (and including) the > character
                msg = msg.Remove(msg.IndexOf(">"));
                // if the resulting string is empty, disregard and move on
                if (msg.Length == 0)
                {
                    continue;
                }
                else if (msg.Substring(0, 1) == "P")
                {
                    Invoke(new Action(() =>
                    {
                        arduinoDataLabel.Text = $"Arduino data:\nReturned Point Data: {msg.Substring(1)}\n";
                    }));
                }
            }
        }

        private void resizePicBoxes(Mat sourceMat, Mat blurredMat, Mat binaryMat, Mat decoratedMat)
        {
            int newHeight = 0;
            Size newSize = new Size(0, 0);
            Mat temp = new Mat();
            PictureBox str = new PictureBox();

            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        {
                            temp = sourceMat;
                            str = rawFrame;
                            break;
                        }
                    case 1:
                        {
                            temp = decoratedMat;
                            str = decoFrame;
                            break;
                        }
                    case 2:
                        {
                            temp = blurredMat;
                            str = blurFrame;
                            break;
                        }
                    case 3:
                        {
                            temp = binaryMat;
                            str = bgrFrame;
                            break;
                        }
                }
                //Dynamically updates each picture box with its respective switch condition.
                newHeight = (temp.Size.Height * str.Size.Width) / temp.Size.Width;
                newSize = new Size(str.Size.Width, newHeight);
                CvInvoke.Resize(temp, temp, newSize);
                str.Image = temp.ToBitmap();
            }
        }

        private void processShapes(Mat binaryMat, Mat decoratedMat)
        {
            using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
            {
                //Finds every contour in the capture frame with no parameters
                CvInvoke.FindContours(binaryMat, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);
                //Updates in for loop each time a shape is detected and stored
                int shapes = 0;
                Invoke(new Action(() =>
                { contoursLabel.Text = $"There are {contours.Size} contours"; }));

                for (int i = 0; i < contours.Size; i++)
                {
                    VectorOfPoint contour = contours[i];
                    double area = CvInvoke.ContourArea(contour);
                    if (area > triangleMinArea && area < triangleMaxArea)
                    {
                        shapes++;
                        //Creates visual outline of shape to show identification in GUI
                        CvInvoke.Polylines(decoratedMat, contour, true, new Bgr(Color.Blue).MCvScalar);
                        Rectangle boundingBox = CvInvoke.BoundingRectangle(contours[i]);
                        //Creates new entry to shapeInfo list.
                        Shape shape = new Shape()
                        {
                            numb = shapes,
                            shapeType = 0,
                            center = CenterCoords(boundingBox),
                            area = area
                        };
                        shapeInfo.Add(shape);
                        //Adds relevent information next to each shape in the decorated picture box.
                        MarkDetectedObject(decoFrame, decoratedMat, contours[i], boundingBox, area, shape.numb);
                    }
                    //Identical process as triangles with different area parameters
                    if (area > squareMinArea && area < squareMaxArea)
                    {
                        shapes++;
                        CvInvoke.Polylines(decoratedMat, contour, true, new Bgr(Color.Yellow).MCvScalar);
                        Rectangle boundingBox = CvInvoke.BoundingRectangle(contours[i]);
                        Shape shape = new Shape()
                        {
                            numb = shapes,
                            shapeType = 1,
                            center = CenterCoords(boundingBox),
                            area = area
                        };
                        shapeInfo.Add(shape);
                        MarkDetectedObject(decoFrame, decoratedMat, contours[i], boundingBox, area, shape.numb);
                    }
                    //Identical process as both shapes prior with the addition of the border information
                    if (area > borderMinArea && area < borderMaxArea)
                    {
                        shapes++;
                        CvInvoke.Polylines(decoratedMat, contour, true, new Bgr(Color.YellowGreen).MCvScalar);
                        Rectangle boundingBox = CvInvoke.BoundingRectangle(contours[i]);
                        pixPerIn = Convert.ToInt32(boundingBox.Width / 11);
                        Shape shape = new Shape()
                        {
                            numb = shapes,
                            shapeType = 2,
                            center = CenterCoords(boundingBox),
                            area = area
                        };
                        shapeInfo.Add(shape);
                        var info = new string[]
                        {
                            $"Border",
                            $"Center at {shape.center}",
                            $"Border height: {boundingBox.Height}",
                            $"Border width: {boundingBox.Width}",
                        };
                        WriteMultilineText(decoratedMat, info, new Point(boundingBox.Left, boundingBox.Bottom + 15));
                        MarkDetectedObject(decoFrame, decoratedMat, contours[i], boundingBox, area, shape.numb);
                    }
                }
            }
        }


        private void displayShapeData()
        {
            if (shapeInfo.Count > 1)
            {
                Invoke(new Action(() =>
                {
                    string str = "Shape";
                    shapeDataLabel.Text = $"\n\nThere are {pixPerIn} pixels per virtual inch\n";
                    foreach (Shape shape in shapeInfo.ToList())
                    {
                        switch (shape.shapeType)
                        {
                            case 0:
                                str = "a Triangle";
                                break;
                            case 1:
                                str = "a Square";
                                break;
                            case 2:
                                str = "the Border";
                                break;
                        }
                        shapeDataLabel.Text += $"\nShape {shape.numb} is {str}, has an area {shape.area} and a center {shape.center}, a driving distance of {shape.drivingDist}, " +
                        $"and must rotate {shape.drivingAngle} degrees.\n";
                    }
                }));
            }
        }

        private void sendCoordinates()
        {
            byte[] buffer = new byte[5]
                        {
                                Encoding.ASCII.GetBytes("<")[0],
                                Convert.ToByte(shapeInfo[0].drivingAngle),
                                Convert.ToByte(shapeInfo[0].drivingDist),
                                Convert.ToByte(shapeInfo[0].shapeType),
                                Encoding.ASCII.GetBytes(">")[0]
                        };
            arduinoSerial.Write(buffer, 0, 5);
            enableCoordinateSending = false;
        }
        private static void MarkDetectedObject(PictureBox decoFrame, Mat frame, VectorOfPoint contour, Rectangle boundingBox, double area, int num)
        {
            CvInvoke.Rectangle(frame, boundingBox, new Bgr(Color.DarkOrange).MCvScalar);
            Point center = new Point(boundingBox.X + boundingBox.Width / 2, boundingBox.Y + boundingBox.Height / 2);
            CvInvoke.Circle(frame, center, 0, new Bgr(Color.Purple).MCvScalar, 4);
            string str = $"Shape {num}";
            CvInvoke.PutText(frame, str, new Point(center.X - 25, center.Y), FontFace.HersheyPlain, 0.8, new Bgr(Color.Red).MCvScalar);
            decoFrame.Image = frame.ToBitmap();
        }
        private Point CenterCoords(Rectangle boundingBox)
        {
            Point center = new Point(boundingBox.X + boundingBox.Width / 2, boundingBox.Y + boundingBox.Height / 2);
            return center;
        }

        private static void WriteMultilineText(Mat frame, string[] lines, Point origin)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                int y = i * 10 + origin.Y; // Moving down on each line
                CvInvoke.PutText(frame, lines[i], new Point(origin.X, y),
                FontFace.HersheyPlain, 0.8, new Bgr(Color.Red).MCvScalar);
            }
        }

        private void gaussianUp_Click(object sender, EventArgs e)
        {
            blur = blur + 2;
            Invoke(new Action(() =>
            {
                blurLabel.Text = $"Blur matrix is {blur} by {blur}.";
            }));
        }
        //Decreases blur effect, prevents negative and even values
        private void gaussianDown_Click(object sender, EventArgs e)
        {
            if (blur >= 3)
            {
                blur = blur - 2;
                Invoke(new Action(() =>
                {
                    blurLabel.Text = $"Blur matrix is {blur} x {blur}.";
                }));
            }
            else
            {
                Invoke(new Action(() =>
                {
                    blurLabel.Text = $"Blur matrix cannot be less than 1 x 1.";
                }));
            }
        }
        //Allows for BGR filter adjustment at runtime.
        private void bgrSlider_Scroll(object sender, EventArgs e)
        {
            value = bgrSlider.Value;
            Invoke(new Action(() =>
            {
                bgrLabel.Text = $"Bgr value is {value}.";
            }));
        }
        //Dynamic modification of triangle area parameter at run time with slider
        private void triAreaSlider_Scroll(object sender, EventArgs e)
        {
            triangleMinArea = triAreaSlider.Value - 500;
            triangleMaxArea = triAreaSlider.Value + 500;
            Invoke(new Action(() =>
            {
                triAreaLabel.Text = $"The area parameter range for triangles is {triangleMinArea} " +
                $"to {triangleMaxArea}.";
            }));
        }
        //Dynamic modification of square area parameter at run time with slider
        private void squareAreaSlider_Scroll(object sender, EventArgs e)
        {
            squareMinArea = squareAreaSlider.Value - 1000;
            squareMaxArea = squareAreaSlider.Value + 1000;
            Invoke(new Action(() =>
            {
                squareAreaLabel.Text = $"The area parameter range for squares is {squareMinArea} " +
                $"to {squareMaxArea}.";
            }));
        }
        //Dynamic modification of the border area parameter at run time with slider
        private void borderAreaSlider_Scroll(object sender, EventArgs e)
        {
            borderMinArea = borderAreaSlider.Value - 5000;
            borderMaxArea = borderAreaSlider.Value + 5000;
            borderMaxArea = borderAreaSlider.Value + 5000;
            Invoke(new Action(() =>
            { borderAreaLabel.Text = $"The area parameter range for squares is {borderMinArea} to {borderMaxArea}."; }));
        }

        private void arduinoDataLabel_TextChanged(object sender, EventArgs e)
        {
            enableCoordinateSending = true;
        }
    }
}
