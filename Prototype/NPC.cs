using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace FireWorkMahem
{
    class NPC : IDisposable
    {
        
        public Texture2D NPCIMG;       //This is the NPCTexture
        public Vector2 Position;       //Just marking its Position
        public Vector2 Direction;      //This is the Direction the Particle is Moving in
        public float RotationAngle;    //The angle at twhich sprite facing
        public float Circle;           //Using this as a MathHelper

         
        public NPC(Texture2D Texture, Vector2 Pos)
        {
            NPCIMG = Texture;
            Position = Pos;
            RotationAngle = 0;
            Circle = MathHelper.Pi * 2;
            Direction = new Vector2(0, 0);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void Update(float deltaTime)
        {
            RotationAngle = (float)Math.Atan2(Direction.X, -Direction.Y);
            RotationAngle = RotationAngle % Circle;

            Position += Direction;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            spritebatch.Draw(NPCIMG, new Rectangle((int)Position.X, (int)Position.Y, 2, 10), null, Color.White, RotationAngle, new Vector2(0, 0), SpriteEffects.None, 0);
            spritebatch.End();
        }

    }
}
