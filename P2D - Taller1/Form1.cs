using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2D___Taller1
{
    public partial class Form1 : Form
    {
        private Button[] Botones;
        private PC Pc;

        public Form1()
        {
            InitializeComponent();
            Botones = new Button[9];
            Pc = new PC();
        }

        private void Form1_Load(object sender, EventArgs e) { }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) { Environment.Exit(0); }

        private void button1_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            b.Enabled = false;
            b.Text = "X";
            b.BackColor = Color.DarkSlateGray;

            ActualizarBotones();

            foreach (Button button in Botones)
            {
                if (button.Enabled)
                {
                    Pc.SeleccionBoton(Botones);
                    break;
                }
            }

            //La aplicación se congela en el último movimiento ya que entra a un loop infinito en el último turno del PC.
        }

        private void ActualizarBotones()
        {
            Botones[0] = button1;
            Botones[1] = button2;
            Botones[2] = button3;
            Botones[3] = button4;
            Botones[4] = button5;
            Botones[5] = button6;
            Botones[6] = button7;
            Botones[7] = button8;
            Botones[8] = button9;
        }
    }
}
