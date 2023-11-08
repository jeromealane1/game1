using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApplication1
{
    static class GlobalVars
    {
        public static GameTime ELAPSED_TIME;
        public static  int SCORE = 0;
        public static int LEVEL = 0;
        public static int HEALTH = 1;
        public static int LIVES = 1;
        public static int KILL_COUNT = 0;
        public static int PLAYFIELD_X = 0;
        public static int PLAYFIELD_Y = 0;
        public static int SCREEN_WIDTH;
        public static int SCREEN_HEIGHT;


        public static void incrementScore(int points)
        {
            SCORE += points;
        }
        public static void incrementLives() {
            LIVES += 1;
        }
        public static void decrementLives() {
            LIVES -= 1;
        }
        public static void incrementHealth() {
            HEALTH += 1;
        }
        public static void decrementHealth()
        {
            HEALTH -= 1;
        }
        public static void incrementKillCount()
        {
            KILL_COUNT += 1;
        }
        //public static var screenCenter = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2);
     
    }
}
