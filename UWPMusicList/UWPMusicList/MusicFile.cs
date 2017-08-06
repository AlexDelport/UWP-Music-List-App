using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;

namespace UWPMusicList
{
    class MusicFile
    {
        string fileName = @"MusicInfo";
        StorageFolder folderPath = ApplicationData.Current.LocalFolder;
        ObservableCollection<MusicInfo> LstMusicInfo = new ObservableCollection<MusicInfo>();

        public async void CreateMusicFile() /* create the Music Info .txt file if it does not exist */
        {
            /* this line is used for testing purposes */
            Debug.WriteLine("\n" + "******************** This is the UWPMusicList app's data storage path *******************   " + folderPath.Name + " folder path: " + folderPath.Path + "\n");

            try
            {
                if (await folderPath.TryGetItemAsync(fileName + ".txt") == null)
                {
                    LstMusicInfo.Add(new MusicInfo()
                    {
                        SongName = "Beatles - Hey Jude",
                        SongFile = "HeyJude.mp3",
                        SongLyrics = "HeyJude",
                    });

                    LstMusicInfo.Add(new MusicInfo()
                    {
                        SongName = "Lorde - Royals",
                        SongFile = "Royals.mp3",
                        SongLyrics = "Royals",
                    });

                    LstMusicInfo.Add(new MusicInfo()
                    {
                        SongName = "The Fosters - Pumped Up Kicks",
                        SongFile = "PumpedUpKicks.mp3",
                        SongLyrics = "PumpedUpKicks",
                    });

                    LstMusicInfo.Add(new MusicInfo()
                    {
                        SongName = "U2 - With Or Without You",
                        SongFile = "WithOrWithoutYou.mp3",
                        SongLyrics = "WithOrWithoutYou",
                    });

                    LstMusicInfo.Add(new MusicInfo()
                    {
                        SongName = "Metallica - Enter Sandman",
                        SongFile = "EnterSandman.mp3",
                        SongLyrics = "EnterSandman",
                    });

                    LstMusicInfo.Add(new MusicInfo()
                    {
                        SongName = "Metallica - The Unforgiven",
                        SongFile = "TheUnforgiven.mp3",
                        SongLyrics = "TheUnforgiven",
                    });

                    LstMusicInfo.Add(new MusicInfo()
                    {
                        SongName = "Metallica - Nothing Else Matters",
                        SongFile = "NothingElseMatters.mp3",
                        SongLyrics = "NothingElseMatters",
                    });

                    /* serialising the Music Info object */
                    String json = JsonConvert.SerializeObject(LstMusicInfo);

                    /* creating the MusicInfo .txt file */
                    var file = await folderPath.CreateFileAsync(fileName + ".txt", CreationCollisionOption.ReplaceExisting);

                    /* opening the MusicInfo .txt file */
                    var data = await file.OpenStreamForWriteAsync();

                    /* writing to the MusicInfo .txt file */
                    using (StreamWriter writer = new StreamWriter(data))
                    {
                        var serializedFile = JsonConvert.SerializeObject(LstMusicInfo);
                        writer.WriteLine(Convert.ToString(serializedFile));
                    }

                    /* this line is used for testing purposes */
                    Debug.WriteLine("\n" + "******************** MusicInfo.txt file Created ********************" + "\n");
                }
            }
            catch (Exception exCreateMusicFile)
            {
                var messageDialog = new MessageDialog("Music file create error." + "\n" + exCreateMusicFile);
                await messageDialog.ShowAsync();
            }

            /* this line is used for testing purposes */
            Debug.WriteLine("\n" + "******************** MusicInfo.txt file already exists ********************" + "\n");
        }
    }
}
