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
using System.Windows.Threading;

namespace ResetTimer
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer t = new DispatcherTimer();
        int min, sec;
        int msgRemainTime = 0;

        public MainWindow()
        {
            InitializeComponent();
            t.Interval = new TimeSpan(0, 0, 1);
            t.Tick += timerTicked;
            t.IsEnabled = true;
            this.Topmost = true;
        }


        private void timerTicked(object sender, EventArgs e)
        {
            if (sec == 59)
            {
                sec = 0; min++;
            }
            else
            {
                sec++;
            }
            Display();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            sec = 0; min = 0;
            Display();
        }

        void Display()
        {
            string ssec = sec.ToString();
            if (sec < 10) ssec = "0" + ssec;
            disp.Content = $"{min}:{ssec}";
            if (msgRemainTime <= 0) clock.Content = time(DateTime.Now);
            else msgRemainTime--;
        }

        string time(DateTime dt)
        {
            string mzero = (dt.Minute < 10) ? "0" : "";
            string szero = (dt.Second < 10) ? "0" : "";
            return $"{dt.Year}/{dt.Month}/{dt.Day} {dt.Hour}:{mzero + dt.Minute}:{szero + dt.Second}";
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Topmost = !this.Topmost;
            clock.Content = (this.Topmost) ? "最前面表示にしました" : "最前面表示を解除しました";
            msgRemainTime = 3;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
    }
}
