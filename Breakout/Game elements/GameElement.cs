using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

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
        Rectangle,
        mixed
    }

    internal enum ElementType
    {
        //unset = 0,   //UNUSED: fill this in with the game's...what we we using this for, again? ElementType?
        Wall,
        Brick,
        Paddle,
        Ball,
        Score
    }


    public class GameElement      //All the stuff necessary to make a single Draw() or DrawString() call
    {
        internal Vector2 vec;
        internal Texture2D texture;
        internal Rectangle rect;
        internal Color color;
        internal SpriteFont font;
        internal string text = "";

        //stuff for the fancy 'mixed' calls:
        internal Rectangle destRect;
        internal Rectangle? sourceRect;
        internal float rotation;
        internal Vector2 origin;
        internal SpriteEffects effects;
        internal float layerDepth;

        internal RenderType renderType;
        internal CallType callType;
 
        //NOTE TO SELF: For next time, let's have GameElement base class then inherit a UIElement and a TextElement.
        // Then each class can have its own constructor(s).
        // UIElement could then be split up into UIElement_Rect and UIElement_Vector2, if we want.  (Which...maybe.)
        internal GameElement(RenderType renderType, SpriteFont font, string text, Vector2 vec, Color color)
        {
            if (renderType == RenderType.UI)
                throw new Exception("RenderType doesn't match the signature, yo");

            this.renderType= renderType;
            this.font = font;
            this.text = text;
            this.vec = vec;
            this.color = color;
        }

        internal GameElement(RenderType renderType, CallType callType, Texture2D texture, Vector2 vec, Color color)
        {
            if (renderType == RenderType.Text)
                throw new Exception("RenderType.Text needs to be accompanied by a font, a string, a Vector2 and color");
            if (callType == CallType.Rectangle || callType == CallType.mixed)
                throw new Exception("CallType doesn't match the signature, yo");

            this.renderType = renderType;
            this.callType = callType;
            this.texture = texture;
            this.vec = vec;
            this.color = color;
        }

        internal GameElement(RenderType renderType, CallType callType, Texture2D texture, Rectangle rect, Color color)
        {
            if (renderType == RenderType.Text)
                throw new Exception("RenderType.Text needs to be accompanied by a font, a string, a Vector2 and color");
            if (callType == CallType.Vector2 || callType == CallType.mixed)
                throw new Exception("CallType doesn't match the signature, yo");

            this.renderType = renderType;
            this.callType = callType;
            this.texture = texture;
            this.rect = rect;
            this.color = color;
        }

        internal GameElement(RenderType renderType, CallType callType, Texture2D texture, Rectangle destRect, Rectangle? sourceRect, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
        {
            if (renderType == RenderType.Text)
                throw new Exception("RenderType.Text needs to be accompanied by a font, a string, a Vector2 and color");
            if (callType == CallType.Vector2 || callType == CallType.Rectangle)
                throw new Exception("CallType doesn't match the signature, yo");

            this.renderType = renderType;
            this.callType = callType;
            this.texture = texture;
            this.destRect = destRect;
            this.sourceRect = sourceRect;
            this.color = color;
            this.rotation = rotation;
            this.origin = origin;
            this.effects = effects;
            this.layerDepth = layerDepth;
        }

    }//END class GameElement
}
