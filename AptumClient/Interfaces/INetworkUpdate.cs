using System;
using System.Collections.Generic;
using System.Text;

namespace AptumClient.Interfaces
{
    public interface INetworkUpdate
    {
        void Connect(string address, int port, string key);
    }
}
