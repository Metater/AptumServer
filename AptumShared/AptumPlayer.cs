using System;
using System.Collections.Generic;
using System.Text;

namespace AptumShared
{
    public class AptumPlayer
    {
        public int id;
        public string name;
        public AptumBoard board;

        public AptumPlayer(int id, string name, AptumBoard board)
        {
            this.id = id;
            this.name = name;
            this.board = board;
        }
    }
}
