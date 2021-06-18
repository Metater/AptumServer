using System;
using System.Collections.Generic;
using System.Text;
using AptumServer.Types;

namespace AptumServer.Utils
{
    public class PieceDictionary
    {
        public const int NumPieceColors = 5;

        private static PieceDictionary instance;

        private static Dictionary<int, List<(int, int)>> pieces = new Dictionary<int, List<(int, int)>>();

        private PieceDictionary()
        {
            pieces.Add((int)PieceType.Dot1x1, new List<(int, int)> { (0, 0) });
            pieces.Add((int)PieceType.Bar1x2, new List<(int, int)> { (0, 0), (0, 1) });
            pieces.Add((int)PieceType.Bar1x3, new List<(int, int)> { (0, 0), (0, 1), (0, 2) });
            pieces.Add((int)PieceType.Bar1x4, new List<(int, int)> { (0, 0), (0, 1), (0, 2), (0, 3) });
        }

        private static void CheckNull()
        {
            if (instance == null)
            {
                instance = new PieceDictionary();
            }
        }

        public static List<(int, int)> GetPiece(int type)
        {
            CheckNull();
            return pieces[type];
        }
        public static int GetPieceCount()
        {
            CheckNull();
            return pieces.Count;
        }
    }
}
