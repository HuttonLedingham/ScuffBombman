using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace bombs
{

    /*
Name: Hutton Ledingham and Yaroslav Dolotov
Assignment: Super BomberMan
Due Date: May 6 2022
Purpose: Holds all the information for a enemy and control the movement. 
*/
    class Enemy
    {
        string enemyType;
        Point location;

        int xMovement = 0;
        int yMovement = 0;

        //Timer newTimer;
        //change later to graphic
        SolidBrush tempColour = new SolidBrush(Color.Red);
        Random randomNumber = new Random();
        
        public Enemy(int x, int y, char typeOfEnemy, Tiles[,] gameData)
        {
            //Get what type of enemy it is.
            if (typeOfEnemy == 'y')
            {
                enemyType = "basic";

                tempColour = new SolidBrush(Color.Yellow);

            }
            else if (typeOfEnemy == 'r')
            {
                enemyType = "advance";


            }

            location = new Point(x,y);
        }

        
        private static void test(Object myObject, EventArgs myEventArgs)
        {

        }

        public void activateEnemy(Tiles[,] gameData)
        {
            //Check what direction the enemy could move in when the match starts.
            if (checkPossibleDirection(gameData, location.X - 1, location.Y))
            {
                xMovement = -1;
            }
            else if (checkPossibleDirection(gameData, location.X + 1, location.Y))
            {
                xMovement = 1;
            }
            else if (checkPossibleDirection(gameData, location.X, location.Y +1))
            {
                yMovement = -1;
            }
            else if (checkPossibleDirection(gameData, location.X, location.Y -1))
            {
                yMovement = 1;
            }
        }
    
        public Tiles[,] enemyMovemment(Tiles[,] gameData)
        {
            //Make the enemy move in a straight line and when the enemy hits a wall go the opposite direction.
            if(enemyType == "basic")
            {
                //if the player ran into a wall go the opposite direction
                if (gameData[location.X + xMovement, location.Y + yMovement].tileType != "Ground" && gameData[location.X + xMovement, location.Y + yMovement].tileType != "Player")
                {
                    xMovement = xMovement * -1;
                    yMovement = yMovement * -1;
                }

                //update the location and apply the changes.
                if (gameData[location.X + xMovement, location.Y + yMovement].tileType == "Ground" || gameData[location.X + xMovement, location.Y + yMovement].tileType == "Player")
                {
                    gameData[location.X, location.Y] = new Tiles("Ground");
                    gameData[location.X + xMovement, location.Y + yMovement] = new Tiles("Enemy");

                    location.X += xMovement;
                    location.Y += yMovement;
                }
            }
            //WHen an advance enemy hits a intersection it randomly choices to either go straight or turn.
            else if (enemyType == "advance")
            {
                List<string> possibleDirection = new List<string>();

                //when the enemy is going left or right.
                if (yMovement == 0)
                {
                    //grab all the possible directions the enemy can go.
                    if (checkPossibleDirection(gameData, location.X + xMovement, location.Y))
                    {
                        possibleDirection.Add("forward");
                    }
                    if (checkPossibleDirection(gameData, location.X, location.Y - 1))
                    {
                        possibleDirection.Add("up");
                    }
                    if (checkPossibleDirection(gameData, location.X, location.Y + 1))
                    {
                        possibleDirection.Add("down");
                    }

                    int rng = randomNumber.Next(0, possibleDirection.Count);

                    //picks a direction to go.
                    if (possibleDirection.Count != 0)
                    {
                        if (possibleDirection[rng] == "up")
                        {
                            xMovement = 0;
                            yMovement = -1;
                        }
                        else if (possibleDirection[rng] == "down")
                        {
                            xMovement = 0;
                            yMovement = 1;
                        }
                    }

                }
                //When the enemy is going up or down
                else if(xMovement == 0)
                {
                    //grab all the possible directions the enemy can go.
                    if (checkPossibleDirection(gameData, location.X, location.Y + yMovement))
                    {
                        possibleDirection.Add("forward");
                    }
                    if (checkPossibleDirection(gameData, location.X - 1, location.Y))
                    {
                        possibleDirection.Add("left");
                    }
                    if (checkPossibleDirection(gameData, location.X + 1, location.Y))
                    {
                        possibleDirection.Add("right");
                    }

                    int rng = randomNumber.Next(0, possibleDirection.Count);

                    //picks a direction to go.
                    if (possibleDirection.Count != 0)
                    {
                        if (possibleDirection[rng] == "right")
                        {
                            xMovement = 1;
                            yMovement = 0;
                        }
                        else if (possibleDirection[rng] == "left")
                        {
                            xMovement = -1;
                            yMovement = 0;
                        }
                    }
                }

                //if the enemy has hit a wall.
                if (gameData[location.X + xMovement, location.Y + yMovement].tileType != "Ground" && gameData[location.X + xMovement, location.Y + yMovement].tileType != "Player")
                {
                    xMovement = xMovement * -1;
                    yMovement = yMovement * -1;
                }



                //apply the changes to the gameData.
                gameData[location.X, location.Y] = new Tiles("Ground");
                gameData[location.X + xMovement, location.Y + yMovement] = new Tiles("Enemy");

                location.X += xMovement;
                location.Y += yMovement;
            }

                return gameData;
        }

        public bool checkPossibleDirection(Tiles[,] gameData, int x, int y)
        {
            //check if the next position is available.
            if (gameData[x, y].tileType == "Ground" || gameData[x, y].tileType == "Player")
            {
                return true;
            }
            return false;
        }

        public Point getLocation()
        {
            return location;
        }

        //change later
        public Brush getColor()
        {
            return tempColour;
        }

        public string getEnemyType()
        {
            return enemyType;
        }
    }
}
