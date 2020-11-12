using System;
using System.Windows;
using Pipes;

namespace Estimulos
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private ClientPipe _clientPipe;
        public MainWindow()
        {
            InitializeComponent();
            CreateClient();
        }

        private void CreateClient()
        {
            _clientPipe = new ClientPipe(
                ".", 
                Pipes.Constants.PipeName, 
                p => p.StartStringReaderAsync());
            _clientPipe.DataReceived += (sndr, args) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ClientTextBpx.Text = args.String;
                    Console.WriteLine(args.String);
                });
                

            };

            _clientPipe.Connect();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _clientPipe.WriteString(SendTextToClient.Text);
        }
    }
}
