using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections; //the hashtable class is in the collections
using System.Windows.Forms; //the keys class is in the forms
namespace SnakeGame
{
    class Input
    {
        private static Hashtable keyTable = new Hashtable();
        //we are creating a new istancce of Hashtable class
        //this class is used to optimise the keys inserted in it

        public static bool KeyPress(Keys key)
        {
            //this function will return a key back to the class
            
            if (keyTable[key] == null)
            {
                //if the hashtable is empty then we return false
                return false;
            }

            return (bool)keyTable[key];
        }

        public static void changeState(Keys key, bool state)
        {
            //this fuction will change state of the keys and the player with it
            // this function has two arguments Key and state
            keyTable[key] = state;

        }

    }
}
