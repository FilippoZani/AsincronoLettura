using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Threading;
using System.IO;

namespace AsincronoLettura
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random r;
        int caratteri;

        public MainWindow()
        {
            InitializeComponent();
            r = new Random();
            caratteri = 0;
            Lettura();
            ProgressBar();
        }

        public async void Lettura()
        {
            await Task.Run(() =>
            {

                using (StreamReader sr = new StreamReader("Data.txt"))
                {
                    string entireFile = sr.ReadToEnd();
                    caratteri = entireFile.Length;
                }
            });
        }

        public async void ProgressBar()
        {
            await Task.Run(() =>
            {
                int i = 0;
                while(i < 100)
                {
                    i += r.Next(1, 8);
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        if(i < 100)
                        {
                            pb1.Value = i;
                        }
                        else
                        {
                            pb1.Value = 100;
                            MessageBox.Show("Il numero di caratteri è: " + caratteri);
                        }
                    }));
                    Thread.Sleep(600);
                }
            });
        }
    }
}
