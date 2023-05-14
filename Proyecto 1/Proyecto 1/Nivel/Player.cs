using Proyecto_1.Properties;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_1
{
    public class Player : Character
    {
        // El jugador comienza con una poción en su inventario.
        public Player()
        {
            Inventario = new Item[4];
            Inventario[0] = new Item(2);
        }

        public override void CharacterMovement(Grid g1, Grid g2) { }

        // Dependiendo de la tecla presionada, el jugador intenta moverse en esa dirección.
        public override void CharacterMovement(Keys k, Grid g, Grid[,] tablero)
        {
            int casillaSeleccionada = 0;
            if (k == Keys.W) casillaSeleccionada = 0;
            else if (k == Keys.S) casillaSeleccionada = 1;
            else if (k == Keys.A) casillaSeleccionada = 2;
            else if (k == Keys.D) casillaSeleccionada = 3;

            if (IsMovementAllowed(g.CasillasVecinas[casillaSeleccionada]))
            {
                g.CasillasVecinas[casillaSeleccionada].Personaje = g.Personaje;
                g.Personaje = null;
                Level.GridSetSprite(g.CasillasVecinas[casillaSeleccionada]);
                Level.GridSetSprite(g);
                NPCMove(tablero);
            }
        }

        // Detecta si el jugador tiene permitido moverse.
        public override bool IsMovementAllowed(Grid g)
        {
            if (g == null || g.Muro != null || (g.Puerta != null && !g.Puerta.Abierta) || g.Personaje is NPC)
                return false;
            return true;
        }

        // Al moverse el jugador, mueven los NPC en el tablero de manera aleatoria.
        private void NPCMove(Grid[,] tablero)
        {
            foreach(Grid g in tablero)
                if (g.Personaje is NPC && !g.Personaje.SeMovio)
                {
                    Character npc = g.Personaje;
                    Grid vecina = null;
                    while (!npc.IsMovementAllowed(vecina)) vecina = g.CasillasVecinas[Level.r.Next(4)];
                    npc.CharacterMovement(vecina, g);
                    npc.SeMovio = true;
                }
        }
    }
}