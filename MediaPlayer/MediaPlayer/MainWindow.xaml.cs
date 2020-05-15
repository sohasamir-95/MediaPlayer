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
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.Windows.Shell;

namespace MediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Uri iconUri;
        public MainWindow()
        {

            InitializeComponent();
            this.Title = "MediaPlayer";
            iconUri = new Uri(@"C:\Users\Carnival Stores\Desktop\MediaPlayer\icons\sound.jfif", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);

        }

        private void openBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd;
            ofd = new OpenFileDialog();
            ofd.AddExtension = true;
            ofd.DefaultExt = ".";
            ofd.Filter = "Media File(*.*) |*.*";
            ofd.ShowDialog();
            listbox1.Items.Add(ofd.FileName);
            MediaIcon();

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(timer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

        }
        void timer_Tick(object sender, EventArgs e)
        {
            slider1.Value = mediaElement.Position.TotalSeconds;

        }
        private void listbox1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            mediaElement.Stop();
            mediaElement.Source = new Uri(listbox1.SelectedItem.ToString());

            mediaElement.Play();


        }
        private void palyBtn_Click(object sender, RoutedEventArgs e)
        {

            if (listbox1.Items.Count >= 1)
            {

                mediaElement.Source = new Uri(listbox1.SelectedItem.ToString());
                mediaElement.Play();

            }
            else
            {
                System.Windows.MessageBox.Show("select Item");
            }

        }

        private void stopBtn_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
        }

        private void pauseBtn_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TimeSpan tt = mediaElement.NaturalDuration.TimeSpan;
            TimeSpan ts = TimeSpan.FromSeconds(e.NewValue);
            mediaElement.Position = ts;
            label1.Content = mediaElement.Position.ToString(@"mm\:ss");
            label2.Content = tt.ToString(@"mm\:ss");

        }

        private void Volume_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement.Volume = Volume_slider.Value;
        }

        private void mediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {


            if (mediaElement.NaturalDuration.HasTimeSpan)
            {
                TimeSpan ts = TimeSpan.FromMilliseconds(mediaElement.NaturalDuration.TimeSpan.TotalMilliseconds);
                slider1.Maximum = ts.TotalSeconds;
            }


        }

        private void mediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {

        }

        private void rewindBtn_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Position = mediaElement.Position.Add(new TimeSpan(0, 0, -5));

        }

        private void fastforwardBtn_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Position = mediaElement.Position.Add(new TimeSpan(0, 0, 5));

        }

        private void MediaIcon()
        {
            string playing = this.Title;
            string type = playing.Substring(playing.IndexOf('.') + 1, 3).ToLower();

            if (type == "mp4")
            {
                iconUri = new Uri(@"C:\Users\Carnival Stores\Desktop\MediaPlayer\image\mp4.png", UriKind.RelativeOrAbsolute);

            }
            else if (type == "mp3")
            {

                iconUri = new Uri(@"C:\Users\Carnival Stores\Desktop\MediaPlayer\image\mp3.jfif", UriKind.RelativeOrAbsolute);

            }
            else if (type == "wav")
            {
                iconUri = new Uri(@"C:\Users\Carnival Stores\Desktop\MediaPlayer\image\wav.jfif", UriKind.RelativeOrAbsolute);

            }
            this.Icon = BitmapFrame.Create(iconUri);

        }

        private void listbox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Title = listbox1.SelectedItem.ToString();
            // MediaIcon();


        }

        private void speedslider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            speedslider.TickFrequency = speedslider.Value;
            mediaElement.SpeedRatio = speedslider.Value;
        }

        private void ThumbButtonInfo_Click(object sender, EventArgs e)
        {

            mediaElement.Position = mediaElement.Position.Add(new TimeSpan(0, 0, -5));

        }

        private void ThumbButtonInfo_Click_1(object sender, EventArgs e)
        {
            if (myInfo.ProgressState == System.Windows.Shell.TaskbarItemProgressState.Normal)
            {
                mediaElement.Stop();

                myInfo.Overlay = new BitmapImage(new Uri(@"C:\Users\Carnival Stores\Desktop\MediaPlayer\image\pause.jfif"));
            }
            else
            {
                myInfo.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Normal;

            }
        }




        private void ThumbButtonInfo_Click_2(object sender, EventArgs e)
        {
            mediaElement.Position = mediaElement.Position.Add(new TimeSpan(0, 0, 5));

        }
    }

}