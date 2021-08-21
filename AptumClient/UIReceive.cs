using System;
using System.Collections.Generic;
using System.Text;
using AptumShared.Enums;

namespace AptumClient
{
    public sealed class UIReceive
    {
        public UIReceive()
        {

        }
        
        // Welcome
        public void SingleplayerButton()
        {
            AptumClientManager.I.uiSendUpdate.SetUIState(UIState.Game);
        }
        public void MultiplayerButton()
        {
            if (AptumClientManager.I.State.isConnected)
            {
                AptumClientManager.I.uiSendUpdate.DisplayMessage("Could not connect to servers", 5);
                return;
            }
            AptumClientManager.I.uiSendUpdate.SetUIState(UIState.Selection);
        }
        public void QuitButton()
        {
            AptumClientManager.I.uiSendUpdate.Quit();
        }

        // Selection
        public void CreateLobbyButton(string name)
        {
            if (!NameCheck(name)) return;

            // Request create lobby

            AptumClientManager.I.State.creatingLobby = true;
            AptumClientManager.I.uiSendUpdate.DisplayMessage("Creating Lobby...", 5);
        }
        public void JoinLobbyButton(string name, string joinCodeRaw)
        {
            if (!NameCheck(name)) return;

            if (int.TryParse(joinCodeRaw, out int joinCode))
            {
                if (joinCode < 0 || joinCode > 1000)
                {
                    AptumClientManager.I.uiSendUpdate.DisplayMessage("Join code out of range!", 5);
                    return;
                }

                // Request join

                AptumClientManager.I.State.joiningLobby = true;
                AptumClientManager.I.uiSendUpdate.DisplayMessage("Joining Lobby...", 5);
                AptumClientManager.I.State.currentJoinCode = joinCode;
            }
            else
            {
                AptumClientManager.I.uiSendUpdate.DisplayMessage("Failed to parse join code!", 5);
                return;
            }
        }
        private bool NameCheck(string name)
        {
            if (AptumClientManager.I.State.joiningLobby || AptumClientManager.I.State.creatingLobby)
            {
                AptumClientManager.I.uiSendUpdate.DisplayMessage("Already connecting to lobby!", 5);
                return false;
            }
            if (name == "")
            {
                AptumClientManager.I.uiSendUpdate.DisplayMessage("Enter your name!", 5);
                return false;
            }
            if (name.Length > 16)
            {
                AptumClientManager.I.uiSendUpdate.DisplayMessage("Name is too long!", 5);
                return false;
            }
            return true;
        }

        // GameOver
        public void PlayAgainButton()
        {

        }
    }
}
