using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoCarroApp
{
    public class player
    {
        private string namePlayer;
        private Image mark;
        public string NamePlayer { get => namePlayer; set => namePlayer = value; }
        public Image Mark { get => mark; set => mark = value; }
        public player(string name,Image mark)
        {
            this.NamePlayer = name;
            this.Mark = mark;
        }
    }
}
