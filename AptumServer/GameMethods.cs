using AptumServer.GameData;
using AptumShared;
using LiteNetLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace AptumServer
{
    public static class GameMethods
    {
        public static void BroadcastToPlayersInGame(AptumGame aptumGame, byte[] data, DeliveryMethod deliveryMethod)
        {
            foreach (AptumPlayer aptumPlayer in aptumGame.players)
            {
                //peerClientIdMap.GetPeer(aptumPlayer.id).Send(data, deliveryMethod);
            }
        }
    }
}
