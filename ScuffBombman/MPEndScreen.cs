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
Purpose: Shows who won in the multiplayer game.
*/
    public partial class MPEndScreen : Form
    {
        public MPEndScreen(int whichPlayerWon)
        {
            InitializeComponent();
            //Put the winning player on the screen
            winMessage.Text = "Congrats to Player " + whichPlayerWon + " for Winning!";

        }

        private void MPEndScreen_Load(object sender, EventArgs e)
        {
        }

        //Back to the main menu
        private void button1_Click(object sender, EventArgs e)
        {
            Menu newForm = new Menu();
            newForm.Visible = true;
            this.Close();
        }
    }
}
