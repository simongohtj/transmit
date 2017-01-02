namespace Transmit
{
    class MessageViewModel : ObservableObject
    {
        private string _message = "Hello World";

        public string Message
        {
            get
            {
                return _message;
            }

            set
            {
                _message = value;
                NotifyPropertyChanged("Message");
            }
        }
    }
}
