using Breakout.Game_elements;
using Breakout.Subsystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout.Game_objects.non_derived
{
    //First, a 'HighScore' class to hold an individual score (and to sort the list of these objects)
    internal class HighScore : IComparable<HighScore>
    {
        internal int score;

        internal HighScore(int score)
        {
            this.score = score;
        }

        public int CompareTo(HighScore other)
        {
            if (this.score > other.score)
                return -1;
            else if (this.score < other.score)
                return 1;
            else
                return 0;
        }//END CompareTo()

    }//END class HighScore


    //Then, a 'HighScore' class to hold a list of HighScore objects
    internal class HighScores
    {
        List<HighScore> highScores;

        internal HighScores()
        {
            highScores = new();
            for (int i = 0; i < 5; i++)
            {
                HighScore aScore = new((i + 1) * 100);  //100, 200, ..., 500
                highScores.Add(aScore);
            }
        }

        /*internal void SetupHighScores(Renderer renderer, SpriteFont headerFont, SpriteFont font)
        {
            //TODO: Change the Vector2s in these to use the StringRenderer class
            //TODO: Add a 'high scores region' to the high scores view and render within that

            GameElement el = new(RenderType.Text, headerFont, "High scores:", new Vector2(100, 100), Color.White);
            renderer.AddToRenderList(el);

            for (int i = 0; i < 5; i++)
            {
                el = new(RenderType.Text, font, $"{highScores[i].score}", new Vector2(100, 150 + 50 * i), Color.White);
                renderer.AddToRenderList(el);
            }

            el = new(RenderType.Text, font, "Press Escape to return to menu", new Vector2(100, 500), Color.White);
            renderer.AddToRenderList(el);
        }*/

        //TODO: Implement (HighScores.ReinitializeHighScores())
        internal void ReinitializeHighScores()
        {
            Debug.Print("TODO: HighScores.ReinitializeHighScores()");
        }

        internal void AddSortChop(HighScore score, int numScores)
        {
            highScores.Add(score);
            highScores.Sort();
            while (highScores.Count > numScores)
                highScores.RemoveAt(numScores);
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
