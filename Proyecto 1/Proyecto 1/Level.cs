using Proyecto_1.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Proyecto_1
{
    public partial class Level : Form
    {
        // Recibe la pantalla de inicio, para poder volver a mostrarla al finalizar el nivel.
        private readonly Start start;
        public static Random r = new Random();

        // Resolución de cada casilla = 48x48;
        private readonly static int Resolucion = 48;

        // Índice del elemento seleccionado en el inventario.
        // Inv1 = 0; Inv2 = 1; Inv3 = 2; Inv4 = 3; Item1 = 4; Item2 = 5; Item3 = 6;
        private int SeleccionInventario;
        private Grid[,] Tablero = new Grid[10, 10];

        // * = Muro; / = Puerta; x = Casilla Final; 0 = Jugador; 1 = NPC; 2 = Objetos;
        private readonly string DisenoTablero = "**********" +
                                                "*0  / 2 2*" +
                                                "*****    *" +
                                                "*  21  2 *" +
                                                "*2  **  1*" +
                                                "*   **   *" +
                                                "* 2     2*" +
                                                "* 1 2*****" +
                                                "*    /  x*" +
                                                "**********";

        public Level(Start startScreen)
        {
            start = startScreen;
            SeleccionInventario = 0;
            InitializeComponent();
            InitializeButtons();
            InitializeInventory();
            InitializeLevel();
            BackColor = Color.DimGray;
        }

        #region Inicialización de Elementos
        private void InitializeButtons()
        {
            InitPB(Abrir, Resources.button_open, new Point(30, 515));
            InitPB(Cerrar, Resources.button_close, new Point(30, 515));
            InitPB(Salir, Resources.button_exit, new Point(110, 515));
            InitPB(Botar, Resources.button_drop, new Point(544, 506));
            InitPB(Tomar, Resources.button_take, new Point(667, 397));
            Abrir.Hide();
            Cerrar.Hide();
            Salir.Hide();
            Tomar.Hide();
        }

        private void InitializeInventory()
        {
            InitPB(Inventario, Resources.inventory, new Point(527, 20));
            InitPB(InvCasilla, Resources.grid_inventory, new Point(650, 20));
            InitPB(Inv1, Resources.inv_empty, new Point(530, 70));
            InitPB(Inv2, Resources.inv_empty, new Point(530, 179));
            InitPB(Inv3, Resources.inv_empty, new Point(530, 288));
            InitPB(Inv4, Resources.inv_empty, new Point(530, 397));
            InitPB(Item1, Resources.inv_empty, new Point(653, 70));
            InitPB(Item2, Resources.inv_empty, new Point(653, 179));
            InitPB(Item3, Resources.inv_empty, new Point(653, 288));
            InitPB(Selector, Resources.inv_selector, new Point(520, 60));
        }

        // Inicializa cada casilla del tablero. Añade los muros, puertas, personajes y objetos a las casillas,
        // según el caracter asignado en DisenoTablero[x, y]. Luego, dibuja la imagen de la casilla.
        // Además, para cada casilla identifica las casillas vecinas;
        private void InitializeLevel()
        {
            for (int y = 0; y < 10; y++)
                for (int x = 0; x < 10; x++)
                {
                    Tablero[x, y] = new Grid(InitPB(new Point(20 + (Resolucion * x), 20 + (Resolucion * y))));

                    char c = DisenoTablero[x + (y * 10)];
                    if (c == '*') Tablero[x, y].Muro = new Wall();
                    else if (c == '/') Tablero[x, y].Puerta = new Door();
                    else if (c == 'x') Tablero[x, y].EsFinal = true;
                    else if (c == '0')
                    {
                        Tablero[x, y].Personaje = new Player();
                        InventorySetSprite(Tablero[x, y]);
                    }
                    else if (c == '1') Tablero[x, y].Personaje = new NPC();
                    else if (c == '2') Tablero[x, y].Inventario = addObjects(r.Next(3) + 1);
                    GridSetSprite(Tablero[x, y]);
                }

            for (int y = 0; y < 10; y++)
                for (int x = 0; x < 10; x++)
                {
                    for (int n = 0; n < 4; n++) Tablero[x, y].CasillasVecinas[n] = null;
                    if (y > 0) Tablero[x, y].CasillasVecinas[0] = Tablero[x, y - 1];
                    if (y < 10 - 1) Tablero[x, y].CasillasVecinas[1] = Tablero[x, y + 1];
                    if (x > 0) Tablero[x, y].CasillasVecinas[2] = Tablero[x - 1, y];
                    if (x < 10 - 1) Tablero[x, y].CasillasVecinas[3] = Tablero[x + 1, y];
                }
        }

        // Inicializa un PictureBox con la imagen y las coordenadas especificadas.
        private void InitPB(PictureBox pb, System.Drawing.Image im, Point pt)
        {
            pb.Image = im;
            pb.Location = pt;
            pb.Size = im.Size;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            Controls.Add(pb);
        }

        // Inicializa un PictureBox en las coordenadas especificadas.
        private PictureBox InitPB(Point coord)
        {
            PictureBox pb = new PictureBox
            {
                Location = coord,
                Size = new Size(Resolucion, Resolucion),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            Controls.Add(pb);
            return pb;
        }

        // En las casillas con objetos, se agregan aleatoriamente 1 a 3 objetos. El tipo de objeto también se define al azar.
        private Item[] addObjects(int n)
        {
            Item[] inv = new Item[3];
            for (int i = 0; i < n; i++) inv[i] = new Item(r.Next(3));
            return inv;
        }
        #endregion

        #region Detección de Teclado
        // Controla las acciones del juego al presionarse una tecla.
        // Si la tecla presionada corresponde a WASD, entonces se realiza el movimiento de los personajes.
        // Si la tecla es una flecha, entonces se navega por el inventario.
        private void Level_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.A || e.KeyCode == Keys.S || e.KeyCode == Keys.D)
            {
                foreach (Grid g in Tablero)
                    if (g.Personaje is Player)
                    {
                        g.Personaje.CharacterMovement(e.KeyCode, g, Tablero);
                        foreach (Grid g2 in Tablero)
                            if (g2.Personaje is NPC) g2.Personaje.SeMovio = false;
                        break;
                    }
                foreach (Grid g in Tablero)
                    if (g.Personaje is Player)
                    {
                        CheckDoor(g);
                        CheckFinalGrid(g);
                        InventorySetSprite(g);
                    }
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left || e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
            {
                InventoryNavigation(e.KeyCode);
                return;
            }
        }

        // Controla la navegación por el inventario con las flechas.
        private void InventoryNavigation(Keys k)
        {
            if (k == Keys.Up && SeleccionInventario > 0 && SeleccionInventario != 4)
            {
                SeleccionInventario--;
                Selector.Location = new Point(Selector.Location.X, Selector.Location.Y - 109);
            }
            else if (k == Keys.Left && SeleccionInventario > 3)
            {
                SeleccionInventario -= 4;
                Selector.Location = new Point(Selector.Location.X - 123, Selector.Location.Y);
            }
            else if (k == Keys.Down && SeleccionInventario != 6 && SeleccionInventario != 3)
            {
                SeleccionInventario++;
                Selector.Location = new Point(Selector.Location.X, Selector.Location.Y + 109);
            }
            else if (k == Keys.Right && SeleccionInventario < 4 && SeleccionInventario != 3)
            {
                SeleccionInventario += 4;
                Selector.Location = new Point(Selector.Location.X + 123, Selector.Location.Y);
            }

            if (SeleccionInventario < 4)
            {
                Botar.Show();
                Tomar.Hide();
            }
            else
            {
                Tomar.Show();
                Botar.Hide();
            }
        }
        #endregion

        #region Funciones Gráficas Auxiliares

        //Función que dibuja el sprite de la casilla dependiendo de su contenido.
        public static void GridSetSprite(Grid g)
        {
            if (g.Muro != null) g.Imagen.Image = Resources.wall;
            else if (g.Puerta != null)
            {
                if (g.Puerta.Abierta) g.Imagen.Image = Resources.door_open;
                else g.Imagen.Image = Resources.door_closed;
            }
            else if (g.Inventario[0] != null)
            {
                if (g.Inventario[0].Tipo == ItemTypes.Coin) g.Imagen.Image = Resources.coin;
                else if (g.Inventario[0].Tipo == ItemTypes.Heart) g.Imagen.Image = Resources.heart;
                else g.Imagen.Image = Resources.potion;
            }
            else if (g.EsFinal) g.Imagen.Image = Resources.floor_final;
            else g.Imagen.Image = Resources.floor;
            if (g.Personaje is Player)
            {
                if (g.Puerta != null) g.Imagen.Image = Resources.player_in_door;
                else if (g.EsFinal) g.Imagen.Image = Resources.player_in_floor_final;
                else g.Imagen.Image = Resources.player;
            }
            if (g.Personaje is NPC) g.Imagen.Image = Resources.npc;
        }

        // Función que dibuja los elementos del inventario.
        private void InventorySetSprite(Grid g)
        {
            PictureBox[] pbArray = { Item1, Item2, Item3 };
            for (int i = 0; i < 3; i++)
            {
                if (g.Inventario[i] == null) pbArray[i].Image = Resources.inv_empty;
                else if (g.Inventario[i].Tipo == ItemTypes.Coin) pbArray[i].Image = Resources.inv_coin;
                else if (g.Inventario[i].Tipo == ItemTypes.Heart) pbArray[i].Image = Resources.inv_heart;
                else pbArray[i].Image = Resources.inv_potion;
            }

            PictureBox[] pbArray2 = { Inv1, Inv2, Inv3, Inv4};
            for (int i = 0; i < 4; i++)
            {
                if (g.Personaje.Inventario[i] == null) pbArray2[i].Image = Resources.inv_empty;
                else if (g.Personaje.Inventario[i].Tipo == ItemTypes.Coin) pbArray2[i].Image = Resources.inv_coin;
                else if (g.Personaje.Inventario[i].Tipo == ItemTypes.Heart) pbArray2[i].Image = Resources.inv_heart;
                else pbArray2[i].Image = Resources.inv_potion;
            }
        }

        // Muestra los botones de Abrir y Cerrar puerta cuando el jugador se encuentra al lado de una.
        private void CheckDoor(Grid g)
        {
            foreach (Grid vecina in g.CasillasVecinas)
            {
                if (vecina.Puerta != null)
                {
                    if (vecina.Puerta.Abierta) Cerrar.Show();
                    else Abrir.Show();
                    return;
                }
                else
                {
                    Abrir.Hide();
                    Cerrar.Hide();
                }
            }
        }

        // Muestra el botón de salir al llegar a la casilla final.
        private void CheckFinalGrid(Grid g)
        {
            if (g.EsFinal) Salir.Show();
            else Salir.Hide();
        }

        // Cada vez que se recoge o se bota un objeto, se reorganiza el inventario para mostrarlo más ordenado
        // y mejorar el funcionamiento.
        private void SortInventory(Grid g)
        {
            for (int i = 0; i < 3; i++)
                if (g.Personaje.Inventario[i] == null)
                {
                    g.Personaje.Inventario[i] = g.Personaje.Inventario[i + 1];
                    g.Personaje.Inventario[i + 1] = null;
                }
            for (int i = 0; i < 2; i++)
                if (g.Inventario[i] == null)
                {
                    g.Inventario[i] = g.Inventario[i + 1];
                    g.Inventario[i + 1] = null;
                }
            InventorySetSprite(g);
        }
        #endregion

        #region Interacción con los Botones
        // Abre la puerta seleccionada.
        private void Abrir_Click(object sender, EventArgs e)
        {
            foreach (Grid g in Tablero)
                if (g.Personaje is Player)
                    foreach (Grid vecina in g.CasillasVecinas)
                        if (vecina.Puerta != null && !vecina.Puerta.Abierta)
                        {
                            vecina.Puerta.Abierta = true;
                            GridSetSprite(vecina);
                            Abrir.Hide();
                            Cerrar.Show();
                        }
        }

        // Cierra la puerta seleccionada.
        private void Cerrar_Click(object sender, EventArgs e)
        {
            foreach (Grid g in Tablero)
                if (g.Personaje is Player)
                    foreach (Grid vecina in g.CasillasVecinas)
                        if (vecina.Puerta != null && vecina.Puerta.Abierta)
                        {
                            vecina.Puerta.Abierta = false;
                            GridSetSprite(vecina);
                            Cerrar.Hide();
                            Abrir.Show();
                        }
        }

        // Bota desde el inventario a la casilla el ítem seleccionado.
        private void Botar_Click(object sender, EventArgs e)
        {
            foreach (Grid g in Tablero)
                if (g.Personaje is Player && g.Personaje.Inventario[SeleccionInventario] != null)
                {
                    Item i = g.Personaje.Inventario[SeleccionInventario];
                    for (int j = 0; j < 3; j++)
                        if (g.Inventario[j] == null)
                        {
                            g.Inventario[j] = i;
                            g.Personaje.Inventario[SeleccionInventario] = null;
                            SortInventory(g);
                            return;
                        }
                    return;
                }
        }

        // Recoge desde la casilla al inventario el ítem seleccionado.
        private void Tomar_Click(object sender, EventArgs e)
        {
            foreach (Grid g in Tablero)
                if (g.Personaje is Player && g.Inventario[SeleccionInventario - 4] != null)
                {
                    Item i = g.Inventario[SeleccionInventario - 4];
                    for (int j = 0; j < 4; j++)
                        if (g.Personaje.Inventario[j] == null)
                        {
                            g.Personaje.Inventario[j] = i;
                            g.Inventario[SeleccionInventario - 4] = null;
                            SortInventory(g);
                            return;
                        }
                    return;
                }
        }

        // Termina el nivel.
        private void Salir_Click(object sender, EventArgs e)
        {
            start.Show();
            Close();
        }
        #endregion
    }
}