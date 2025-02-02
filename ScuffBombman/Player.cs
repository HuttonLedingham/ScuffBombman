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
Purpose: Does the movement for the player. And holds all the information of the player and powerups. So we can apply the powerups to the bombs.
*/

    class Player
    {
        Point location;
        int bombRange = 5;
        int fireDuration = 1;
        double bombDuration = 1;
        int health = 1;
        int bombLimit = 2;
        int Score = 0;
        int activeBombs = 0;
        int playerRotation = 0;
        bool playerDead = false;

        int imageIndexY = 0;

        int mv;
        int mh;

        //new player and a new game
        public Player(int xLocation, int yLocation)
        {
            location.X = xLocation;
            location.Y = yLocation;
        }

        //make player dead
        public Player(int xLocation, int yLocation, bool deadOrAlive)
        {
            location.X = xLocation;
            location.Y = yLocation;

            playerDead = deadOrAlive;
        }


        //new player but load in the score from the save file.
        public Player(int xLocation, int yLocation, int newScore, int playerNumber)
        {
            location.X = xLocation;
            location.Y = yLocation;

            Score = newScore;

            //grab the right graphic for the player
            if(playerNumber == 1)
            {
                imageIndexY = 3;
            }
            else if (playerNumber == 2)
            {
                imageIndexY = 4;
            }
            else if (playerNumber == 3)
            {
                imageIndexY = 5;
            }
            else if (playerNumber == 4)
            {
                imageIndexY = 6;
            }
        }

        //Change the moving direction of the player.
        public void getInput(int inputX, int inputY)
        { 
            mh = inputX;
            mv = inputY;

            Console.WriteLine(mv);
        }

        //check if the player is dead or not
        public Tiles[,] CheckPlayerMovement(Tiles[,] gameData, List<PowerUps> tempPUList)
        {
            //if the player is dead it can't move
            if (!playerDead)
            {
                //each time when the timer ticks the player will moving in the direction given by the getInput function
                if (mv == 1)
                {
                    gameData = MovePlayer(gameData, 0, -1, tempPUList);
                }
                if (mv == -1)
                {
                    gameData = MovePlayer(gameData, 0, 1, tempPUList);
                }
                if (mh == 1)
                {
                    gameData = MovePlayer(gameData, -1, 0, tempPUList);
                }
                if (mh == -1)
                {
                    gameData = MovePlayer(gameData, 1, 0, tempPUList);
                }

                //reset the inputs
                mv = 0;
                mh = 0;

            }
            return gameData;
        }

        public Tiles[,] MovePlayer(Tiles[,] gameData  ,int h, int v, List<PowerUps> powerUpsList)
        {
            //if the spot is open the player is able to move.
            if (gameData[location.X + h, location.Y + v].tileType == "Ground")
            {
                //move the player onto the open spot.
                gameData[location.X + h, location.Y + v] = new Tiles("Player");
                //replace where the player was previously with ground.
                gameData = PlayerCameFrom(gameData);
                //update location
                changeLocation(h,v);
            }
            //if the moves into the fire, the player takes damage
            else if (gameData[location.X + h, location.Y + v].tileType == "Fire")
            {
                //the player takes damaga
                takeDamage();
                gameData = PlayerCameFrom(gameData);
                changeLocation(h, v);
            }
            //if the player moves on to a powerup the player claims it
            else if (gameData[location.X + h, location.Y + v].tileType == "PowerUp")
            {
                gameData[location.X + h, location.Y + v] = new Tiles("Player");
                gameData = PlayerCameFrom(gameData);
                changeLocation(h, v);
                //find which powerup the player walked into.
                for (int i = 0; i < powerUpsList.Count; i++)
                {
                    if (powerUpsList[i].getLocation().X == location.X && powerUpsList[i].getLocation().Y == location.Y)
                    {
                        //find which type of powerup the player ran into and make the right changes.
                        if (powerUpsList[i].powerType() == 1)
                        {
                            bombRange++;
                        }
                        else if (powerUpsList[i].powerType() == 2)
                        {
                            health++;
                        }
                        else if (powerUpsList[i].powerType() == 3)
                        {
                            bombLimit++;
                        }
                        else
                        {
                            bombDuration = bombDuration - 0.5;
                        }
                    }
                }
            }

            return gameData;
        }

        private Tiles[,] PlayerCameFrom(Tiles[,] gameData)
        {
            //replace where the player came from with a ground object
            if (gameData[location.X, location.Y].tileType == "Player")
            {
                gameData[location.X, location.Y] = new Tiles("Ground");
            }

            return gameData;
        }

        //adds score
        public void addScore(int amount)
        {
            Score += amount;
        }

        //checks if the player is dead
        public bool checkIfDead()
        {
            if(health == 0)
            {
                return true;
            }
            return false;
        }

        public int getImageIndexY()
        {
            return imageIndexY;
        }

        //checks if the player is on that spot
        public bool ifPlayerisOnThatSpot(int x, int y)
        {
            if (location.X == x && location.Y == y)
            {
                return true;
            }
            return false;
        }

        public bool getGetPlayerStatus()
        {
            return playerDead;
        }

        public void addBomb()
        {
            activeBombs++;
        }

        public void removeBomb()
        {
            activeBombs--;
        }

        public int getActiveBombs()
        {
            return activeBombs;
        }

        //new
        public int getScore()
        {
            return Score;
        }

        public int getBombRange()
        {
            return bombRange;
        }

        public int getFireDuration()
        {
            return fireDuration;
        }

        //change the rotation so we can change the rotation of the graphic
        public void changeRotation(int newRotation)
        {
            playerRotation = newRotation;
        }

        public int getRotation()
        {
            return playerRotation;
        }

        public int getBombLimit()
        {
            return bombLimit;
        }

        public Point getLocation()
        {
            return location;
        }

        public double getBombDuration()
        {
            return bombDuration;
        }

        public void takeDamage()
        {
            health--;
        }
        
        //update the player location
        public void changeLocation(int xChange, int yChange)
        {
            location.X += xChange;
            location.Y += yChange;

        }

    }
}
