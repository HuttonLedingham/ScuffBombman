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
Purpose: Holds all the information for a fire object. So each one can hold it's own timer and disappear on it's own and hold different images for each one. So we can have specialize sprites.
*/

    class Fire
    {
        int fireTime;
        Point Location;
        double Timer;
        int imageIndexX;
        int imageIndexY;

        public Fire(int buringTime, int x, int y, string fireImage)
        {
            fireTime = buringTime;

            Location.X = x;
            Location.Y = y;


            //find what type where the fire is and hold the right graphic to fit that spot.
            if(fireImage.ToUpper() == "up".ToUpper() || fireImage.ToUpper() == "down".ToUpper())
            {
                imageIndexX = 2;
                imageIndexY = 8;
            }
            else if(fireImage.ToUpper() == "left".ToUpper() || fireImage.ToUpper() == "right".ToUpper())
            {
                imageIndexX = 3;
                imageIndexY = 7;
            }
            else if (fireImage.ToUpper() == "center".ToUpper())
            {
                imageIndexX = 0;
                imageIndexY = 8;
            }
            else if(fireImage.ToUpper() == "endUp".ToUpper())
            {
                imageIndexX = 1;
                imageIndexY = 9;
            }
            else if (fireImage.ToUpper() == "endDown".ToUpper())
            {
                imageIndexX = 3;
                imageIndexY = 9;
            }
            else if (fireImage.ToUpper() == "endLeft".ToUpper())
            {
                imageIndexX = 5;
                imageIndexY = 9;
            }
            else if (fireImage.ToUpper() == "endRight".ToUpper())
            {
                imageIndexX = 4;
                imageIndexY = 8;
            }
            else if (fireImage.ToUpper() == "tile".ToUpper())
            {
                imageIndexX = 3;
                imageIndexY = 5;
            }
        }

        public Point getLocation()
        {
            return Location;
        }

        public int getImageIndexX()
        {
            return imageIndexX;
        }

        public int getImageIndexY()
        {
            return imageIndexY;
        }

        public void addTime(double addInterval)
        {
            Timer += 0.01;
        }

        public bool checkfireIsOut()
        {
            if(Timer > fireTime)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
