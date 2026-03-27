using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SatelliteDataLibrary.Controllers;

namespace SatelliteDataProcessing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DisplayFuncController? controller;
        public MainWindow()
        {
            
            InitializeComponent();


            try
            {
                controller = new DisplayFuncController();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void BinRCA1Btn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BinITA1Btn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void SortSlctA1Btn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void SortIsrtA1Btn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BinRCB2Btn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BinITB2Btn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void SortSlctB2Btn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void SortIsrtB2Btn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void LoadDataBtn_Click(object sender, RoutedEventArgs e)
        {
            Display(AllDtaListBox, SnsrA1LstView, SnsrB1LstView, MuTxt, SigmaTxt); 
        }
        private void Display(ListBox ListBox, ListView ListA, ListView ListB, TextBox mu, TextBox sigma)
        {

            if (!int.TryParse(mu.Text, out int muValue) || !int.TryParse(sigma.Text, out int sigmaValue))
            {
                MessageBox.Show("Invalid input");
                return;
            }
            if (ListBox == null) return;
            if (ListA == null) return;
            if (ListB == null) return;
            ListBox.Items.Clear();
            ListA.Items.Clear();
            ListB.Items.Clear();
            var lists = controller.DataHandler(sigmaValue, muValue);
            foreach (var sensor in lists.A)
            {
                ListBox.Items.Add($"A: {sensor}");
                ListA.Items.Add(sensor);
            }
            foreach (var sensor in lists.B)
            {
                ListBox.Items.Add($"B: {sensor}");
                ListB.Items.Add(sensor);
            }
        }
    }
    
}