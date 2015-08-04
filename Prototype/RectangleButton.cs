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
    class RectangleButton : Button
    {

        public RectangleButton(Texture2D spritesheet, Vector2 Position, int height, int width)
            : base(_texture,_position,_height,_width)
        {

        }

    }
}
