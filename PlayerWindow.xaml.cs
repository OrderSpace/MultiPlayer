using System;
using System.Windows;

namespace MultiPlayer
{
    public partial class PlayerWindow : Window
    {
        public PlayerWindow(string uri)
        {
            InitializeComponent();

            mediaElement.Source = new Uri(uri);
        }
        ~PlayerWindow()
        {
            mediaElement.Close();
        }

        private void OnClickPlayMedia(object sender, RoutedEventArgs args)
        {
            mediaElement.Play();

            InitializePropertyValues();
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

        private void ChangeMediaSpeedRatio(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            mediaElement.SpeedRatio = (double)speedRatioSlider.Value;
        }

        private void Element_MediaOpened(object sender, EventArgs e)
        {
            timelineSlider.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;
        }

        private void Element_MediaEnded(object sender, EventArgs e)
        {
            mediaElement.Stop();
        }

        private void SeekToMediaPosition(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            int SliderValue = (int)timelineSlider.Value;

            TimeSpan ts = new(0, 0, 0, 0, SliderValue);
            mediaElement.Position = ts;
        }

        void InitializePropertyValues()
        {
            mediaElement.Volume = (double)volumeSlider.Value;
            mediaElement.SpeedRatio = (double)speedRatioSlider.Value;
        }
    }
}
