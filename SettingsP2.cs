﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public enum DirectionsP2
    {
        //this is a enum class called Directions
        // we are using enum because its easier to classify the directions
        // for this game
        A,
        D,
        W,
        S

    };
    class SettingsP2
    {
        public static int Width { get; set; } //set the width as int class
        public static int Height { get; set; } //set the height as int class
        public static int Speed { get; set; } //set the speed as int class
        public static int Score { get; set; } //set the score as int class
        public static int Points { get; set; } // set the points as int class
        public static bool GameOver { get; set; } //set the game over as boolean class
        public static DirectionsP2 direction { get; set; } // set the direction as the class we mentioned above

        public SettingsP2()
        {
            //this is the default settings function
            Width = 16; //set the width to 16
            Height = 16;   
            Speed = 20;
            Score = 0;
            Points = 100;
            GameOver = false; // set game over to false
            direction = DirectionsP2.S; // the default direction will be down

        }
    }
}