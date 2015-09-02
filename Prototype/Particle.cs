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
    class Particle : IDisposable
    {
        public Texture2D ParticleIMG;  //This is the Particle Texture
        public Vector2 Position;       //This is the position of the particle
        public Vector2 Direction;      //This is the Direction the Particle is Moving in
        public float RotationAngle;    //The angle at twhich sprite facing
           
        public float Circle;        //Using this as a MathHelper
        public float Life;          //How long it will last before diying
        public Color FWCOLOUR;      //This will be used for deciding the firework colour :)
        
        public Particle(Texture2D Texture, Vector2 Pos, Vector2 Dir, int colour)
        {
            ParticleIMG = Texture;
            Position = Pos;
            Direction = Dir;
          
            RotationAngle = 0;
          
            Life = 1.0f;
            
            Circle = MathHelper.Pi * 2;

           
           
           switch (colour)
           {
               case 0:  FWCOLOUR = Color.Red;     break;
               case 1:  FWCOLOUR = Color.Orange;     break;
               case 2:  FWCOLOUR = Color.Yellow;     break;
               case 3:  FWCOLOUR = Color.LimeGreen;     break;
               case 4:  FWCOLOUR = Color.Aqua;     break;
               case 5: FWCOLOUR = Color.Violet; break;
               case 6: FWCOLOUR = Color.Pink; break;
           }
            
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

         
            
            Life -= deltaTime;
            


        }
   
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            spritebatch.Draw(ParticleIMG, new Rectangle((int)Position.X, (int)Position.Y, 2, 10), null, FWCOLOUR, RotationAngle, new Vector2(0, 0), SpriteEffects.None, 0);
            spritebatch.End();
        }
    }
}
