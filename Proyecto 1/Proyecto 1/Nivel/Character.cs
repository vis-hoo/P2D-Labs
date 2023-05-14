using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_1
{
    // Clase abstracta Character. Las clases Player y NPC heredan de esta.
    // Los personajes pueden tener inventario, y tiene las funciones necesarias
    // para determinar si se debe mover. En caso de que sí deba, entonces se mueve.
    public abstract class Character
    {
        public bool SeMovio = false;
        public Item[] Inventario;
        public abstract void CharacterMovement(Grid g1, Grid g2);
        public abstract void CharacterMovement(Keys k, Grid g, Grid[,] tablero);
        public abstract bool IsMovementAllowed(Grid g);
    }
}
