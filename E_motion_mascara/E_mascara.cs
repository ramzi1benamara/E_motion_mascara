using System;
using System.Linq;
using System.Windows.Forms;
using Leap;
using System.IO.Ports;

namespace E_motion_mascara
{
    public partial class E_mascara : Form, ILeapEventDelegate, IE_mascara
    { private Controller controller;
        private LeapEventListener listener;
        public E_mascara()
        {
            InitializeComponent();
            this.controller = new Controller();
            this.listener = new LeapEventListener(this);
            controller.AddListener(listener);
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //comboBox1.DataSource = SerialPort.GetPortNames();
            timer1.Start();

        }
        double rt = 0;
        public float positionx;
        public float positiony;
        public float positionz;
        public double velocityx;
        public double velocityy;
        public double velocityz;
        private void timer1_Tick(object sender, EventArgs e)
        {
            rt = rt + 1;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //sensport.Close();
        }

        delegate void LeapEventDelegate(string EventName);
        public void LeapEventNotification(string EventName)
        {
            if (!this.InvokeRequired)
            {
                switch (EventName)
                {
                    case "onInit":
                        break;
                    case "onConnect":
                        connectHandler();
                        break;
                    case "onFrame":
                        detectGesture(this.controller.Frame());
                        detectHandPosition(this.controller.Frame());
                        detectFingers(this.controller.Frame());
                        break;
                }
            }
            else
            {
                BeginInvoke(new LeapEventDelegate(LeapEventNotification), new object[] { EventName });
            }
        }
        
        private void detectFingers(Leap.Frame frame)
        {
            foreach (Finger finger in frame.Fingers)
            {
                // richTextBox2.AppendText("Finger Type:" + finger.Id + Environment.NewLine +
                //  "Finger position:" + finger.TipPosition + Environment.NewLine+
                // "Finger stabilized position "+ finger.StabilizedTipPosition);
                foreach (Bone.BoneType boneType in (Bone.BoneType[])Enum.GetValues(typeof(Bone.BoneType)))
                {
                    Bone bone = finger.Bone(boneType);
                    //richTextBox3.AppendText("Bone Type:" + bone.Type + Environment.NewLine + "Bone Direction" +
                    //   bone.Direction);
                }

            }
        }

        private void detectHandPosition(Leap.Frame frame)
        {
           
            if (frame.Hands.Count == 0) { velocityx = velocityy = velocityz = 0; }
            else
            {

                HandList allHands = frame.Hands;

                foreach (Hand hand in allHands)
                {

                    float pitch = hand.Direction.Pitch;
                    float yaw = hand.Direction.Yaw;
                    float roll = hand.Direction.Roll;
                     positionx = hand.PalmPosition.x;
                     positiony = hand.PalmPosition.y;
                     positionz = hand.PalmPosition.z;
                     velocityx = hand.PalmVelocity.x;
                     velocityy = hand.PalmVelocity.y;
                     velocityz = hand.PalmVelocity.z;


                    double degPitch = pitch * (180 / Math.PI);
                    double degYaw = yaw * (180 / Math.PI);
                    double degRoll = roll * (180 / Math.PI);

                    int intPitch = (int)degPitch;
                    int intYaw = (int)degYaw;
                    int intRoll = (int)degRoll;


                    string a = positionx.ToString();
                    string b = positiony.ToString();
                    string c = positionz.ToString();
                    string d = velocityx.ToString();
                    string e = velocityy.ToString();
                    string f = velocityz.ToString();
                    this.Invoke(new EventHandler(displaydata_event));
                    // data_tb.Text= "positionx,positiony,positionz,velocityx,velocityy,velocityz";
                    //data_tb.Text = "";

                    data_tb.AppendText(DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "," + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "," + a + "," + b + "," + c + "," + d + "," + e + "," + f + "\n");
                }
            }

        }

        private void detectGesture(Leap.Frame frame)
        {
            GestureList gestures = frame.Gestures();
            for (int i = 0; i < gestures.Count(); i++)
            {
                Gesture gesture = gestures[i];
                switch (gesture.Type)
                {
                    case Gesture.GestureType.TYPE_CIRCLE:
                        //richTextBox1.AppendText("Movement detected!" + Environment.NewLine);
                        break;

                    case Gesture.GestureType.TYPE_KEY_TAP:
                        //richTextBox1.AppendText("Movement detected!" + Environment.NewLine);

                        break;

                    case Gesture.GestureType.TYPE_SWIPE:
                       // richTextBox1.AppendText("Translation detected!" + Environment.NewLine);

                        break;

                    case Gesture.GestureType.TYPE_INVALID:
                        //richTextBox1.AppendText("NO MOVEMENT ");
                        break;


                }
            }
        }

        private void connectHandler()
        {
            this.controller.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);
            this.controller.EnableGesture(Gesture.GestureType.TYPE_KEY_TAP);
            this.controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE);
            this.controller.EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);
            this.controller.EnableGesture(Gesture.GestureType.TYPE_INVALID);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {
        }

       

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtRoll_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtYaw_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPitch_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click_1(object sender, EventArgs e)
        {

        }

        private void Start_btn(object sender, EventArgs e) { 
         
        }

        private void sensport_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            
            //data_combine = sensport.ReadLine();
        }

        private void displaydata_event(object sender, EventArgs e)
        {
            
        }

        private void Stop_btn(object sender, EventArgs e)
        {
           
        }

        private void Save_btn(object sender, EventArgs e)
        {
            try
            { if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                  
                    System.IO.File.WriteAllText(saveFileDialog1.FileName+".csv", data_tb.Text);
                    MessageBox.Show(" Movement has been saved " );
                }
                //string pathfile = @"C:\Users\Ramzi\Desktop\DATA_C#_matlab\";
                //string filename = "Test_movement.txt";
                //System.IO.File.WriteAllText(pathfile + filename, data_tb.Text);
                //MessageBox.Show("Movement has been saved " + pathfile, "Save File");
            }
            catch (Exception ex3)
            {
                MessageBox.Show(ex3.Message, "ERROR");
            }
        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void data_tb_TextChanged(object sender, EventArgs e)
        {

        }
    }
    public interface ILeapEventDelegate
    {
        void LeapEventNotification(string EventName);
    }

    public class LeapEventListener : Listener
    {
        ILeapEventDelegate eventDelegate;

        public LeapEventListener(ILeapEventDelegate delegateObject)
        {
            this.eventDelegate = delegateObject;
        }
        public override void OnInit(Controller controller)
        {
            this.eventDelegate.LeapEventNotification("onInit");
        }
        public override void OnConnect(Controller controller)
        {
            this.eventDelegate.LeapEventNotification("onConnect");
        }
        public override void OnFrame(Controller controller)
        {
            this.eventDelegate.LeapEventNotification("onFrame");
        }
        public override void OnExit(Controller controller)
        {
            this.eventDelegate.LeapEventNotification("onExit");
        }
        public override void OnDisconnect(Controller controller)
        {
            this.eventDelegate.LeapEventNotification("onDisconnect");
        }
    }
}

