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
Purpose: So we can use a 2d array of the Tiles class instead of an enum so we can use this in other objects.
*/

    class Tiles
    {
        public string tileType { get; }

        public Tiles(string typeOfTile)
        {
            tileType = typeOfTile;
        }
    }
}
