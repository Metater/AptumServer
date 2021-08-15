using AptumShared;
using System;
using System.Collections.Generic;
using System.Text;

namespace AptumServer
{
    public class AptumServerPlayer
    {
        public AptumPlayer player;
        public int gameJoinCode = -1;

        public AptumServerPlayer(AptumPlayer player)
        {
            this.player = player;
        }
    }
}
