using ConwaysGameOfLife;
using System;
using System.Text;
using System.Xml.Serialization;

public class Program
{

    static int DIM = 5;
    static IGameRuleStrategy currentRuleSet = new ConwayClassicRuleStrategy();
    static List<string> rulesetChoice = new();

    static void Main()
    {
        initializeRulesetChoice();
        List<string> choices = initializeRulesetChoice();

        Console.WriteLine("Please input the dimension of the n x n game board.");

        string userInputDimension = Console.ReadLine();

        int.TryParse(userInputDimension, out DIM);

        string userInputRuleset = "info";
        Console.WriteLine("Which Ruleset do you want to apply? Hit return without typing anything to continue with the default ruleset");

        while (userInputRuleset.Equals("info"))
        {
            if (userInputRuleset.Equals('?')) Console.WriteLine("Here is a full list of all possible rulesets: ");
            foreach (string choice in choices)
            {
                Console.WriteLine(choice);
            }

            userInputRuleset = Console.ReadLine();
            switch (userInputRuleset)
            {
                case "?": 
                    userInputRuleset = "info";
                    break;
                case "34/3": 
                    currentRuleSet = new ThreeFourToThreeRuleStrategy();
                    break;
                case "236/3":
                    currentRuleSet = new TwoThreeSixToThreeRuleStrategy();
                    break;
                case "c":
                default: 
                    currentRuleSet = new ConwayClassicRuleStrategy();
                    break;
            }
        }
        
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


        Cell[,] gameBoard = new Cell[DIM, DIM];
        initializeGameBoard(ref gameBoard);

        do
        {
            Console.Clear();
            Console.Write(printGameBoard(ref gameBoard));
            NextGeneration(ref gameBoard);
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

    public static string printGameBoard(ref Cell[,] gameBoard)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < gameBoard.GetLength(0); i++)
        {
            sb.Append("\n");
            for (int j = 0; j < gameBoard.GetLength(1); j++)
            {
                sb.Append(gameBoard[i,j].ToString());
            }
        }
        return sb.ToString();
    }

    private static void initializeGameBoard(ref Cell[,] gameBoard)
    {
        Random r = new Random();
        for (int i = 0; i < gameBoard.GetLength(0); i++)
        {
            for (int j = 0; j < gameBoard.GetLength(1); j++)
            {
                bool alive = r.Next(2) == 1 ? true : false;
                gameBoard[i, j] = new Cell(i,j,alive);
            }
        }
        AddAllNeighbours(ref gameBoard);
    }

    private static List<string> initializeRulesetChoice()
    {
        List<string> choices = new();
        choices.Add(nameof(ConwayClassicRuleStrategy) + ": c");
        choices.Add(nameof(ThreeFourToThreeRuleStrategy) + ": 34/3");
        choices.Add(nameof(TwoThreeSixToThreeRuleStrategy) + ": 236/3");

        return choices;
    }

    public static void AddAllNeighbours(ref Cell[,] gameBoard)
    {
        for(int i = 0; i < gameBoard.GetLength(0); i++)
        {
            for(int j = 0; j < gameBoard.GetLength(1); j++)
            {
                Cell currentCell = gameBoard[i, j];
                currentCell.Neighbours.Clear();
                //top left
                if (i > 0 && j > 0 && gameBoard[i - 1, j - 1].Alive)
                    currentCell.Neighbours.Add(gameBoard[i - 1, j - 1]);

                //top
                if (i > 0 && gameBoard[i - 1, j].Alive) 
                    currentCell.Neighbours.Add(gameBoard[i - 1, j]);

                //top right
                if (i > 0 && j < gameBoard.GetLength(1) - 1 && gameBoard[i - 1, j + 1].Alive) 
                    currentCell.Neighbours.Add(gameBoard[i - 1, j + 1]);

                //bottom right
                if (i < gameBoard.GetLength(0) - 1 && j < gameBoard.GetLength(1) - 1 && gameBoard[i + 1, j + 1].Alive)
                    currentCell.Neighbours.Add(gameBoard[i + 1, j + 1]);

                //bottom
                if (i < gameBoard.GetLength(0) - 1 && gameBoard[i + 1, j].Alive)
                    currentCell.Neighbours.Add(gameBoard[i + 1, j]);

                //bottom left
                if (i < gameBoard.GetLength(0) - 1 && j > 0 && gameBoard[i+1, j-1].Alive)
                    currentCell.Neighbours.Add(gameBoard[i + 1, j - 1]);

                //left
                if (j > 0 && gameBoard[i,j-1].Alive) 
                    currentCell.Neighbours.Add(gameBoard[i, j - 1]);

                //right
                if (j < gameBoard.GetLength(1) - 1 && gameBoard[i, j+1].Alive)
                    currentCell.Neighbours.Add(gameBoard[i, j + 1]);
            }
        }
    }

    public static void NextGeneration(ref Cell[,] gameBoard)
    {
        Cell[,] temp = new Cell[gameBoard.GetLength(0), gameBoard.GetLength(1)];

        
        for(int i = 0; i < gameBoard.GetLength(0); i++)
        {
            for(int j = 0; j < gameBoard.GetLength(1); j++)
            {
                ApplyRules(gameBoard[i,j]);
                temp[i,j] = gameBoard[i,j];
            }
        }
        gameBoard = temp;
        AddAllNeighbours(ref gameBoard);
    }

    public static void ApplyRules(Cell cell)
    {
        if (cell.Alive) currentRuleSet.DeathRule(cell);
        else currentRuleSet.BirthRule(cell);
    }
}