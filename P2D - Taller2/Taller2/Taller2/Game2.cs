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
    public partial class Game2 : Form
    {
        int Points;

        public Game2(int points)
        {
            InitializeComponent();
            Points = points;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Game3 g3 = new Game3(Points);
            g3.Show();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Points++;
            Game3 g3 = new Game3(Points);
            g3.Show();
            Close();
        }

        private void Game2_Load(object sender, EventArgs e)
        {
            Height = 600;
            Width = 390;
            BackgroundImageLayout = ImageLayout.Stretch;
            BackgroundImage = Properties.Resources._3;
            StartPosition = FormStartPosition.CenterScreen;
            MinimizeBox = false;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            button1.Location = new Point(20, 413);
            button1.Width = 155;
            button1.Height = 120;
            button1.Text = "";
            button1.FlatStyle = FlatStyle.Flat;
            button1.BackColor = Color.Transparent;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;

            button2.Location = new Point(194, 413);
            button2.Width = 155;
            button2.Height = 120;
            button2.Text = "";
            button2.FlatStyle = FlatStyle.Flat;
            button2.BackColor = Color.Transparent;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button2.FlatAppearance.MouseDownBackColor = Color.Transparent;
        }
    }
}
