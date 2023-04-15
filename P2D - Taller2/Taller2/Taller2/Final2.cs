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
    public partial class Final2 : Form
    {
        public Final2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Start s = new Start();
            s.Show();
            Close();
        }

        private void Final2_Load(object sender, EventArgs e)
        {
            Height = 600;
            Width = 390;
            BackgroundImageLayout = ImageLayout.Stretch;
            BackgroundImage = Properties.Resources._8;
            StartPosition = FormStartPosition.CenterScreen;
            MinimizeBox = false;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            button1.Location = new Point(7, 7);
            button1.Width = 72;
            button1.Height = 28;
            button1.Text = "";
            button1.FlatStyle = FlatStyle.Flat;
            button1.BackColor = Color.Transparent;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;
        }
    }
}
