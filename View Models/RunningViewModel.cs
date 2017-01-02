using System;
using System.Windows.Input;

namespace Transmit
{
    class RunningViewModel : ObservableObject
    {
        private ButtonSend buttonGo;

        public RunningViewModel()
        {
            buttonGo = new ButtonSend(this);
        }

        public string ReceivedMessage
        {
            get { return CommunicatorModel.Instance.ReceivedMessage; }
            set
            {
                CommunicatorModel.Instance.ReceivedMessage = value;
                NotifyPropertyChanged();
            }
        }

        public string SendMessage
        {
            get { return CommunicatorModel.Instance.SendMessage; }
            set
            {
                CommunicatorModel.Instance.SendMessage = value;
                NotifyPropertyChanged();
            }
        }

        private CommunicatorModel CM = CommunicatorModel.Instance;
        public CommunicatorModel CommsModel
        {
            get { return CM; }
            set
            {
                CM = value;
                NotifyPropertyChanged();
            }
        }

        public string OtherIP => CommunicatorModel.Instance.OtherIP;

        public int OtherPort => CommunicatorModel.Instance.OtherPort;

        public int MePort => CommunicatorModel.Instance.MyPort;

        public void Send(string message) => CommunicatorModel.Instance.Send(message);

        public ICommand OnClick_BtnSend => buttonGo;
    }

    class ButtonSend : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private RunningViewModel runningViewModel;

        public ButtonSend(RunningViewModel runningViewModel)
        {
            this.runningViewModel = runningViewModel;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => runningViewModel.Send(parameter as string);
    }
}
