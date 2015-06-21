///<Author>Aaron Cox</Author>
///<Basic_Usage>
///
/// In Game.cs, rather than creating a SpriteBatch, Create a SpriteBatchExtended instance instead
/// 
/// As SpriteBatchExtended inherits from SpriteBatch
/// Compatibility is maintained.
/// 
///</Basic_Usage>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FireWorkMahem
{
    public class SpriteBatchExtended : SpriteBatch
    {
        Texture2D pixel;

        public SpriteBatchExtended(GraphicsDevice graphicsDevice)
            : base(graphicsDevice)
        {
            // create an array of 1 pixel colored white
            Color[] pixels = new Color[1]  {  Color.White  };
            
            // create an empty texture
            pixel = new Texture2D(graphicsDevice, 1, 1);

            // set the pixels of the texture
            pixel.SetData(pixels);
            
        }

        /// <summary>
        /// Draws a line from p1 to p2 with the specified color
        /// </summary>
        /// <param name="p1">starting point for the line</param>
        /// <param name="p2">ending point for the line</param>
        /// <param name="colour">color the line should be drawn</param>
        public void DrawLine(Vector2 p1, Vector2 p2, Color color)
        {
            DrawLine(p1, p2, color, 1.0f);
        }

        /// <summary>
        /// Draws a line from p1 to p2 with the specified color and thickness
        /// </summary>
        /// <param name="p1">starting point for the line</param>
        /// <param name="p2">ending point for the line</param>
        /// <param name="color">color the line should be drawn</param>
        /// <param name="thickness">how thick the line should be rendered</param>
        public void DrawLine(Vector2 p1, Vector2 p2, Color color, float thickness)
        {
            Vector2 diff = p2 - p1;
            float len = diff.Length();
            float rot = (float)Math.Atan2(diff.Y, diff.X);

            this.Draw(pixel, p1, null, color, rot, new Vector2(0.0f, 0.5f), new Vector2(len, thickness), SpriteEffects.None, 0.0f);
        }
    }
}
