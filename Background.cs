using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Background:Sprite
    {
        const int START_POSITION_X = 00;
        const int START_POSITION_Y = 00;
        string BACKGROUND_NAME = "startest.jpg";
        const int BACKGROUND_SPEED = 10;
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_LEFT = -1;
        const int MOVE_RIGHT = 1;
        Vector2 mDirection = Vector2.Zero;
        Vector2 mSpeed = Vector2.Zero;
        public Background(string name= null) {
            if (name != null)
            {
                BACKGROUND_NAME = name;
            }
        }
        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager theContentManager)
        {

   


            Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            base.LoadContent(theContentManager, BACKGROUND_NAME);
            //Console.Write("sprite made");
            //Source = new Rectangle(0, 0, 200, Source.Height);
        }

        private void UpdateMovement(GameTime theGameTime)
        {
          
                mSpeed = Vector2.Zero;
                mDirection = Vector2.Zero;
                mSpeed.Y = BACKGROUND_SPEED;
                mDirection.Y = MOVE_DOWN;  
            
            
        }
        public override void Update(GameTime theGameTime)
        {
            UpdateMovement(theGameTime);
            base.Update(theGameTime, mSpeed, mDirection);
        }

    }
    
}
