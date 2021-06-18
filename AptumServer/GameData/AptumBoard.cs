using System;
using System.Collections.Generic;
using System.Text;

namespace AptumServer.GameData
{
    public class AptumBoard
    {
        public int ownerId;
        public bool[,] board = new bool[8, 8];

        public AptumBoard(int ownerId)
        {
            this.ownerId = ownerId;
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    board[x, y] = false;
                }
            }
        }

        public void PlaceCell((int, int) pos)
        {
            board[pos.Item1, pos.Item2] = true;
        }
        public void RemoveCell((int, int) pos)
        {
            board[pos.Item1, pos.Item2] = false;
        }

        public void PlacePiece((int, int) pos, List<(int, int)> cells)
        {
            foreach ((int, int) cellOffset in cells)
            {
                (int, int) offsetPos = (pos.Item1 + cellOffset.Item1, pos.Item2 + cellOffset.Item2);
                PlaceCell(offsetPos);
            }
        }

        public bool CheckCellEmptyAndValid((int, int) pos)
        {
            if (pos.Item1 < 0 || pos.Item1 > 8 || pos.Item2 < 0 || pos.Item2 > 8) return false;
            return board[pos.Item1, pos.Item2];
        }

        public bool CheckPieceFit((int, int) pos, List<(int, int)> cells)
        {
            foreach ((int, int) cellOffset in cells)
            {
                (int, int) offsetPos = (pos.Item1 + cellOffset.Item1, pos.Item2 + cellOffset.Item2);
                if (!CheckCellEmptyAndValid(offsetPos))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
