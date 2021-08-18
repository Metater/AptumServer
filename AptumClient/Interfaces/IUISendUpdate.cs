using AptumShared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AptumClient.Interfaces
{
    public interface IUISendUpdate
    {
        void DisplayMessage(string message, float duration);
        void DisplayJoinCode(int joinCode);
        void SetUIState(UIState uiState);
        void Quit();
    }
}
