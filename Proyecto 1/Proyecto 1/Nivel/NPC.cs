using Proyecto_1.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_1
{
    public class NPC : Character
    {
        public NPC() { SeMovio = false; }

        public override void CharacterMovement(Keys k, Grid g, Grid[,] tablero) { }

        // Mueve al NPC a la casilla designada.
        public override void CharacterMovement(Grid newGrid, Grid oldGrid)
        {
            newGrid.Personaje = oldGrid.Personaje;
            oldGrid.Personaje = null;
            Level.GridSetSprite(newGrid);
            Level.GridSetSprite(oldGrid);
        }

        // Detecta si el NPC tiene permitido moverse. Los NPC no pueden atravesar puertas, aunque estén abiertas.
        public override bool IsMovementAllowed(Grid g)
        {
            if (g == null || g.Muro != null || g.Puerta != null || g.Personaje is Player || g.Personaje is NPC) return false;
            return true;
        }
    }
}
