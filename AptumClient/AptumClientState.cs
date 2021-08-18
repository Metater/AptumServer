using System;
using System.Collections.Generic;
using System.Text;

namespace AptumClient
{
    public class AptumClientState
    {
        public bool creatingLobby = false;
        public bool joiningLobby = false;
        public bool isConnected = false;
        public bool isInGame = false;
        public int currentJoinCode = -1;

        public int boardLayout = -1;
    }
}
