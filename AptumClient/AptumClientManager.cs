using AptumClient.Interfaces;

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
        internal IUISendUpdate uiSendUpdate;
        internal INetworkUpdate networkUpdate;

        public State State;
        public NetReceive NetReceive;
        public UIReceive UIReceive;
        public Graphics Graphics;
        public Network Network;

        public void Init(INetSendUpdate netSendUpdate, IGraphicsUpdate graphicsUpdate, IUISendUpdate uiSendUpdate, INetworkUpdate networkUpdate)
        {
            this.netSendUpdate = netSendUpdate;
            this.graphicsUpdate = graphicsUpdate;
            this.uiSendUpdate = uiSendUpdate;
            this.networkUpdate = networkUpdate;
            State = new State();
            NetReceive = new NetReceive();
            UIReceive = new UIReceive();
            Graphics = new Graphics();
            Network = new Network();

            networkUpdate.Connect("192.168.1.92", 12733, "Aptum");
        }
    }
}
