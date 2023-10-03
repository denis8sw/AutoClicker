using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput.Native;
using WindowsInput;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace MC_AutoClicker


{

    public partial class Form1 : Form
    {

        InputSimulator sim = new InputSimulator();
        Random random = new Random();

        bool isClicker = false;
        bool isRandomizer = false;
        private bool isSimulatingClick = false;
        bool normalMode = false;
        bool normalMode2 = false;

        bool cpsBoostMode = false;
        bool cpsBoostMode2 = false;

        bool rightClicker = false;

        bool tripple = false;
        bool fourth = false;

        bool tripple2 = false;
        bool fourth2 = false;

        private bool theKeybindK1;
        private bool theKeybindK2;
        private bool theKeybindK3;
        private bool theKeybindK4;
        private bool theKeybindK5;

        private bool theKeybindK6;
        private bool theKeybindK7;



        private bool isDragging = false;
        private Point dragOffset;

        sbyte progressBarRed = 2;



        public Form1()
        {
            InitializeComponent();
        }

        public void GetSettings()
        {
            textBox1.Text = Properties.Settings.Default.CPSBoostL;
            textBox2.Text = Properties.Settings.Default.NormalModeL;
            textBox3.Text = Properties.Settings.Default.CPSBoostR;
            textBox4.Text = Properties.Settings.Default.NormalModeR;
            textBox5.Text = Properties.Settings.Default.Randomizer;

            textBox7.Text = Properties.Settings.Default.EnableL;
            textBox6.Text = Properties.Settings.Default.EnableR;
        }

        public void saveSettings()
        {
            Properties.Settings.Default.CPSBoostL = textBox1.Text;
            Properties.Settings.Default.NormalModeL = textBox2.Text;
            Properties.Settings.Default.CPSBoostR = textBox3.Text;
            Properties.Settings.Default.NormalModeR = textBox4.Text;
            Properties.Settings.Default.Randomizer = textBox5.Text;

            Properties.Settings.Default.EnableL = textBox7.Text;
            Properties.Settings.Default.EnableR = textBox6.Text;

            Properties.Settings.Default.Save();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
            timer3.Start();
            timer4.Start();
            Randomizer.Start();

            this.MouseDown += MainForm_MouseDown;
            this.MouseMove += MainForm_MouseMove;
            this.MouseUp += MainForm_MouseUp;


            ModifyProgressBarColor.SetState(progressBar1, progressBarRed);

            checkBox3.Visible = false;
            checkBox4.Visible = false;

            checkBox5.Visible = false;
            checkBox6.Visible = false;

            GetSettings();
        }


        private void savebtn_Click(object sender, EventArgs e)
        {
            saveSettings();
            GetSettings();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {



            label1.Text = "Delay " + timer1.Interval + " ms";

            if (isRandomizer == false)
            {
                timer1.Interval = trackBar1.Value;
            }
            else
            {
                int minInterval = trackBar1.Value;
                int maxInterval = trackBar1.Value + 20;
                int randomInterval = random.Next(minInterval, maxInterval + 1);
                timer1.Interval = randomInterval;
            }



            if (isClicker && sim.InputDeviceState.IsKeyDown(VirtualKeyCode.LBUTTON) && cpsBoostMode && tripple == false && fourth == false)
            {
                sim.Mouse.LeftButtonClick();
                sim.Mouse.LeftButtonClick();
            }

            if (isClicker && sim.InputDeviceState.IsKeyDown(VirtualKeyCode.LBUTTON) && cpsBoostMode && tripple == true)
            {
                sim.Mouse.LeftButtonClick();
                sim.Mouse.LeftButtonClick();
                sim.Mouse.LeftButtonClick();
            }

            if (isClicker && sim.InputDeviceState.IsKeyDown(VirtualKeyCode.LBUTTON) && cpsBoostMode && fourth == true)
            {
                sim.Mouse.LeftButtonClick();
                sim.Mouse.LeftButtonClick();
                sim.Mouse.LeftButtonClick();
                sim.Mouse.LeftButtonClick();
            }


            if (isClicker && normalMode)
            {
                sim.Mouse.LeftButtonClick();
            }

        }


        private void timer4_Tick(object sender, EventArgs e)
        {

            if (isRandomizer == false)
            {
                timer4.Interval = trackBar3.Value;
            }
            else
            {
                int minInterval = trackBar3.Value;
                int maxInterval = trackBar3.Value + 20;
                int randomInterval = random.Next(minInterval, maxInterval + 1);
                timer4.Interval = randomInterval;
            }



            if (rightClicker && sim.InputDeviceState.IsKeyDown(VirtualKeyCode.RBUTTON) && cpsBoostMode2 && tripple2 == false && fourth2 == false)
            {
                sim.Mouse.RightButtonClick();
                sim.Mouse.RightButtonClick();
            }

            if (rightClicker && sim.InputDeviceState.IsKeyDown(VirtualKeyCode.RBUTTON) && cpsBoostMode2 && tripple2 == true)
            {
                sim.Mouse.RightButtonClick();
                sim.Mouse.RightButtonClick();
                sim.Mouse.RightButtonClick();
            }

            if (rightClicker && sim.InputDeviceState.IsKeyDown(VirtualKeyCode.RBUTTON) && cpsBoostMode2 && fourth2 == true)
            {
                sim.Mouse.RightButtonClick();
                sim.Mouse.RightButtonClick();
                sim.Mouse.RightButtonClick();
                sim.Mouse.RightButtonClick();
            }


            if (rightClicker && normalMode2)
            {
                sim.Mouse.RightButtonClick();

            }
        }


        private void timer2_Tick(object sender, EventArgs e)
        {

            if (CpsBoost.Checked)
            {
                checkBox3.Visible = true;
                checkBox4.Visible = true;
            }
            else
            {
                checkBox3.Visible = false;
                checkBox4.Visible = false;
            }

            if (checkBox1.Checked)
            {
                checkBox5.Visible = true;
                checkBox6.Visible = true;
            }
            else
            {
                checkBox5.Visible = false;
                checkBox6.Visible = false;
            }


            if (checkBox3.Checked)
            {
                checkBox4.Enabled = false;
                tripple = true;
            }
            else
            {
                checkBox4.Enabled = true;
                tripple = false;
            }


            if (checkBox4.Checked)
            {
                checkBox3.Enabled = false;
                fourth = true;
            }
            else
            {
                checkBox3.Enabled = true;
                fourth = false;

            }

            if (checkBox5.Checked)
            {
                checkBox6.Enabled = false;
                tripple2 = true;
            }
            else
            {
                checkBox6.Enabled = true;
                tripple2 = false;
            }

            if (checkBox6.Checked)
            {
                checkBox5.Enabled = false;
                fourth2 = true;
            }
            else
            {
                checkBox5.Enabled = true;
                fourth2 = false;
            }

            if(textBox1.Text == "")
            {
               
            } else
            {
                char keyBind1 = char.Parse(textBox1.Text);
                VirtualKeyCode keyCode = ConvertCharToVirtualKeyCode(keyBind1);
                theKeybindK1 = sim.InputDeviceState.IsKeyDown(keyCode);
            }

            if (textBox2.Text == "")
            {

            }
            else
            {
                char keyBind1 = char.Parse(textBox2.Text);
                VirtualKeyCode keyCode = ConvertCharToVirtualKeyCode(keyBind1);
                theKeybindK2 = sim.InputDeviceState.IsKeyDown(keyCode);
            }

            if (textBox3.Text == "")
            {

            }
            else
            {
                char keyBind1 = char.Parse(textBox3.Text);
                VirtualKeyCode keyCode = ConvertCharToVirtualKeyCode(keyBind1);
                theKeybindK3 = sim.InputDeviceState.IsKeyDown(keyCode);
            }

            if (textBox4.Text == "")
            {

            }
            else
            {
                char keyBind1 = char.Parse(textBox4.Text);
                VirtualKeyCode keyCode = ConvertCharToVirtualKeyCode(keyBind1);
                theKeybindK4 = sim.InputDeviceState.IsKeyDown(keyCode);
            }

            if (textBox5.Text == "")
            {

            }
            else
            {
                char keyBind1 = char.Parse(textBox5.Text);
                VirtualKeyCode keyCode = ConvertCharToVirtualKeyCode(keyBind1);
                theKeybindK5 = sim.InputDeviceState.IsKeyDown(keyCode);
            }

            if (textBox7.Text == "")
            {

            }
            else
            {
                char keyBind1 = char.Parse(textBox7.Text);
                VirtualKeyCode keyCode = ConvertCharToVirtualKeyCode(keyBind1);
                theKeybindK7 = sim.InputDeviceState.IsKeyDown(keyCode);
            }

            if (textBox6.Text == "")
            {

            }
            else
            {
                char keyBind1 = char.Parse(textBox6.Text);
                VirtualKeyCode keyCode = ConvertCharToVirtualKeyCode(keyBind1);
                theKeybindK6 = sim.InputDeviceState.IsKeyDown(keyCode);
            }


            var keyBind = sim.InputDeviceState.IsKeyDown(VirtualKeyCode.VK_G);
            var keyBind2 = sim.InputDeviceState.IsKeyDown(VirtualKeyCode.VK_F);
            var keyBind3 = sim.InputDeviceState.IsKeyDown(VirtualKeyCode.VK_V);
            var keyBind4 = sim.InputDeviceState.IsKeyDown(VirtualKeyCode.VK_H);


            var keyBind5 = sim.InputDeviceState.IsKeyDown(VirtualKeyCode.VK_J);
            var keyBind6 = sim.InputDeviceState.IsKeyDown(VirtualKeyCode.VK_C);

            var keyBind7 = sim.InputDeviceState.IsKeyDown(VirtualKeyCode.VK_U);


            CpsBoost.Text = "CPSBoostMode[" + textBox1.Text + "]";
            NormalCps.Text = "NormalMode[" + textBox2.Text + "]";
            
            checkBox1.Text = "CPSBoostMode[" + textBox3.Text + "]";
            checkBox2.Text = "NormalMode[" + textBox4.Text + "]";

            randomCheck.Text = "Randomizer[" + textBox5.Text + "]";


          

            if (AutoClicker.Checked)
            {
                toggleLeftClick.BackColor = Color.Green;
                toggleLeftClick.Text = "Enabled [G]";
                toggleLeftClick.Text = "Enabled [" + textBox7.Text + "]";
            }
            else
            {
                toggleLeftClick.BackColor = Color.Red;
                toggleLeftClick.Text = "Disabled [G]";
                toggleLeftClick.Text = "Disabled [" + textBox7.Text + "]";
            }

            if (keyBind && textBox7.Text == "G")
            {
                Thread.Sleep(50);
                int num = 1;

                if (num == 1)
                {
                    isClicker = !isClicker;
                }
                Thread.Sleep(50);
                num = 0;

                AutoClicker.Checked = !AutoClicker.Checked;
            } else if (textBox7.Text == "")
            {

            }

            if(theKeybindK7)
            {
                Thread.Sleep(50);
                int num = 1;

                if (num == 1)
                {
                    isClicker = !isClicker;
                }
                Thread.Sleep(50);
                num = 0;

                AutoClicker.Checked = !AutoClicker.Checked;
            }

            if (keyBind2 && textBox2.Text == "F")
            {
                Thread.Sleep(50);
                int num = 1;

                if (num == 1)
                {
                    normalMode = !normalMode;
                }
                Thread.Sleep(50);
                num = 0;

                NormalCps.Checked = !NormalCps.Checked;
            } else if (textBox2.Text == "")
            {

            }

            if (theKeybindK2)
            {
                Thread.Sleep(50);
                int num = 1;

                if (num == 1)
                {
                    normalMode = !normalMode;
                }
                Thread.Sleep(50);
                num = 0;

                NormalCps.Checked = !NormalCps.Checked;
            }



            if (keyBind3 && textBox1.Text == "V")
            {
                Thread.Sleep(50);
                int num = 1;

                if (num == 1)
                {
                    cpsBoostMode = !cpsBoostMode;
                }
                Thread.Sleep(50);
                num = 0;

                CpsBoost.Checked = !CpsBoost.Checked;
            } else if (textBox1.Text == "")
            {

            }

            if (theKeybindK1)
            {
                Thread.Sleep(50);
                int num = 1;

                if (num == 1)
                {
                    cpsBoostMode = !cpsBoostMode;
                }
                Thread.Sleep(50);
                num = 0;

                CpsBoost.Checked = !CpsBoost.Checked;
            }






            if (keyBind4 && textBox5.Text == "H")
            {
                Thread.Sleep(50);
                int num = 1;

                if (num == 1)
                {
                    isRandomizer = !isRandomizer;
                }
                Thread.Sleep(50);
                num = 0;

                randomCheck.Checked = !randomCheck.Checked;
            } else if(textBox5.Text == "")
            {

            }

            if (theKeybindK5)
            {
                Thread.Sleep(50);
                int num = 1;

                if (num == 1)
                {
                    isRandomizer = !isRandomizer;
                }
                Thread.Sleep(50);
                num = 0;

                randomCheck.Checked = !randomCheck.Checked;
            }


            if (keyBind5 && textBox3.Text == "J")
            {
                Thread.Sleep(50);
                int num = 1;

                if (num == 1)
                {
                    cpsBoostMode2 = !cpsBoostMode2;
                }
                Thread.Sleep(50);
                num = 0;

                checkBox1.Checked = !checkBox1.Checked;
            } else if(textBox3.Text == "")
            {

            }

            if (theKeybindK3)
            {
                Thread.Sleep(50);
                int num = 1;

                if (num == 1)
                {
                    cpsBoostMode2 = !cpsBoostMode2;
                }
                Thread.Sleep(50);
                num = 0;

                checkBox1.Checked = !checkBox1.Checked;
            }


            if (keyBind6 && textBox4.Text == "C")
            {
                Thread.Sleep(50);
                int num = 1;

                if (num == 1)
                {
                    normalMode2 = !normalMode2;
                }
                Thread.Sleep(50);
                num = 0;

                checkBox2.Checked = !checkBox2.Checked;
            } else if (textBox4.Text == "")
            {

            }

            if (theKeybindK4)
            {
                Thread.Sleep(50);
                int num = 1;

                if (num == 1)
                {
                    normalMode2 = !normalMode2;
                }
                Thread.Sleep(50);
                num = 0;

                checkBox2.Checked = !checkBox2.Checked;
            }

            if (keyBind7 && textBox6.Text == "U")
            {
                Thread.Sleep(50);
                int num = 1;

                if (num == 1)
                {
                    rightClicker = !rightClicker;
                }
                Thread.Sleep(50);
                num = 0;
            } else if (textBox6.Text == "")
            {

            }

            if(theKeybindK6)
            {
                Thread.Sleep(50);
                int num = 1;

                if (num == 1)
                {
                    rightClicker = !rightClicker;
                }
                Thread.Sleep(50);
                num = 0;
            }



            if (rightClicker)
            {
                toggleRightClicker.BackColor = Color.Green;
                toggleRightClicker.Text = "Enabled [" + textBox6.Text + "]";
            }
            else
            {
                toggleRightClicker.BackColor = Color.Red;
                toggleRightClicker.Text = "Disabled [" + textBox6.Text + "]";
            }



            if (!AutoClicker.Checked)
            {
                isClicker = false;
            }

            if (!randomCheck.Checked)
            {
                isRandomizer = false;
            }

            if (!NormalCps.Checked)
            {
                normalMode = false;
            }

            if (!CpsBoost.Checked)
            {
                cpsBoostMode = false;
            }


            if (!checkBox1.Checked)
            {
                cpsBoostMode2 = false;
            }


            if (!checkBox2.Checked)
            {
                normalMode2 = false;
            }

            


        }


        private void Randomizer_Tick(object sender, EventArgs e)
        {
            if (isRandomizer)
            {
                trackBar2.Value = random.Next(40, 100);

                if (trackBar2.Value > 80)
                {
                    Thread.Sleep(60);
                    trackBar2.Value = 100;
                }

                progressBar1.Value = random.Next(40, 100);
            }

        }

        private void AutoClicker_CheckedChanged(object sender, EventArgs e)
        {
            isClicker = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            isRandomizer = true;
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            cpsBoostMode = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            normalMode = true;
        }



        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragOffset = e.Location;
            }
        }


        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point newLocation = this.PointToScreen(e.Location);
                newLocation.Offset(-dragOffset.X, -dragOffset.Y);
                this.Location = newLocation;
            }
        }


        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mouseHoverLabel(object sender, EventArgs e)
        {

            label2.Cursor = System.Windows.Forms.Cursors.Hand;
            minIcon.Cursor = System.Windows.Forms.Cursors.Hand;



        }

        private void mouseUnHoverLabel(object sender, EventArgs e)
        {
            label2.Cursor = System.Windows.Forms.Cursors.Default;
            minIcon.Cursor = System.Windows.Forms.Cursors.Default;

        }



        int r = 255, g = 0, b = 0;

        private void rjButton1_Click(object sender, EventArgs e)
        {
            AutoClicker.Checked = !AutoClicker.Checked;
            isClicker = !isClicker;

            if (AutoClicker.Checked)
            {
                toggleLeftClick.BackColor = Color.Green;
                toggleLeftClick.Text = "Enabled [G]";
            }
            else
            {
                toggleLeftClick.BackColor = Color.Red;
                toggleLeftClick.Text = "Disabled [G]";
            }
        }

        private void HoverBtn(object sender, EventArgs e)
        {
            toggleLeftClick.Cursor = System.Windows.Forms.Cursors.Hand;
            toggleRightClicker.Cursor = System.Windows.Forms.Cursors.Hand;

            savebtn.Cursor = System.Windows.Forms.Cursors.Hand;
        }

        private void HoverUnBtn(object sender, EventArgs e)
        {
            toggleLeftClick.Cursor = System.Windows.Forms.Cursors.Default;
            toggleRightClicker.Cursor = System.Windows.Forms.Cursors.Default;

            savebtn.Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void rjButton1_Click_1(object sender, EventArgs e)
        {
            rightClicker = !rightClicker;


            if (rightClicker)
            {
                toggleRightClicker.BackColor = Color.Green;
                toggleRightClicker.Text = "Enabled [U]";
            }
            else
            {
                toggleRightClicker.BackColor = Color.Red;
                toggleRightClicker.Text = "Disabled [U]";
            }
        }

        private void checkBox1_CheckedChanged_2(object sender, EventArgs e)
        {
            cpsBoostMode2 = true;
        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            normalMode2 = true;
        }

        private VirtualKeyCode ConvertCharToVirtualKeyCode(char character)
        {
            // Map the character to a VirtualKeyCode.
            switch (char.ToUpper(character))
            {
                case 'A':
                    return VirtualKeyCode.VK_A;
                case 'B':
                    return VirtualKeyCode.VK_B;
                case 'C':
                    return VirtualKeyCode.VK_C;
                case 'D':
                    return VirtualKeyCode.VK_D;
                case 'E':
                    return VirtualKeyCode.VK_E;
                case 'F':
                    return VirtualKeyCode.VK_F;
                case 'G':
                    return VirtualKeyCode.VK_G;
                case 'H':
                    return VirtualKeyCode.VK_H;
                case 'I':
                    return VirtualKeyCode.VK_I;
                case 'J':
                    return VirtualKeyCode.VK_J;
                case 'K':
                    return VirtualKeyCode.VK_K;
                case 'L':
                    return VirtualKeyCode.VK_L;
                case 'M':
                    return VirtualKeyCode.VK_M;
                case 'N':
                    return VirtualKeyCode.VK_N;
                case 'O':
                    return VirtualKeyCode.VK_O;
                case 'P':
                    return VirtualKeyCode.VK_P;
                case 'Q':
                    return VirtualKeyCode.VK_Q;
                case 'R':
                    return VirtualKeyCode.VK_R;
                case 'S':
                    return VirtualKeyCode.VK_S;
                case 'T':
                    return VirtualKeyCode.VK_T;
                case 'U':
                    return VirtualKeyCode.VK_U;
                case 'V':
                    return VirtualKeyCode.VK_V;
                case 'W':
                    return VirtualKeyCode.VK_W;
                case 'X':
                    return VirtualKeyCode.VK_X;
                case 'Y':
                    return VirtualKeyCode.VK_Y;
                case 'Z':
                    return VirtualKeyCode.VK_Z;
                case '0':
                    return VirtualKeyCode.VK_0;
                case '1':
                    return VirtualKeyCode.VK_1;
                case '2':
                    return VirtualKeyCode.VK_2;
                case '3':
                    return VirtualKeyCode.VK_3;
                case '4':
                    return VirtualKeyCode.VK_4;
                case '5':
                    return VirtualKeyCode.VK_5;
                case '6':
                    return VirtualKeyCode.VK_6;
                case '7':
                    return VirtualKeyCode.VK_7;
                case '8':
                    return VirtualKeyCode.VK_8;
                case '9':
                    return VirtualKeyCode.VK_9;
                default:
                    return VirtualKeyCode.VK_9;
            }
        }

       

        private void label5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        private void timer3_Tick(object sender, EventArgs e)
        {
            BorderLabel.BackColor = Color.FromArgb(r, g, b);
            BorderLabel.ForeColor = Color.FromArgb(r, g, b);

            label2.ForeColor = Color.FromArgb(r, g, b);
            Title.ForeColor = Color.FromArgb(r, g, b);

            minIcon.ForeColor = Color.FromArgb(r, g, b);

            if (r > 0 && b == 0)
            {
                r--;
                g++;
            }

            if (g > 0 && r == 0)
            {
                g--;
                b++;
            }

            if (b > 0 && g == 0)
            {
                b--;
                r++;
            }
        }
    }


    public static class ModifyProgressBarColor
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
        public static void SetState(this ProgressBar pBar, int state)
        {
            SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
        }
    }



}
