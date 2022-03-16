using System;
using System.Windows;
using System.Windows.Threading;

namespace MultiPlayer
{
    public partial class PlayerWindow : Window
    {
        private DispatcherTimer timer;

        public PlayerWindow(string uri)
        {
            InitializeComponent();

            Grid_Control.IsEnabled = false;
            Grid_Control.Opacity = 0.0;

            mediaElement.Source = new Uri(uri);

            mediaElement.Volume = (double)volumeSlider.Value;
            mediaElement.SpeedRatio = 1.0;

            timer = new();
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += new EventHandler(Timer_Tick);
        }

        ~PlayerWindow()
        {
            mediaElement.Close();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timelineSlider.Value = mediaElement.Position.TotalMilliseconds;
            currentTime.Text = mediaElement.Position.ToString(@"hh\:mm\:ss");
        }

        private void OnClickPlayMedia(object sender, RoutedEventArgs args)
        {
            mediaElement.Play();
        }

        private void OnClickPauseMedia(object sender, RoutedEventArgs args)
        {
            mediaElement.Pause();
        }

        private void OnClickTopMedia(object sender, RoutedEventArgs e)
        {
            Topmost = !Topmost;
        }

        private void OnClickMinMedia(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            mediaElement.Volume = (double)volumeSlider.Value;
        }

        private void Element_MediaOpened(object sender, EventArgs e)
        {
            timelineSlider.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;
            totalTime.Text = mediaElement.NaturalDuration.TimeSpan.ToString(@"hh\:mm\:ss");
            timer.Start();
        }

        private void Element_MediaEnded(object sender, EventArgs e)
        {
            timer.Stop();
            mediaElement.Stop();
        }

        private void SeekToMediaPosition(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            mediaElement.Position = TimeSpan.FromMilliseconds(timelineSlider.Value);
        }

        private void Window_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Grid_Control.IsEnabled = true;
            Grid_Control.Opacity = 0.8;
        }

        private void Window_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Grid_Control.IsEnabled = false;
            Grid_Control.Opacity = 0.0;
        }
    }
}
