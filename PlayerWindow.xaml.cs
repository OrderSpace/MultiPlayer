using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

            Topmost = false;
            Button_Top.Foreground = new SolidColorBrush(Color.FromRgb(131, 131, 131));
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
            if (Topmost)
            {
                Button_Top.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
            else
            {
                Button_Top.Foreground = new SolidColorBrush(Color.FromRgb(131, 131, 131));
            }
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

        private void SeekLostMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (isEnable)
            {
                mediaElement.Position = TimeSpan.FromMilliseconds(timelineSlider.Value);
                mediaElement.Play();
                isPlaying = true;
            }
        }

        private void Button_TimeControl(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
            isPlaying = false;
            double timeIncrement = Convert.ToDouble((sender as Button).Content.ToString().Trim('s'));
            mediaElement.Position = TimeSpan.FromMilliseconds(mediaElement.Position.TotalMilliseconds + timeIncrement*1000);
            mediaElement.Play();
            isPlaying = true;
        }
    }
}
