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
    public class MenuState : AIE.IGameState
    {
        Texture2D Background;
        Texture2D ButtonTexture;


        //Wont need these for now
        //-----------------------------
        Button SelectGame;
        Button SelectInstructions;
        Button SelectCredits;
        //------------------------------

        public MenuState() : base()
        {
           Load();

           SelectGame = new Button(ButtonTexture, new Vector2(470, 300), 150, 150);

        }

        public void Load()
        {
            ButtonTexture = Content.Load<Texture2D>("Button.png");
            Background = Content.Load<Texture2D>("FMWIDTH.png");
        }

        public override void OnPop()
        {
        }

        public override void OnPush()
        {
        }

        public override void Update(GameTime gameTime)
        {
             Point Mouseposition = Mouse.GetState().Position;

             SelectGame.SelectedIsTrue(false);

             if (Mouseposition.X >= 470 - 75 && Mouseposition.X <= 470 + 75  &&
                 Mouseposition.Y >= 300 - 15 && Mouseposition.Y <= 300 + 15)
             {
                 SelectGame.SelectedIsTrue(true);

                 if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                 {
                     AIE.GameStateManager.ChangeState("PLAYSTATE");
                 }
             }

             SelectGame.Update();
        }
     

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Background, new Rectangle(0, 0, 960, 640), Color.White);
            SelectGame.Draw(spriteBatch);
            spriteBatch.End();

            
        }
    }     
}
   