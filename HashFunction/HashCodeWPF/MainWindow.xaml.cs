using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace HashCodeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public int numHashes;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            PopUp.Visibility = Visibility.Hidden;
        }

        private void btn_HashCode_Click(object sender, RoutedEventArgs e)
        {
            PopUp.Visibility = Visibility.Visible;
        }

        private async void btn_validate_Click(object sender, RoutedEventArgs e)
        {
            numHashes = int.Parse(tb_Hashage.Text);
            await Task.WhenAll(
                Program.GenerateHashesAsync(Program.hashGroups, numHashes),
                Program.FindOccurrencesAsync(Program.hashGroups, numHashes)
            );
        }

        private void tb_Hashage_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
