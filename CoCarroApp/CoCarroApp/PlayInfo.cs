using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoCarroApp
{
    public class PlayInfo
    {
        private Point point;

        public Point Point { get => point; set => point = value; }
        public int IndexCurrentPlayer { get => indexCurrentPlayer; set => indexCurrentPlayer = value; }

        private int indexCurrentPlayer;

        public PlayInfo(Point point,int currentplayer)
        {
            this.point = point;
            this.indexCurrentPlayer = currentplayer;
        }
        
    }
}
