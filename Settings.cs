using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    class Settings
    {
        public static int Width { get; set; } //set the width as int class
        public static int Height { get; set; } //set the height as int class
        public static int Speed { get; set; } //set the speed as int class
        public static int Score { get; set; } //set the score as int class
        public static int Points { get; set; } // set the points as int class
        public static bool GameOver { get; set; } //set the game over as boolean class
        public static DirectionsP1 P1Directions { get; set; } // set the direction as the class we mentioned above
        public static DirectionsP2 P2Directions { get; set; } // set the direction as the class we mentioned above
    }
}
