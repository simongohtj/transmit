namespace Transmit
{
    class Messages : ObservableObject
    {
        private string _message;

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
