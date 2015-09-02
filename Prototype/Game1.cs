using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Lidgren;

namespace FireWorkMahem
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D _BlockTexture;
        SpriteFont _SpriteFont;

        Player _player1;
        List<Player> _playerList;


        float DeltaTime = 0;
        int counter = 0;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 650;
            graphics.PreferredBackBufferWidth = 850;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            _playerList = new List<Player>();
            // TODO: Add your initialization logic here
            _playerList.Add(new Player(new Vector2(325, 425), Color.Red, 0, new Vector2(0, 1), _BlockTexture, _BlockTexture, _SpriteFont));
        
        }
        
        
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Initialise all content here
            _BlockTexture = Content.Load<Texture2D>("PlayerSprite.png");
            _SpriteFont = Content.Load<SpriteFont>("SpriteFont1");



        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

           for (int i = 0; i < _playerList.Count; i++)
           {
               for (int j = 0; j < _playerList.Count; j++)
               {
                  
          
               }

               _playerList[i].Update(DeltaTime);
           }
            // TODO: Add your update logic here
          

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

           // _player1.Draw(spriteBatch);


            for (int i = 0; i < _playerList.Count; i++)
            {
                _playerList[i].Draw(spriteBatch);
            }

            base.Draw(gameTime);
        }
    }
}
