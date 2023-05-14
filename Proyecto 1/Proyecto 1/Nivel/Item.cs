using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_1
{
    // Cada objeto puede ser de uno de los siguientes tipos: Moneda, Corazón o Poción.
    public enum ItemTypes { Coin, Heart, Potion }
    public class Item
    {
        public ItemTypes Tipo;
        public Item(int tipo) => Tipo = (ItemTypes)tipo;
    }
}
