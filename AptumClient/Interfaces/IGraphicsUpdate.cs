using AptumShared.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AptumClient.Interfaces
{
    public interface IGraphicsUpdate
    {
        void ClearLine(int boardIndex);
        void PlacePiece(int boardIndex, Piece piece, (int, int) pos);
        void WipeBoard(int boardIndex);
    }
}
