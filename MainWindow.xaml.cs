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
        public MainWindow()
        {
            InitializeComponent();

            //Codigo para generar los botones de las letras
            char[] letras = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T',
                'U', 'V', 'W', 'X', 'Y', 'Z' };
            string[] palabras = { "HOLA", "PRUEBA" };
            for(int i = 0; i < 27; i++)
            {
                Button letra = new Button();
                letra.Content = letras[i];
                Letras_UniformGrid.Children.Add(letra);
            }

            //Codigo para hacer las palabras
            Random semilla = new Random();
            int aleatorio = semilla.Next(0,1);
            string palarba = palabras[aleatorio];
            for(int i = 0; i < palarba.Length; i++)
            {
                DockPanel dockPanel = new DockPanel();
                TextBlock letra = new TextBlock();
                TextBlock abajo = new TextBlock();
                abajo.Background = Brushes.Black;
                dockPanel.Children.Add(abajo);
                dockPanel.Children.Add(letra);
                DockPanel.SetDock(abajo, Dock.Bottom);
                abajo.Height = 5;
                dockPanel.Height = 100;
                dockPanel.Width = 100;
                // dockPanel.Margin =; poner margenes
                Palabra_StackPanel.Children.Add(dockPanel);
            }
        }
    }
}
