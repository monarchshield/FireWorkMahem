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
using Microsoft.Xna.Framework.Storage;

namespace FireWorkMahem
{
    public class Button
    {
        Texture2D ButtonTexture;
        Vector2 ButtonPosition;

        SpriteFont font;
        Color m_colour;
        
        
        bool Selected; 
        int ButtonWidth;
        int ButtonHeight;

        public Button(Texture2D _texture, Vector2 _position, int _height, int _width)
        {
           
            ButtonTexture = _texture;
            ButtonPosition = _position; //Sets the Button position
           
            ButtonWidth = _width;
            ButtonHeight = _height;
            m_colour = new Color(255,255,255,255);
            Selected = false;
        }


        public void Update(GameTime gameTime)
        {

            //Put Colour Change Function here
            if (Selected == true)
                m_colour = new Color(100, 100, 255, 255);
            
            else
                m_colour = new Color(255, 255, 255, 255);
            
        }

        public void Draw( SpriteBatch spritebatch)
        {
            spritebatch.Draw(ButtonTexture, new Rectangle((int)ButtonPosition.X - ButtonWidth/2 , (int)ButtonPosition.Y - ButtonHeight/2 , ButtonWidth, ButtonHeight), m_colour);
       
        }

        public void SelectedIsTrue(bool trueOrFalse)
        {
            Selected = trueOrFalse;
        }

        public Vector2 GetPosition()
        {
            return ButtonPosition;
        }
    }
}
