using MonoGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using WindowsFormsApplication1;
using System.Collections.Generic;
using WindowsFormsApplication1.ParticleEffects;
using ShapeBlaster;
using Game1;

class playerSprite:Sprite
{


    const string SHIP_ASSETNAME = "SquareGuy";
    const int START_POSITION_X =400;
    const int START_POSITION_Y = 400;
    const int SHIP_SPEED = 160;
    const int MOVE_UP = -1;
    const int MOVE_DOWN = 1;
    const int MOVE_LEFT = -1;
    const int MOVE_RIGHT = 1;
    List<Bullet> bullets = new List<Bullet>();
    Animation shipanim;
    public string origin = "player";
    ContentManager mContentManager;

    enum State
    {
        moving,
        Shooting
    }

    State mCurrentState = State.moving;

    Vector2 mDirection = Vector2.Zero;
    Vector2 mSpeed = Vector2.Zero;

    KeyboardState mPreviousKeyboardState;
    public void LoadContent(ContentManager theContentManager)
    {
        mContentManager = theContentManager;
       
        shipanim = new Animation(mContentManager, "Art/Tyrian-Carrot-746836", 50f, 5, false);
      
        foreach (Bullet b in bullets)
        {
            b.LoadContent(theContentManager);
        }
 

        Position = new Vector2(START_POSITION_X, START_POSITION_Y);
        base.LoadContent(theContentManager, SHIP_ASSETNAME);
        //Console.Write("sprite made");
        //Source = new Rectangle(0, 0, 200, Source.Height);
    }

    private void UpdateMovement(KeyboardState aCurrentKeyboardState,GameTime theGameTime)
    {
         
        if (mCurrentState == State.moving)
        {
            mSpeed = Vector2.Zero;
            mDirection = Vector2.Zero;

            if (aCurrentKeyboardState.IsKeyDown(Keys.Left) == true)
            {
                mSpeed.X = SHIP_SPEED;
                mDirection.X = MOVE_LEFT;
                shipanim.PlayAnimation(theGameTime,true);//ship animation
               // Console.Write("moveleft ");
            }
            else if (aCurrentKeyboardState.IsKeyDown(Keys.Right) == true)
            {
                mSpeed.X = SHIP_SPEED;
                mDirection.X = MOVE_RIGHT;
                shipanim.PlayAnimation(theGameTime, false);//ship animation
                // Console.Write("moveright ");
            }
            else {
                shipanim.currentFrame = 2;
            }

            if (aCurrentKeyboardState.IsKeyDown(Keys.Up) == true)
            {
                mSpeed.Y = SHIP_SPEED;
                mDirection.Y = MOVE_UP;
            }
            else if (aCurrentKeyboardState.IsKeyDown(Keys.Down) == true)
            {
                mSpeed.Y = SHIP_SPEED;
                mDirection.Y = MOVE_DOWN;
            }
            if (aCurrentKeyboardState.IsKeyDown(Keys.Space) == true) 
            {
                Bullet fire = new Bullet();
                mCurrentState = State.Shooting;
                fire.x = this.Position.X;
                fire.x = this.Position.Y;
                //Console.Write("Fire!!! ");
            }
        }
    }
    private void UpdateFireball(GameTime theGameTime, KeyboardState aCurrentKeyboardState)
    {
        foreach (Bullet aBullet in bullets)
        {
            aBullet.Update(theGameTime);
        }

        if (aCurrentKeyboardState.IsKeyDown(Keys.RightControl) == true && mPreviousKeyboardState.IsKeyDown(Keys.RightControl) == false)
        {
            Gun g = new Gun(this, mContentManager);
            //g.ShootBullet();
            g.ShootMultipleBullets(origin);
            //ShootBullet();
        }
    }
        
    public void MakeParticle()
    { 
        for (int i = 0; i < 10; i++)
        {
            float speed = 1f;//* (1f - 1 / rand.NextFloat(1f, 10f));
            var state = new ParticleState()
            {
                //Velocity = rand.NextVector2(speed, speed),
                Velocity = new Vector2(speed, speed),
                Type = ParticleType.None,
                LengthMultiplier = 1f
            };


            Game1.Game1.ParticleManager1.CreateParticle(Art.LineParticle, this.Position, Color.Red, 190f, new Vector2(1.3f), state);
        }
    }

    private void ShootBullet()
    {
        
            bool aCreateNew = true;//create a new bullet
            foreach (Bullet abullet in bullets)
            {
                if (abullet.Visible == false)
                {
                    aCreateNew = false;
                    abullet.Fire(Position + new Vector2(Size.Width / 2, Size.Height / 2),
                        new Vector2(200, 0), new Vector2(1, 0));
                    break;
                }
            }

            if (aCreateNew == true)
            {
                //Gun spreadgun = new Gun(this, theContentManager);
                Bullet abullet = new Bullet();
                EntityManager.Add(abullet);
                abullet.LoadContent(mContentManager);
                abullet.Fire(Position + new Vector2(Size.Width / 2, Size.Height / 2),new Vector2(200, 200), new Vector2(1, 0));//fires a new bullet in at a certan speed and direction
                bullets.Add(abullet);
            }
        
    }
    public override void Update(GameTime theGameTime)
    {

        //Console.WriteLine(this.Position);
        //shipanim.PlayAnimation(theGameTime);//ship animation
        shipanim.PlayStillFrame(2);
       
        shipanim.position = this.Position;
        KeyboardState aCurrentKeyboardState = Keyboard.GetState();
        UpdateFireball(theGameTime, aCurrentKeyboardState);
        UpdateMovement(aCurrentKeyboardState, theGameTime);
        mPreviousKeyboardState = aCurrentKeyboardState;
        MakeParticle();
        base.Update(theGameTime, mSpeed, mDirection);

        
    }

    public override void Draw(SpriteBatch theSpriteBatch)
    {
        foreach (Bullet abullet in bullets)
        {
            abullet.Draw(theSpriteBatch);

        }
        shipanim.Draw(theSpriteBatch);
        //WindowsFormsApplication1.Program.MyGame.ParticleManager1.Draw(theSpriteBatch);
        base.Draw(theSpriteBatch);
    }
}


