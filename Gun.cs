using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1;

namespace Game1
{
    class Gun:Sprite
    {
        List<Bullet> bullets = new List<Bullet>();
        ContentManager mContentManager;
        public Gun(Sprite p, ContentManager c)
        {
            mContentManager = c;
            this.Position = p.Position;
        }
        public void ShootBullet()
        {

            bool aCreateNew = true;
            foreach (Bullet abullet in bullets)
            {
                if (abullet.Visible == false)
                {
                    aCreateNew = false;
                    //abullet.Fire(Position + new Vector2(Size.Width / 2, Size.Height / 2),
                      //  new Vector2(200, 0), new Vector2(1, 0));
                    abullet.Fire(Position + new Vector2(Size.Width / 2, Size.Height / 2),
                    new Vector2(200, 200), new Vector2( (float)Math.Cos(Position.X)*10, 2));
                    break;
                }
            }

            if (aCreateNew == true)
            {
                Bullet abullet = new Bullet();
                EntityManager.Add(abullet);
                abullet.LoadContent(mContentManager);
                //abullet.Position.X
                //abullet.Position.X = (float)Math.Cos(Position.X);
                //abullet.Position.Y = (float)Math.Sin(Position.Y);
                   
                //abullet.Fire(Position + new Vector2(Size.Width / 2, Size.Height / 2),
                    //new Vector2(200, 200), new Vector2( (float)Math.Cos(Position.X), 2));
                //Console.WriteLine("this is the current value of position x: "+Position.X);
                bullets.Add(abullet);//add a bullet to the bullets array list
            }

        }//end shoot

        // this method is for loading and shooting multiple bullets at the same time
        // we will start with five
        public void ShootMultipleBullets(String origin)
        {
            List<Bullet> bullets = new List<Bullet>();
            Bullet newBullet1;
            Bullet newBullet2;
            Bullet newBullet3;
            bool aCreateNew = true;

            for(int i =0;i<10;i++){
                Bullet newBullet = new Bullet(origin);
                bullets.Add(newBullet);
                //Console.Write("created new bullet");

            }//end foreach
            foreach (Bullet bullet in bullets) {//populate the entity manager and load all the bullets at once

                for (int i = -200; i > 200; i++)
                {
                    if (bullet.Visible == false)
                    {
                        aCreateNew = false;
                        //bullet.Fire(Position + new Vector2(Size.Width / 2, Size.Height / 2),
                        //new Vector2(200, 0), new Vector2(1, 0));

                        bullet.Fire(Position + new Vector2(Size.Width / 2, Size.Height / 2),
                        new Vector2(200, 200), new Vector2((float)Math.Cos(Position.X)*i + 200, 2));
                        break;
                    }//end if
                }//end for

                if (aCreateNew == true)
                {
                    EntityManager.Add(bullet);
                    bullet.LoadContent(mContentManager);
                    bullet.Fire(Position + new Vector2(Size.Width / 2, Size.Height / 2),
                        new Vector2(200, 200), new Vector2((float)Math.Cos(Position.X)*10 + 50, 2));
                }//end if
            }//end foreach
        }

       // public void Bullet spreadGun()
        //{
            //int x =Math.Cos();
            //int y =Math.Sin;

            //Bullet spread = new Bullet(Math.Sin(float(this.Position.X)), float(Math.Cos(this.Position.Y)));

            //return 0;    
            
        //};
        public override void Update(GameTime theGameTime)
        {

        }

    }


   
}
