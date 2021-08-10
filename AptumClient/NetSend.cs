using AptumClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AptumClient
{
    public sealed class NetSend
    {
        private INetSendUpdate netSender;

        public void SubscribeNetSender(INetSendUpdate netSender)
        {
            this.netSender = netSender;
        }
    }
}
