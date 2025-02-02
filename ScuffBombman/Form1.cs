using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Input;

namespace bombs
{

    /*
Name: Hutton Ledingham and Yaroslav Dolotov
Assignment: Super BomberMan
Due Date: May 6 2022
Purpose: The main form to run the game on and loads the game. Does all the functions to get the game going.
*/


    public partial class Form1 : Form
    {
        Tiles[,] gameData;
        List<Fire> fireList = new List<Fire>();
        Bomb bombHolder = new Bomb();
        Bitmap graphicBits = new Bitmap(Image.FromFile("../../sbmGraphics.png"));
        List<BreakableTiles> breakableTilesList = new List<BreakableTiles>();
        List<Enemy> enemiesList = new List<Enemy>();
        List<Player> playerList = new List<Player>();
        List<PowerUps> powerUpsList = new List<PowerUps>();
        Point powerUpLocation;


        Size gridSize = new Size();
        Size cellSize = new Size(32, 32);
        int tempScore = 0;
        int level = 1;
        int lvlCalc = 0;
        int numberOfPlayers = 1;
        int activePlayers =1;

        Random rng = new Random();

        Random powerUp = new Random();
        int powerUpType = 0;


        //new game
        public Form1()
        {
            InitializeComponent();
            ReadMap("../../TextFile1.txt");
            fillInMap();
            //makes score label transparent
            scoreLabel.BackColor = Color.FromArgb(150, Color.White);

        }

        //loading from a previous save
        public Form1(int pastPlayerLocationX, int pastPlayerLocationY, int pastLevel, int pastScore)
        {
            InitializeComponent();
            level = pastLevel;

            tempScore = pastScore;
            //get it to load from the previous save.
            ReadMap("../../LevelSaveFile.txt");

            scoreLabel.BackColor = Color.FromArgb(150, Color.White);
        }

        //Multiplayer
        public Form1(int playerCount)
        {
            InitializeComponent();
            //picks the multiplayer level.
            level = 0;
            numberOfPlayers = playerCount;
            activePlayers = numberOfPlayers;
            saveToolStripMenuItem.Visible = false;
            ReadMap("../../TextFile1.txt");
            fillInMap();
            scoreLabel.Visible = false;
        }

        //Randomly place breakable tiles around the map.
        public void fillInMap()
        {
            List<Point> availableSpots = new List<Point>();

            int changeInX = 0;
            int changeInY = 0;

            //Get all the available spots.
            for (int i = 0; i < gridSize.Width; i++)
            {
                for(int j = 0; j < gridSize.Height; j++)
                {
                    if (gameData[i,j].tileType == "Ground")
                    {
                        availableSpots.Add(new Point(i, j));
                    }
                }
            }

            //remove all the spots that could make the player not able to move or can't place a bomb down wthout killing themselves.
            for (int h = 0; h < playerList.Count; h++)
            {
                for (int k = 0; k < availableSpots.Count;)
                {
                    changeInX = Math.Abs(playerList[h].getLocation().X - availableSpots[k].X);
                    changeInY = Math.Abs(playerList[h].getLocation().Y - availableSpots[k].Y);

                    if (!(Math.Abs(changeInX + changeInY) != 1 && (changeInY > 1 || changeInX > 1)))
                    {
                        availableSpots.Remove(availableSpots[k]);
                    }
                    else
                    {
                        k++;
                    }
                }
            }

            Random rng = new Random();

            int randomNumber;

            //goes through the whole list of open spots and randomly decides if there is going to be a spot there.
            while(availableSpots.Count != 0) 
            { 
                randomNumber = rng.Next(1,10);

                if (randomNumber <= 3)
                {
                    //create a breakable tile 
                    gameData[availableSpots[0].X, availableSpots[0].Y] = new Tiles("breakableTile");
                    breakableTilesList.Add(new BreakableTiles(availableSpots[0].X, availableSpots[0].Y, 4, 2, powerUp.Next(0,10)));
                }

                //remove the available as it is already used.
                availableSpots.Remove(availableSpots[0]);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void ReadMap(string file)
        {
            string lineOfText = null;
            try
            {
                StreamReader mapFile = new StreamReader(file);
                while (!mapFile.EndOfStream)
                {
                    lineOfText = mapFile.ReadLine();
                    Console.WriteLine(lineOfText[0]);
                    if (lineOfText[0] == '-') continue;
                    if (lineOfText[0] == '@')
                    {
                        lvlCalc = Convert.ToInt32(lineOfText[1].ToString() + lineOfText[2].ToString());
                        
                        if (lineOfText[0] == '@' && level == lvlCalc)
                        {
                            lvlCalc = (Convert.ToInt32(lineOfText[1]) * 10) + Convert.ToInt32(lineOfText[2]);
                            Console.WriteLine(lvlCalc);
                            lineOfText = mapFile.ReadLine();
                            string[] data = lineOfText.Split(',');
                            gridSize.Width = Convert.ToInt16(data[0]);
                            gridSize.Height = Convert.ToInt16(data[1]);
                            gameData = new Tiles[gridSize.Width, gridSize.Height];
                            
                            for (int row = 0; row < gridSize.Height; row++)
                            {
                                lineOfText = mapFile.ReadLine();
                                for (int col = 0; col < lineOfText.Length; col++)
                                {
                                    SetCell(col, row, lineOfText[col]);
                                }
                            }
                        }
                    }
                    else if (lineOfText[2] == ' ')
                    {
                        if (lineOfText[0] == '@' && lineOfText[1] == Convert.ToChar(level.ToString()))
                        {
                            lineOfText = mapFile.ReadLine();
                            string[] data = lineOfText.Split(',');
                            gridSize.Width = Convert.ToInt16(data[0]);
                            gridSize.Height = Convert.ToInt16(data[1]);
                            gameData = new Tiles[gridSize.Width, gridSize.Height];
                            for (int row = 0; row < gridSize.Height; row++)
                            {
                                lineOfText = mapFile.ReadLine();
                                for (int col = 0; col < lineOfText.Length; col++)
                                {
                                    SetCell(col, row, lineOfText[col]);
                                }
                            }
                        }
                    }
                }
                mapFile.Close();
                this.ClientSize = new Size(cellSize.Width * gridSize.Width, cellSize.Height * gridSize.Height + menuStrip1.Height);
                this.CenterToScreen();

                //activates enemy so they will move in a direction
                for(int i = 0; i < enemiesList.Count; i++)
                {
                    enemiesList[i].activateEnemy(gameData);
                }

                //enabled the timers
                timer2.Enabled = true;
                timer3.Enabled = true;
                this.Invalidate();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading file " +ex);
            }
        }
        private void SetCell(int x, int y, char character)
        {
            gameData[x, y] = new Tiles("Ground");

            if (character.Equals(' '))
            {
                gameData[x, y] = new Tiles("Ground");
            }
            if (character.Equals('#'))
            {
                gameData[x, y] = new Tiles("Wall");
            }
            if (character.Equals('p'))
            {
                //if the numberOfPlayers is not greater than one the game would be multiplayer.
                if (playerList.Count < numberOfPlayers)
                {
                    gameData[x, y] = new Tiles("Player");

                    playerList.Add(new Player(x, y, tempScore, playerList.Count + 1));
                }
                else
                {
                    gameData[x, y] = new Tiles("Ground");
                }
            }
            if (character.Equals('b'))
            {
                gameData[x, y] = new Tiles("breakableTile");
                breakableTilesList.Add(new BreakableTiles(x, y, 4, 2, rng.Next()));
            }
            if (character.Equals('y') || character.Equals('r'))
            {
                gameData[x, y] = new Tiles("Enemy");
                enemiesList.Add(new Enemy(x,y,character,gameData));
            }
        }
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void startToolStripMenuItem_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(0, menuStrip1.Height);
            int imageIndexX = 0;
            int imageIndexY = 0;
            //vertical lines
            for (int i = 0; i < gridSize.Width; i++)
            {
                e.Graphics.DrawLine(Pens.Black, i * cellSize.Width, 0, i * cellSize.Width, ClientRectangle.Bottom);
            }

            //horizontal lines
            for (int i = 0; i < gridSize.Height; i++)
            {
                e.Graphics.DrawLine(Pens.Black, 0, i * cellSize.Height, ClientRectangle.Right, i * cellSize.Height);
            }

            for (int i = 0; i < gridSize.Width; i++)
            {
                for (int j = 0; j < gridSize.Height; j++)
                {
                    Rectangle srcRect;
                    Rectangle destRect = new Rectangle(i * cellSize.Width, j * cellSize.Height, cellSize.Width, cellSize.Height);

                    //automatically put in the ground graphic
                    imageIndexX = 0;
                    imageIndexY = 2;
                    srcRect = new Rectangle(imageIndexX * cellSize.Width, imageIndexY * cellSize.Height, cellSize.Width, cellSize.Height);
                    e.Graphics.DrawImage(graphicBits, destRect, srcRect, GraphicsUnit.Pixel);

                    //place the right graphic for the right spot on the gameDataw 
                    if (gameData[i, j].tileType == "Ground")
                    {
                        imageIndexX = 0;
                        imageIndexY = 2;
                        srcRect = new Rectangle(imageIndexX * cellSize.Width, imageIndexY * cellSize.Height, cellSize.Width, cellSize.Height);
                        e.Graphics.DrawImage(graphicBits, destRect, srcRect, GraphicsUnit.Pixel);
                    }
                    if (gameData[i, j].tileType == "Wall")
                    {
                        //get all the corners
                        if (i == 0 && j == 0)
                        {
                            imageIndexX = 0;
                            imageIndexY = 0;
                        }
                        else if (i == gridSize.Width - 1 && j == gridSize.Height - 1)
                        {
                            imageIndexX = 3;
                            imageIndexY = 1;
                        }
                        else if (i == 0 && j == gridSize.Height - 1)
                        {
                            imageIndexX = 0;
                            imageIndexY = 1;
                        }
                        else if (i == gridSize.Width - 1 && j == 0)
                        {
                            imageIndexX = 3;
                            imageIndexY = 0;
                        }
                        //the sides of the arena
                        else if (i == 0 || i == gridSize.Width - 1)
                        {
                            imageIndexX = 1;
                            imageIndexY = 1;
                        }
                        else if (j == 0 || j == gridSize.Height - 1)
                        {
                            imageIndexY = 1;
                            imageIndexX = 2;
                        }
                        else
                        {
                            imageIndexX = 2;
                            imageIndexY = 2;
                        }

                        srcRect = new Rectangle(imageIndexX * cellSize.Width, imageIndexY * cellSize.Height, cellSize.Width, cellSize.Height);
                        e.Graphics.DrawImage(graphicBits, destRect, srcRect, GraphicsUnit.Pixel);
                    }
                    if (gameData[i, j].tileType == "Player")
                    {

                        imageIndexY = 3;
                        
                        //find the exact player on this spot and place the graphic associate to that player.
                        for(int h = 0; h < playerList.Count; h++)
                        {
                            if (playerList[h].getLocation().X == i && playerList[h].getLocation().Y == j)
                            {
                                srcRect = new Rectangle(playerList[h].getRotation() * cellSize.Width, playerList[h].getImageIndexY() * cellSize.Height, cellSize.Width, cellSize.Height);
                                e.Graphics.DrawImage(graphicBits, destRect, srcRect, GraphicsUnit.Pixel);

                            }
                        }

                        //if the player was on a bomb
                        if (bombHolder.getList() != null)
                        {
                            for (int h = 0; h < bombHolder.getList().Count; h++)
                            {
                                if (bombHolder.getList()[h].getLocation().X == i && bombHolder.getList()[h].getLocation().Y == j)
                                {
                                    imageIndexX = 4;
                                    imageIndexY = 3;
                                    srcRect = new Rectangle(imageIndexX * cellSize.Width, imageIndexY * cellSize.Height, cellSize.Width, cellSize.Height);
                                    e.Graphics.DrawImage(graphicBits, destRect, srcRect, GraphicsUnit.Pixel);
                                }
                            }
                        }
                    }
                    if (gameData[i, j].tileType == "Bomb")
                    {
                        imageIndexX = 4;
                        imageIndexY = 3;
                        srcRect = new Rectangle(imageIndexX * cellSize.Width, imageIndexY * cellSize.Height, cellSize.Width, cellSize.Height);
                        e.Graphics.DrawImage(graphicBits, destRect, srcRect, GraphicsUnit.Pixel);
                    }
                    if (gameData[i, j].tileType == "Fire")
                    {
                        //do the same thing for the player but for a bomb and other object after.
                        for (int h = 0; h < fireList.Count; h++)
                        {
                            if (fireList[h].getLocation().X == i && fireList[h].getLocation().Y == j)
                                srcRect = new Rectangle(fireList[h].getImageIndexX() * cellSize.Width, fireList[h].getImageIndexY() * cellSize.Height, cellSize.Width, cellSize.Height);
                            e.Graphics.DrawImage(graphicBits, destRect, srcRect, GraphicsUnit.Pixel);
                        }
                    }
                    if (gameData[i, j].tileType == "breakableTile")
                    {
                        for (int h = 0; h < breakableTilesList.Count; h++)
                        {
                            if (breakableTilesList[h].getLocation().X == i && breakableTilesList[h].getLocation().Y == j)
                            {
                                srcRect = new Rectangle(breakableTilesList[h].getImageIndexX() * cellSize.Width, breakableTilesList[h].getImageIndexY() * cellSize.Height, cellSize.Width, cellSize.Height);
                                e.Graphics.DrawImage(graphicBits, destRect, srcRect, GraphicsUnit.Pixel);
                            }
                        }
                    }
                    if (gameData[i, j].tileType == "Enemy")
                    {
                        for (int h = 0; h < enemiesList.Count; h++)
                        {
                            if (enemiesList[h].getLocation().X == i && enemiesList[h].getLocation().Y == j)
                            {
                                e.Graphics.FillRectangle(enemiesList[h].getColor(), destRect);
                            }
                        }
                    }
                    if (gameData[i, j].tileType == "PowerUp")
                    {
                        for (int h = 0; h < powerUpsList.Count; h++)
                        {
                            if (powerUpsList[h].getLocation().X == i && powerUpsList[h].getLocation().Y == j)
                            {
                                e.Graphics.FillRectangle(powerUpsList[h].getColor(), destRect);
                            }
                        }
                    }
                }
            }
        }

        public void checkIfPlayerIsIn(int playerNumber, int rotation, int changeX, int changeY)
        {
            //check if the player exists
            if(playerNumber <= playerList.Count)
            {
                //Move the player
                playerList[playerNumber - 1].getInput(changeX, changeY);
                timer1.Enabled = true;
                //change the rotation so we can change the graphic of the player.
                playerList[playerNumber - 1].changeRotation(rotation);
                //check if the player is dead.
                checkIfPlayerIsDead(playerNumber);
            }

        }
        

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                //player 2
                case Keys.Up:
                    checkIfPlayerIsIn(2, 0, 0, 1);

                    break;
                case Keys.Down:
                    checkIfPlayerIsIn(2, 3, 0, -1);

                    break;
                case Keys.Left:
                    checkIfPlayerIsIn(2, 2, 1, 0);

                    break;
                case Keys.Right:
                    checkIfPlayerIsIn(2, 1, -1, 0);

                    break;
                case Keys.End:
                    placeBomb(2);

                    break;

                //Player 1
                case Keys.W:
                    checkIfPlayerIsIn(1, 0, 0, 1);

                    break;
                case Keys.S:
                    checkIfPlayerIsIn(1, 3, 0, -1);

                    break;
                case Keys.A:
                    checkIfPlayerIsIn(1, 2, 1, 0);

                    break;
                case Keys.D:
                    checkIfPlayerIsIn(1, 1, -1, 0);

                    break;
                case Keys.E:
                    placeBomb(1);

                    break;
                //Player 3
                case Keys.T:
                    checkIfPlayerIsIn(3, 0, 0, 1);

                    break;
                case Keys.G:
                    checkIfPlayerIsIn(3, 3, 0, -1);

                    break;
                case Keys.F:
                    checkIfPlayerIsIn(3, 2, 1, 0);

                    break;
                case Keys.H:
                    checkIfPlayerIsIn(3, 1, -1, 0);

                    break;
                case Keys.Y:
                    placeBomb(3);

                    break;
                //Player 4
                case Keys.I:
                    checkIfPlayerIsIn(4, 0, 0, 1);

                    break;
                case Keys.K:
                    checkIfPlayerIsIn(4, 3, 0, -1);

                    break;
                case Keys.J:
                    checkIfPlayerIsIn(4, 2, 1, 0);

                    break;
                case Keys.L:
                    checkIfPlayerIsIn(4, 1, -1, 0);

                    break;
                case Keys.O:
                    placeBomb(4);
                    break;
            }
        }

        public void placeBomb(int playerNumber)
        {
            //check if the player is in the game
            if (playerNumber <= playerList.Count)
                if (playerList[playerNumber - 1].getActiveBombs() < playerList[playerNumber - 1].getBombLimit() && !playerList[playerNumber - 1].getGetPlayerStatus())
                {
                    //add a bomb to the list so we can use it for the timer.
                    bombHolder.addBomb(new Bomb(playerList[playerNumber - 1].getLocation(), playerList[playerNumber - 1].getBombRange(), playerList[playerNumber - 1].getBombDuration(), playerList[playerNumber - 1].getFireDuration(), playerNumber - 1));
                    timer2.Enabled = true;
                    //add an active bomb to the player so we can limit the active bombs placed by the player.
                    playerList[playerNumber -1].addBomb();
                    Invalidate();
                }
        }
        

        public void checkIfPlayerIsDead(int playerNumber)
        {
            //check if the current player is dead.
            if(playerList[playerNumber -1].checkIfDead())
            {
                activePlayers--;

                //so whens it's singleplayer it doesn't get rid of the stats
                if (activePlayers > 0)
                {
                    //put the player into a dead state.
                    playerList[playerNumber - 1] = new Player(0, 0, true);
                }
                //if there is only one active player go to the end screen.
                if (activePlayers <= 1)
                {
                    for (int h = 0; h < playerList.Count; h++)
                    {
                        if (!playerList[h].getGetPlayerStatus())
                        {
                            GameOver(playerList[h]);
                        }
                    }
                }
            }
        }

        


        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void explodeBomb(Bomb currentBomb)
        {
            //grab the locations of the bombs
            int bombLocationX = currentBomb.getLocation().X;
            int bombLocationY = currentBomb.getLocation().Y;

            bool down = true;
            bool up = true;
            bool right = true;
            bool left = true;
            int i = 1;

            //place a fire piece where the bomb is
            gameData[bombLocationX, bombLocationY] = new Tiles("Fire");
            fireList.Add(new Fire(currentBomb.getFireDuration(), bombLocationX, bombLocationY, "center"));
            bombHitsPlayer(bombLocationX, bombLocationY);

            //it goes in all four directions of where the bomb is and stops when all four directions hit a wall.
            while ((down || up || right || left) && i <= currentBomb.getRange())
            {
                
                if (down)
                {
                    //when this direction hits a wall it will turn false and no longer be called anymore in this loop.
                    down = addFire(bombLocationX, bombLocationY + i, "down", i, currentBomb.getRange(), currentBomb);
                }
                if (up)
                {
                    up = addFire(bombLocationX, bombLocationY - i, "up", i, currentBomb.getRange(), currentBomb);
                }
                if (right)
                {
                    right = addFire(bombLocationX + i, bombLocationY, "right", i, currentBomb.getRange(), currentBomb);
                }
                if (left)
                {
                    left = addFire(bombLocationX - i, bombLocationY, "left", i, currentBomb.getRange(), currentBomb);
                }
                i++;

            }

             Invalidate();
        }

        private bool addFire(int fireLocationX, int fireLocationY, string diretion, int currentEntry, int Range, Bomb currentBomb)
        {
            bool tempBool = true;

            if (gameData[fireLocationX, fireLocationY].tileType != "Wall" && gameData[fireLocationX, fireLocationY].tileType != "breakableTile")
            {
                //the player takes damage
                bombHitsPlayer(fireLocationX,fireLocationY);

                //kills the enemy
                if(gameData[fireLocationX, fireLocationY].tileType == "Enemy")
                {
                    killEnemy(fireLocationX,fireLocationY);
                    playerList[0].addScore(200);
                }

                //apply the change to the gameData
                gameData[fireLocationX, fireLocationY] = new Tiles("Fire");

                //make the end of the fire trail has a different graphic.
                if (currentEntry != Range)
                {
                    fireList.Add(new Fire(currentBomb.getFireDuration(), fireLocationX, fireLocationY, diretion));
                }
                else
                {
                    fireList.Add(new Fire(currentBomb.getFireDuration(), fireLocationX, fireLocationY, "end" + diretion));
                }
            }
            //checks for a breakableTile
            else if (checkForTile(fireLocationX, fireLocationY))
            {
                tempBool = breakTile(tempBool, fireLocationX, fireLocationY, diretion, currentBomb);
            }
            //the fire hit a stoppable object
            else
            {
                tempBool = false;
            }

            return tempBool;
        }


        public void killEnemy(int x, int y)
        {
            for(int i = 0; i < enemiesList.Count; i++)
            {
                if(enemiesList[i].getLocation().X == x && enemiesList[i].getLocation().Y == y)
                {
                    enemiesList.Remove(enemiesList[i]);
                }
            }
        }

        public void bombHitsPlayer(int fireLocationX, int fireLocationY)
        {
            if (gameData[fireLocationX, fireLocationY].tileType == "Player")
            {
                for (int i = 0; i < playerList.Count; i++)
                {
                    if (playerList[i].ifPlayerisOnThatSpot(fireLocationX, fireLocationY))
                    {
                        playerList[i].takeDamage();

                        checkIfPlayerIsDead(i + 1);

                        
                    }
                }
            }
        }


        public bool checkForTile(int fireLocationX, int fireLocationY)
        {
            if ((gameData[fireLocationX, fireLocationY].tileType == "breakableTile"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool breakTile(bool currentBool, int locationX, int locationY, string direction, Bomb currentBomb)
        {
            for (int i = 0; i < breakableTilesList.Count; i++)
            {
                if (breakableTilesList[i].getLocation().X == locationX && breakableTilesList[i].getLocation().Y == locationY)
                {
                    gameData[locationX, locationY] = new Tiles("breakableTile");

                    breakableTilesList[i].onFire(currentBomb.getFireDuration(), 3,2);
                    
                    currentBool = false;
                }
            }
            return currentBool;
        }



        private void DropPowerUp(BreakableTiles currentTile)
        {
            if (currentTile.PowerUp() > 1)
            {
                powerUpType = powerUp.Next(1, 5);
                powerUpsList.Add(new PowerUps(currentTile.getLocation().X, currentTile.getLocation().Y, powerUpType));
                Console.WriteLine(powerUpType);
                powerUpLocation.X = currentTile.getLocation().X;
                powerUpLocation.Y = currentTile.getLocation().Y;
                gameData[currentTile.getLocation().X, currentTile.getLocation().Y] = new Tiles("PowerUp");
            }


            Invalidate();
        }

        private void GameOver(Player stats)
        {
            //check if it's MP or SP, so we can load different end screens depending the situation.
            if (numberOfPlayers == 1)
            {
                DeathScreen newForm = new DeathScreen(stats.getScore(), level);
                newForm.Visible = true;
                this.Close();
            }
            else
            {
                MPEndScreen newForm = new MPEndScreen(5);
                newForm.Visible = true;
                this.Close();
            }
        }

        private void GameOver(int whichPlayerIsLeft)
        {

            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            for (int i = 0; i < playerList.Count; i++)
            {
                gameData = playerList[i].CheckPlayerMovement(gameData, powerUpsList);

                for (int h = 0; h < powerUpsList.Count; h++)
                {
                    for (int j = 0; j < playerList.Count; j++)
                    {
                        if (powerUpsList[h].getLocation().X == playerList[j].getLocation().X && powerUpsList[h].getLocation().Y == playerList[j].getLocation().Y)
                        {
                            powerUpsList.Remove(powerUpsList[h]);
                        }
                    }
                }

                //check if the player is dead
                checkIfPlayerIsDead(i + 1);
            }


            //check if the player is off the bomb
            gameData = bombHolder.ifPlayerIsOffBomb(gameData);

            Invalidate();
        }

        //makes sure stuff disapear on time.
        private void timer2_Tick(object sender, EventArgs e)
        {
            //bomb timer
            for (int j = 0; j < bombHolder.getList().Count; j++)
            {
                bombHolder.getList()[j].addTime((timer1.Interval / 1000));
                if (bombHolder.getList()[j].checkTimeisUp())
                {
                    playerList[bombHolder.getList()[j].getWhichPlayerBomb()].removeBomb();
                    //get the bomb to explode and place fire.
                    explodeBomb(bombHolder.getList()[j]);
                    bombHolder.getList().Remove(bombHolder.getList()[j]);
                }
            }

            //fire timer
            for (int j = 0; j < fireList.Count; j++)
            {
                //add on time to the fire block.
                fireList[j].addTime((timer1.Interval / 1000));

                //if the fire dissapeared for some reason put it back in.
                if (gameData[fireList[j].getLocation().X, fireList[j].getLocation().Y].tileType != "Fire")
                {
                    gameData[fireList[j].getLocation().X, fireList[j].getLocation().Y] = new Tiles("Fire");
                }

                //Check if the fire has reached it time and if it has replace that spot with ground.
                if (fireList[j].checkfireIsOut())
                {
                    gameData[fireList[j].getLocation().X, fireList[j].getLocation().Y] = new Tiles("Ground");

                    for (int h = 0; h < powerUpsList.Count; h++)
                    {
                        //check if the powerup was there before the fire.
                        if (fireList[j].getLocation().X == powerUpsList[h].getLocation().X && fireList[j].getLocation().Y == powerUpsList[h].getLocation().Y)
                        {
                            gameData[fireList[j].getLocation().X, fireList[j].getLocation().Y] = new Tiles("PowerUp");

                        }
                    }

                    fireList.Remove(fireList[j]);

                }
            }

            //timer for the tiles on fire
            for (int j = 0; j < breakableTilesList.Count; j++)
            {
                //check if any of the tiles are burning
                if (breakableTilesList[j].getFireStatus())
                {
                    breakableTilesList[j].addTime();
                    //check if the time is up
                    if (breakableTilesList[j].checkTimeisUp())
                    {
                        //add code to drop power up. Note: could be in the breakableTiles class if we want.
                        playerList[0].addScore(100);
                        gameData[breakableTilesList[j].getLocation().X, breakableTilesList[j].getLocation().Y] = new Tiles("Ground");
                        DropPowerUp(breakableTilesList[j]);

                        breakableTilesList.Remove(breakableTilesList[j]);
                    }
                }
            }

            if (playerList[0] != null)
            {
                scoreLabel.Text = "Score: " + playerList[0].getScore();
            }
            Invalidate();


        }

        //enemy movement
        private void timer3_Tick(object sender, EventArgs e)
        {
            for (int j = 0; j < enemiesList.Count; j++)
            {
                gameData = enemiesList[j].enemyMovemment(gameData);

                if (enemiesList[j].getLocation().X == playerList[0].getLocation().X && enemiesList[j].getLocation().Y == playerList[0].getLocation().Y)
                {
                    playerList[0].takeDamage();
                    if (playerList[0].checkIfDead())
                    {
                        GameOver(playerList[0]);
                    }
                }
            }
        }

        public void playerDamage()
        {
            playerList[0].takeDamage();
        }



        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveGame();

            Application.Restart();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.Exit();
        }

        public void SaveGame()
        {
            TextBox compileBox = new TextBox();

            //if the file doesn't exist make a new one
            if (!File.Exists("../../LevelSaveFile.txt"))
            {
                File.Create("../../LevelSaveFile.txt").Close();
            }

            //load up the player values
            compileBox.Text = "hs:" + playerList[0].getScore() + Environment.NewLine;
            compileBox.Text += "ls:" + level + Environment.NewLine;
            compileBox.Text += "L:" + playerList[0].getLocation().X + "," + playerList[0].getLocation().Y + Environment.NewLine;
            compileBox.Text += "-" + Environment.NewLine;
            compileBox.Text += "@" + level + " " + Environment.NewLine;
            compileBox.Text += gridSize.Height + "," + gridSize.Width + Environment.NewLine;

            //save the level layout
            for(int i = 0; i < gridSize.Height; i++)
            {
                for (int j = 0; j < gridSize.Width; j++)
                {
                    if (gameData[j,i].tileType == "Wall")
                    {
                        compileBox.Text += "#";
                    }
                    else if (gameData[j, i].tileType == "Player")
                    {
                        compileBox.Text += "p";
                    }
                    else if (gameData[j, i].tileType == "breakableTile")
                    {
                        compileBox.Text += "b";
                    }
                    else if (gameData[j, i].tileType == "Enemy")
                    {
                        for (int h = 0; h < enemiesList.Count; h++)
                        {
                            if (enemiesList[h].getLocation().X == j && enemiesList[h].getLocation().Y  == i)
                            {
                                if (enemiesList[h].getEnemyType() == "basic")
                                {
                                    compileBox.Text += "y";
                                }
                                else if (enemiesList[h].getEnemyType() == "advance")
                                {
                                    compileBox.Text += "r";
                                }
                            }
                        }
                    }
                    else
                    {
                        compileBox.Text += " ";
                    }
                }

                compileBox.Text += Environment.NewLine;
            }

            //put it all onto a textfile
            File.WriteAllText("../../LevelSaveFile.txt", compileBox.Text);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            SaveGame();

        }

        private void backToMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //only save if it's singleplayer
            if (playerList.Count == 1)
            {
                SaveGame();
            }
            Menu newForm = new Menu();
            newForm.Visible = true;
            this.Close();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
