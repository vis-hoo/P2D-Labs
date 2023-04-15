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
    public partial class Game5 : Form
    {
        int Points;

        public Game5(int points)
        {
            InitializeComponent();
            Points = points;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form f = new Form();
            if (Points == 0 || Points == 1) f = new Final1();
            else if (Points == 2 || Points == 3) f = new Final2();
            else if (Points == 4 || Points == 5) f = new Final3();
            f.Show();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Points++;
            Form f = new Form();
            if (Points == 0 || Points == 1) f = new Final1();
            else if (Points == 2 || Points == 3) f = new Final2();
            else if (Points == 4 || Points == 5) f = new Final3();
            f.Show();
            Close();
        }

        private void Game5_Load(object sender, EventArgs e)
        {
            Height = 600;
            Width = 390;
            BackgroundImageLayout = ImageLayout.Stretch;
            BackgroundImage = Properties.Resources._6;
            StartPosition = FormStartPosition.CenterScreen;
            MinimizeBox = false;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            button1.Location = new Point(26, 425);
            button1.Width = 157;
            button1.Height = 104;
            button1.Text = "";
            button1.FlatStyle = FlatStyle.Flat;
            button1.BackColor = Color.Transparent;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;

            button2.Location = new Point(196, 425);
            button2.Width = 150;
            button2.Height = 104;
            button2.Text = "";
            button2.FlatStyle = FlatStyle.Flat;
            button2.BackColor = Color.Transparent;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button2.FlatAppearance.MouseDownBackColor = Color.Transparent;
        }
    }
}
