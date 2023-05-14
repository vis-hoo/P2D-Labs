using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_1
{
    // La puerta puede estar abierta o cerrada. En caso de estar cerrada, bloquea el paso del personaje.
    public class Door
    {
        public bool Abierta;
        public Door() { Abierta = false; }
    }
}
