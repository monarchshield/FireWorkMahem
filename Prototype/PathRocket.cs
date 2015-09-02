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
    class PathRocket : FireWork
    {
       

        public List<Vector2> TravelPoints;
        public float length;
        public float EntranceTime;

        public PathRocket(Texture2D spritesheet, Vector2 Position, List<Vector2> Points, float EntryTime)
            : base(spritesheet, Position, Points[0], EntryTime)
        {
            TravelPoints = Points;
            //Direction = FWPosition - TravelPoints[0];
            //Direction.Normalize();
          
        }

        public override void Update(float deltaTime)
        {

            if (TravelPoints.Count != 0)
            {
                Direction = FWPosition - TravelPoints[0];
                Direction.Normalize();

                length = (FWPosition - TravelPoints[0]).Length();

                if (length < 10)
                {
                    TravelPoints.RemoveAt(0);
                }

                if (TravelPoints.Count == 0)
                {

                }
            }


      

            RotationAngle = (float)Math.Atan2(-Direction.X, Direction.Y);
            RotationAngle = RotationAngle % this.Circle;
            
            FrameCD -= deltaTime;

            if(FrameCD < 0)
            {
                currentFrame++;

                if (currentFrame == totalFrames)
                {
                    currentFrame = 0;
                }

                FrameCD = 0.25f;
            }
            
            FWPosition -= Direction;
        }
               

    }
}
