using System;
using MonoGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Globalization;
using Game1;

namespace WindowsFormsApplication1
{
    class Enemy:Sprite
    {
        const string SHIP_ASSETNAME = "Enemy";
        const int START_POSITION_X = 400;
        const int START_POSITION_Y = 400;
        const int HIT_POINTS = 1;
        const int SHIP_SPEED = 160;
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_LEFT = -1;
        const int MOVE_RIGHT = 1;
        int SPAWN_X;
        int SPAWN_Y;
        int SPAWN_TIME;
        public bool ALIVE;
        public string origin = "enemy";
        //public Gun;
        ContentManager mContentManager;
        Gun g = null;
        enum State
        {
            Moving
        }

        State mCurrentState = State.Moving;

        Vector2 mDirection = Vector2.Zero;
        Vector2 mSpeed = Vector2.Zero;
        public Enemy() { }
        public Enemy(int x, int y, string type = null)
        {
            SPAWN_X = x;
            SPAWN_Y = y;
            SPAWN_TIME = 5;//default test spawn times
        }
        public Enemy(int x, int y, int spawnTime,  string type = null)
        {
            SPAWN_X = x;
            SPAWN_Y = y;
            SPAWN_TIME = spawnTime;//default test spawn times
        }
        public void setSpawnTime(int spawn) {
            SPAWN_TIME = spawn;
        }

        
        public void LoadContent(ContentManager theContentManager)
        {
            mContentManager = theContentManager;
            g = new Gun(this, mContentManager);
            if (SPAWN_X == 0)
            {
                Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            }
            else {
                Position = new Vector2(SPAWN_X, SPAWN_Y);
            }
            base.LoadContent(theContentManager, SHIP_ASSETNAME);
            //Console.Write("sprite made ");
        }//end load content

        public void WasShot()
        {
            IsExpired = true;
            //Console.WriteLine("shot!!!");
        }
        private void UpdateMovement(GameTime theGameTime)
        {
            if (mCurrentState == State.Moving)
            {
                mSpeed = Vector2.Zero;
                mDirection = Vector2.Zero;
                mSpeed.X = SHIP_SPEED;
                mDirection.X = MOVE_LEFT;
               
                if (Convert.ToInt32(theGameTime.TotalGameTime.Seconds) % 1 == 0)//refactor to enemy behavoir class 
                {
                    mSpeed.X = SHIP_SPEED;
                    mDirection.X = MOVE_LEFT;
                }
                if(Convert.ToInt32(theGameTime.TotalGameTime.Seconds) % 2 == 0)
                {
                    mSpeed.X = SHIP_SPEED;
                    mDirection.X = MOVE_RIGHT;
                }
                mSpeed.Y = 10;
                mDirection.Y = MOVE_DOWN;               
            }
            /*
            const float PI = (float)Math.PI;
            int rad = 450;
            int radb = 450;
            float speedScale = 0;
            mSpeed.X = SHIP_SPEED;
            mDirection.X = MOVE_LEFT;
            //speedScale = Convert.ToDouble((0.001 * 2 * Math.PI) /  SHIP_SPEED+1);
            speedScale = (float)(0.001 * 2 * PI) / (float)(1.4999999999);
           // mSpeed.X = SHIP_SPEED;
            //mDirection.X = MOVE_LEFT;
            //if (mCurrentState == State.Moving) {
            var angle = theGameTime.TotalGameTime.Milliseconds*speedScale;
           
               // mSpeed.X = SHIP_SPEED;
              //  mDirection.X = this.mDirection.X + Convert.ToInt64(Math.Cos(Convert.ToInt32(theGameTime.TotalGameTime.Seconds)));
                this.Position.X += (float) (Math.Cos(angle) * rad);
                
                this.Position.Y += (float)(Math.Sin(angle) * radb);

                //Console.WriteLine("cos " + Math.Cos(Convert.ToInt32(theGameTime.TotalGameTime.Seconds)));
                //Console.WriteLine("angle "+angle);
                //Console.WriteLine("elapsed time" + theGameTime.ElapsedGameTime.Seconds);
            //}//*/// circle behavior
        }//update movement

        public void moveLeft(int x, int y, int speed) 
        { 
            
        }

        public override void Update(GameTime theGameTime)
        {
            if (GlobalVars.ELAPSED_TIME.TotalGameTime.TotalSeconds >= SPAWN_TIME)
            {
                ALIVE = true;
            }
            if (ALIVE == true && IsExpired == false)
            { 
                UpdateMovement(theGameTime);

          

                base.Update(theGameTime, mSpeed, mDirection);
            }
            if(GlobalVars.ELAPSED_TIME.TotalGameTime.TotalSeconds >=SPAWN_TIME+5){
                 // g = new Gun(this, mContentManager);
                //g.ShootBullet();
                //g.ShootMultipleBullets(origin);
            }
        }//end update

        public override void Draw(SpriteBatch theSpriteBatch)
        {
            if (ALIVE == true&& IsExpired == false)
            {
                base.Draw(theSpriteBatch);
            }
        }//end draw
    }
}
