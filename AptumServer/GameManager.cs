﻿using AptumServer.GameData;
using AptumShared;
using System;
using System.Collections.Generic;
using System.Text;

namespace AptumServer
{
    public class GameManager
    {
        private AptumServer aptumServer;

        public List<AptumGame> games = new List<AptumGame>();
        public Dictionary<int, AptumGame> joinCodeGameMap = new Dictionary<int, AptumGame>();


        public GameManager(AptumServer aptumServer)
        {
            this.aptumServer = aptumServer;
        }


        public bool IsClientIdInGame(int id)
        {
            return games.Exists((aptumGame) => aptumGame.ContainsClientId(id));
        }

        public AptumGame CreateLobby(AptumPlayer leader)
        {
            AptumGame aptumGame = new AptumGame(aptumServer, leader);
            games.Add(aptumGame);
            return aptumGame;
        }

        public bool TryGetGameWithClientId(int id, out AptumGame outAptumGame)
        {
            outAptumGame = games.Find((aptumGame) => aptumGame.ContainsClientId(id));
            return outAptumGame != null;
        }

        public bool TryGetGameWithJoinCode(int joinCode, out AptumGame outAptumGame)
        {
            return joinCodeGameMap.TryGetValue(joinCode, out outAptumGame);
        }

        public bool TryJoinGame(AptumPlayer player, int joinCode, out AptumGame outAptumGame)
        {
            if (TryGetGameWithJoinCode(joinCode, out AptumGame aptumGame))
            {
                if (!aptumGame.started && !aptumGame.full)
                {
                    outAptumGame = aptumGame;
                    aptumGame.Join(player);
                    return true;
                }
            }
            outAptumGame = null;
            return false;
        }
    }
}
