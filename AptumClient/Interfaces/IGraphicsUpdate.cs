using System;
using System.Collections.Generic;
using System.Text;

namespace AptumClient.Interfaces
{
    public interface IGraphicsUpdate
    {
        void ClearLine(int boardId);
        void PlacePiece(int boardId, (int, int) pos);
        void WipeBoard(int boardId);
    }
}
