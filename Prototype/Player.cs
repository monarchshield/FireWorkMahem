using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace FireWorkMahem
{
    /// <summary>
    /// Creating a custom player class
    /// </summary>
  
    public class Player
    {
        Vector2 _direction; //The default direction the player is going in
        Color _colour; //The colour the player is
        
        Vector2 _position; //The player current position;
        Vector2 _previousPosition; //Every second the previous position will be stored as to spawn a block
        
        Texture2D _texture; //Just for Rendering out the sprite;
        Texture2D _blockTexture; //This is just a block texture for the player;
        SpriteFont _font; //the font the player will use
        
        bool _dead; //is the player dead or alive
        int _ID; //This is just a id of what the player number is
        int _width; //This is the width of the player
        int _height; //this is the height of the player

        List<Block> _blocks; //the player will have a list of blocks. so if the player dies the blocks will die with them :D
        float _delay = .50f; //the delay between spawning blocks
        float _timer = .50f; //a decrimental value between the blocks spawning
        

        public Player()
        {

        }

       public Player(Vector2 Position, Color Colour, int ID, Vector2 direction, Texture2D sprite, Texture2D blockTexture, SpriteFont font) 
        {
            _blockTexture = blockTexture;
            _ID = ID;
            _direction = direction;
            _position = Position;
            _previousPosition = Position;

            _colour = Colour;
            _dead = false;
            _texture = sprite;
            _font = font;

            _blocks = new List<Block>();

            _width = _texture.Width;
            _height = _texture.Height;
        }

        public void Update(float deltaTime)
        {
            _timer -= deltaTime;
            MovementKeys();
            SpawnBlock();

            _position += _direction;
        }

       public void Draw(SpriteBatch _spritebatch)
        {
            _spritebatch.Begin();
          
           if (_blocks != null)
            {
                foreach (var Block in _blocks)
                {
                    Block.Draw(_spritebatch);
                }
            }


           _spritebatch.Draw(_texture, new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height), _colour);
           _spritebatch.DrawString(_font, "P" + _ID.ToString(), _position, Color.White);
            _spritebatch.End();
        }

        void MovementKeys()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Console.WriteLine("Up Key is Pressed");
                _direction = new Vector2(0, -1);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Console.WriteLine("Left Key is Pressed");
                _direction = new Vector2(-1, 0);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Console.WriteLine("Down Key is Pressed");
                _direction = new Vector2(0, 1);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Console.WriteLine("Right Key is Pressed");
                _direction = new Vector2(1, 0);
            }
        }
        void SpawnBlock()
        {
            if(_timer < 0)
            {
                _blocks.Add(new Block(_blockTexture, _previousPosition, _colour, _ID));
                _timer = _delay;
                _previousPosition = _position;
            }
        }

       public Vector2 GetPosition() { return _position; }
       public int GetHeight() { return _height; }
       public int GetWidth() { return _width;  }


    }


}
