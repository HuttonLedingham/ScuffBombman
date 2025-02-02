using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace bombs
{

    /*
Name: Hutton Ledingham and Yaroslav Dolotov
Assignment: Super BomberMan
Due Date: May 6 2022
Purpose: Holds what powerup is at which points.
*/

    class PowerUps
    {
        int powerUp;
        Point location;

        SolidBrush tempColour = new SolidBrush(Color.Blue);
        public PowerUps(int x, int y, int powerUpType)
        {
            location.X = x;
            location.Y = y;
            powerUp = powerUpType;
            if(powerUp == 1)
            {
                tempColour = new SolidBrush(Color.Salmon);
            }
            else if (powerUp == 2)
            {
                tempColour = new SolidBrush(Color.BlueViolet);
            }
            else if (powerUp == 3)
            {
                tempColour = new SolidBrush(Color.DarkOliveGreen);
            }
            else
            {
                tempColour = new SolidBrush(Color.Violet);
            }

        }
        public Point getLocation()
        {
            return location;
        }
        public Brush getColor()
        {
            return tempColour;
        }
        public int powerType()
        {
            return powerUp;
        }
    }
}
