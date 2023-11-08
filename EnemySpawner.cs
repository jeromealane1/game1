using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WindowsFormsApplication1
{
    class EnemySpawner
    {//this class is the refactored portion of the json level loader for enemies
        int TEST_X;
        int TEST_Y;
        List<Enemy> enemies = new List<Enemy>();
        Timer spawnTimer;

        public EnemySpawner(int x, int y,GameTime gameTime = null)
        {
            TEST_X = x;
            TEST_Y = y;
            //enemies.Add(spawner(TEST_X,TEST_Y,5));
           // enemies.Add(spawner(TEST_X, TEST_Y+10, 6));
           // enemies.Add(spawner(TEST_X, TEST_Y+20, 7));

        }
        public EnemySpawner(int x, int y, List<Enemy> enemyList,GameTime gameTime = null)
        {
            TEST_X = x;
            TEST_Y = y;
            //enemies.Add(spawner(TEST_X, TEST_Y, 5));
            //enemies.Add(spawner(TEST_X, TEST_Y + 10, 6));
            //enemies.Add(spawner(TEST_X, TEST_Y + 20, 7));
            enemies = enemyList;

        }
        public EnemySpawner(List<Enemy> enemyList)
        {
            enemies = enemyList;
        }        
        
        public Enemy spawner(int x,int y,int spawnTime,string Type=null){
            return new Enemy(x, y); //spawn an enemy at a certain time type is null by  default to summon the default enemy 
        }
        public List<Enemy> getEnemies(){
            return enemies;
        }
    }
}
