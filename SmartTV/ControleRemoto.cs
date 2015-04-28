using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartTV
{
    public partial class ControleRemoto : Form
    {
        public ControleRemoto()
        {
            InitializeComponent();
        }

        private void btnRequestParing_Click(object sender, EventArgs e)
        {
            Requisitar("<auth><type>AuthKeyReq</type></auth>", true);
        }

        private void btnParear_Click(object sender, EventArgs e)
        {
            Requisitar(string.Format("<auth><type>AuthReq</type><value>{0}</value></auth>", txtParingKey.Text), true);
        }

        public int Requisitar(string request, bool auth = false)
        {
            try
            {
                IPAddress host = IPAddress.Parse(txtIP.Text);
                IPEndPoint hostep = new IPEndPoint(host, 8080);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


                socket.Connect(hostep);
                StringBuilder sb = new StringBuilder();

                if (auth)
                    sb.AppendLine("POST /roap/api/auth HTTP/1.1");
                else
                    sb.AppendLine("POST /roap/api/command HTTP/1.1");

                sb.AppendLine("Host: 192.168.131.137");
                sb.AppendLine("Content-type: application/atom+xml");
                sb.AppendLine("Content-length: " + request.Length);
                sb.AppendLine("Connection: close");
                sb.AppendLine("");
                sb.AppendLine(request);

                var byteCount = socket.Send(Encoding.ASCII.GetBytes(sb.ToString()));
                if (byteCount > 0)
                {
                    byte[] bytes = new byte[byteCount];
                    byteCount = socket.Receive(bytes, SocketFlags.None);
                    txtResponse.Text = Encoding.UTF8.GetString(bytes);
                }

                socket.Close();
            }
            catch (SocketException e)
            {
                Console.WriteLine("{0} Error code: {1}.", e.Message, e.ErrorCode);
                return (e.ErrorCode);
            }
            return 0;
        }

        enum CommandEnum
        {
            POWER = 1,
            NUMBER_0 = 2,
            NUMBER_1 = 3,
            NUMBER_2 = 4,
            NUMBER_3 = 5,
            NUMBER_4 = 6,
            NUMBER_5 = 7,
            NUMBER_6 = 8,
            NUMBER_7 = 9,
            NUMBER_8 = 10,
            NUMBER_9 = 11,
            UP = 12,
            DOWN = 13,
            LEFT = 14,
            RIGHT = 15,
            OK = 20,
            HOME_MENU = 21,
            BACK = 23,
            VOLUME_UP = 24,
            VOLUME_DOWN = 25,
            MUTE_TOGGLE = 26,
            CHANNEL_UP = 27,
            CHANNEL_DOWN = 28,
            BLUE = 29,
            GREEN = 30,
            RED = 31,
            YELLOW = 32,
            PLAY = 33,
            PAUSE = 34,
            STOP = 35,
            FAST_FORWARD = 36,
            REWIND = 37,
            SKIP_FORWARD = 38,
            SKIP_BACKWARD = 39,
            RECORD = 40,
            RECORDING_LIST = 41,
            REPEAT = 42,
            LIVE_TV = 43,
            EPG = 44,
            PROGRAM_INFORMATION = 45,
            ASPECT_RATIO = 46,
            EXTERNAL_INPUT = 47,
            PIP_SECONDARY_VIDEO = 48,
            SHOW_SUBTITLE = 49,
            PROGRAM_LIST = 50,
            TELE_TEXT = 51,
            MARK = 52,
            VIDEO_3D = 400,
            LR_3D = 401,
            DASH = 402,
            PREVIOUS_CHANNEL = 403,
            FAVORITE_CHANNEL = 404,
            QUICK_MENU = 405,
            TEXT_OPTION = 406,
            AUDIO_DESCRIPTION = 407,
            ENERGY_SAVING = 409,
            AV_MODE = 410,
            SIMPLINK = 411,
            EXIT = 412,
            RESERVATION_PROGRAM_LIST = 413,
            PIP_CHANNEL_UP = 414,
            PIP_CHANNEL_DOWN = 415,
            SWITCH_VIDEO = 416,
            APPS = 417
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SendCommand(CommandEnum.VOLUME_UP);

        }

        private void SendCommand(CommandEnum cmd)
        {
            Requisitar(string.Format("<command><name>HandleKeyInput</name><value>{0}</value></command>", (int)cmd));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SendCommand(CommandEnum.VOLUME_DOWN);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SendCommand(CommandEnum.UP);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            SendCommand(CommandEnum.RIGHT);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            SendCommand(CommandEnum.LEFT);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SendCommand(CommandEnum.DOWN);

        }

        private void button7_Click(object sender, EventArgs e)
        {
            SendCommand(CommandEnum.OK);

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            SendCommand(CommandEnum.UP);

        }

        private int i;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
            {

            switch ((int)keyData)
            {
                case (int)Keys.Up:
                    SendCommand(CommandEnum.UP);
                    break;
                case (int)Keys.Down:
                    SendCommand(CommandEnum.DOWN);
                    break;
                case (int)Keys.Right:
                    SendCommand(CommandEnum.RIGHT);
                    break;
                case (int)Keys.Left:
                    SendCommand(CommandEnum.LEFT);
                    break;
                case (int)Keys.Enter:
                    SendCommand(CommandEnum.OK);
                    break;
                case 109:
                    SendCommand(CommandEnum.VOLUME_DOWN);
                    break;
                case 107:
                    SendCommand(CommandEnum.VOLUME_UP);
                    break;
            }

            Thread.Sleep(80);
            // Call the base class
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Volume(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            SendCommand(CommandEnum.HOME_MENU);

        }

        private void button9_Click(object sender, EventArgs e)
        {
            SendCommand(CommandEnum.EXIT);

        }

        private void button10_Click(object sender, EventArgs e)
        {
            SendCommand(CommandEnum.BACK);

        }

        private void button11_Click(object sender, EventArgs e)
        {
            SendCommand(CommandEnum.LR_3D);

        }

        private void button13_Click(object sender, EventArgs e)
        {
            SendCommand(CommandEnum.CHANNEL_UP);

        }

        private void button12_Click(object sender, EventArgs e)
        {
            SendCommand(CommandEnum.CHANNEL_DOWN);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            SendCommand(CommandEnum.POWER);

        }

        private void button15_Click(object sender, EventArgs e)
        {
            SendCommand(CommandEnum.PROGRAM_LIST);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }




    }
}
