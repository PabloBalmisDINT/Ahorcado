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

        public void IniciarPartida()
        {
            vidas = 10;
            letrasAcertadas = 0;
            BitmapImage imagen = new BitmapImage();
            imagen.BeginInit();
            imagen.UriSource = new Uri("./assets/0.jpg", UriKind.Relative);
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
            string[] palabras = { "HOLA", "PRUEBA"};
            Random semilla = new Random();
            int aleatorio = semilla.Next(0, palabras.Length);
            palabra = palabras[aleatorio];//Genero la palabra de la partida
            for (int i = 0; i < palabra.Length; i++)//Creo las "casillas" donde ira cada letra
            {
                DockPanel dockPanel = new DockPanel();
                TextBlock letra = new TextBlock();
                TextBlock abajo = new TextBlock();
                letra.FontSize = 90;
                abajo.Background = Brushes.Black;
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
            string rutaImagen = "";
            switch(vidas)
            {
                case 10:
                    {
                        rutaImagen = "./assets/0.jpg";
                        
                        break;
                    }
                case 9:
                    {
                        rutaImagen = "./assets/1.jpg";
                        break;
                    }
                case 8:
                    {
                        rutaImagen = "./assets/2.jpg";
                        break;
                    }
                case 7:
                    {
                        rutaImagen = "./assets/3.jpg";
                        break;
                    }
                case 6:
                    {
                        rutaImagen = "./assets/4.jpg";
                        break;
                    }
                case 5:
                    {
                        rutaImagen = "./assets/5.jpg";
                        break;
                    }
                case 4:
                    {
                        rutaImagen = "./assets/6.jpg";
                        break;
                    }
                case 3:
                    {
                        rutaImagen = "./assets/7.jpg";
                        break;
                    }
                case 2:
                    {
                        rutaImagen = "./assets/8.jpg";
                        break;
                    }
                case 1:
                    {
                        rutaImagen = "./assets/9.jpg";
                        break;
                    }
                case 0:
                    {
                        rutaImagen = "./assets/10.jpg";
                        break;
                    }
            }
            imagen.UriSource = new Uri(rutaImagen, UriKind.Relative);//cambio la foto del ahorcado acorde a las vidas
            imagen.EndInit();
            Imagen_Image.Source = imagen;
            if (vidas == 0) //Ejecuto la parte de perder la partida
            {
                for(int i = 0; i < Letras_UniformGrid.Children.Count; i++)
                {
                    Button botonLetra = (Button) Letras_UniformGrid.Children[i];
                    botonLetra.IsEnabled = false;
                }
                MessageBox.Show("Has perdido");
            }
        }

        private void Button_Click_Letras(object sender, RoutedEventArgs e)
        {
            Button boton = (Button) sender;
            boton.IsEnabled = false;
            char letra = boton.Tag.ToString()[0];
            char[] letrasPalabra = palabra.ToCharArray();
            bool acertado = false;
            for(int i = 0; i < letrasPalabra.Length; i++)
            {
                if(letra == letrasPalabra[i])
                {
                    DockPanel dockPanelPalabra = (DockPanel) Palabra_StackPanel.Children[i];
                    TextBlock textBoxLetra = (TextBlock) dockPanelPalabra.Children[1];
                    textBoxLetra.Text = letra.ToString();
                    acertado = true;
                    letrasAcertadas++;
                }
            }
            if(!acertado)
            {
                FallarLetra();
            }
            if(letrasAcertadas == letrasPalabra.Length)
            {
                MessageBox.Show("Has ganado");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//Boton de rendirse
        {
            char[] letrasPalabra = palabra.ToCharArray();
            for(int i = 0; i < letrasPalabra.Length; i++)
            {
                DockPanel dockPanelPalabra = (DockPanel)Palabra_StackPanel.Children[i];
                TextBlock textBoxLetra = (TextBlock)dockPanelPalabra.Children[1];
                textBoxLetra.Text = letrasPalabra[i].ToString();
            }
        }

        private void WindowKeyDown(object sender, KeyEventArgs e) //Metodo para activar las teclas pulsando el teclado
        {
            //hacer el metodo de las teclas para pulsar
        }
    }
}
