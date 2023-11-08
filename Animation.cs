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

namespace WindowsFormsApplication1
{
    class Animation
    {
        public Vector2 position { get; set; }
        Texture2D animation;
        Rectangle sourceRect;
        //Vector2 position;
        float elapsed;
        float frameTime;
        int numOfFrames;
        public int currentFrame{get;set;}
        int width;
        int height;
        int frameWidth;
        int frameHeight;
        bool looping;
        public Animation(ContentManager Content,string asset,float frameSpeed,int numOfFrames,bool looping) 
        {
            this.frameTime = frameSpeed;
            this.numOfFrames = numOfFrames;
            this.looping = looping;
            this.animation = Content.Load<Texture2D>(asset);
            frameHeight = (animation.Height);
            frameWidth = (animation.Width /numOfFrames);
            //this.position = position;
            currentFrame = 2;
            //position = new Vector2(100, 100);
        }
        public void PlayAnimation(GameTime gameTime,bool reverse) 
        {
            
            elapsed +=(float) gameTime.ElapsedGameTime.TotalMilliseconds;
            sourceRect = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            if (elapsed > frameTime)
            {
                if (currentFrame >= numOfFrames - 1 )
                {
                    if (looping)
                        currentFrame = 0;

                }
                else
                {
                    if (reverse == true && currentFrame > 0)
                    {
                        currentFrame--;

                    }
                    else if(reverse==false)
                    {
                        currentFrame++;
                    }
                }
                elapsed = 0;
            }
        }
        public void PlayStillFrame(int frame)
        {
            sourceRect = new Rectangle(frame * frameWidth, 0, frameWidth, frameHeight);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(animation, position, sourceRect, Color.White, 0.0f, new Vector2(0, 0),1f, SpriteEffects.None, 1.0f);
        }

    }
}
