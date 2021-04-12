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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public TextBox dataBox;
        public Label displayLabel;

        public MainWindow()
        {
            InitializeComponent();
            dataBox = (TextBox)this.FindName("DataText");
            displayLabel = (Label)this.FindName("DisplayLabel");
        }

        private void DisplayButton_Click(object sender, RoutedEventArgs e)
        {
            displayLabel.Content = dataBox.Text;
            dataBox.Text = "";
        }
    }
}
