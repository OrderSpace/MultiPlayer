using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace MultiPlayer
{
    public partial class MainWindow : Window
    {
        private List<PlayerWindow> playerList;

        public MainWindow()
        {
            InitializeComponent();
            playerList = new List<PlayerWindow>();
        }

        ~MainWindow()
        {
            foreach (PlayerWindow playerWindow in playerList)
            {
                playerWindow.Close();
            }
        }

        private void Button_Video1_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "mp4文件(*.mp4)|*.mp4";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TextBox_VIdeo1.Text = openFileDialog.FileName;
            }
        }

        private void Button_Video2_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "mp4文件(*.mp4)|*.mp4";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TextBox_VIdeo2.Text = openFileDialog.FileName;
            }
        }

        private void Button_Start_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(TextBox_VIdeo1.Text))
            {
                MessageBox.Show("Video1 was not found", "Error");
                return;
            }

            //if (!File.Exists(TextBox_VIdeo2.Text))
            //{
            //    MessageBox.Show("Video2 was not found", "Error");
            //    return;
            //}

            PlayerWindow playerWindow1 = new(TextBox_VIdeo1.Text);
            playerList.Add(playerWindow1);
            playerWindow1.Show();

            //PlayerWindow playerWindow2 = new(TextBox_VIdeo2.Text);
            //playerWindow2.Show();
            //playerList.Add(playerWindow2);
        }

        private void Button_Abort_Click(object sender, RoutedEventArgs e)
        {
            foreach (PlayerWindow playerWindow in playerList)
            {
                playerWindow.Close();
            }
        }

    }
}
