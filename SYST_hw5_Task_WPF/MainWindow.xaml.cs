using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SYST_hw5_Task_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CancellationTokenSource cancellationTokenSource;
        private CancellationToken token;

        public MainWindow()
        {
            InitializeComponent();

        }

        private void btnStartTaskClick(object sender, RoutedEventArgs e)
        {
            InitCancelToken();

            Task.Factory.StartNew(() =>
            {
                WriteInTextblock("Task started");
                SleepOneSec();

                for (int i = 0; i < int.MaxValue; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        WriteInTextblock("Task interrupted");
                        return;
                    }
                    else
                    {
                        WriteInTextblock(i.ToString());
                        SleepOneSec();
                    }
                }
            });
        }

        private void btnStopTaskClick(object sender, RoutedEventArgs e)
        {
            cancellationTokenSource.Cancel();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        private void InitCancelToken()
        {
            cancellationTokenSource = new CancellationTokenSource();
            token = cancellationTokenSource.Token;
        }

        private static void SleepOneSec()
        {
            Thread.Sleep(1000);
        }

        private void WriteInTextblock(string str)
        {
            Dispatcher.Invoke(() => { txtBlck.Text = str; });
        }
    }
}
