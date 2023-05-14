using Proyecto_1.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_1
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Size = new Size(704, 731);
            pictureBox1.Image = Resources.Start;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            Jugar.Location = new Point(238, 610);
            Jugar.Size = new Size(228, 90);
            Jugar.Image = Resources.play_button;
            Jugar.SizeMode = PictureBoxSizeMode.StretchImage;
            Jugar.BackColor = Color.Transparent;
        }

        // Comienza el nivel al apretar en el botón jugar.
        private void Jugar_Click(object sender, EventArgs e)
        {
            Level level = new Level(this);
            level.Show();
            Hide();
        }
    }
}
