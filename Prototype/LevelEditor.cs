using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading.Tasks;





namespace FireWorkMahem
{
    class LevelEditor
    {
        public enum RocketType
        {
            RegularRocket,
            PathRocket,
            BossRocket,
            None,
        }


        private SpriteFont font;
        private string stringValue;
        private bool Nullstring;

        private List<FireWork> RocketList;
        private Texture2D Image;
        private Texture2D RegFirework;
        private Texture2D FPRocket;
        private Texture2D Selector;
        private Texture2D DotTexture;
       

        private Vector2 Position;
        private Vector2 MousePosition;
        private Vector2 RocketPos;
        private Vector2 SingularPoint;
        private Vector2 SetTargetPoint;
        
        private bool RocketSetted;
        private bool RocketTypeSelected;
        
        private List<Vector2> Points;
        
        
        private int width;
        private int height;
        private int box;
        private int SlotSize;
        private int NumOfRockets;

        public RocketType Selected;
        private float Delay;
        private float Leftquad;
        private float EntryTime;


        LevelEditor() { }

        public LevelEditor(Texture2D GUImage, Texture2D Select, Texture2D dot,Vector2 Pos, int Totaltypes, Texture2D RegFW, Texture2D FPFW, SpriteFont Text)
        {



            font = Text;
            DotTexture = dot;
            Selector = Select;
            Image = GUImage;
            Position = Pos;
            NumOfRockets = Totaltypes - 1;
            RocketSetted = false;
            Selected = RocketType.None;
            Points = new List<Vector2>();
            RocketPos = new Vector2(0, 0);
            SingularPoint = new Vector2(0, 0);
            SetTargetPoint = new Vector2(0, 0);

            Leftquad =  (float)Image.Width / 2;
            width = Image.Width / 2;
            height = Image.Height / 2;
            box = 0;
            SlotSize = Image.Width / NumOfRockets;
            EntryTime = 0;


            stringValue = string.Empty;

            RegFirework = RegFW;
            FPRocket = FPFW;

            RocketList = new List<FireWork>();

            RocketTypeSelected = false;
            Nullstring = true;


            Delay = 1.0f;
        }

        public void Update(float deltatime, Vector2 MousePos, float a_entryTime)
        {

            EntryTime = a_entryTime;

            MousePosition = MousePos;

            SelectRocket();
            TriggerDelay(deltatime);
            SetRocket();
            SetSettings();



        }
        
        //Just adding a delay after the initial selection so that it doesnt 
        //Spam put down the object when you choose it
        void TriggerDelay(float deltatime)
        {
            if (RocketTypeSelected)
            {
                Delay -= deltatime;

                //if (Delay < 0)
                //    Delay = 1.0f;
            }

           
        }

        //Select what rocket type you want 
        void SelectRocket()
        {
            if (!RocketTypeSelected)
            {
                if (Mouse.GetState().RightButton == ButtonState.Pressed)
                {
                    if (MousePosition.X <= Position.X + Image.Width && MousePosition.X >= Position.X
                        && MousePosition.Y <= Position.Y + Image.Height && MousePosition.Y >= Position.Y)
                    {

                        box = ((int)MousePosition.X - (int)Leftquad) / (SlotSize - 10);

                        if (box > NumOfRockets)
                            box = NumOfRockets;
                        
                        switch (box)
                        {
                            case 0: Selected = RocketType.RegularRocket; break;
                            case 1: Selected = RocketType.PathRocket; break;
                            case 2: Selected = RocketType.BossRocket; break;
                            case 3: Selected = RocketType.None; break;
                            case 4: Selected = RocketType.None; break;
                            case 5: Selected = RocketType.None; break;
                            case 6: Selected = RocketType.None; break;
                        }
                       
                        if (Selected != RocketType.None) 
                        RocketTypeSelected = true;
                    }
                }
            }
        }

        void SetRocket()
        {
            
            KeyboardState state = Keyboard.GetState();
              
            if (state.IsKeyDown(Keys.D1))
            {
                RocketTypeSelected = false;
            }

            if (state.IsKeyDown(Keys.D2))
            {
                Points.Clear();
            }

            if (RocketTypeSelected)
            {
                if (Selected == RocketType.RegularRocket && Mouse.GetState().LeftButton == ButtonState.Pressed && Delay < 0)
                {
                    RocketList.Add(new FireWork(RegFirework, MousePosition,SetTargetPoint, EntryTime));
                    Delay = 1.0f;
                    //RocketTypeSelected = false;
                }

                if (Selected == RocketType.RegularRocket && Mouse.GetState().MiddleButton == ButtonState.Pressed)
                {
                    SetTargetPoint = MousePosition;
                    Delay = 1.0f;
                }

                if (Selected == RocketType.PathRocket && Mouse.GetState().MiddleButton == ButtonState.Pressed && Delay < 0)
                {

                    SetTargetPoint = MousePosition;
                    Points.Add(SetTargetPoint);
                    Delay = .50f;
                }

                if (Selected == RocketType.PathRocket && Mouse.GetState().LeftButton == ButtonState.Pressed && Delay < 0)
                {
                    RocketList.Add(new PathRocket(FPRocket, MousePosition, Points, EntryTime));
                    Delay = 1.0f;
                }
            }
        }

        void SetSettings()
        {
            var keyboardState = Keyboard.GetState();
            var keys = keyboardState.GetPressedKeys();




            if (keys.Length > 0 && Delay <= 0)
            {
                var keyValue = keys[0].ToString();

                switch (keyValue)
                {
                    case "End": keyValue = "1"; break;
                    case "Down": keyValue = "2"; break;
                    case "PageDown": keyValue = "3"; break;
                    case "Left": keyValue = "4"; break;
                    case "OemClear": keyValue = "5"; break;
                    case "Right": keyValue = "6"; break;
                    case "Home": keyValue = "7"; break;
                    case "Up": keyValue = "8"; break;
                    case "PageUp": keyValue = "9"; break;
                    case "Insert": keyValue = "0"; break;
                    //case "d2:" KeyValue = "2"; break;
                    //case "d3": KeyValue = "3"; break;

                    default:
                        break;
                }


                if (stringValue.Length != 3)
                {
                    stringValue += keyValue;
                    Nullstring = true;
                }


                if (Keyboard.GetState().IsKeyDown(Keys.Back))
                {
                    stringValue = stringValue.Remove(0, stringValue.Length);
                    Nullstring = false;
                }
                Delay = .20f;
                
               // int m;
               // 
               //
               // if (stringValue.Length != 0) 
               //   m = Int32.Parse(stringValue);

            }
     
        }


        public void Draw(SpriteBatch spritebatch)
        {
            
            
            SpriteBatchExtended sbe = spritebatch as SpriteBatchExtended;

            

            //Drawing a debug line

           spritebatch.Begin();
           //Drawing a debug line for Directional Stuff

            if(Selected == RocketType.RegularRocket)
           sbe.DrawLine(MousePosition, SetTargetPoint, Color.Red);

            if (Selected == RocketType.PathRocket && Points.Count > 0)
                sbe.DrawLine(MousePosition, Points[0], Color.Red);

           spritebatch.Draw(Selector, new Rectangle((int)Position.X + (SlotSize - 11) * box, (int)Position.Y, Selector.Width, Selector.Height), Color.White);
           spritebatch.Draw(Image, new Rectangle((int)Position.X,(int)Position.Y, Image.Width, Image.Height), Color.White);
           spritebatch.Draw(RegFirework, new Rectangle((int)MousePosition.X , (int) MousePosition.Y - RegFirework.Height/2, 20, 43), new Rectangle(0, 0, 20, 43), Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0.0f);
           spritebatch.Draw(DotTexture, new Rectangle((int)SetTargetPoint.X - DotTexture.Width / 4, (int)SetTargetPoint.Y - DotTexture.Height / 4, DotTexture.Width /2, DotTexture.Height/ 2), Color.White);



           spritebatch.DrawString(font, "Entry Time: " + EntryTime.ToString(), new Vector2(0, 0), Color.White);

           //Drawing a path debug line
            int i = 0;

           foreach (Vector2 point in Points)
           {

               spritebatch.Draw(DotTexture, new Rectangle((int)point.X - DotTexture.Width / 4, (int)point.Y - DotTexture.Height / 4, DotTexture.Width / 2, DotTexture.Height / 2), Color.Red);
               
               if(i != 0)
               sbe.DrawLine(point, Points[i-1], Color.White, 3.0f);
               

               i++;
           }



           spritebatch.End();
            foreach (FireWork rocket in RocketList)
            {
                rocket.Draw(spritebatch);
                spritebatch.Begin();
                sbe.DrawLine(rocket.FWPosition, rocket.TargetPoint, Color.White, 1.0f);
                spritebatch.End();
            }



        }
        
    }
}
