using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// get win media player interface
using WMPLib;
// import saved highscore
using Honk_a_Pepe.Properties;

namespace Honk_a_Pepe
{
    public partial class Form1 : Form
    {
        Random r = new Random();
        int score = 0;
        int misses = 0;
        Boolean running = true;
        WMPLib.WindowsMediaPlayer wmp = new WMPLib.WindowsMediaPlayer();
        Boolean flag = true;
        int highscore = int.Parse(Settings.Default["highscore"].ToString());
        int counter;

        public Form1()
        {
            InitializeComponent();
            wmp.URL = "honk_song.mp3";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // play background music
            wmp.controls.play();
            // make it full screen
            this.TopMost = true;
            this.WindowState = FormWindowState.Maximized;
            // disable resize button
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            // initial score
            toolStripStatusLabel1.Text = "Top Score: 0\n     Misses: 0";
            toolStripStatusLabel2.Text = "HIGHSCORE: " + highscore;
        }
        
        void honk()
        {
            if (running)
            {
                score++;
                toolStripStatusLabel1.Text = "Top Score: " + score + "\n     Misses: " + misses;
                toolStripStatusLabel2.Text = "HIGHSCORE: " + highscore;
                // re-apprear
                int x, y;
                x = r.Next(0, 1450);
                y = r.Next(0, 650);
                pictureBox1.Location = new Point(x, y);
                // sound effect
                System.Media.SoundPlayer sp = new System.Media.SoundPlayer();
                sp.SoundLocation = "honk.wav";
                sp.Play();
            }
            else
            {
                toolStripStatusLabel1.Text = "GAME OVER\nTop Score: " + score + "\n     Misses: " + misses;
                toolStripStatusLabel2.Text = "HIGHSCORE: " + highscore;
            }
        }

        void miss()
        {
            if (running)
            {
                misses++;
                toolStripStatusLabel1.Text = "Top Score: " + score + "\n     Misses: " + misses;
                toolStripStatusLabel2.Text = "HIGHSCORE: " + highscore;
            }
            else
            {
                toolStripStatusLabel1.Text = "GAME OVER\nTop Score: " + score + "\n     Misses: " + misses;
                toolStripStatusLabel2.Text = "HIGHSCORE: " + highscore;
            }
        }

        void restart()
        {
            score = 0;
            misses = 0;
            toolStripStatusLabel1.Text = "Top Score: " + score + "\n     Misses: " + misses;
            toolStripStatusLabel2.Text = "HIGHSCORE: " + highscore;
            running = true;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // new position section
            int x, y;
            x = r.Next(0, 1420);
            y = r.Next(0, 650);
            pictureBox1.Location = new Point(x, y);
            // game over section
            if (misses >= 15) {
                timer1.Stop();
                toolStripStatusLabel1.Text = "GAME OVER\nTop Score: " + score + "\n     Misses: " + misses ;
                running = false;
                if(score > highscore)
                {
                    highscore = score;
                    Settings.Default["highscore"] = score;
                    Settings.Default.Save();
                }
             // replay button section
                if(flag == true)
                {
                    Button button = new Button();
                    button.Text = "Play again";
                    button.Click += new System.EventHandler(this.button1_Click);
                    statusStrip1.Items.Add(new ToolStripControlHost(button));
                    flag = false;
                }
            }
            // counter section
            if (counter < 17) counter++;
            if(counter == 15)
            {
                pictureBox3.Location = new Point(1380, 0);
                pictureBox2.Location = new Point(1460, 0);
            }
            if(counter == 17)
            {
                pictureBox3.Location = new Point(-200, -200);
                pictureBox2.Location = new Point(-200, -200);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            restart();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            honk();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            miss();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // red pill
            score = score * 2;
            toolStripStatusLabel1.Text = "Top Score: " + score + "\n     Misses: " + misses;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            // blue pill
            score = score + 15;
            toolStripStatusLabel1.Text = "Top Score: " + score + "\n     Misses: " + misses;
        }
    }
}
