using ConwaysGameOfLife_UI.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife_UI.Models
{
    public class GameModel
    {
        public bool[,] Cells { get; set; }

        public IGameRule GameRule { get; set; }

        public GameModel(int width, int height)
        {
            Cells = new bool[width, height];
            InitializeRandom();
        }

        public void InitializeRandom()
        {
            Random r = new Random();
            for(int i = 0; i < Cells.GetLength(0); i++)
            {
                for(int j = 0; j < Cells.GetLength(1); j++)
                {
                    Cells[i, j] = r.Next(2) == 1;
                }
            }
        }

        public void NextGeneration()
        {
            bool[,] temp = new bool[Cells.GetLength(0), Cells.GetLength(1)];


            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    int cellNeighbourCount = CountNeighbours(i, j);
                    if (Cells[i, j]) GameRule.DeathRule(cellNeighbourCount);
                    else GameRule.BirthRule(cellNeighbourCount);

                    temp[i, j] = Cells[i, j];
                }
            }
            Cells = temp;
        }

        private int CountNeighbours(int i, int j)
        {
            int neighbourCount = 0;

            //top left
            if (i > 0 && j > 0) neighbourCount += Cells[i - 1, j - 1] ? 1 : 0;
            //top
            if (i > 0) neighbourCount += Cells[i - 1, j] ? 1 : 0;
            //top right
            if (i > 0 && j < Cells.GetLength(1) - 1) neighbourCount += Cells[i - 1, j + 1] ? 1 : 0;
            //bottom right
            if (i < Cells.GetLength(0) - 1 && j < Cells.GetLength(1) - 1) neighbourCount += Cells[i + 1, j + 1] ? 1 : 0;
            //bottom
            if (i < Cells.GetLength(0) - 1) neighbourCount += Cells[i + 1, j] ? 1 : 0;
            //bottom left
            if (i < Cells.GetLength(0) - 1 && j > 0) neighbourCount += Cells[i + 1, j - 1] ? 1 : 0;
            //left
            if (j > 0) neighbourCount += Cells[i, j - 1] ? 1 : 0;
            //right
            if (j < Cells.GetLength(1) - 1) neighbourCount += Cells[i, j + 1] ? 1 : 0;

            return neighbourCount;
        }
    }
}
