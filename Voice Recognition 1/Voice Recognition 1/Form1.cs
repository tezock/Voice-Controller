using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using WindowsInput;
using System.Drawing.Text;
using System.Threading;

namespace Voice_Recognition_1
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine(); // Creates new Speech Recognition object
        InputSimulator isim = new InputSimulator(); // Creates InputSimulator Object
        String last = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Choices commands = new Choices();
            Choices displacements = new Choices();
            displacements.Add(new string[] { "10", "20", "30", "40", "50", "60", "70", "80", "90", "100", "110", "120", "130", "140", "150", "160", "170", "180", "190", "200", "210", "220", "230", "240", "250", "260", "270", "280", "290", "300", "310", "320", "330", "340", "350", "360", "370", "380", "390", "400", "410", "420", "430", "440", "450", "460", "470", "480", "490", "500" });
            commands.Add(new string[] { "say hello", "print my name", "Escape", "Click", "Double", "Right Arrow", "Left Arrow", "Scroll Up", "Scroll Down", "Mouse Up", "Mouse Left", "Mouse Down", "Mouse Right", "Mouse Stop", "End Control", "10", "50", "100", "500"});

            GrammarBuilder gDisplacement = new GrammarBuilder(displacements);
            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Append(commands);
            Grammar grammer = new Grammar(gBuilder);

            recEngine.LoadGrammarAsync(grammer);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.SpeechRecognized += recEngine_SpeechRecognized;
            
        }
        
        void MoveMouse(String direction, int magnitude)
        {
            int dirx = 0;
            int diry = 0;
            switch (direction)
            {
                case "Up":
                    diry = -1 * magnitude;
                    break;
                case "Down":
                    diry = magnitude;
                    break;
                case "Left":
                    dirx = -1 * magnitude;
                    break;
                case "Right":
                    dirx = magnitude;
                    break;

            }
            
            isim.Mouse.MoveMouseBy(dirx, diry);
        }
        

        void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            DateTime now = DateTime.Now;
            
            
            switch (e.Result.Text)
            {
                case "say hello":
                    MessageBox.Show("Hello rob. How are you?");
                    break;

                case "print my name":
                    textBox1.Text += System.Environment.NewLine + now + ": rob";
                    break;

                case "Escape":
                    SendKeys.Send("{ESC}");
                    textBox1.Text += System.Environment.NewLine + now + ": Escape Key Pressed";
                    break;

                case "Click":
                    isim.Mouse.LeftButtonClick();
                    textBox1.Text += System.Environment.NewLine + now + ": Mouse Clicked";
                    break;

                case "Double":
                    isim.Mouse.LeftButtonDoubleClick();
                    textBox1.Text += System.Environment.NewLine + now + ": Mouse Double Clicked";
                    break;

                case "Right Arrow":
                    SendKeys.Send("{RIGHT}");
                    textBox1.Text += System.Environment.NewLine + now + ": Right Arrow Key Pressed";
                    break;

                case "Left Arrow":
                    SendKeys.Send("{LEFT}");
                    textBox1.Text += System.Environment.NewLine + now + ": Left Arrow Key Pressed";
                    break;

                case "Scroll Up":
                    SendKeys.Send("{PGUP}");
                    textBox1.Text += System.Environment.NewLine + now + ": Scrolled Up";
                    break;

                case "Scroll Down": //Scrolls Down
                    SendKeys.Send("{PGDN}");
                    textBox1.Text += System.Environment.NewLine + now + ": Scrolled Down";
                    break;

                case "Mouse Up":
                    //isim.Mouse.MoveMouseBy(0, -300);
                    last = "Up";
                    textBox1.Text += System.Environment.NewLine + now + ": Mouse Moved Up";
                    break;

                case "Mouse Down":
                    //isim.Mouse.MoveMouseBy(0, 300);
                    last = "Down";
                    textBox1.Text += System.Environment.NewLine + now + ": Mouse Moved Down";
                    break;

                case "Mouse Left":
                    //isim.Mouse.MoveMouseBy(-25, 0);
                    last = "Left";
                    textBox1.Text += System.Environment.NewLine + now + ": Mouse Moved Left";
                    break;

                case "Mouse Right":
                    //isim.Mouse.MoveMouseBy(300, 0);
                    last = "Right";
                    textBox1.Text += System.Environment.NewLine + now + ": Mouse Moved Right";
                    break;

                    /*
                case "Mouse Stop":
                    
                    textBox1.Text += System.Environment.NewLine + now + ": Mouse Stopped";
                    break;
                    */

                case "End Control": //Ends Control
                    MessageBox.Show("Voice Control Ended.");
                    textBox1.Text += System.Environment.NewLine + now + ": Control Ended";
                    recEngine.RecognizeAsyncStop();
                    btnDisable.Enabled = false;
                    break;

                case "10":
                    textBox1.Text += " 10 px";
                    if ((last.Equals("Up") || last.Equals("Down")) || (last.Equals("Left") || last.Equals("Right")))
                    {
                        MoveMouse(last, 10);
                    }
                    break;

                case "50":
                    textBox1.Text += " 50 px";
                    if ((last.Equals("Up") || last.Equals("Down")) || (last.Equals("Left") || last.Equals("Right")))
                    {
                        MoveMouse(last, 50);
                    }
                    break;

                case "100":
                    textBox1.Text += " 100 px";
                    if ((last.Equals("Up") || last.Equals("Down")) || (last.Equals("Left") || last.Equals("Right")))
                    {
                        MoveMouse(last, 100);
                    }
                    break;

                case "500":
                    textBox1.Text += " 500 px";
                    if ((last.Equals("Up") || last.Equals("Down")) || (last.Equals("Left") || last.Equals("Right")))
                    {
                        MoveMouse(last, 500);
                    }
                    break;


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            btnDisable.Enabled = true;
            btnEnable.Enabled = false;
            textBox1.Text = "- Control Log -";
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsyncStop();
            btnDisable.Enabled = false;
            btnEnable.Enabled = true;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Welcome to Robert's Voice Controller. \n" +
                "\n" +
                "Commands: \n" +
                "\"Escape\": Escape Key \n" +
                "\"Click\": Clicks Mouse \n" +
                "\"Double\": Double Clicks Mouse \n" +
                "\"Right Arrow\": Right Arrow Button \n" +
                "\"Left Arrow\": Left Arrow Button \n" +
                "\"Scroll Up\": Page Up Key \n" +
                "\"Scroll Down\": Page Down Key \n" +
                "\n" +
                "Mouse Movement \n" +
                "\"Mouse Up\" \n" +
                "\"Mouse Down\" \n" +
                "\"Mouse Right\" \n" +
                "\"Mouse Left\" \n" +
                "\n" +
                "Note: Each mouse direction command may be paired with \n" +
                "either \'10\', \'50\', \'100\', or \'500\' in order to move \n" +
                "an amount of pixels in the indicated direction. \n" +
                "(e.g. \"Move Up 10\")");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
