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
    /// Creating a block class. this will be spawned by a player periodically
    /// to construct walls that would destroy one who touches them
    /// </summary>
    public class Block
    {
        Texture2D _Texture;   //The image the object is using
        Vector2 _Position;    //The static position of the object is obviously
        Color _Colour;        //The colour the object will render
        int _ID;              //The ID of the player who spawned this object in

        int _width;            //The width of the player texture
        int _height;           //The height of the player texture   

        Block() { }
        
        public Block(Texture2D texture, Vector2 pos, Color colour, int id)
        {
            _Texture = texture;
            _Position = pos;
            _Colour = colour;
            _ID = id;
        }

        public void Update(Player _player)
        {
            //AABB Intersections 
            
            

        }

        public void Draw(SpriteBatch _spritebatch)
        {
           // _spritebatch.Begin();
            _spritebatch.Draw(_Texture, new Rectangle((int)_Position.X, (int)_Position.Y, _Texture.Height, _Texture.Width),_Colour);
            //_spritebatch.End();
        }

    }
}
