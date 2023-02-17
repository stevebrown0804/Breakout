using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout.Game_elements
{
    internal enum RenderType
    {
        //unset = 0,
        UI,
        Text
    }

    internal enum CallType
    {
        //unset = 0,
        Vector2,
        Rectangle
    }

    internal enum ElementType
    {
        unset = 0,   //TODO: fill this in with the game's...what we we using this for, again? ElementType?
        Wall,
        Brick,
        Paddle,
        Ball,
        Score   //TOOD: What else goes in this list?  TBD
    }


    internal class GameElement      //All the stuff necessary to make a single Draw() or DrawString() call
    {
        //Member variables
        internal Vector2 coords;
        internal Texture2D texture;
        internal Rectangle rect;
        internal Color color;
        internal SpriteFont font;
        internal string text = "";        

        internal RenderType renderType;
        internal CallType callType;
        //END Member variables

        //
        //Constructors
        //
        internal GameElement(RenderType renderType, SpriteFont font, string text, Vector2 vec, Color color)
        {
            if (renderType == RenderType.UI)
                throw new Exception("RenderType.UI needs to be accompanied by a CallType, a [Vector2/Rectangle] and a Color");

            //TODO: GameElement(RenderType renderType, SpriteFont font, string text, Vector2 vec, Color color) (constructor)
        }

        internal GameElement(RenderType renderType, CallType callType, Texture2D texture, Vector2 vec, Color color)
        {
            if (renderType == RenderType.Text)
                throw new Exception("RenderType.Text needs to be accompanied by a string, a Vector2 and color");

            if (callType == CallType.Rectangle)
                throw new Exception("CallType.Rectangle needs to be accompanied by a Rectangle parameter.");

            //TODO: GameElement(RenderType renderType, CallType callType, Texture2D texture, Vector2 vec, Color color) (constructor)
        }

        internal GameElement(RenderType renderType, CallType callType, Texture2D texture, Rectangle rect, Color color)
        {
            if (renderType == RenderType.Text)
                throw new Exception("RenderType.Text needs to be accompanied by a string, a font, a Vector2 and color");

            if (callType == CallType.Vector2)
                throw new Exception("CallType.Vector2 needs to be accompanied by a Vector2 parameter.");

            //TODO: GameElement(RenderType renderType, CallType callType, Texture2D texture, Rectangle rect, Color color) (constructor)
        }

        //END Constructors

        
        //Methods

        //END Methods
    }
}
