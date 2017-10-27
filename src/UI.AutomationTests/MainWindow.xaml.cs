using System.Windows;

namespace UI.AutomationTests
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnClick_Click(object sender, RoutedEventArgs e)
        {
            var a = string.IsNullOrEmpty(txtA.Text) ? 0 : int.Parse(txtA.Text);
            var b = string.IsNullOrEmpty(txtB.Text) ? 0 : int.Parse(txtB.Text);
            txtC.Text = (a + b).ToString();
        }
    }
}
