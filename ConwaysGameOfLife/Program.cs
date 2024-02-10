using System;
using System.Text;

public class Program
{

    static int DIM = 5;

    static void Main()
    {
        Console.WriteLine("Please input the dimension of the n x n game board.");

        string userInputDimension = Console.ReadLine();

        int.TryParse(userInputDimension, out DIM);

        Console.WriteLine("If you want to manually step through each generation, input 'm'");
        Console.WriteLine("If you want to automatically step through each generation, input 'a'");
        Console.WriteLine("Please choose your preferred render mode now: ");
        char renderMode = Console.ReadLine().ToLower().ElementAt(0);
        int sleepIntervall = 20;
        if (renderMode == 97)
        {
            Console.WriteLine("automatic rendering requires an aditional waiting timer in milli seconds. Please input waiting intervall: ");
            int.TryParse(Console.ReadLine(), out sleepIntervall);
        }


        int[,] gameBoard = new int[DIM, DIM];
        initializeGameBoard(ref gameBoard);

        do
        {
            Console.Clear();
            Console.Write(printGameBoard(ref gameBoard));
            nextGeneration(ref gameBoard);
            if(renderMode == 97)
            {
                Thread.Sleep(sleepIntervall);
            }
            else
            {
                if (Console.ReadLine().ToLower().Equals("q"))
                {
                    break;
                }
                else
                {
                    continue;
                }

            }
        }
        while(true);
    }

    public static string printGameBoard(ref int[,] gameBoard)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < gameBoard.GetLength(0); i++)
        {
            sb.Append("\n");
            for (int j = 0; j < gameBoard.GetLength(1); j++)
            {
                if (gameBoard[i, j] == 0) sb.Append(" . ");
                else sb.Append(" O ");
            }
        }
        return sb.ToString();
    }

    private static void initializeGameBoard(ref int[,] gameBoard)
    {
        Random r = new Random();
        for (int i = 0; i < gameBoard.GetLength(0); i++)
        {
            for (int j = 0; j < gameBoard.GetLength(1); j++)
            {
                gameBoard[i, j] = r.Next(2);
            }
        }
    }

    public static void nextGeneration(ref int[,] gameBoard)
    {
        int[,] temp = new int[gameBoard.GetLength(0), gameBoard.GetLength(1)];

        
        for(int i = 0; i < gameBoard.GetLength(0); i++)
        {
            for(int j = 0; j < gameBoard.GetLength(1); j++)
            {
                int neighbourCount = CountNeighbours(i, j, ref gameBoard);
                temp[i,j] = ApplyRules(i, j, neighbourCount, ref gameBoard);
            }
        }
        gameBoard = temp;
    }

    static int ApplyRules(int i, int j, int neighbourCount, ref int[,] gameBoard)
    {
        if (gameBoard[i,j] == 0)
        {
            if (neighbourCount == 3)
            {
                return 1;
            }
            else return 0;
        }
        else if (gameBoard[i,j] == 1)
        {
            if (neighbourCount < 2)
            {
                return 0;
            }
            else if (neighbourCount > 3)
            {
                return 0;
            }
            else return 1;
        }
        return 0;
    }

    static int CountNeighbours(int i, int j, ref int[,] gameBoard)
    {
        int neighbourCount = 0;

        //top left
        if(i > 0 && j > 0) neighbourCount += gameBoard[i - 1, j - 1] == 1 ? 1 : 0;
        //top
        if (i > 0) neighbourCount += gameBoard[i - 1, j] == 1 ? 1 : 0;
        //top right
        if (i > 0 && j < gameBoard.GetLength(1)-1) neighbourCount += gameBoard[i - 1, j + 1] == 1 ? 1 : 0;
        //bottom right
        if (i < gameBoard.GetLength(0)-1 && j < gameBoard.GetLength(1)-1) neighbourCount += gameBoard[i + 1, j + 1] == 1 ? 1 : 0;
        //bottom
        if (i < gameBoard.GetLength(0)-1) neighbourCount += gameBoard[i + 1, j] == 1 ? 1 : 0;
        //bottom left
        if (i < gameBoard.GetLength(0)-1 && j > 0) neighbourCount += gameBoard[i + 1, j - 1] == 1 ? 1 : 0;
        //left
        if (j > 0) neighbourCount += gameBoard[i, j - 1] == 1 ? 1 : 0;
        //right
        if (j < gameBoard.GetLength(1)-1) neighbourCount += gameBoard[i, j + 1] == 1 ? 1 : 0;

        return neighbourCount;
    }
}