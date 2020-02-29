using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    class Snake
    {
        public int Width { get; set; } //set the width as int class
        public int Height { get; set; } //set the height as int class
        public int Speed { get; set; } //set the speed as int class
        public int Score { get; set; } //set the score as int class
        public int HP { get; set; } // set the points as int class
        public bool GameOver { get; set; } //set the game over as boolean class
             
        public Snake(int width, int height, int speed, int score, int hp, bool gameOver)
        {
            Width = width;
            Height = height;
            Speed = speed;
            Score = score;
            HP = hp;
            GameOver = false;
        }
    }
}
