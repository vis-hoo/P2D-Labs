using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_1
{
    // Cada casilla posee una imagen, un inventario, y un conjunto de casillas vecinas.
    // También, cada casilla puede contener un muro, una puerta, un personaje, un objeto,
    // o puede ser la casilla final del nivel.
    public class Grid
    {
        public PictureBox Imagen;
        public bool EsFinal;
        public Wall Muro;
        public Door Puerta;
        public Character Personaje;
        public Grid[] CasillasVecinas;
        public Item[] Inventario;

        public Grid(PictureBox imagen)
        {
            Imagen = imagen;
            EsFinal = false;
            Muro = null;
            Puerta = null;
            Personaje = null;
            CasillasVecinas = new Grid[4]; // 0 = Top; 1 = Bottom; 2 = Left; 3 = Right;
            Inventario = new Item[3];
        }
    }
}