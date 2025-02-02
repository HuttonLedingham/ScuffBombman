using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bombs
{

    /*
Name: Hutton Ledingham and Yaroslav Dolotov
Assignment: Super BomberMan
Due Date: May 6 2022
Purpose: user picks how many players for the multiplayer game.
*/
    public partial class MPMenu : Form
    {
        public MPMenu()
        {
            InitializeComponent();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void MPMenu_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int amountOfPlayers = 1;

            //grab the amount of players the user has selected.
            if (radioButton1.Checked)
            {
                amountOfPlayers = 2;
            }
            else if (radioButton2.Checked)
            {
                amountOfPlayers = 3;
            }
            else if (radioButton3.Checked)
            {
                amountOfPlayers = 4;
            }

             Form1 newForm = new Form1(amountOfPlayers);
            newForm.Visible = true;
            this.Close();
        }
    }
}
