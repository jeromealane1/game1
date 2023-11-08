using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1;

namespace Game1
{
    class Level
    {
        string filePath;
        //JObject level;
        dynamic jsonLevel;
        //JProperty levelObjectProperties;
        EnemySpawner spawn;
        int enemyNum;
        Rootobject lev;
        RootEnemy enem;
        
 
        //Enemy enemy = new Enemy();
        List<Enemy> enemyList = new List<Enemy>();

        public Level(string path) {
            filePath = path;
            
            //level = readLevel(filePath);
           // jsonLevel = readLevelObject(filePath);
            
        }
        public string currentLevel()
        {
            //JProperty prop = level.Properties().FirstOrDefault(p => p.Name.Contains("level"));
           // var jsonString = File.ReadAllText(filePath);
            //lev = JsonConvert.DeserializeObject<Rootobject>(jsonString);

            Rootobject levelData = readLevel();
            string extractEnemy = levelData.level.enemies.enemielist.enemyType1[1].enemy.FirstOrDefault().ToString();
            string extractLevelNumber = levelData.level.levelnumber.ToString();
            RootEnemy[] extractBenemy = levelData.level.enemies.enemielist.enemyType1;

            //string enemList = JsonConvert.DeserializeObject<RootEnemy>(extractEnemy).enemy[0].ToString();
            //enem = JsonConvert.DeserializeObject<RootEnemy>(extractEnemy);
            foreach (RootEnemy enemyType in extractBenemy)
            {
                //spawn.spawner(enemyType[1]., enemyType[2], enemyType[3]);
                //return enemyType.enemy[1].ToString();
            }
            //lev = level;
            //string name = (string)level["level"]["enemies"]["enemielist"].Values().FirstOrDefault().Values().FirstOrDefault().ToString();
            // jsonLevel.level.enemies.enemielist.enemyType1.ToString();
            //enem.enemy[0].ToString();
            
            //string name = (string)level["level"]["enemies"]["enemielist"]["enemyType1"].Values()[0].FirstOrDefault().Root.ToString();
            //return enem.enemy.ToString();
            return extractLevelNumber;
           
        }
        public List<Enemy> getEnemies()
        {
            List<Enemy> enemies;
            Rootobject getEnemies = readLevel();
             RootEnemy[] extractEnemy = getEnemies.level.enemies.enemielist.enemyType1;
            foreach (var enemyType in extractEnemy)
            {
               //enemyList.Add(spawn.spawner(Convert.ToInt32(enemyType.enemy[1].ToString()),Convert.ToInt32(enemyType.enemy[2].ToString()), Convert.ToInt32(enemyType.enemy[3].ToString()),null));
               enemyList.Add(new Enemy(Convert.ToInt32(enemyType.enemy[1].ToString()), Convert.ToInt32(enemyType.enemy[2].ToString()),Convert.ToInt32(enemyType.enemy[3].ToString())));

            }
            //spawn.spawner()
            //string name = (string)level["level"]["enemies"]["enemielist"]["enemyType1"].Values()[1].ToString();
            return enemyList;
        }
        public Rootobject readLevel() 
        { 
             var jsonString = File.ReadAllText(filePath);
            lev = JsonConvert.DeserializeObject<Rootobject>(jsonString);
            return lev;
        }

        public Enemy spawnEnemy(int x, int y, int time){
            return this.spawn.spawner(x, y, time,null);
        }

    }
}
