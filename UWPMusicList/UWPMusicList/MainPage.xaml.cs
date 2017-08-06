using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPMusicList
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        string fileName = @"MusicInfo";
        StorageFolder folderPath = ApplicationData.Current.LocalFolder;
        ObservableCollection<MusicInfo> LstMusicInfo = new ObservableCollection<MusicInfo>();

        private void btnSelectMusicInfo_Click(object sender, RoutedEventArgs e)
        {
            ReadMusicFile();
        }

        private void lbMusicLyrics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtMusicLyrics.Text = "";
            ReadLyricFile();
        }

        public async void ReadMusicFile()
        {
            try
            {
                /* opening the MusicInfo .txt file */
                if (await folderPath.TryGetItemAsync(fileName + ".txt") != null)
                {
                    /* this line is used for testing purposes */
                    Debug.WriteLine("\n" + "******************** MusicInfo.txt file already exists ********************" + "\n");

                    StorageFile storageFile = await folderPath.GetFileAsync(fileName + ".txt");

                    string json = await FileIO.ReadTextAsync(storageFile);

                    LstMusicInfo = JsonConvert.DeserializeObject<ObservableCollection<MusicInfo>>(json);

                    bool statusJson = IsNullOrWhiteSpace(json);

                    if (statusJson == false)
                    {
                        foreach (MusicInfo dataLine in LstMusicInfo)
                        {
                            lbMusicFile.Items.Add(dataLine.SongName);
                            lbMusicPath.Items.Add(dataLine.SongFile);
                            lbMusicLyrics.Items.Add(dataLine.SongLyrics);
                        }
                    }
                }
            }
            catch (Exception exReadMusicFile)
            {
                var messageDialog = new MessageDialog("Music file read error." + "\n" + exReadMusicFile);
                await messageDialog.ShowAsync();
            }
        }

        public async void ReadLyricFile()
        {
            string lyricFileName;
            lyricFileName = Convert.ToString(lbMusicLyrics.SelectedItem);

            try
            {
                /* opening the Song Lyric .txt file */
                if (await folderPath.TryGetItemAsync(lyricFileName + ".txt") != null)
                {
                    StorageFile storageFile = await folderPath.GetFileAsync(lyricFileName + ".txt");

                    var lyricText = await FileIO.ReadTextAsync(storageFile);
                    txtMusicLyrics.Text = Convert.ToString(lyricText);
                }
            }
            catch (Exception exDisplayLyrics)
            {
                var messageDialog = new MessageDialog("Lyric file read error." + "\n" + exDisplayLyrics);
                await messageDialog.ShowAsync();
            }
        }


        public static bool IsNullOrWhiteSpace(string jsonstring)
        {
            if (jsonstring != null)
            {
                for (int i = 0; i < jsonstring.Length; i++)
                {
                    if (!char.IsWhiteSpace(jsonstring[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }        
    }
}
