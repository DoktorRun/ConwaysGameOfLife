using ConwaysGameOfLife_UI.Abstractions;
using ConwaysGameOfLife_UI.Implementations;
using System;
using System.CodeDom.Compiler;
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
            //GameRule = new CopyWorld();
            GameRule = new ConwayClassicRuleset();
        }

        public void InitializeRandom()
        {
            Random r = new Random();
            for(int i = 0; i < Cells.GetLength(0); i++)
            {
                for(int j = 0; j < Cells.GetLength(1); j++)
                {
                    Cells[i,j] = false;
                    //Cells[i, j] = r.Next(2) == 1;
                }
            }
        }

        public void FlipCell(int i, int j)
        {
            Cells[i, j] = !Cells[i, j];
        }

        public async void NextGeneration()
        {
            bool[][] temp = new bool[Cells.GetLength(0)][];
            Task<bool[]>[] tasks = new Task<bool[]>[Cells.GetLength(0)];

            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                int rowIndex = i;
                temp[i] = new bool[Cells.GetLength(1)];
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    temp[i][j] = Cells[i, j];
                }
                tasks[i] = Task.Run(() => NextGenerationRow(rowIndex, temp[rowIndex])); //Hier der Fehler
            }

            await Task.WhenAll(tasks);

            for(int i = 0; i < Cells.GetLength(0); i++)
            {
                for(int j = 0; j < Cells.GetLength(1); j++)
                {
                    Cells[i,j] = tasks[i].Result[j];
                }
            }
        }

        public Task<bool[]> NextGenerationRow(int row, bool[] currentGen)
        {
            bool[] result = new bool[currentGen.Length];
            for(int i = 0; i < currentGen.Length; i++)
            {
                int cellNeighbourCount = CountNeighbours(row, i);
                if (currentGen[i]) result[i] = GameRule.DeathRule(cellNeighbourCount);
                else result[i] = GameRule.BirthRule(cellNeighbourCount);
            }
            return Task.FromResult(result);
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
