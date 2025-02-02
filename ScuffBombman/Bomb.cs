using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bombs
{
    /*
Name: Hutton Ledingham and Yaroslav Dolotov
Assignment: Super BomberMan
Due Date: May 6 2022
Purpose: Controls the timer for the bomb so it explodes on time. 
*/

    class Bomb
    {
        double timer;
        double explodeTime = 1;
        Point bombLocation;
        List<Bomb> bombList;
        int range;
        int fireDuration;
        int belongToPlayer = 0;

        string[] hi = new string[2] { "h", "t" };

        public Bomb()
        {
            bombList = new List<Bomb>();
        }

        public void makeList()
        {

        }

        private static void test(Object myObject, EventArgs myEventArgs)
        {

        }


        public Bomb(Point placeLocation, int maximumRange, double timeToExplode, int fireTime, int whichPlayerBomb)
        {
            //apply all the perameters the bomb needs.
            bombLocation = placeLocation;
            explodeTime = timeToExplode;
            range = maximumRange;
            fireDuration = fireTime;
            belongToPlayer = whichPlayerBomb;
        }

        public int getWhichPlayerBomb()
        {
            return belongToPlayer;
        }

        public int getFireDuration()
        {
            return fireDuration;
        }

        public Point getLocation()
        {
            return bombLocation;
        }

        public int getRange()
        {
            return range;
        }

        public List<Bomb> getList()
        {
            return bombList;
        }

        public void addBomb(Bomb newBomb)
        {
            bombList.Add(newBomb);
        }

        public void addTime(double timeInteravel)
        {
            //update the timer so the bomb will explode.
            timer += 0.01;
        }

        public bool checkTimeisUp()
        {
            //check if the timer is up.
            if (timer >= explodeTime)
            {
                return true;

            }
            else
            {
                return false;
            }
        }

        public Tiles[,] ifPlayerIsOffBomb(Tiles[,] gameData)
        {
            //check if the player is off a bomb and place the bomb back there.
            for (int j = 0; j < bombList.Count; j++)
            {
                if (gameData[bombList[j].getLocation().X, bombList[j].getLocation().Y].tileType != "Player")
                {
                    gameData[bombList[j].getLocation().X, bombList[j].getLocation().Y] = new Tiles("Bomb");
                }
            }

            return gameData;
        }
        DateTime y = new DateTime();

        
    }
}
