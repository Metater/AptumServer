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
        internal IUIWriteUpdate uiWriteUpdate;
        internal INetworkUpdate networkUpdate;

        public AptumClientState AptumClientState;
        public NetReceive NetReceive;
        public NetSend NetSend;
        public UIRead UIRead;
        public UIWrite UIWrite;
        public Graphics Graphics;
        public Network Network;

        public void Init(INetSendUpdate netSendUpdate, IGraphicsUpdate graphicsUpdate, IUIWriteUpdate uiWriteUpdate, INetworkUpdate networkUpdate)
        {
            this.netSendUpdate = netSendUpdate;
            this.graphicsUpdate = graphicsUpdate;
            this.uiWriteUpdate = uiWriteUpdate;
            this.networkUpdate = networkUpdate;
            AptumClientState = new AptumClientState();
            NetReceive = new NetReceive();
            NetSend = new NetSend();
            UIRead = new UIRead();
            UIWrite = new UIWrite();
            Graphics = new Graphics();
            Network = new Network();

            networkUpdate.Connect("192.168.1.92", 12733, "Aptum");
        }
    }
}
