using AptumShared.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AptumClient.Interfaces
{
    public interface IGraphicsUpdate
    {
        void PlacePiece(int boardIndex, Piece piece, (int, int) pos);
        void WipeLine(int boardIndex, int index, bool horizontal);
        void WipeBoard(int boardIndex);
    }
}
