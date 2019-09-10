using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    class SnakeSegment
    {
        public int X { get; set; } //this is a public int class called X
        public int Y { get; set; } //this is a public int class called Y

        public SnakeSegment()
        {
            //this function is resetting the X and Y to 0
            X = 0;
            Y = 0;
        }
    }
}
