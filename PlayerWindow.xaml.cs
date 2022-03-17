using System;
using System.Windows;

namespace MultiPlayer
{
    public partial class PlayerWindow : Window
    {
        public bool isPlaying;

        public bool isEnable;

        public PlayerWindow(string uri)
        {
            isPlaying = false;
            isEnable = false;

            InitializeComponent();

            Grid_Control.IsEnabled = false;
            Grid_Control.Opacity = 0.0;

            mediaElement.Source = new Uri(uri);

            mediaElement.Volume = 0.0;
            mediaElement.SpeedRatio = 1.0;
        }

        ~PlayerWindow()
        {
            isPlaying = false;
            isEnable = false;
            
        }

        private void OnClickPlayMedia(object sender, RoutedEventArgs args)
        {
            if (isEnable)
            {
                mediaElement.Play();
                isPlaying = true;
            }
        }

        private void OnClickPauseMedia(object sender, RoutedEventArgs args)
        {
            if (isEnable)
            {
                mediaElement.Pause();
                isPlaying = false;
            }
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
        }

        private void Element_MediaEnded(object sender, EventArgs e)
        {
            isPlaying = false;
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

        private void SeekGotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (isEnable)
            {
                mediaElement.Pause();
                isPlaying = false;
            }
        }

        private void SeekSeekGotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (isEnable)
            {
                mediaElement.Play();
                isPlaying = true;
            }
        }
    }
}
