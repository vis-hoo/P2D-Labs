using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Taller2
{
    public partial class Game4 : Form
    {
        int Points;

        public Game4(int points)
        {
            InitializeComponent();
            Points = points;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Game5 g5 = new Game5(Points);
            g5.Show();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Points++;
            Game5 g5 = new Game5(Points);
            g5.Show();
            Close();
        }

        private void Game4_Load(object sender, EventArgs e)
        {
            Height = 600;
            Width = 390;
            BackgroundImageLayout = ImageLayout.Stretch;
            BackgroundImage = Properties.Resources._5;
            StartPosition = FormStartPosition.CenterScreen;
            MinimizeBox = false;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            button1.Location = new Point(26, 433);
            button1.Width = 152;
            button1.Height = 97;
            button1.Text = "";
            button1.FlatStyle = FlatStyle.Flat;
            button1.BackColor = Color.Transparent;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;

            button2.Location = new Point(194, 433);
            button2.Width = 156;
            button2.Height = 97;
            button2.Text = "";
            button2.FlatStyle = FlatStyle.Flat;
            button2.BackColor = Color.Transparent;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button2.FlatAppearance.MouseDownBackColor = Color.Transparent;
        }
    }
}
