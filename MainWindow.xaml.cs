using System.ComponentModel;
using System.Diagnostics;
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
using System.Xml.Serialization;
using SatelliteDataLibrary.Controllers;

namespace SatelliteDataProcessing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DisplayFuncController controller = new DisplayFuncController();
        SearchAndSortController sortSearchController = new SearchAndSortController();

        public MainWindow()
        {    
            InitializeComponent();
            
            if (MuTxt.Text == "") MuTxt.Text = "10";
            if (SigmaTxt.Text == "") SigmaTxt.Text = "10";
        }
        #region SearchBtns
        private void BinRCA1Btn_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(SearchTrgtATxt.Text, out _)){ MessageBox.Show("Error: No Value"); return; }
            BinSearchRC1ATxt.Text = $"{SearchrtSWWrapper(SearchHandle, ConvertAndCheckTxt, SearchAndSortController.SearchType.BinaryRecursive, SearchAndSortController.Dataset.A1).ElapsedMilliseconds} ms";
        }
        private void BinITA1Btn_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(SearchTrgtATxt.Text, out _)) { MessageBox.Show("Error: No Value"); return; }
            BinSearchIT1ATxt.Text = $"{SearchrtSWWrapper(SearchHandle, ConvertAndCheckTxt, SearchAndSortController.SearchType.BinaryIterative, SearchAndSortController.Dataset.A1).ElapsedMilliseconds} ms";
        }
        private void BinRCB2Btn_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(SearchTrgtBTxt.Text, out _)) { MessageBox.Show("Error: No Value"); return; }           
            BinSearchRC2BTxt.Text = $"{SearchrtSWWrapper(SearchHandle, ConvertAndCheckTxt, SearchAndSortController.SearchType.BinaryRecursive, SearchAndSortController.Dataset.B2).ElapsedMilliseconds} ms";
        }
        private void BinITB2Btn_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(SearchTrgtBTxt.Text, out _)) { MessageBox.Show("Error: No Value"); return; }           
            BinSearchIT2BTxt.Text = $"{SearchrtSWWrapper(SearchHandle, ConvertAndCheckTxt, SearchAndSortController.SearchType.BinaryIterative, SearchAndSortController.Dataset.B2).ElapsedMilliseconds} ms";
        }
        #endregion
        #region SortBtns
        private void SortSlctA1Btn_Click(object sender, RoutedEventArgs e)
        {
            SortSlctA1Txt.Text = $"{SortSWWrapper(SortHandle, SearchAndSortController.SortType.Selection, SearchAndSortController.Dataset.A1).ElapsedMilliseconds} ms";
        }
        private void SortIsrtA1Btn_Click(object sender, RoutedEventArgs e)
        {
            SortInrtA1Txt.Text = $"{SortSWWrapper(SortHandle, SearchAndSortController.SortType.Insertion, SearchAndSortController.Dataset.A1).ElapsedMilliseconds} ms";
        }
        private void SortSlctB2Btn_Click(object sender, RoutedEventArgs e)
        {
            SortSlctB2Txt.Text = $"{SortSWWrapper(SortHandle, SearchAndSortController.SortType.Selection, SearchAndSortController.Dataset.B2).ElapsedMilliseconds} ms";
        }
        private void SortIsrtB2Btn_Click(object sender, RoutedEventArgs e)
        {
            SortInrtB2Txt.Text = $"{SortSWWrapper(SortHandle, SearchAndSortController.SortType.Insertion, SearchAndSortController.Dataset.B2).ElapsedMilliseconds} ms";
        }
        #endregion
        #region DataGenerationInterfaces
        private void LoadDataBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayDataToFrontEnd(AllDtaListBox, SnsrA1LstView, SnsrB1LstView, MuTxt, SigmaTxt); 
        }
        private void sigincr_Click(object sender, RoutedEventArgs e)
        {

            if  (int.TryParse(SigmaTxt.Text, out int sig))
            {
                if ( sig < 20)
                {
                    SigmaTxt.Text = (sig + 1).ToString();
                }
            }         
        }
        private void sigdecr_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(SigmaTxt.Text, out int sig))
            {
                if (sig > 10 )
                {
                    SigmaTxt.Text = (sig - 1).ToString();
                }
            }
        }
        private void muincr_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(MuTxt.Text, out int mu))
            {
                if ( mu < 20)
                {
                    MuTxt.Text = (mu + 1).ToString();
                }
            }
        }

        private void mudecr_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(MuTxt.Text, out int mu))
            {
                if (mu > 10)
                {
                    MuTxt.Text = (mu - 1).ToString();
                }
            }
        }
        #endregion
        #region FrontendFunctions


        private void DisplayDataToFrontEnd(ListView ListView, ListBox ListA, ListBox ListB, TextBox mu, TextBox sigma)
        {
            if (!int.TryParse(mu.Text, out int muValue) || !int.TryParse(sigma.Text, out int sigmaValue))
            {
                MessageBox.Show("Invalid input");
                return;
            }
            if (ListView == null) return;
            if (ListA == null) return;
            if (ListB == null) return;
            ListView.Items.Clear();
            ListA.Items.Clear();
            ListB.Items.Clear();
            var lists = controller.DataHandler(sigmaValue, muValue);
            foreach (var sensor in lists.A)
            {
                ListA.Items.Add(sensor);
            }
            foreach (var sensor in lists.B)
            {
                ListB.Items.Add(sensor);
            }

            int rowCount = Math.Max(lists.A.Count, lists.B.Count);
            for (int i = 0; i < rowCount; i++)
            {
                var row = new SensorRow
                {
                    AData = i < lists.A.Count ? lists.A[i].ToString() : string.Empty,
                    BData = i < lists.B.Count ? lists.B[i].ToString() : string.Empty
                };
                ListView.Items.Add(row);
            }
        }
        private void SortHandle(SearchAndSortController.SortType sortType, SearchAndSortController.Dataset dataset)
        {
            var data = dataset == SearchAndSortController.Dataset.A1 ? controller.Basic.A : controller.Basic.B;
            var sort = sortSearchController.SortData(data, sortType, dataset);

            var ListViewTarget = dataset == SearchAndSortController.Dataset.A1 ? SnsrA1LstView : SnsrB1LstView;
            ListViewTarget.Items.Clear();
            foreach (var sensor in sort)
            {
                ListViewTarget.Items.Add(sensor);
            }
        }
        private void SearchHandle(double target, SearchAndSortController.SearchType searchType, SearchAndSortController.Dataset dataset)
        {
            var data = dataset == SearchAndSortController.Dataset.A1 ? new LinkedList<double>(sortSearchController.SortedA) : new LinkedList<double>(sortSearchController.SortedB);
            var search = sortSearchController.SearchData(data, target, searchType,dataset);
            var ListViewTarget = dataset == SearchAndSortController.Dataset.A1 ? SnsrA1LstView: SnsrB1LstView;
            for (int i = 0; i < ListViewTarget.Items.Count; i++)
            {
                var container = ListViewTarget.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                if (container != null)
                    container.Background = Brushes.Transparent;
            }
            foreach (var index in search)
            {
                ListViewTarget.ScrollIntoView(ListViewTarget.Items[index]);
                ListViewTarget.UpdateLayout(); 

                var container = ListViewTarget.ItemContainerGenerator.ContainerFromIndex(index) as ListBoxItem;
                if (container != null)
                    container.Background = Brushes.Yellow;
            }
        }

        private Stopwatch SortSWWrapper(Action<SearchAndSortController.SortType, SearchAndSortController.Dataset> method, SearchAndSortController.SortType sortType, SearchAndSortController.Dataset dataset)
        {
            var sw = Stopwatch.StartNew();
            method(sortType, dataset);
            sw.Stop();
            return sw;
        }
        private Stopwatch SearchrtSWWrapper(Action<double, SearchAndSortController.SearchType, SearchAndSortController.Dataset> method, Func<SearchAndSortController.Dataset, double> convert, SearchAndSortController.SearchType searchType, SearchAndSortController.Dataset dataset)
        {
            double target = convert(dataset);
            var sw = Stopwatch.StartNew();
            method(target,searchType, dataset);
            sw.Stop();
            return sw;
        }
        private double ConvertAndCheckTxt(SearchAndSortController.Dataset dataset)
        {
            var textbox = dataset == SearchAndSortController.Dataset.A1 ? SearchTrgtATxt : SearchTrgtBTxt;
            if (!double.TryParse(textbox.Text, out double Target))
            {
                MessageBox.Show("Cannot Convert");
                return 0;
            }
            return Target;
        }
        #endregion
    }
}