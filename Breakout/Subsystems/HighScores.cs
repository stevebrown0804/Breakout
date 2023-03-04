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
    public class HighScore : IComparable<HighScore>
    {
        public int score;

        internal HighScore() { }    //Necessary for serialization

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
    //[Serializable]
    public class HighScores
    {
        public List<HighScore> highScoresList;

        internal HighScores()
        {
            highScoresList = new();
            /*for (int i = 0; i < 5; i++)
            {
                HighScore aScore = new(i + 1);  //100, 200, ..., 500  (ish)
                highScoresList.Add(aScore);
            }
            highScoresList.Reverse();*/
        }

        internal void ReinitializeHighScores()
        {
            highScoresList.Clear();
            for (int i = 0; i < 5; i++)
            {
                HighScore aScore = new(0); //new(i + 1);  //100, 200, ..., 500  (ish)
                highScoresList.Add(aScore);
            }
            highScoresList.Reverse();

            AddSortChop(new HighScore(0), 5);       //just to try stuff

        }

        //returns true is score is in the list after chopping
        //note: doesn't care if score was already there (eg. you just duplicated an existing high score)
        internal bool AddSortChop(HighScore score, int numScores)
        {
            highScoresList.Add(score);
            highScoresList.Sort();
            while (highScoresList.Count > numScores)
                highScoresList.RemoveAt(numScores);

            if (highScoresList.Contains(score))
                return true;

            return false;
        }

        internal List<HighScore> GetHighScoresList()
        {
            return highScoresList;
        }

        internal List<string> GetHighScoresListOfStrings()
        {
            List<string> list = new();
            for (int i = 0; i < highScoresList.Count; i++)
            {
                list.Add($"{highScoresList[i].score}");
            }
            return list;
        }

        internal void EmptyHighScoresList()
        {
            highScoresList.Clear();
        }

    }//END class HighScores
}
