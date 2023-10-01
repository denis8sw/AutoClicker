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

        private bool isDragging = false;
        private Point dragOffset;

        sbyte progressBarRed = 2;



        public Form1()
        {
            InitializeComponent();
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

        }


  
        private void timer1_Tick(object sender, EventArgs e)
        {

            label1.Text = "Delay " + timer1.Interval + " ms"; 

            if (isRandomizer == false)
            {
                timer1.Interval = trackBar1.Value;
            } else
            {
                int minInterval = trackBar1.Value;
                int maxInterval = trackBar1.Value + 20;
                int randomInterval = random.Next(minInterval, maxInterval + 1);
                timer1.Interval = randomInterval;
            }



            if (isClicker && sim.InputDeviceState.IsKeyDown(VirtualKeyCode.LBUTTON) && cpsBoostMode)
            {
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



            if (rightClicker && sim.InputDeviceState.IsKeyDown(VirtualKeyCode.RBUTTON) && cpsBoostMode2)
            {
                sim.Mouse.RightButtonClick();
            }



            if (rightClicker && normalMode2)
            {
                sim.Mouse.RightButtonClick();
   
            }
        }


        private void timer2_Tick(object sender, EventArgs e)
        {
            var keyBind = sim.InputDeviceState.IsKeyDown(VirtualKeyCode.VK_G);
            var keyBind2 = sim.InputDeviceState.IsKeyDown(VirtualKeyCode.VK_F);
            var keyBind3 = sim.InputDeviceState.IsKeyDown(VirtualKeyCode.VK_V);
            var keyBind4 = sim.InputDeviceState.IsKeyDown(VirtualKeyCode.VK_H);


            var keyBind5 = sim.InputDeviceState.IsKeyDown(VirtualKeyCode.VK_J);
            var keyBind6 = sim.InputDeviceState.IsKeyDown(VirtualKeyCode.VK_C);

            var keyBind7 = sim.InputDeviceState.IsKeyDown(VirtualKeyCode.VK_U);



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

            if (keyBind)
            {
                Thread.Sleep(50);
                int num = 1;

                if(num == 1)
                {
                    isClicker = !isClicker;
                }
                Thread.Sleep(50);
                num = 0;

                AutoClicker.Checked = !AutoClicker.Checked;
            }

            if (keyBind2)
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

            if (keyBind3)
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

            if (keyBind4)
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


            if(keyBind5)
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


            if (keyBind6)
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

            if(keyBind7)
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
                toggleRightClicker.Text = "Enabled [U]";
            }
            else
            {
                toggleRightClicker.BackColor = Color.Red;
                toggleRightClicker.Text = "Disabled [U]";
            }

            if (!AutoClicker.Checked)
            {
                isClicker = false;
            }

            if(!randomCheck.Checked)
            {
                isRandomizer = false;
            }

            if(!NormalCps.Checked)
            {
                normalMode = false;
            }

            if(!CpsBoost.Checked)
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
            if(isRandomizer)
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

            if(AutoClicker.Checked)
            {
                toggleLeftClick.BackColor = Color.Green;
                toggleLeftClick.Text = "Enabled [G]";
            } else
            {
                toggleLeftClick.BackColor = Color.Red;
                toggleLeftClick.Text = "Disabled [G]";
            }
        }

        private void HoverBtn(object sender, EventArgs e)
        {
            toggleLeftClick.Cursor = System.Windows.Forms.Cursors.Hand;
            toggleRightClicker.Cursor = System.Windows.Forms.Cursors.Hand;
        }

        private void HoverUnBtn(object sender, EventArgs e)
        {
            toggleLeftClick.Cursor = System.Windows.Forms.Cursors.Default;
            toggleRightClicker.Cursor = System.Windows.Forms.Cursors.Default;
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

            if(g > 0 && r == 0)
            {
                g--;
                b++;
            }

            if(b > 0 && g == 0)
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
