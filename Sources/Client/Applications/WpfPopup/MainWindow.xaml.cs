using System.Timers;
using System.Windows;
using Microsoft.Win32.SafeHandles;

namespace Applications.Wpf.Popupnik
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string slotName = @"\\.\mailslot\messngr";
        private SafeFileHandle serverSlotHandle;
        private Timer timer;

        public MainWindow()
        {
            InitializeComponent();
            InitializeTimer();
            InitializeMailSlot();
        }

        private void InitializeMailSlot()
        {
            /*serverSlotHandle = Win32Helper.CreateMailslot(slotName, 0, -1, IntPtr.Zero);

            if (serverSlotHandle.IsInvalid)
            {
                MessageBox.Show("Could not create MailSlot!");
                Application.Current.Shutdown();
            }*/
        }

        private void InitializeTimer()
        {
            timer = new Timer(111);
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();

            CheckMailSlot();

            timer.Start();
        }

        private void CheckMailSlot()
        {
            /*var messageMaxSize = new IntPtr();
            var messageSize = new IntPtr();
            var numberOfMessages = new IntPtr();
            var readTimeout = new IntPtr();

            Win32Helper.GetMailslotInfo(serverSlotHandle, out messageMaxSize, out messageSize, out numberOfMessages,
                                        out readTimeout);
            {
                Debug.WriteLine(string.Format("Next size: {0}", messageSize));
                if (numberOfMessages.ToInt32() == 0)
                {
                    Debug.WriteLine("No new messages");
                    return;
                }

                Debug.WriteLine(string.Format("Number of waiting messages: {0}", numberOfMessages.ToInt32()));

                using (var fs = new FileStream(serverSlotHandle, FileAccess.Read))
                {
                    //fs.SafeFileHandle.Close();
                    using (var sr = new StreamReader(fs))
                    {
                        var msg = new char[messageSize.ToInt32()];
                        sr.Read(msg, 0, messageSize.ToInt32());

                        var message = new string(msg);
                        Dispatcher.Invoke(DispatcherPriority.Normal, (Action) (() => textBox1.AppendText(message)));
                    }
                }
            }*/
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            SendToMailSlot();
        }

        private void SendToMailSlot()
        {
            /*SafeFileHandle slotHandle = Win32Helper.CreateFile(slotName,
                                                               (uint) FileAccess.Write,
                                                               (uint) FileShare.Read,
                                                               0,
                                                               (uint) FileMode.Open,
                                                               (uint) FileAttributes.Normal,
                                                               0);
            if (slotHandle.IsInvalid)
            {
                MessageBox.Show("Could not open mailslot!");
                return;
            }

            using (var fs = new FileStream(slotHandle, FileAccess.Write))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.Write("Hello, world. {0}.\n", DateTime.Now);
                    //sw.Flush();
                }
            }*/
        }
    }
}