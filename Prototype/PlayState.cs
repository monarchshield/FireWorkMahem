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
   public class PlayState : AIE.IGameState
    {
       //Float Variables
       float DeltaTime;
       float Cooldown;

       //Just a spritefont so that i can render out text :)
       SpriteFont font;


        //Use this for the main game duh :P 
       Texture2D Background;
       Texture2D RedRocketSpriteSheet;
       Texture2D BlueRocketSpriteSheet;
       Texture2D ParticleTexture;
       Texture2D SelectBar;
       Texture2D ChosenRocket;
       Texture2D DotTexture;

       //Mouse Position Vectors
       Point MousePosition; 
       Vector2 MousePos;

       //Making a Test Rocket
       FireWork TestRocket;
       PathRocket Rocket2;

       //Making a level Editor Object
       LevelEditor Editor;

       //Creating a List of Particles
       List<Particle> Particles;
       List<Vector2> Points;


       Random ColourRand;
       int SparkColour;

        public PlayState()
            : base()
        {
            Load();
            Init();


            Editor = new LevelEditor(SelectBar, ChosenRocket, DotTexture, new Vector2(250, 500), 7, RedRocketSpriteSheet, BlueRocketSpriteSheet,font);

            Points.Add(new Vector2(100, 50));
            Points.Add(new Vector2(200, 300));
            Points.Add(new Vector2(300, 150));
            Points.Add(new Vector2(150, 75));
            Points.Add(new Vector2(125, 200));
            //
            TestRocket = new FireWork(RedRocketSpriteSheet, new Vector2(0, 0));
            //Rocket2 = new PathRocket(RedRocketSpriteSheet, new Vector2(300, 300), Points);
           
        }

       public void Load()
        {


            //Initialise All Your Textures Here :D
            Background = Content.Load<Texture2D>("FMWIDTH.png");
            RedRocketSpriteSheet = Content.Load<Texture2D>("FireWorkSpriteSheet.png");
            BlueRocketSpriteSheet = Content.Load<Texture2D>("BlueFWSpritesheet.png");
            ParticleTexture = Content.Load<Texture2D>("Particle.png");
            SelectBar = Content.Load<Texture2D>("SelectionBar2.png");
            ChosenRocket = Content.Load<Texture2D>("Select.png");
            DotTexture = Content.Load<Texture2D>("Dot.png");
            font = Content.Load<SpriteFont>("SpriteFont1");
        }
       public void Init()
        {
            //Initialise List and Variables here

            //Int
            SparkColour = 0;

            //Float
            DeltaTime = 0;
            Cooldown = .25f;

            //Vectors

            //Lists
            Particles = new List<Particle>();
            Points = new List<Vector2>();

            //Misc, Other
            ColourRand = new Random();
        }


       public override void OnPop()
        {
        }
       public override void OnPush()
        {
        }
       public override void Update(GameTime gameTime)
        {
            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Cooldown -= DeltaTime;

           SparkColour = ColourRand.Next(0, 6);

            MousePosition = Mouse.GetState().Position;
            MousePos = new Vector2(MousePosition.X, MousePosition.Y);

            TestRocket.Update(DeltaTime);
            TestRocket.Seek(MousePos, DeltaTime);

            //Rocket2.Update(DeltaTime);

            Editor.Update(DeltaTime, MousePos);


            if (Mouse.GetState().MiddleButton == ButtonState.Pressed && Cooldown < 0)
            {
                CrossHeirFireWork();
                //StarFireWork();

                Cooldown = .25f;
            }

            UpdateParticles();
        
        }
       public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Background, new Rectangle(0, 0, 960, 640), Color.White);
            spriteBatch.End();

          

            foreach (Particle obj in Particles)
            {
                obj.Draw(spriteBatch);
            }

            //Rocket2.Draw(spriteBatch);

            Editor.Draw(spriteBatch);
           

            TestRocket.Draw(spriteBatch);
           // Rocket2.Draw(spriteBatch);
        }
       
       public void CrossHeirFireWork()
        {
            Particles.Add(new Particle(ParticleTexture, MousePos, new Vector2(-1, -1),SparkColour));
            Particles.Add(new Particle(ParticleTexture, MousePos, new Vector2(-1, 1), SparkColour));
            Particles.Add(new Particle(ParticleTexture, MousePos, new Vector2(1, -1), SparkColour));
            Particles.Add(new Particle(ParticleTexture, MousePos, new Vector2(1, 1),  SparkColour));

            Particles.Add(new Particle(ParticleTexture, MousePos, new Vector2(0, -1), SparkColour));
            Particles.Add(new Particle(ParticleTexture, MousePos, new Vector2(0, 1),  SparkColour));
            Particles.Add(new Particle(ParticleTexture, MousePos, new Vector2(-1, 0), SparkColour));
            Particles.Add(new Particle(ParticleTexture, MousePos, new Vector2(1, 0),  SparkColour));
            
        }
       public void StarFireWork()
       {
           Particles.Add(new Particle(ParticleTexture, MousePos, new Vector2( 0, -1),       0));
           Particles.Add(new Particle(ParticleTexture, MousePos, new Vector2( 1,   .65f),   0));
           Particles.Add(new Particle(ParticleTexture, MousePos, new Vector2( 1,  -.65f),   0));
           Particles.Add(new Particle(ParticleTexture, MousePos, new Vector2(-1, -.65f),    0));
           Particles.Add(new Particle(ParticleTexture, MousePos, new Vector2(-1,  .65f),    0));
       }
       public void UpdateParticles()
       {
           foreach (var obj in Particles)
           {
               obj.Update(DeltaTime);
           }

           DeleteParticles();
       }
       public void DeleteParticles()
       {
           
            foreach (var obj in Particles)
            {
                if (obj.Life < 0)
                {
            
                    obj.Dispose();
                    Particles.Remove(obj);
                    break;
                }
            }

       }
    }
}
