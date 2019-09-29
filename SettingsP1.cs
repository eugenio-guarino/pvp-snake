using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    class SettingsP1 : Settings
    {
        public SettingsP1()
        {
            Width = 16; //set the width to 16
            Height = 16;   
            Speed = 20;
            Score = 0;
            Points = 100;
            GameOver = false; // set game over to false
            p1Directions = DirectionsP1.Down; // the default direction will be down
        }
    }

    public enum DirectionsP1
    {
        Left,
        Right,
        Up,
        Down
    };
}
