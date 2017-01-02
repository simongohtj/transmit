using NetMQ;
using NetMQ.Sockets;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Transmit
{
    public sealed class CommunicatorModel : ObservableObject
    {
        private static readonly Lazy<CommunicatorModel> instance = new Lazy<CommunicatorModel>(() => new CommunicatorModel());

        public static CommunicatorModel Instance { get { return instance.Value; } }

        private SubscriberSocket SubSocket { get; set; } = new SubscriberSocket();
        private PublisherSocket PubSocket { get; set; } = new PublisherSocket();

        private string myIP = "localhost";
        private int myPort = 3333;

        private string otherIP = "192.168.0.25";
        private int otherPort = 7777;

        private string topic = string.Empty;

        private string receivedMessage = string.Empty;
        private string sendMessage = "Qwerty";

        private const int HIGH_WATER_MARK = 1000;

        /// <summary>
        /// Flag to continue/end thread operations
        /// </summary>
        private bool IsRunThread = true;

        private object _object = new object();

        public string MyIP
        {
            get { return myIP; }
            set
            {
                myIP = value;
                NotifyPropertyChanged();
            }
        }

        public int MyPort
        {
            get { return myPort; }
            set
            {
                myPort = value;
                NotifyPropertyChanged();
            }
        }

        public string OtherIP
        {
            get { return otherIP; }
            set
            {
                otherIP = value;
                NotifyPropertyChanged();
            }
        }

        public int OtherPort
        {
            get { return otherPort; }
            set
            {
                otherPort = value;
                NotifyPropertyChanged();
            }
        }

        public string Topic
        {
            get { return topic; }
            set
            {
                topic = value;
                NotifyPropertyChanged();
            }
        }

        public string ReceivedMessage
        {
            get { return receivedMessage; }
            set
            {
                receivedMessage = value;
                NotifyPropertyChanged();
            }
        }

        public string SendMessage
        {
            get { return sendMessage; }
            set
            {
                sendMessage = value;
                NotifyPropertyChanged();
            }
        }

        private CommunicatorModel() { }

        public void Start(string myIP, int myPort, string otherIP, int otherPort, string topic = "")
        {
            MyIP = myIP;
            MyPort = myPort;
            OtherIP = otherIP;
            OtherPort = otherPort;
            Topic = topic;

            SubSocket.Options.ReceiveHighWatermark = HIGH_WATER_MARK;
            PubSocket.Options.SendHighWatermark = HIGH_WATER_MARK;

            PubSocket.Bind($@"tcp://0.0.0.0:{MyPort}");

            Task taskReceive = new Task(() => Receive());
            taskReceive.Start();
        }

        public void Stop()
        {
            //stops receiving thread
            IsRunThread = false;
        }

        private void Receive()
        {
            SubSocket.Connect($@"tcp://{OtherIP}:{OtherPort}");
            SubSocket.Subscribe(topic);

            string receivedBuffer = string.Empty;

            while (IsRunThread)
            {
                receivedBuffer = string.Empty;
                receivedBuffer = SubSocket.ReceiveFrameString();
                if (receivedBuffer != string.Empty)
                {
                    lock (_object)
                    {
                        ReceivedMessage = receivedBuffer;
                    }
                }

                Thread.Sleep(0);
            }
        }

        public void Send(string message)
        {
            PubSocket.SendFrame(message);
            //PubSocket.SendMoreFrame("QWERTY").SendFrame(message);
        }
    }
}
