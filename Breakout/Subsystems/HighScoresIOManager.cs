using Breakout.Game_states;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Diagnostics;

namespace Breakout.Subsystems
{
    internal class HighScoresIOManager
    {
        bool saving = false;
        bool loading = false;

        private HighScores m_loadedState = null;



        internal HighScores GetHighScores()
        {
            return m_loadedState;
        }

        internal void SaveHighScores(HighScores highScores)
        {
            lock (this)
            {
                if (!saving)
                {
                    saving = true;
                    //HighScores hs = highScores;
                    finalizeSaveAsync(highScores);
                }
            }
        }

        internal void ReadInHighScores()
        {
            lock (this)
            {
                if (!loading)
                {
                    loading = true;
                    finalizeLoadAsync();
                }
            }
        }

        private async void finalizeSaveAsync(HighScores state)
        {
            await Task.Run(() =>
            {
                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    try
                    {
                        using (IsolatedStorageFileStream fs = storage.OpenFile("HighScores.xml", FileMode.Create))
                        {
                            if (fs != null)
                            {
                                XmlSerializer mySerializer = new XmlSerializer(typeof(HighScores));
                                mySerializer.Serialize(fs, state);
                            }
                        }
                    }
                    catch (IsolatedStorageException)
                    {
                        // Ideally show something to the user, but this is demo code :)
                    }
                }

                saving = false;
            });
        }

        private async void finalizeLoadAsync()
        {
            await Task.Run(() =>
            {
                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    try
                    {
                        if (storage.FileExists("HighScores.xml"))
                        {
                            using (IsolatedStorageFileStream fs = storage.OpenFile("HighScores.xml", FileMode.Open))
                            {
                                if (fs != null)
                                {
                                    XmlSerializer mySerializer = new XmlSerializer(typeof(HighScores));
                                    m_loadedState = (HighScores)mySerializer.Deserialize(fs);
                                }
                            }
                        }
                    }
                    catch (IsolatedStorageException e)
                    {
                        Debug.Print($"HighScoresIOManager says: {e.Message}");
                    }
                }

                loading = false;
            });
        }
    }
}
