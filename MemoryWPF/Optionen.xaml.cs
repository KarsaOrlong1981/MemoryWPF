using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MemoryWPF
{
    /// <summary>
    /// Interaktionslogik für Optionen.xaml
    /// </summary>
    public partial class Optionen : Window
    {
        UniformGrid playGround;
        public Optionen(UniformGrid playground)
        {
            InitializeComponent();

            playGround = playground;
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            int seconds = 0;
            int skill = 0;
            switch (cb.SelectedIndex.ToString())
            {
                case "0":
                    skill = 6;
                    break;
                case "1":
                    skill = 2;
                    break;
                case "2":
                    skill = 0;
                    break;
            }
            if (tb.Text != string.Empty)
            {
                if (s2.IsChecked == true)
                    seconds = 2000;
                if (s4.IsChecked == true)
                    seconds = 4000;
                if (s6.IsChecked == true)
                    seconds = 6000;

                new MemoryField(playGround, tb.Text, seconds, skill);
                Close();
            }
            else
                MessageBox.Show("Bitte einen Benutzernamen eingeben!");
           
        }
    }
}
