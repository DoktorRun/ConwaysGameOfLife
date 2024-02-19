using ConwaysGameOfLife;

namespace GameOfLifeTest
{
    [TestClass]
    public class FigureTest
    {
        private TestContext testContext;

        public TestContext TestContext
        {
            get { return testContext; }
            set { testContext = value; }
        }

        [TestMethod]
        public void TestGliderAfterTwoGenerations()
        {
            // Arrange
            Cell[,] gliderArray = new Cell[5, 5];
            gliderArray[2, 0] = new Cell(2, 0, true);
            gliderArray[2, 1] = new Cell(2, 1, true);
            gliderArray[2, 2] = new Cell(2, 2, true);
            gliderArray[1, 2] = new Cell(1, 2, true);
            gliderArray[0, 1] = new Cell(0, 1, true);

            Cell[,] expectedAfterTwoGenerations = new Cell[5, 5];
            expectedAfterTwoGenerations[1, 2] = new Cell(1, 2, true);
            expectedAfterTwoGenerations[2, 0] = new Cell(2, 0, true);
            expectedAfterTwoGenerations[2, 2] = new Cell(2, 2, true);
            expectedAfterTwoGenerations[3, 1] = new Cell(3, 1, true);
            expectedAfterTwoGenerations[3, 2] = new Cell(3, 2, true);

            for (int i = 0; i < gliderArray.GetLength(0); i++)
            {
                for (int j = 0; j < gliderArray.GetLength(1); j++)
                {
                    if (gliderArray[i, j] is null)
                    {
                        gliderArray[i, j] = new Cell(i, j);
                    }
                    if (expectedAfterTwoGenerations[i, j] is null)
                    {
                        expectedAfterTwoGenerations[i, j] = new Cell(i, j);
                    }
                }
            }

            Program.AddAllNeighbours(ref gliderArray);
            Program.AddAllNeighbours(ref expectedAfterTwoGenerations);

            // Act
            Program.NextGeneration(ref gliderArray);
            Program.NextGeneration(ref gliderArray);

            // Assert
            CollectionAssert.AreEqual(expectedAfterTwoGenerations, gliderArray);
        }

        [TestMethod]
        public void TestStaticObjectsAfterRandomGenerations()
        {
            // Arrange
            Cell[,] objects = new Cell[20, 20];

            // Block
            objects[0, 0] = new Cell(0, 0, true); //O O
            objects[0, 1] = new Cell(0, 1, true); //O O
            objects[1, 0] = new Cell(1, 0, true);
            objects[1, 1] = new Cell(1, 1, true);
            // Beehive
            objects[0, 5] = new Cell(0, 5, true); //. O .
            objects[1, 4] = new Cell(1, 4, true); //O . O 
            objects[1, 6] = new Cell(1, 6, true); //O . O
            objects[2, 4] = new Cell(2, 4, true); //. O .
            objects[2, 6] = new Cell(2, 6, true);
            objects[3, 5] = new Cell(3, 5, true);
            // Boat
            objects[0, 10] = new Cell(0, 10, true); //. O O
            objects[0, 11] = new Cell(0, 11, true); //O . O
            objects[1, 9] = new Cell(1, 9, true);   //. O .
            objects[1, 11] = new Cell(1, 11, true);
            objects[2, 10] = new Cell(2, 10, true);
            // Loaf
            objects[0, 16] = new Cell(0, 16, true); //. . O .
            objects[1, 15] = new Cell(1, 15, true); //. O . O
            objects[1, 17] = new Cell(1, 17, true); //O . . O
            objects[2, 14] = new Cell(2, 14, true); //. O O .
            objects[2, 17] = new Cell(2, 17, true);
            objects[3, 15] = new Cell(3, 15, true);
            objects[3, 16] = new Cell(3, 16, true);
            // Tub
            objects[6, 1] = new Cell(6, 1, true); //. O .
            objects[7, 0] = new Cell(7, 0, true); //O . O
            objects[7, 2] = new Cell(7, 2, true); //. O .
            objects[8, 1] = new Cell(8, 1, true);
            // Barge
            objects[6, 7] = new Cell(6, 7, true); //. . O .
            objects[7, 6] = new Cell(7, 6, true); //. O . O
            objects[7, 8] = new Cell(7, 8, true); //O . O .
            objects[8, 5] = new Cell(8, 5, true); //. O . .
            objects[8, 7] = new Cell(8, 7, true);
            objects[9, 6] = new Cell(9, 6, true);
            // Pond
            objects[7, 12] = new Cell(7, 12, true); //. O O .
            objects[7, 13] = new Cell(7, 13, true); //O . . O
            objects[8, 11] = new Cell(8, 11, true); //O . . O
            objects[8, 14] = new Cell(8, 14, true); //. O O .
            objects[9, 11] = new Cell(9, 11, true);
            objects[9, 14] = new Cell(9, 14, true);
            objects[10, 12] = new Cell(10, 12, true);
            objects[10, 13] = new Cell(10, 13, true);

            // Copy static objects
            Cell[,] expected = new Cell[20, 20];
            Array.Copy(objects, expected, objects.Length);

            for (int i = 0; i < objects.GetLength(0); i++)
            {
                for (int j = 0; j < objects.GetLength(0); j++)
                {
                    if (objects[i, j] is null)
                    {
                        objects[i, j] = new Cell(i, j);
                    }
                    if (expected[i, j] is null)
                    {
                        expected[i, j] = new Cell(i, j);
                    }
                }
            }

            Program.AddAllNeighbours(ref objects);
            Program.AddAllNeighbours(ref expected);

            // Act
            Random r = new Random();
            int rounds = r.Next(100);
            for (int roundCounter = 0; roundCounter < rounds; roundCounter++)
            {
                Program.NextGeneration(ref objects);
            }

            // Assert
            CollectionAssert.AreEqual(expected, objects);
        }

        [TestMethod]
        public void TestOscillatorsCycleTwo()
        {
            Cell[,] oscillators = new Cell[20, 20];

            // Blinker
            oscillators[0, 1] = new Cell(0, 1, true);
            oscillators[1, 1] = new Cell(1, 1, true);
            oscillators[2, 1] = new Cell(2, 1, true);

            // Uhr
            oscillators[0, 5] = new Cell(0, 5, true);
            oscillators[1, 5] = new Cell(1, 5, true);
            oscillators[1, 7] = new Cell(1, 7, true);
            oscillators[2, 4] = new Cell(2, 4, true);
            oscillators[2, 6] = new Cell(2, 6, true);
            oscillators[3, 6] = new Cell(3, 6, true);

            // Kröte
            oscillators[6, 1] = new Cell(6, 1, true);
            oscillators[6, 2] = new Cell(6, 2, true);
            oscillators[7, 0] = new Cell(7, 0, true);
            oscillators[8, 3] = new Cell(8, 3, true);
            oscillators[9, 2] = new Cell(9, 2, true);
            oscillators[9, 1] = new Cell(9, 1, true);

            // Bipole
            oscillators[12, 0] = new Cell(12, 0, true);
            oscillators[12, 1] = new Cell(12, 1, true);
            oscillators[13, 0] = new Cell(13, 0, true);
            oscillators[13, 1] = new Cell(13, 1, true);
            oscillators[14, 2] = new Cell(14, 2, true);
            oscillators[14, 3] = new Cell(14, 3, true);
            oscillators[15, 2] = new Cell(15, 2, true);
            oscillators[15, 3] = new Cell(15, 3, true);

            // Tripole
            oscillators[12, 6] = new Cell(12, 6, true);
            oscillators[12, 7] = new Cell(12, 7, true);
            oscillators[13, 6] = new Cell(13, 6, true);
            oscillators[14, 7] = new Cell(14, 7, true);
            oscillators[14, 9] = new Cell(14, 9, true);
            oscillators[15, 10] = new Cell(15, 10, true);
            oscillators[16, 10] = new Cell(16, 10, true);
            oscillators[16, 9] = new Cell(16, 9, true);

            Cell[,] expectedEven = new Cell[oscillators.GetLength(0), oscillators.GetLength(1)];
            Array.Copy(oscillators, expectedEven, oscillators.Length);
            Random r = new Random();

            for (int i = 0; i < oscillators.GetLength(0); i++)
            {
                for (int j = 0; j < oscillators.GetLength(0); j++)
                {
                    if (oscillators[i, j] is null)
                    {
                        oscillators[i, j] = new Cell(i, j);
                    }
                    if (expectedEven[i, j] is null)
                    {
                        expectedEven[i, j] = new Cell(i, j);
                    }
                }
            }

            Program.AddAllNeighbours(ref oscillators);
            Program.AddAllNeighbours(ref expectedEven);

            int rounds = r.Next(100);
            rounds *= 2;

            for (int roundCounter = 0; roundCounter < rounds; roundCounter++)
            {
                Program.NextGeneration(ref oscillators);
            }

            CollectionAssert.AreEqual(expectedEven, oscillators);
            Program.NextGeneration(ref oscillators);
            CollectionAssert.AreNotEqual(expectedEven, oscillators);
        }

        [TestMethod]
        public void TestThreeFourToThreeCustomRuleOnConeFigure()
        {
            Program.currentRuleSet = new ThreeFourToThreeRuleStrategy();
            Cell[,] gameBoard = new Cell[5, 5];
            gameBoard[0, 0] = new Cell(0, 0, true); // O . .
            gameBoard[1, 1] = new Cell(1, 1, true); // . O O
            gameBoard[1, 2] = new Cell(1, 2, true); // . O
            gameBoard[2, 1] = new Cell(2, 1, true);




            Cell[,] expectedOdd = new Cell[5, 5];
            expectedOdd[0, 1] = new Cell(0, 1, true); // . O .
            expectedOdd[1, 1] = new Cell(1, 1, true); // O O .
            expectedOdd[1, 0] = new Cell(1, 0, true); // . . O
            expectedOdd[2, 2] = new Cell(2, 2, true);

            Cell[,] expectedEven = new Cell[5, 5];
            Array.Copy(gameBoard, expectedEven, gameBoard.Length);

            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    if (gameBoard[i, j] is null)
                    {
                        gameBoard[i, j] = new Cell(i, j);
                    }
                    if (expectedEven[i, j] is null)
                    {
                        expectedEven[i, j] = new Cell(i, j);
                    }
                    if (expectedOdd[i, j] is null)
                    {
                        expectedOdd[i, j] = new Cell(i, j);
                    }
                }
            }

            Program.AddAllNeighbours(ref gameBoard);
            Program.AddAllNeighbours(ref expectedEven);
            Program.AddAllNeighbours(ref expectedOdd);

            Random r = new Random();
            int rounds = r.Next(1, 10);
            for (int roundCounter = 0; roundCounter < rounds; roundCounter++)
            {
                Program.NextGeneration(ref gameBoard);
            }
            if (rounds % 2 == 0)
            {
                CollectionAssert.AreEqual(expectedEven, gameBoard);
            }
            else if (rounds % 2 == 1)
            {
                CollectionAssert.AreEqual(expectedOdd, gameBoard);
            }
        }
    }


}
