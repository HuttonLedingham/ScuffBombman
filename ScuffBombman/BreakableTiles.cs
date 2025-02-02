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
Purpose: Holds the information of the breakable tiles. Such as if it holds powerups and when to disappear when hit from fire.
*/

    class BreakableTiles
    {
        Point location;
        int indexX;
        int indexY;
        double timeToDisapear;
        double timer;
        bool isBurning = false;
        SolidBrush tempColour = new SolidBrush(Color.Blue);
        int powerUpChance;


        public BreakableTiles( int imageIndexX, int imageIndexY)
        {

        }
        public BreakableTiles(int gridPosX, int gridPosY, int imageIndexX, int imageIndexY, int item)
        {
            indexX = imageIndexX;
            indexY = imageIndexY;

            powerUpChance = item;

            location = new Point(gridPosX, gridPosY);
        }


        public void onFire(int time, int imageIndexX, int imageIndexY)
        {
            timeToDisapear = time;

            isBurning = true;

            indexX = imageIndexX;
            indexY = imageIndexY;
        }

        public bool getFireStatus()
        {
            return isBurning;
        }

        public int getImageIndexX()
        {
            return indexX;
        }

        public int getImageIndexY()
        {
            return indexY;
        }

        public void addTime()
        {
            timer += 0.01;
        }

        public int PowerUp()
        {
            Console.WriteLine("New, " + powerUpChance);
            return powerUpChance;
        }

        public bool checkTimeisUp()
        {
            if(timer >= timeToDisapear)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Point getLocation()
        {
            return location;
        }

    }
}
