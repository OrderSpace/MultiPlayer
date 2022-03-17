using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MultiPlayer
{
    public partial class MainWindow : Window
    {
        PlayerWindow playerWindow1;
        PlayerWindow playerWindow2;

        string master;

        private DispatcherTimer timer;

        public MainWindow()
        {
            master = "";

            timer = new();
            timer.Interval = TimeSpan.FromSeconds(0.3);
            timer.Tick += new EventHandler(Timer_Tick);

            InitializeComponent();
        }

        ~MainWindow()
        {
            if (playerWindow1 != null)
            {
                playerWindow1.Close();
            }
            if (playerWindow2 != null)
            {
                playerWindow2.Close();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (playerWindow1 != null && playerWindow2 != null)
            {
                switch (master)
                {
                    case "Video1":
                        playerWindow2.mediaElement.Position = playerWindow1.mediaElement.Position;
                        if (!playerWindow1.isPlaying && playerWindow2.isPlaying)
                        {
                            playerWindow2.mediaElement.Pause();
                            playerWindow2.isPlaying = false;
                        }
                        else if (playerWindow1.isPlaying && !playerWindow2.isPlaying)
                        {
                            playerWindow2.mediaElement.Play();
                            playerWindow2.isPlaying = true;
                        }
                        break;
                    case "Video2":
                        playerWindow1.mediaElement.Position = playerWindow2.mediaElement.Position;
                        if (!playerWindow2.isPlaying && playerWindow1.isPlaying)
                        {
                            playerWindow1.mediaElement.Pause();
                            playerWindow1.isPlaying = false;
                        }
                        else if (playerWindow2.isPlaying && !playerWindow1.isPlaying)
                        {
                            playerWindow1.mediaElement.Play();
                            playerWindow1.isPlaying = true;
                        }
                        break;
                }
            }

            if (playerWindow1 != null)
            {
                playerWindow1.timelineSlider.Value = playerWindow1.mediaElement.Position.TotalMilliseconds;
                playerWindow1.currentTime.Text = playerWindow1.mediaElement.Position.ToString(@"hh\:mm\:ss");
            }

            if(playerWindow2 != null)
            {
                playerWindow2.timelineSlider.Value = playerWindow2.mediaElement.Position.TotalMilliseconds;
                playerWindow2.currentTime.Text = playerWindow2.mediaElement.Position.ToString(@"hh\:mm\:ss");
            }
        }

        private void Button_Video1_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "mp4文件(*.mp4)|*.mp4";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TextBox_Video1.Text = openFileDialog.FileName;
            }
        }

        private void Button_Video2_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "mp4文件(*.mp4)|*.mp4";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TextBox_Video2.Text = openFileDialog.FileName;
            }
        }

        private void Button_Start_Click(object sender, RoutedEventArgs e)
        {
            Button_Start.IsEnabled = false;

            ComboBox_Master.Items.Clear();

            if (File.Exists(TextBox_Video1.Text))
            {
                playerWindow1 = new(TextBox_Video1.Text);
                playerWindow1.Title = "Video1: " + Path.GetFileName(TextBox_Video1.Text);
                playerWindow1.Show();
                ComboBox_Master.Items.Add("Video1");
            }

            if (File.Exists(TextBox_Video2.Text))
            {
                playerWindow2 = new(TextBox_Video2.Text);
                playerWindow2.Title = "Video2: " + Path.GetFileName(TextBox_Video2.Text);
                playerWindow2.Show();
                ComboBox_Master.Items.Add("Video2");
            }

        }

        private void Button_Abort_Click(object sender, RoutedEventArgs e)
        {
            if (playerWindow1 != null)
            {
                playerWindow1.Close();
            }
            if (playerWindow2 != null)
            {
                playerWindow2.Close();
            }

            master = "";
            ComboBox_Master.Items.Clear();
            timer.Stop();

            Button_Start.IsEnabled = true;
        }

        private void Button_Speed_Click(object sender, RoutedEventArgs e)
        {
            string speed = ComboBox_Speed.SelectionBoxItem.ToString().Trim('x');
            if (!string.IsNullOrEmpty(speed))
            {
                if (playerWindow1 != null)
                {
                    playerWindow1.mediaElement.SpeedRatio = Convert.ToDouble(speed);
                }

                if (playerWindow2 != null)
                {
                    playerWindow2.mediaElement.SpeedRatio = Convert.ToDouble(speed);
                }
            }
        }

        private void Button_Master_Click(object sender, RoutedEventArgs e)
        {
            master = "" + ComboBox_Master.SelectionBoxItem.ToString();
            if (!timer.IsEnabled)
            {
                timer.Start();
                if(playerWindow1 != null)
                {
                    playerWindow1.isEnable = true;
                }
                if(playerWindow2 != null)
                {
                    playerWindow2.isEnable = true;
                }                
            }
        }

        private void TextBox_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Link;
            e.Handled = true;
        }

        private void TextBox_PreviewDrop(object sender, DragEventArgs e)
        {
            (sender as TextBox).Text = (e.Data.GetData(DataFormats.FileDrop) as string [])[0];
        }
    }
}
