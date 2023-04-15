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
    public partial class Start : Form
    {
        int Points;

        public Start()
        {
            InitializeComponent();
            Points = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Game1 g1 = new Game1(Points);
            Visible = false;
            g1.Show();
        }

        private void Start_Load(object sender, EventArgs e)
        {
            Height = 600;
            Width = 390;
            BackgroundImageLayout = ImageLayout.Stretch;
            BackgroundImage = Properties.Resources._1;
            StartPosition = FormStartPosition.CenterScreen;
            MinimizeBox = false;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            button1.Location = new Point(30, 217);
            button1.Width = 313;
            button1.Height = 190;
            button1.Text = "";
            button1.FlatStyle = FlatStyle.Flat;
            button1.BackColor = Color.Transparent;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;
        }
    }
}
