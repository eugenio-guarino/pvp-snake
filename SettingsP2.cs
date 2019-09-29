using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    class SettingsP2 : Settings
    {
        public SettingsP2()
        {
            //this is the default settings function
            Width = 16; //set the width to 16
            Height = 16;   
            Speed = 20;
            Score = 0;
            Points = 100;
            GameOver = false; // set game over to false
            p2Directions = DirectionsP2.S; // the default direction will be down

        }
    }
    public enum DirectionsP2
    {
        A,
        D,
        W,
        S
    };
}
