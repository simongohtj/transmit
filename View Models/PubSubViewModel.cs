using System;
using System.Windows.Input;

namespace Transmit
{
    class PubSubViewModel : ObservableObject
    {
        private ButtonGo buttonGo;

        public PubSubViewModel()
        {
            buttonGo = new ButtonGo(this);
        }

        public string MyIP
        {
            get { return CommunicatorModel.Instance.MyIP; }
            set { CommunicatorModel.Instance.MyIP = value; }
        }

        public int MyPort
        {
            get { return CommunicatorModel.Instance.MyPort; }
            set { CommunicatorModel.Instance.MyPort = value; }
        }

        public string OtherIP
        {
            get { return CommunicatorModel.Instance.OtherIP; }
            set { CommunicatorModel.Instance.OtherIP = value; }
        }

        public int OtherPort
        {
            get { return CommunicatorModel.Instance.OtherPort; }
            set { CommunicatorModel.Instance.OtherPort = value; }
        }

        public string Topic
        {
            get { return CommunicatorModel.Instance.Topic; }
            set { CommunicatorModel.Instance.Topic = value; }
        }

        public void Start()
        {
            CommunicatorModel.Instance.Start(MyIP, MyPort, OtherIP, OtherPort, Topic);
        }

        public ICommand OnClick_ButtonGo
        {
            get { return buttonGo; }
        }
    }

    class ButtonGo : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private PubSubViewModel pubSubViewModel;

        public ButtonGo(PubSubViewModel pubSubViewModel)
        {
            this.pubSubViewModel = pubSubViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            pubSubViewModel.Start();
        }
    }
}
