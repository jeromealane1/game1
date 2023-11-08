using System;
using MonoGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace WindowsFormsApplication1
{
    class Bullet:Sprite
    {
        const string SHIP_ASSETNAME = "Bullet";//this is how you change assets. these are loaded by name from the content folder.
        const int MAX_DISTANCE = 1000;
        const int BULLET_SPEED = 160;
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_LEFT = -1;
        const int MOVE_RIGHT = 1;
        public float x{get;set;}
        public bool Visible = true;
        public float y{get;set;}
        public Texture2D DrawTexture { get; set; }
       
        public Vector2 Direction { get; set; }
        public float Speed { get; set; }
        public int ActiveTime { get; set; }
        public int TotalActiveTime { get; set; }
        public string origin;
        
        Vector2 mStartPosition;
       
        enum State
        {
            Forward,//shoots forward
            Spread,//shoots spread
            Backshot//shoots backward

        }
        public Bullet()
        {
           
        }
        public Bullet(String origin) {
            this.origin = origin;
            //Console.Write("origin: "+ origin);
        }
        public Bullet(float x, float y) 
        {
            this.x = x;
            this.y = y;

        }
        public Bullet(Texture2D texture, Vector2 position, Vector2 direction, float speed, int activeTime)
        {           
            this.DrawTexture = texture;
            this.Position = position;
            this.Direction = direction;
            this.Speed = speed;
            this.ActiveTime = activeTime;
            this.TotalActiveTime = 0;
        }
        State mCurrentState = State.Forward;
        //State mCurrentState = State.Spread;

        Vector2 mDirection = Vector2.Zero;
        Vector2 mSpeed = Vector2.Zero;

        
        public void LoadContent(ContentManager theContentManager)
        {
            Position = new Vector2(this.x, this.y);
            //base.LoadContent(theContentManager, SHIP_ASSETNAME);
            //this.DrawTexture = 
            base.LoadContent(theContentManager, SHIP_ASSETNAME);
            Scale = 1.3f;
            //Console.Write("Bullet made ");
        }


        private void UpdateMovement(GameTime theGameTime)
        {
            mCurrentState = State.Forward;
            if (mCurrentState == State.Forward)//if the bullet state is to move forwrad(y value) then continue to update it
            {
                mSpeed = Vector2.Zero;
                mDirection = Vector2.Zero;
                mSpeed.Y = BULLET_SPEED;
                mDirection.Y = MOVE_UP;       

            }
            else if (mCurrentState == State.Backshot)
            {
                mSpeed = Vector2.Zero;
                mDirection = Vector2.Zero;
                mSpeed.Y = BULLET_SPEED;
                mDirection.Y = (float)Math.Cos(MOVE_UP);
            }
            else if (mCurrentState == State.Spread)
            {
                mSpeed = Vector2.Zero;
                mDirection = Vector2.Zero;
                mSpeed.Y = BULLET_SPEED;
                mDirection.Y = (float)Math.Sin(MOVE_UP);
                mDirection.X = (float)Math.Cos(MOVE_RIGHT)*200;
            }

        }//end update movement

        public void Fire(Vector2 theStartPosition, Vector2 theSpeed, Vector2 theDirection)
        {
            Position = theStartPosition;
            mStartPosition = theStartPosition;
            mSpeed = theSpeed;
            mDirection = theDirection;
            Visible = true;
           // Console.WriteLine("fired");

        }//end fire

        public override void Update(GameTime theGameTime)
        {
            
            if (Vector2.Distance(mStartPosition, Position) > MAX_DISTANCE)
            {
                Visible = false;
                IsExpired = true;
            }

            if (Visible == true)
            {
                base.Update(theGameTime, mSpeed, mDirection);

                //Console.WriteLine(this.Position);
            }

            UpdateMovement(theGameTime);
            base.Update(theGameTime, mSpeed, mDirection);
        }//end update

        public override void Draw(SpriteBatch theSpriteBatch)
        {
            if (Visible == true)
            {
                base.Draw(theSpriteBatch);
            }
        }//end draw
    }
}
