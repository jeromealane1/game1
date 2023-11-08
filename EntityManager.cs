using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    static class EntityManager
    {
        static List<Sprite> entities = new List<Sprite>();
        static List<Enemy> enemies = new List<Enemy>();
        static List<Bullet> bullets = new List<Bullet>();

        static bool isUpdating;
        static List<Sprite> addedEntities = new List<Sprite>();

        public static int Count { get { return entities.Count; } }

        public static void Add(Sprite entity)
        {
            if (!isUpdating)
                entities.Add(entity);
            else
                addedEntities.Add(entity);
        }
        private static void AddEntity(Sprite entity)
        {
            entities.Add(entity);
            if (entity is Bullet)
                bullets.Add(entity as Bullet);
            else if (entity is Enemy)
                enemies.Add(entity as Enemy);
        }
        private static void AddEntity(List<Object> entityList)
        {
            if (entityList.GetType is List<Bullet>) {
                bullets = entityList.Select(s => (Bullet)s).ToList();
            }
        }//end add entity

        private static bool IsColliding(Sprite a, Sprite b)
        {
            //Console.Write("debug bullet collided"); check to see if bullets are colliding with each other 
            float radius = a.Radius + b.Radius;//remember to refactor these if statements out
            if(a is Bullet && b is Bullet){
                return false;
            }else if(a is Enemy && b is Enemy){//remember to refactor these if statements out. this check belongs in the handlecolissions method
                return false;
            }
            else if (a.origin == b.origin) {

                //return false;
            }
            return !a.IsExpired && !b.IsExpired && Vector2.DistanceSquared(a.Position, b.Position) < radius * radius;
        }

        static void HandleCollisions(List<Sprite> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                for (int j = i + 1; j < entities.Count; j++)
                {//looping through all elements in entities list
                    //if (IsColliding(entities[i], entities[j]))
                    //if (IsColliding(entities[i], entities[j]) && entities[i] is playerSprite == false && entities[j] is Bullet == false)                 
                    if (IsColliding(entities[i], entities[j]) && entities[i] is playerSprite == false && entities[j] is Enemy == false)
                    {
                        //if (entities[i] is  Bullet==true && entities[j] is Bullet==false)
                        //{
                            //entities[i].WasShot();
                            Console.WriteLine("expired");
                            entities[i].IsExpired = true;
                            GlobalVars.incrementScore(5);
                            GlobalVars.incrementKillCount(); 
                        //}//end if
                        // bullets[j].IsExpired = true;
                    }//end if
                }// end for loop
            }//end for
        }//end HandleCollisions

        public static void Update(GameTime theGameTime)
        {
            isUpdating = true;

            foreach (var entity in entities)
            {
                entity.Update(theGameTime);

                if (entity.Position.X > GlobalVars.SCREEN_WIDTH) {
                    entity.Position.X = GlobalVars.SCREEN_WIDTH-entity.Size.Width-10;//finishe all the cases
                }
            }
            

            isUpdating = false;

            foreach (var entity in addedEntities)
                entities.Add(entity);

            HandleCollisions(entities);
            addedEntities.Clear();
            
            // remove any expired entities.
            entities = entities.Where(x => !x.IsExpired).ToList();
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var entity in entities)               
                entity.Draw(spriteBatch);
           
        }
    }
}
