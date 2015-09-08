using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        private bool HideLayers;

        private List<FireWork> RocketList;
        private Texture2D Image;
        private Texture2D RegFirework;
        private Texture2D FPRocket;
        private Texture2D Selector;
        private Texture2D DotTexture;
        private Texture2D ButtonTexture;

        private Vector2 Position;
        private Vector2 MousePosition;
        private Vector2 RocketPos;
        private Vector2 SingularPoint;
        private Vector2 SetTargetPoint;
        
        private bool RocketSetted;
        private bool RocketTypeSelected;
        
        private List<Vector2> Points;

        private Button HideLayerButton;
        private Button SaveLevelButton;
        
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

        public LevelEditor(Texture2D GUImage, Texture2D Select, Texture2D dot,Vector2 Pos, int Totaltypes, Texture2D RegFW, Texture2D FPFW, SpriteFont Text, Texture2D button)
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

            ButtonTexture = button;

            HideLayerButton = new Button(ButtonTexture, new Vector2(70, 40), 150, 150);
            SaveLevelButton = new Button(ButtonTexture, new Vector2(70, 80), 150, 150);

            Delay = 1.0f;
        }

        public void Update(float deltatime, Vector2 MousePos)
        {

            //EntryTime = a_entryTime;

            MousePosition = MousePos;
            ShowLayers();

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

            }
        }

        //This button hides all the other rockets on a particular layer so that you can see where
        //Everything will be in a second of duration
        void ShowLayers()
        {

            HideLayerButton.Update();

            if (HideLayerButton.BeenClicked() && Delay < 0)
            {
                HideLayers = !HideLayers;
                Delay = 1.0f;
            }
        }

        void SaveLevel()
        {
            SaveLevelButton.Update();

            if (HideLayerButton.BeenClicked() && Delay < 0)
            {
                //TODO: Add Saving class code here and save out your level in xml format!

                Delay = 1.0f;
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
              
            if (state.IsKeyDown(Keys.X))
            {
                RocketTypeSelected = false;
            }

            if (state.IsKeyDown(Keys.Z))
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

            bool correctNum = true;


            if (keys.Length > 0 && Delay <= 0)
            {
               
                string keyValue = keys[0].ToString();

                switch (keyValue)
                {

                    //Regular Keyvalue stuff
                    case "D1": keyValue = "1"; break;
                    case "D2": keyValue = "2"; break;
                    case "D3": keyValue = "3"; break;
                    case "D4": keyValue = "4"; break;
                    case "D5": keyValue = "5"; break;
                    case "D6": keyValue = "6"; break;
                    case "D7": keyValue = "7"; break;
                    case "D8": keyValue = "8"; break;
                    case "D9": keyValue = "9"; break;
                    case "D0": keyValue = "0"; break;

                    //Just In case stuff perhaps?
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

                    //TrippleyIncase?! :I
                    case "NumPad1": keyValue = "1"; break;
                    case "NumPad2": keyValue = "2"; break;
                    case "NumPad3": keyValue = "3"; break;
                    case "NumPad4": keyValue = "4"; break;
                    case "NumPad5": keyValue = "5"; break;
                    case "NumPad6": keyValue = "6"; break;
                    case "NumPad7": keyValue = "7"; break;
                    case "NumPad8": keyValue = "8"; break;
                    case "NumPad9": keyValue = "9"; break;
                    case "NumPad0": keyValue = "0"; break;

                    default:
                        correctNum = false;
                        break;
                }


                if (stringValue.Length != 3 && correctNum)
                {
                    stringValue += keyValue;
                    Nullstring = true;

                    EntryTime = Int32.Parse(stringValue);
                }


                if (Keyboard.GetState().IsKeyDown(Keys.Back) || Keyboard.GetState().IsKeyDown(Keys.C))
                {
                    stringValue = stringValue.Remove(0, stringValue.Length);
                    Nullstring = false;
                }
                Delay = .30f;
                

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

           spritebatch.DrawString(font, "Entry Time: " + stringValue.ToString(), new Vector2(0, 0), Color.White);
          
           HideLayerButton.Draw(spritebatch);
           spritebatch.DrawString(font, "Hide Layers", new Vector2(10, 25), Color.Red);

           SaveLevelButton.Draw(spritebatch);
           spritebatch.DrawString(font, "Save Level", new Vector2(10, 65), Color.Red);

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

                if (HideLayers == false)
                {
                    rocket.Draw(spritebatch);

                    spritebatch.Begin();
                    sbe.DrawLine(rocket.FWPosition, rocket.TargetPoint, Color.White, 1.0f);
                    spritebatch.End();


                }
                else
                {
                    if (rocket.m_entryTime == EntryTime)
                    {
                        rocket.Draw(spritebatch);

                        spritebatch.Begin();
                        sbe.DrawLine(rocket.FWPosition, rocket.TargetPoint, Color.White, 1.0f);
                        spritebatch.End();
                    }
                }
            }



        }
        
    }
}
