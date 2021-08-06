using System;

namespace AptumClient
{
    public sealed class AptumClientManager
    {
        private static readonly AptumClientManager instance = new AptumClientManager();
        static AptumClientManager() { }
        private AptumClientManager() { }
        public static AptumClientManager I { get { return instance; } }


        public AptumClientState AptumClientState = new AptumClientState();
        public NetReceive NetReceive = new NetReceive();
        public NetSend NetSend = new NetSend();
        public UIIn UIIn = new UIIn();
        public UIOut UIOut = new UIOut();
    }
}
