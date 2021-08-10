using AptumShared.Packets;
using System;
using System.Collections.Generic;
using System.Text;

namespace AptumClient.Interfaces
{
    public interface INetSendUpdate
    {
        void Send(RequestCreateLobbyPacket packet);
        void Send(RequestJoinLobbyPacket packet);
        void Send(RequestStartGamePacket packet);
        void Send(RequestPlacePiecePacket packet);
        void Send(RequestPlayAgainPacket packet);
    }
}
