using AptumShared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AptumShared.Structs
{
    public struct Piece
    {
        public PieceType piece;
        public ColorType color;
        public List<(int, int)> cellOffsets;

        public Piece(PieceType piece, List<(int, int)> cellOffsets)
        {
            this.piece = piece;
            color = ColorType.Red;
            this.cellOffsets = cellOffsets;
        }

        public void SetColor(ColorType color)
        {
            this.color = color;
        }
    }
}
