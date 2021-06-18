using System;
using System.Collections.Generic;
using System.Text;

namespace AptumServer.GameData
{
    public class AptumGame : ITickable
    {
        private AptumServer aptumServer;

        public int leaderId;
        public string leaderName;
        public AptumBoard leaderBoard;
        public bool full = false;
        public int otherId;
        public string otherName;
        public AptumBoard otherBoard;

        public int pieceGenerationSeed;

        public AptumGame(AptumServer aptumServer, int leaderId, string leaderName)
        {
            this.aptumServer = aptumServer;
            this.leaderId = leaderId;
            this.leaderName = leaderName;
            leaderBoard = new AptumBoard(leaderId);
        }

        public void Join(int otherId, string otherName)
        {
            if (full) return;
            this.otherId = otherId;
            this.otherName = otherName;
            otherBoard = new AptumBoard(otherId);
            pieceGenerationSeed = aptumServer.rand.Next();
        }

        public void Tick(long id)
        {

        }

        public bool ContainsClientId(int id)
        {
            if (full)
            {
                if (id == leaderId || id == otherId) return true;
            }
            else
            {
                if (id == leaderId) return true;
            }
            return false;
        }
    }
}
