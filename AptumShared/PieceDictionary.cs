using System;
using System.Collections.Generic;
using AptumShared.Enums;
using AptumShared.Structs;

namespace AptumShared
{
    public class PieceDictionary
    {
        public const int NumPieceColors = 5;

        private static PieceDictionary instance;

        private static Dictionary<PieceType, List<(int, int)>> pieces = new Dictionary<PieceType, List<(int, int)>>();

        private PieceDictionary()
        {
            pieces.Add(PieceType.Dot1x1, new List<(int, int)> { (0, 0) });
            pieces.Add(PieceType.Bar1x2, new List<(int, int)> { (0, 0), (0, 1) });
            pieces.Add(PieceType.Bar1x3, new List<(int, int)> { (0, 0), (0, 1), (0, 2) });
            pieces.Add(PieceType.Bar1x4, new List<(int, int)> { (0, 0), (0, 1), (0, 2), (0, 3) });
            pieces.Add(PieceType.Pipe2x1, new List<(int, int)> { (0, 0), (1, 0) });
            pieces.Add(PieceType.Pipe3x1, new List<(int, int)> { (0, 0), (1, 0), (2, 0) });
            pieces.Add(PieceType.Pipe4x1, new List<(int, int)> { (0, 0), (1, 0), (2, 0), (3, 0) });
            pieces.Add(PieceType.Box2x2, new List<(int, int)> { (0, 0), (1, 0), (0, 1), (1, 1) });
            pieces.Add(PieceType.J2x2, new List<(int, int)> { (0, 0), (1, 0), (1, 1) });
            pieces.Add(PieceType.RJ2x2, new List<(int, int)> { (0, 0), (1, 0), (0, 1) });
        }

        private static void CheckNull()
        {
            if (instance == null)
            {
                instance = new PieceDictionary();
            }
        }

        public static Piece GetPiece(PieceType piece)
        {
            CheckNull();
            return new Piece(piece, pieces[piece]);
        }
        public static int GetPieceCount()
        {
            CheckNull();
            return pieces.Count;
        }
    }
}
