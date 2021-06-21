using System;
using System.Collections.Generic;
using System.Text;
using AptumShared.Structs;
using AptumShared.Utils;

namespace AptumShared
{
    public class AptumBoard
    {
        public bool[,] Board { get; private set; } = new bool[8, 8];
        public Piece[] PiecePool { get; private set; } = new Piece[3];
        public bool[] PiecePoolBool { get; private set; } = new bool[3];

        public event Action<(int, int)> OnPlaceCell;
        public event Action<(int, int)> OnRemoveCell;
        public event Action<int> OnWipedLines;
        public event Action OnFillPool;
        public event Action OnGameStuck;

        private PieceGenerator pieceGenerator;
        private int nextPieceIndex = 0;

        public AptumBoard()
        {

        }

        public void AddPieceGenerator(PieceGenerator pieceGenerator)
        {
            this.pieceGenerator = pieceGenerator;
            FillPiecePool();
        }

        public bool PlaceSlot(int slot, (int, int) pos)
        {
            if (!PiecePoolBool[slot]) return false;
            Piece piece = PiecePool[slot];
            if (CheckPieceFit(pos, piece)) PlacePiece(pos, piece);
            else return false;
            PiecePoolBool[slot] = false;
            if (CheckPiecePoolEmpty()) FillPiecePool();

            int wipes = TryWipe();
            if (wipes > 0) OnWipedLines?.Invoke(wipes);

            if (CheckStuck()) OnGameStuck?.Invoke();

            return true;
        }

        public void ResetBoard()
        {
            ClearBoard();
        }


        #region Board
        private void PlaceCell((int, int) pos)
        {
            Board[pos.Item1, pos.Item2] = true;
            OnPlaceCell?.Invoke(pos);
        }
        private void RemoveCell((int, int) pos)
        {
            Board[pos.Item1, pos.Item2] = false;
            OnRemoveCell?.Invoke(pos);
        }
        private void PlacePiece((int, int) pos, Piece piece)
        {
            foreach ((int, int) cellOffset in piece.cellOffsets)
            {
                (int, int) offsetPos = (pos.Item1 + cellOffset.Item1, pos.Item2 + cellOffset.Item2);
                PlaceCell(offsetPos);
            }
        }
        private void FillPiecePool()
        {
            for (int i = 0; i < 3; i++)
            {
                PiecePool[i] = pieceGenerator.GetPieceAtIndex(nextPieceIndex);
                nextPieceIndex++;
                PiecePoolBool[i] = true;
            }
            OnFillPool?.Invoke();
        }
        private void ClearBoard()
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    RemoveCell((x, y));
                }
            }
        }
        #endregion Board


        #region Checks
        private bool CheckCellValid((int, int) pos)
        {
            return pos.Item1 >= 0 && pos.Item1 <= 7 && pos.Item2 >= 0 && pos.Item2 <= 7;
        }
        private bool CheckCellEmptyAndValid((int, int) pos)
        {
            if (!CheckCellValid(pos)) return false;
            return !Board[pos.Item1, pos.Item2];
        }
        private bool CheckPieceFit((int, int) pos, Piece piece)
        {
            foreach ((int, int) cellOffset in piece.cellOffsets)
            {
                (int, int) offsetPos = (pos.Item1 + cellOffset.Item1, pos.Item2 + cellOffset.Item2);
                if (!CheckCellEmptyAndValid(offsetPos)) return false;
            }
            return true;
        }
        private bool CheckPieceFitOnBoard(Piece piece)
        {
            bool fits = false;
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (CheckPieceFit((x, y), piece)) fits = true;
                }
            }
            return fits;
        }
        private bool CheckLine(int index, bool horizontal)
        {
            if (horizontal)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (!Board[i, index]) return false;
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    if (!Board[index, i]) return false;
                }
            }
            return true;
        }
        private bool CheckPiecePoolEmpty()
        {
            bool empty = true;
            for (int i = 0; i < 3; i++)
            {
                if (PiecePoolBool[i]) empty = false;
            }
            return empty;
        }
        private bool CheckStuck()
        {
            for (int i = 0; i < 3; i++)
            {
                if (PiecePoolBool[i])
                {
                    if (CheckPieceFitOnBoard(PiecePool[i])) return false;
                }
            }
            return true;
        }
        #endregion Checks


        #region Wipes
        private void WipeLine(int index, bool horizontal)
        {
            if (horizontal)
            {
                for (int i = 0; i < 8; i++) RemoveCell((i, index));
            }
            else
            {
                for (int i = 0; i < 8; i++) RemoveCell((index, i));
            }
        }
        private int TryWipe()
        {
            int wipes = 0;
            for (int i = 0; i < 8; i++)
            {
                if (CheckLine(i, true))
                {
                    wipes++;
                    WipeLine(i, true);
                }
                if (CheckLine(i, false))
                {
                    wipes++;
                    WipeLine(i, false);
                }
            }
            return wipes;
        }
        #endregion Wipes
    }
}
