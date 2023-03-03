using Breakout.Game_elements;
using Breakout.Game_states;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Breakout.Subsystems
{
    //First, a 'HighScore' class to hold an individual score (and to sort the list of these objects)
    class HighScore : IComparable<HighScore>
    {
        internal int score;

        internal HighScore(int score)
        {
            this.score = score;
        }

        public int CompareTo(HighScore other)
        {
            if (score > other.score)
                return -1;
            else if (score < other.score)
                return 1;
            else
                return 0;
        }//END CompareTo()

    }//END class HighScore


    //Then, a 'HighScore' class to hold a list of HighScore objects
    public class HighScores
    {
        List<HighScore> highScores;

        internal HighScores()
        {
            highScores = new();
            for (int i = 0; i < 5; i++)
            {
                HighScore aScore = new((i + 1) * 10);  //100, 200, ..., 500  (ish)
                highScores.Add(aScore);
            }
        }

         //TODO: Implement (HighScores.ReinitializeHighScores())
        internal void ReinitializeHighScores()
        {
            Debug.Print("TODO: HighScores.ReinitializeHighScores()");
        }

        internal bool AddSortChop(HighScore score, int numScores)
        {
            highScores.Add(score);
            highScores.Sort();
            while (highScores.Count > numScores)
                highScores.RemoveAt(numScores);

            if (highScores.Contains(score))
                return true;

            return false;
        }

        internal List<HighScore> GetHighScoresList()
        {
            return highScores;
        }

        internal List<string> GetHighScoresListOfStrings()
        {
            List<string> list = new();
            for (int i = 0; i < highScores.Count; i++)
            {
                list.Add($"{highScores[i].score}");
            }
            return list;
        }

    }//END class HighScores
}
