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

namespace bombs
{

    /*
Name: Hutton Ledingham and Yaroslav Dolotov
Assignment: Super BomberMan
Due Date: May 6 2022
Purpose: Start a new game or read from a savefile to continue off.
*/
    public partial class SPMenu : Form
    {
        int playerLocationX;
        int playerLocationY;
        int level;
        int score;

        public SPMenu()
        {
            InitializeComponent();

            //get the current highscores
            if (File.Exists("../../UserSaveFile.txt"))
            {
                ContinueButton.Enabled = true;

                StreamReader saveFile = new StreamReader("../../UserSaveFile.txt");

                string lineOfText;
                string[] data;

                //grab the user's high scores they have saved.
                while (!saveFile.EndOfStream)
                {
                    lineOfText = saveFile.ReadLine();

                    if (lineOfText[0] == 'h' && lineOfText[1] == 's')
                    {
                        data = lineOfText.Split(':');

                        HighScoreLabel.Text = "HighScore: " + data[1];
                    }
                    else if (lineOfText[0] == 'l' && lineOfText[1] == 's')
                    {
                        data = lineOfText.Split(':');

                        HighestLevelLabel.Text = "Highest Level: " + data[1];
                    }
                }

                saveFile.Close();
            }

            //read from the current level file
            if (File.Exists("../../LevelSaveFile.txt"))
            {
                ContinueButton.Enabled = true;

                StreamReader saveFile = new StreamReader("../../LevelSaveFile.txt");

                string lineOfText;
                string[] data;

                //load the current level and scores and display them to the player.
                while (!saveFile.EndOfStream)
                {
                    lineOfText = saveFile.ReadLine();
                    
                    //highscores
                    if (lineOfText[0] == 'h' && lineOfText[1] == 's')
                    {
                        data = lineOfText.Split(':');

                        CurrentScorelLabel.Text = "Current Score: " + data[1];
                        score = Convert.ToInt32(data[1]);
                    }
                    //current level
                    else if (lineOfText[0] == 'l' && lineOfText[1] == 's')
                    {
                        data = lineOfText.Split(':');
                        level = Convert.ToInt32(data[1]);
                        CurrentLevelLebal.Text = "Current Level: " + data[1];
                    }
                    //player location.
                    else if (lineOfText[0] == 'L')
                    {
                        data = lineOfText.Split(':');

                        data = data[1].Split(',');
                        Console.WriteLine(data[1]);

                        playerLocationX = Convert.ToInt32(data[0]);
                        playerLocationY = Convert.ToInt32( data[1]);

                    }
                }

                saveFile.Close();
            }
            else
            {
                //if the user doesn't have a saved level.
                ContinueButton.BackColor = Color.Gray;
                ContinueButton.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Menu newForm = new Menu();
            newForm.Visible = true;
            this.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //creates a new save file
            if (File.Exists("../../LevelSaveFile.txt"))
            {
                File.Delete("../../LevelSaveFile.txt");
            }
            File.Create("../../LevelSaveFile.txt").Close();

            TextBox compileBox = new TextBox();
            compileBox.Text = "hs:0" + Environment.NewLine;
            compileBox.Text += "ls:0";
            File.WriteAllText("../../LevelSaveFile.txt", compileBox.Text);


            Form1 newForm = new Form1();
            newForm.Visible = true;
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void SPMenu_Load(object sender, EventArgs e)
        {

        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            //read from the 
            Form1 newForm = new Form1(playerLocationX,playerLocationY,level,score);
            newForm.Visible = true;
            this.Close();
        }
    }
}
