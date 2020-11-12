using System;
using System.Windows;
using Pipes;

namespace Vng2020
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        private ServerPipe _serverPipe;
        public MainWindow()
        {
            InitializeComponent();
            CreateServer();
        }

        private void CreateServer()
        {
            _serverPipe = new ServerPipe(Pipes.Constants.PipeName, p => p.StartStringReaderAsync());
            _serverPipe.DataReceived += (sndr, args) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MyTextBox.Text = args.String;
                    Console.WriteLine(args.String);
                });
                
            };

            _serverPipe.Connected += (sndr, args) =>
            {
                Console.WriteLine(args.ToString());
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _serverPipe.WriteString(SendText.Text);
        }
    }
}
