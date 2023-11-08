using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;





    public abstract class Sprite 
    {

        public string AssetName;
        public bool IsExpired;
        public string origin = "sprite";
        //The Size of the Sprite (with scale applied)
        public Rectangle Size;

        //The amount to increase/decrease the size of the original sprite. 
        private float mScale = 1.0f;
        public Vector2 Position = new Vector2(0, 0);
        public float Orientation;
        public float Radius = 20;   // used for circular collision detection
         

    //The texture object used when drawing the sprite
    private Texture2D mSpriteTexture;


    public float Scale
    {
        get { return mScale; }
        set
        {
            mScale = value;
            //Recalculate the Size of the Sprite with the new scale
            Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale));
        }
    }

    public void LoadContent(ContentManager theContentManager, string theAssetName)
    {
        
        mSpriteTexture = theContentManager.Load<Texture2D>(theAssetName);
        AssetName = theAssetName;
        Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale));
    }
    public abstract void Update(GameTime theGameTime);
    public void Update(GameTime theGameTime, Vector2 theSpeed, Vector2 theDirection)
    {
        Position += theDirection * theSpeed * (float)theGameTime.ElapsedGameTime.TotalSeconds;
    }


    public virtual void Draw(SpriteBatch theSpriteBatch)
    {
         theSpriteBatch.Draw(mSpriteTexture, Position,
                new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height),
                Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
    }
    
}
    

