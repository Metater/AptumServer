using System;
using System.Collections.Generic;
using System.Text;

namespace AptumServer.GameData
{
    public class AptumBoard
    {
        public int ownerId;

        public AptumBoard(int ownerId)
        {
            this.ownerId = ownerId;
        }
    }
}
