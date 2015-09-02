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
    class FireWork : IDisposable
    {
        public Texture2D FWTexture;      //FireWork Texture
        public Vector2 FWPosition;       //The current Position 
        public Vector2 Direction;        //The Fireworks Movement Direction
        public Vector2 TargetPoint;
        
        public float RotationAngle;      //Angle Towards Sprite Moving Towards
        public float FrameCD;
        public float Range;

        public float Circle;             //Using this as a MathHelper

        public int Rows { get; set; }    //How Many Rows in the Animation
        public int Columns { get; set; } //How many Columns in the animation
        public int currentFrame;        //Current Frame Of the Animation Sequence
        public int totalFrames;         //Total amount of frames within animation



        //Playing with the forces of evil
        public Vector2 m_friction;
        public Vector2 m_velocity;
        public Vector2 m_force;
        public Vector2 m_acceleration;

        float m_entryTime;
        float m_mass;
        float m_damping;
        Vector2 MaxVeloxity;	



        public FireWork(Texture2D spritesheet, Vector2 Position, float EntryTime)
        {
            FWTexture = spritesheet;
            FWPosition = Position;
            RotationAngle = 0;
            Rows = 1;
            Columns = 5;
            totalFrames = Rows * Columns;

            Circle = MathHelper.Pi * 2;
            FrameCD = .25f;


            Range = 0;
            m_friction = new Vector2(0, 0);
            m_velocity = new Vector2(0, 0);
            m_force = new Vector2(0, 0);
            m_mass = 10.0f;
            m_damping = -2.5f;

            m_acceleration = new Vector2(0, 0);
            MaxVeloxity = new Vector2(2, 2);
            m_entryTime = EntryTime;

        }

        public FireWork(Texture2D spritesheet, Vector2 Position, Vector2 Target, float EntryTime)
        {
            FWTexture = spritesheet;
            FWPosition = Position;
            RotationAngle = 0;
            Rows = 1;
            Columns = 5;
            totalFrames = Rows * Columns;

            Circle = MathHelper.Pi * 2;
            FrameCD = .25f;

            TargetPoint = Target;
            Direction = FWPosition - Target;
            Direction.Normalize();

            RotationAngle = (float)Math.Atan2(-Direction.X, Direction.Y);
            RotationAngle = RotationAngle % Circle;


            Range = 0;
            m_friction = new Vector2(0, 0);
            m_velocity = new Vector2(0, 0);
            m_force    =  new Vector2(0, 0);
            m_mass = 10.0f;
            m_damping = 2.0f;
            m_entryTime = EntryTime;


            m_acceleration = new Vector2(0, 0);

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public virtual void Update(float deltaTime)
        {
          
            

            RotationAngle = (float)Math.Atan2(m_velocity.X, -m_velocity.Y);
            RotationAngle = RotationAngle % Circle;

            FrameCD -= deltaTime;

            if (FrameCD < 0)
            {
                currentFrame++;

                if (currentFrame == totalFrames)
                {
                    currentFrame = 0;
                }

                FrameCD = 0.25f;
            }
            
             //FWPosition -= Direction;
        }

        public void Seek(Vector2 TargetPos, float dt)
        {
            Range = (TargetPos - FWPosition).Length();

	    							
            Direction = TargetPos - FWPosition;
            Direction.Normalize();
	        Direction *= MaxVeloxity;
	        m_force = Direction;
	        m_friction = m_velocity * m_damping;
            m_acceleration = (m_force + m_friction ) * 1 / m_mass;
            m_velocity += m_acceleration * dt;
            FWPosition += m_velocity * 1.5f;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            int width = FWTexture.Width / Columns;
            int height = FWTexture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            
            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)FWPosition.X,(int)FWPosition.Y, width, height);

            spriteBatch.Begin();
            spriteBatch.Draw(FWTexture, destinationRectangle, sourceRectangle, Color.White,RotationAngle, new Vector2(0,0) ,SpriteEffects.None,0.0f);
            spriteBatch.End();
        }

    }
}
