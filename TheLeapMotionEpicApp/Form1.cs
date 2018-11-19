using System;
using Leap;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Threading.Tasks;
using SkyScanner.Services;
using System.Configuration;
using SkyScanner.Settings;
using NodaTime;
using System.Linq;

namespace TheLeapMotionEpicApp
{

    public partial class Form1 : Form
    {

        private byte[] imagedata = new byte[1];
        private string prevHand = "no hands";
        private int timer;
        private bool actionMade = false;
        private YoutubeVids ytv;
        private Controller controller = new Controller();
        Bitmap bitmap = new Bitmap(640, 480, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

        public int j=0;
        Hackathon currentObj;

        public Form1()
        {
            InitializeComponent();
            controller.EventContext = WindowsFormsSynchronizationContext.Current;
            controller.FrameReady += newFrameHandler;
            controller.ImageReady += onImageReady;
            controller.ImageRequestFailed += onImageRequestFailed;
            ytv = new YoutubeVids();
            //set greyscale palette for image Bitmap object
            ColorPalette grayscale = bitmap.Palette;
            for (int i = 0; i < 256; i++)
            {
                grayscale.Entries[i] = Color.FromArgb((int)255, i, i, i);
            }
            bitmap.Palette = grayscale;
            currentObj = Parser.Start(j);
        }

        void newFrameHandler(object sender, FrameEventArgs eventArgs)
        {
            

            Frame frame = eventArgs.frame;
                
            this.displayID.Text = frame.Id.ToString();
            this.displayTimestamp.Text = frame.Timestamp.ToString();
            this.displayFPS.Text = frame.CurrentFramesPerSecond.ToString();
            string whichHand = "";
                
            if (frame.Hands.Count == 1)
                foreach (Hand hand in frame.Hands)
                {
                    if (hand.IsLeft)
                    {
                        whichHand = "left";
                    }
                    else
                        if (hand.IsRight)
                        {
                            whichHand = "right";
                        }
                }
            else
                whichHand = "both hands";
            if(frame.Hands.Count == 0)
                whichHand = "no hands";
            
            if (prevHand == whichHand)
            {
                timer++;
            }
            else timer = 0;
            if(timer == 40)
            {
                this.displayHandCount.Text = whichHand;
                if(whichHand == "right" && actionMade == false)
                {
                   //Process.Start("chrome", @ytv.getRandomUrl());
                    j+=2;
                    actionMade = true;
                }
                if(whichHand == "left" && actionMade == false)
                {
                    char[] delimiterChars = { '"' };
                    char[] delim2 = { ',' };
                    string[] locs = currentObj.Hackathon_location.Split(delim2);
                   // var thingum = api.QueryFlights("London", locs[0]).Result;
                    //string json = JsonConvert.SerializeObject(thingum, Formatting.Indented);
                    //System.IO.File.WriteAllText(@"D:\Visual Studio Repository\skyscanner-test\api_output.txt", json);
                    string text = System.IO.File.ReadAllText(@"D:\Visual Studio Repository\skyscanner-test\api_output.txt");
                    //System.Console.WriteLine("Original text: '{0}'", text);

                    string[] words = text.Split(delimiterChars);
                    // System.Console.WriteLine("{0} words in text:", words.Length);

                    Process.Start("chrome", words[5]);
                    //Process.Start("chrome", @ytv.getRandomUrl());
                    actionMade = true;
                }

                timer = 0;
            }
            if (whichHand == "no hands")
                actionMade = false;

            prevHand = whichHand;

            controller.RequestImages(frame.Id, Leap.Image.ImageType.DEFAULT, imagedata);
            currentObj = Parser.Start(j);
            DisplayHackathon(currentObj);
        }

        void onImageRequestFailed(object sender, ImageRequestFailedEventArgs e)
        {
            if (e.reason == Leap.Image.RequestFailureReason.Insufficient_Buffer)
            {
                imagedata = new byte[e.requiredBufferSize];
            }
            System.Console.WriteLine("Image request failed: " + e.message);
        }

        void onImageReady(object sender, ImageEventArgs e)
        {
            
            Rectangle lockArea = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bitmapData = bitmap.LockBits(lockArea, ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
            byte[] rawImageData = imagedata;
            System.Runtime.InteropServices.Marshal.Copy(rawImageData, 0, bitmapData.Scan0, e.image.Width * e.image.Height * 2 * e.image.BytesPerPixel);
            bitmap.UnlockBits(bitmapData);
            displayImages.Image = bitmap;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void debugText_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.Text = currentObj.Hackathon_name;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


    }
}
