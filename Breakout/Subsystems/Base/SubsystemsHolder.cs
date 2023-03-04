using Breakout.Game_elements;
using Breakout.Subsystems.misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout.@Subsystems.Base
{
    public class SubsystemsHolder
    {
        public BO_Keyboard keyboard;
        public Renderer renderer;
        public StringRenderer stringRenderer;
        public BoxRenderer boxRenderer;

        //is this a subsystem?  *shrug* I suppose if we say that it is, then it is  *thumbs up*
        public HighScores highScores;
        //LOW-PRIORITY TODO: migrate the hsiom to subsystemsHolder
        public HighScoresIOManager hsiom;

        //can this go in here?  TBD!  //Follow-up: yep!
        public Spacing spacing;

        public SubsystemsHolder(GraphicsDeviceManager graphics) 
        {
            bool isDoneReadingInFile = false;

            keyboard = new();
            renderer = new();
            stringRenderer = new();
            boxRenderer = new();
            
            //let's read in the HighScores.xml file, if it exists
            hsiom = new();
            if (!hsiom.FileExists())
            {
                Debug.Print($"SubsystemHolder constructor says: File does NOT exist");

                highScores = new();
                highScores.ReinitializeHighScores();
                Debug.Print($"SubsystemHolder constructor says: We just new'd then reinitialized highScores; count is: {highScores.GetHighScoresList().Count}");
                hsiom.SaveHighScores(highScores);
                hsiom.WaitToFinish();

                /*bool isDone = false;
                while (!isDone)
                {
                    if (!hsiom.IsBusy())
                        isDone = true;
                }*/
            }
            else
            {
                Debug.Print($"SubsystemHolder constructor says: File exists");
                hsiom.ReadInHighScores();
                while (!isDoneReadingInFile)   //do we still need this while loop?  TBD  <--yep! apparently it just takes a while to GetHighScores()
                { 
                    if(!hsiom.IsBusy())
                    {
                        Debug.Print($"SubsystemHolder constructor says: hsiom.GetHighScores returned: {hsiom.GetHighScores()}, count: {hsiom.GetHighScores().GetHighScoresList().Count}");

                        highScores = hsiom.GetHighScores();
                        if (highScores != null)
                            isDoneReadingInFile = true;
                    }
                    else
                    {
                        //Debug.Print("SubsystemsHolder constructor says: hsiom.IsBusy atm");
                    }
                    //highScores.AddSortChop(new HighScore(0), 5);
                }
            }
            
            if (highScores == null)
                throw new Exception("Subsystems.Holder constructor says: highScores in null (after hsion.ReadInHighScores)");

            //moving on...            
            spacing = new(graphics);
        }

    }//END class SubsystemsHolder
}
