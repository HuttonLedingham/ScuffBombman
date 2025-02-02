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
    public partial class DeathScreen : Form
    {

        /*
Name: Hutton Ledingham and Yaroslav Dolotov
Assignment: Super BomberMan
Due Date: May 6 2022
Purpose: Tell the player they died and display the score they got.
*/
        public DeathScreen(int score, int level)
        {
            InitializeComponent();

            ScoreLabel.Text = "Score: " + score;
            LevelLabel.Text = "Level: " + level;

            //delete this file as it's not useful anymore

            File.Delete("../../LevelSaveFile.txt");

            string lineOfText;
            string[] data;
            TextBox compileBox = new TextBox();

            if (!File.Exists("../../UserSaveFile.txt"))
            {
                File.Create("../../UserSaveFile.txt").Close();

                compileBox.Text = "hs:0" + Environment.NewLine ;
                compileBox.Text += "ls:0" + Environment.NewLine;

                File.WriteAllText("../../SaveFile.txt", compileBox.Text);
            }

            StreamReader saveFile = new StreamReader("../../UserSaveFile.txt");

            while (!saveFile.EndOfStream)
            {
                lineOfText = saveFile.ReadLine();

                
                if (lineOfText[0] == 'h' && lineOfText[1] == 's')
                {
                    data = lineOfText.Split(':');
                    //check if the game score is higher than the current highscore
                    if (score > Convert.ToInt32(data[1]))
                    {
                        compileBox.Text += "hs:" + score + Environment.NewLine;
                    }
                    else
                    {
                        compileBox.Text += lineOfText + Environment.NewLine;
                    }
                }
                else if (lineOfText[0] == 'l' && lineOfText[1] == 's')
                {
                    data = lineOfText.Split(':');

                    //check if the game level is higher than the current highest level
                    if (level > Convert.ToInt32(data[1]))
                    {
                        compileBox.Text += "ls:" + level;
                    }
                    else
                    {
                        compileBox.Text += lineOfText + Environment.NewLine;

                    }
                }
            }

            saveFile.Close();

            //paste it all into the text file
            File.WriteAllText("../../UserSaveFile.txt", compileBox.Text);
        }

        private void DeathScreen_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //go back to the menu
            SPMenu newForm = new SPMenu();
            newForm.Visible = true;
            this.Close();
        }
    }
}
