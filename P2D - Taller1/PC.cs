using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2D___Taller1
{
    class PC
    {
        public PC() { }

        public void SeleccionBoton(Button[] buttons)
        {
            Thread.Sleep(150);
            Random r = new Random();
            int boton;
            bool finalizado = false;

            do
            {
                boton = r.Next(9);
                if (buttons[boton].Enabled)
                {
                    buttons[boton].Enabled = false;
                    buttons[boton].Text = "O";
                    buttons[boton].BackColor = Color.DarkSlateGray;
                    finalizado = true;
                }
            } while (!finalizado);
        }
    }
}
