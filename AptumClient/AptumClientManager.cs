using AptumClient.Interfaces;
using System;

namespace AptumClient
{
    public sealed class AptumClientManager
    {
        private static readonly AptumClientManager instance = new AptumClientManager();
        static AptumClientManager() { }
        private AptumClientManager() { }
        public static AptumClientManager I { get { return instance; } }

        internal INetSendUpdate netSendUpdate;
        internal IGraphicsUpdate graphicsUpdate;

        public AptumClientState AptumClientState;
        public NetReceive NetReceive;
        public NetSend NetSend;
        public UIRead UIRead;
        public UIWrite UIWrite;
        public Graphics Graphics;

        public void Init()
        {
            AptumClientState = new AptumClientState();
            NetReceive = new NetReceive();
            NetSend = new NetSend();
            UIRead = new UIRead();
            UIWrite = new UIWrite();
            Graphics = new Graphics();
        }
    }
}
