using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class GameState
    {

        public GameStateType GameStateTypePropertie { get; set; }

        public enum GameStateType{
        menu,
        gameplay,
        gameover

        }
    }
}
