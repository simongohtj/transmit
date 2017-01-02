namespace Transmit
{
    /// <summary>
    /// Interaction logic for InformationWindow.xaml
    /// </summary>
    public partial class RunningView
    {
        /// <summary>
        /// Reference to previous View to faciliate moving back
        /// </summary>
        PubSubView pubSubView;

        public RunningView() => InitializeComponent();

        public RunningView(PubSubView view)
        {
            InitializeComponent();

            pubSubView = view;
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) => pubSubView.Show();
    }
}
