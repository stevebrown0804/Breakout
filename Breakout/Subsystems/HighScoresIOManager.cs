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
    public class HighScoresIOManager
    {
        bool saving = false;
        bool loading = false;

        private HighScores m_loadedState = null;

        internal HighScoresIOManager() { }

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

        internal bool FileExists()
        {
            IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication();
            if (storage.FileExists("HighScores.xml"))
                return true;

            return false;
        }

        internal bool IsBusy()
        {
            return saving || loading;
        }

        internal void WaitToFinish()
        {
            bool isDone = false;
            while (!isDone)
            {
                if (!IsBusy())
                    isDone = true;
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

                                //Debug.Print($"HSIOM.finalizeSaveAsync() says: ({fs}, {state}) has been serializaed");
                            }
                        }
                    }
                    catch (IsolatedStorageException e)
                    {
                        Debug.Print($"HighScoresIOManager.finalizeSaveAsync() says: {e.Message}");
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
                            //Debug.Print("storage.FileExists(\"HighScores.xml\") is TRUE");
                            using (IsolatedStorageFileStream fs = storage.OpenFile("HighScores.xml", FileMode.Open))
                            {
                                if (fs != null)
                                {
                                    //Debug.Print("hsiom.finalizeLoadAsync says: fs is non-null");
                                    XmlSerializer mySerializer = new XmlSerializer(typeof(HighScores));
                                    m_loadedState = (HighScores)mySerializer.Deserialize(fs);
                                    //Debug.Print($"({fs}, {m_loadedState}) has been deserialized");
                                }
                                else
                                {
                                    //Debug.Print("hsiom.finalizeLoadAsync says: fs is null! Problem?");
                                }
                            }
                        }
                        else
                        {
                            //Debug.Print("storage.FileExists(\"HighScores.xml\") is FALSE! Problem?");
                        }
                    }
                    catch (IsolatedStorageException e)
                    {
                        Debug.Print($"HighScoresIOManager.finalizeLoadAsync() says: {e.Message}");
                    }
                }

                loading = false;
            });
        }
    }
}
