using AptumShared.Enums;
using AptumShared.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AptumShared.Utils
{
    public class PieceGenerator
    {
        private Random rand;
        private List<Piece> cachedPieces = new List<Piece>();
        private int GreatestPieceIndexCached => cachedPieces.Count - 1;

        public PieceGenerator(int seed)
        {
            rand = new Random(seed);
        }

        public Piece GetPieceAtIndex(int index)
        {
            int numPiecesToGenerate = index - GreatestPieceIndexCached;
            if (numPiecesToGenerate > 0)
            {
                for (int i = 0; i < numPiecesToGenerate; i++)
                {
                    int type = rand.Next(PieceDictionary.GetPieceCount() - 1);
                    int color = rand.Next(PieceDictionary.NumPieceColors - 1);
                    Piece piece = PieceDictionary.GetPiece((PieceType)type);
                    piece.SetColor((ColorType)color);
                    cachedPieces.Add(piece);
                }
                return cachedPieces[index];
            }
            else
            {
                return cachedPieces[index];
            }
        }
    }
}
