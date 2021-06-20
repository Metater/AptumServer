using System;
using System.Collections.Generic;
using System.Text;

namespace AptumServer.Utils
{
    public class PieceGenerator
    {
        private int seed;
        private Random rand;
        private List<(int, int)> cachedPieces = new List<(int, int)>();
        private int GreatestPieceIndexCached => cachedPieces.Count - 1;

        public PieceGenerator(int seed)
        {
            this.seed = seed;
            rand = new Random(seed);
        }

        public (int, int) GetPieceAtIndex(int index)
        {
            int numPiecesToGenerate = GreatestPieceIndexCached - index;
            if (numPiecesToGenerate > 0)
            {
                for (int i = 0; i < numPiecesToGenerate; i++)
                {
                    int pieceType = rand.Next(PieceDictionary.GetPieceCount() - 1);
                    int pieceColor = rand.Next(PieceDictionary.NumPieceColors - 1);
                    (int, int) piece = (pieceType, pieceColor);
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
