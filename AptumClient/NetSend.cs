using AptumClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AptumClient
{
    public sealed class NetSend
    {
        private INetSender netSender;

        public void SubscribeNetSender(INetSender netSender)
        {
            this.netSender = netSender;
        }
    }
}
