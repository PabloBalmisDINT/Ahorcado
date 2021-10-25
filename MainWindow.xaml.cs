using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ahorcado
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string palabra;
        int vidas = 10;
        int letrasAcertadas = 0;
        public MainWindow()
        {
            InitializeComponent();
            IniciarPartida();
            
        }

        public void IniciarPartida() //Pongo todo para empezar una partida nueva
        {
            vidas = 10;
            letrasAcertadas = 0;
            BitmapImage imagen = new BitmapImage();
            imagen.BeginInit();
            imagen.UriSource = new Uri("./assets/10.jpg", UriKind.Relative);
            imagen.EndInit();
            Imagen_Image.Source = imagen;
            Palabra_StackPanel.Children.Clear();
            Letras_UniformGrid.Children.Clear();
            GenerarBotones();
            GenerarPalabras();
        }

        private void GenerarBotones()
        {
            char[] letras = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T',
                'U', 'V', 'W', 'X', 'Y', 'Z' };
            for (int i = 0; i < 27; i++)
            {
                Button letra = new Button();
                Viewbox viewbox = new Viewbox();
                letra.Content = viewbox;
                TextBlock textBlock = new TextBlock();
                viewbox.Child = textBlock;
                textBlock.Text = letras[i].ToString();
                letra.Tag = letras[i];
                letra.Click += Button_Click_Letras;
                Letras_UniformGrid.Children.Add(letra);
            }
        }

        private void GenerarPalabras()
        {
            string[] palabras = { "ACDC", "BLACK SABBATH", "BON JOVI", "BRUCE SPRINGSTEEN", "EXTREMODURO", "DESAKATO", "FOO FIGHTERS", "GREEN DAY", "GRETA VAN FLEET", 
                "GUNS N ROSES", "IRON MAIDEN", "LA RAIZ", "LED ZEPPELIN", "LINKIN PARK", "METALLICA", "MOTORHEAD", "NIRVANA", "OZZY OSBOURNE", "PARAMORE", "THE POLICE",
                "QUEEN", "RAGE AGAINST THE MACHINE", "RED HOT CHILI PEPPERS", "SCORPIONS", "SONS OF AGUIRRE", "VAN HALEN", "TRIVIUM", "THE WARNING"};
            Random semilla = new Random();
            int aleatorio = semilla.Next(0, palabras.Length);
            palabra = palabras[aleatorio];//Genero la palabra de la partida
            for (int i = 0; i < palabra.Length; i++)//Creo las "casillas" donde ira cada letra
            {
                DockPanel dockPanel = new DockPanel();
                TextBlock letra = new TextBlock();
                TextBlock abajo = new TextBlock();
                letra.FontSize = 90;
                if(palabra.ToCharArray()[i] == ' ') // Esto lo hago para poder poder poner`palabras con espacios
                {
                    abajo.Background = Brushes.White;
                }
                else
                {
                    abajo.Background = Brushes.Black;
                }
                dockPanel.Children.Add(abajo);
                dockPanel.Children.Add(letra);
                DockPanel.SetDock(abajo, Dock.Bottom);
                abajo.Height = 5;
                dockPanel.Height = 100;
                dockPanel.Width = 75;
                dockPanel.Margin = new Thickness(5);
                Palabra_StackPanel.Children.Add(dockPanel);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) //Boton de nueva partida
        {
            IniciarPartida();
        }
        private void FallarLetra()
        {
            vidas--;
            BitmapImage imagen = new BitmapImage();
            imagen.BeginInit();
            string rutaImagen = "./assets/" + vidas + ".jpg"; //cambio la foto del ahorcado acorde a las vidas
            imagen.UriSource = new Uri(rutaImagen, UriKind.Relative);
            imagen.EndInit();
            Imagen_Image.Source = imagen;
            if (vidas == 0) //Ejecuto la parte de perder la partida
            {
                DesactivarBotones();
                MessageBox.Show("Has perdido", "Fin de la partida", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }

        private void Button_Click_Letras(object sender, RoutedEventArgs e) 
        {
            Button boton = (Button) sender;
            LogicaLetras(boton);
        }

        private void LogicaLetras(Button boton) //Logica de las letras
        {
            boton.IsEnabled = false;
            char letra = boton.Tag.ToString()[0];
            char[] letrasPalabra = palabra.ToCharArray();
            bool acertado = false;
            for (int i = 0; i < letrasPalabra.Length; i++)
            {
                if (letra == letrasPalabra[i])
                {
                    DockPanel dockPanelPalabra = (DockPanel)Palabra_StackPanel.Children[i];
                    TextBlock textBoxLetra = (TextBlock)dockPanelPalabra.Children[1];
                    textBoxLetra.Text = letra.ToString();
                    acertado = true;
                    letrasAcertadas++;
                }
            }
            if (!acertado)
            {
                FallarLetra();
            }
            if (letrasAcertadas == palabra.Replace(" ", "").Length)
            {
                DesactivarBotones();
                MessageBox.Show("Has ganado", "Fin de la parida", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//Boton de rendirse
        {
            char[] letrasPalabra = palabra.ToCharArray();
            for(int i = 0; i < letrasPalabra.Length; i++) //Muestro la palabra a adivinar
            {
                DockPanel dockPanelPalabra = (DockPanel)Palabra_StackPanel.Children[i];
                TextBlock textBoxLetra = (TextBlock)dockPanelPalabra.Children[1];
                textBoxLetra.Text = letrasPalabra[i].ToString();
            }
            DesactivarBotones();
        }

        private void WindowKeyDown(object sender, KeyEventArgs e) //Metodo para activar las teclas pulsando el teclado
        {
            Key tecla = e.Key;
            UIElementCollection letras = Letras_UniformGrid.Children;
            for(int i = 0; i <  letras.Count; i++) //Recorro todos los botones y si su tag coincide con el boton activo ese boton
            {
                Button botonLetra = (Button)letras[i];
                if(i == 14 && tecla.ToString() == "Oem3") //Comprobar la ñ
                {
                    LogicaLetras(botonLetra);
                }
                if(botonLetra.Tag.ToString() == tecla.ToString())
                {
                    if (botonLetra.IsEnabled)//Compruebo que el boton que voy a pulsar no este desactivado para no volver a pulsarlo
                    {
                        LogicaLetras(botonLetra);
                    }
                }
            }
        }

        private void DesactivarBotones()
        {
            for (int i = 0; i < Letras_UniformGrid.Children.Count; i++)
            {
                Button botonLetra = (Button)Letras_UniformGrid.Children[i];
                botonLetra.IsEnabled = false;
            }
        }
    }
}
