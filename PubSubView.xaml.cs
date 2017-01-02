using System.Windows;

namespace Transmit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class PubSubView
    {
        public PubSubView()
        {
            InitializeComponent();
        }

        private void BtnGo_Click(object sender, RoutedEventArgs e)
        {
            new RunningView(this).Show();

            Hide();
        }
    }
}
