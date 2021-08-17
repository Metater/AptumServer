using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using LiteNetLib;
using LiteNetLib.Utils;
using AptumShared.Packets;
using AptumServer.GameData;
using AptumShared;
using AptumShared.Enums;

namespace AptumServer
{
    public class AptumServerListener : INetEventListener
    {
        private AptumServer aptumServer;
        private NetManager server;
        private GameManager gameManager;

        public NetPacketProcessor packetProcessor = new NetPacketProcessor();

        public AptumServerListener(AptumServer aptumServer)
        {
            this.aptumServer = aptumServer;
            packetProcessor.SubscribeReusable<RequestCreateLobbyPacket, NetPeer>(OnRequestCreateLobbyReceived);
            packetProcessor.SubscribeReusable<RequestJoinLobbyPacket, NetPeer>(OnRequestJoinLobbyPacketReceived);
            packetProcessor.SubscribeReusable<RequestStartGamePacket, NetPeer>(OnRequestStartGameReceived);
            packetProcessor.SubscribeReusable<RequestPlacePiecePacket, NetPeer>(OnRequestPlacePieceReceived);
            packetProcessor.SubscribeReusable<RequestPlayAgainPacket, NetPeer>(OnRequestPlayAgainReceived);
            gameManager = aptumServer.gameManager;
        }

        public void AddNetManagerReference(NetManager server)
        {
            this.server = server;
        }

        #region NetworkEvents
        public void OnConnectionRequest(ConnectionRequest request)
        {
            if (server.ConnectedPeersCount < 100)
                request.AcceptIfKey("Aptum");
            else
                request.Reject();
        }
        public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
        {

        }
        public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
        {

        }
        public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            packetProcessor.ReadAllPackets(reader, peer);
            reader.Recycle();
        }
        public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
        {

        }
        public void OnPeerConnected(NetPeer peer)
        {
            int clientId = aptumServer.AddClient(peer);
            Console.WriteLine($"[Server (Client Connected)] Client connected, assigned id {clientId}");
        }
        public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            int clientId = aptumServer.peerClientIdMap.GetClientId(peer);
            Console.WriteLine($"[Server (Client Disconnected)] Client disconnected, with id {clientId}");
            aptumServer.KickClient(clientId);
        }
        #endregion NetworkEvents


        #region ReceivedPacketImplementation
        private void OnRequestCreateLobbyReceived(RequestCreateLobbyPacket packet, NetPeer peer)
        {
            int clientId = aptumServer.peerClientIdMap.GetClientId(peer);
            bool inGame = gameManager.IsClientIdInGame(clientId);
            if (inGame) return;
            if (aptumServer.TryGetPlayer(clientId, out AptumServerPlayer player))
            {
                AptumGame lobby = gameManager.CreateLobby(player.player);
                Console.WriteLine($"[Action (Created Lobby)] Client {clientId} named \"{packet.LeaderName}\" made a lobby");
                CreatedLobbyPacket createdLobbyPacket = new CreatedLobbyPacket
                { JoinCode = lobby.joinCode };
                peer.Send(packetProcessor.Write(createdLobbyPacket), DeliveryMethod.ReliableOrdered);
            }
        }
        private void OnRequestJoinLobbyPacketReceived(RequestJoinLobbyPacket packet, NetPeer peer)
        {
            int clientId = aptumServer.peerClientIdMap.GetClientId(peer);
            bool inGame = gameManager.IsClientIdInGame(clientId);
            if (inGame) return;
            AptumServerPlayer player;
            bool gotPlayer = aptumServer.TryGetPlayer(clientId, out player);
            if (gotPlayer && gameManager.TryJoinGame(player.player, packet.JoinCode, out AptumGame aptumGame))
            {
                JoinedLobbyPacket joinedLobbyPacket = new JoinedLobbyPacket();
                peer.Send(packetProcessor.Write(joinedLobbyPacket), DeliveryMethod.ReliableOrdered);

                UpdatePlayersPacket updatePlayersPacket = new UpdatePlayersPacket
                { PlayerIds = aptumGame.GetPlayerIds() };
                aptumServer.BroadcastToPlayersInGame(aptumGame, packetProcessor.Write(updatePlayersPacket), DeliveryMethod.ReliableOrdered);
            }
            else
            {
                DenyPacket denyPacket = new DenyPacket
                { DenyBitField = (long)DenyReason.JoinLobby };
                peer.Send(packetProcessor.Write(denyPacket), DeliveryMethod.ReliableOrdered);
            }
        }
        private void OnRequestStartGameReceived(RequestStartGamePacket packet, NetPeer peer)
        {
            //int clientId = aptumServer.peerClientIdMap.GetClientId(peer);
            //bool inGame = aptumServer.ClientIdInGame(clientId);
        }
        private void OnRequestPlacePieceReceived(RequestPlacePiecePacket packet, NetPeer peer)
        {
            /*
            int clientId = aptumServer.peerClientIdMap.GetClientId(peer);
            if (packet.SlotToPlacePieceFrom >= 0 && packet.SlotToPlacePieceFrom <= 3)
            {
                if (aptumServer.GetGameWithClientId(clientId, out AptumGame aptumGame))
                {
                    AptumPlayer aptumPlayer = aptumGame.GetPlayerFromId(clientId);
                    if (aptumPlayer.board.CheckPieceFit((packet.RootX, packet.RootY), PieceDictionary.GetPiece(aptumPlayer.piecePool[packet.SlotToPlacePieceFrom].Item1)))
                    {
                        aptumGame.PlacePieceFromSlot(clientId, packet.SlotToPlacePieceFrom, packet.RootX, packet.RootY);
                    }
                }
            }
            */
        }
        private void OnRequestPlayAgainReceived(RequestPlayAgainPacket packet, NetPeer peer)
        {
            //int clientId = aptumServer.peerClientIdMap.GetClientId(peer);
            //bool inGame = aptumServer.ClientIdInGame(clientId);
        }
        #endregion ReceivedPacketImplementation
    }
}
