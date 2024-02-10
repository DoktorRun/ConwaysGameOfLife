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
            //Arrange
            int[,] gliderArray = new int[5, 5];
            gliderArray[2, 0] = 1;
            gliderArray[2, 1] = 1;
            gliderArray[2, 2] = 1;
            gliderArray[1, 2] = 1;
            gliderArray[0, 1] = 1;

            int[,] expectedAfterTwoGenerations = new int[5, 5];
            expectedAfterTwoGenerations[1, 2] = 1;
            expectedAfterTwoGenerations[2, 0] = 1;
            expectedAfterTwoGenerations[2, 2] = 1;
            expectedAfterTwoGenerations[3, 1] = 1;
            expectedAfterTwoGenerations[3, 2] = 1;

            //Act
            Program.nextGeneration(ref gliderArray);
            Program.nextGeneration(ref gliderArray);

            //Assert
            CollectionAssert.AreEqual(expectedAfterTwoGenerations, gliderArray);

        }

        [TestMethod]
        public void TestStaticObjectsAfterRandomGenerations()
        {
            //Arrange
            int[,] objects = new int[20, 20];

            //Block
            objects[0, 0] = 1;  //O O
            objects[0, 1] = 1;  //O O
            objects[1, 0] = 1;
            objects[1, 1] = 1;
            //Beehive
            objects[0, 5] = 1; //. O .
            objects[1, 4] = 1; //O . O 
            objects[1, 6] = 1; //O . O
            objects[2, 4] = 1; //. O .
            objects[2, 6] = 1;
            objects[3, 5] = 1;
            //Boat
            objects[0, 10] = 1; //. O O
            objects[0, 11] = 1; //O . O
            objects[1, 9] = 1; //. O .
            objects[1, 11] = 1;
            objects[2, 10] = 1;
            //Loaf
            objects[0, 16] = 1; //. . O .
            objects[1, 15] = 1; //. O . O
            objects[1, 17] = 1; //O . . O
            objects[2, 14] = 1; //. O O .
            objects[2, 17] = 1;
            objects[3, 15] = 1;
            objects[3, 16] = 1;
            //Tub
            objects[6, 1] = 1; //. O .
            objects[7, 0] = 1; //O . O
            objects[7, 2] = 1; //. O .
            objects[8, 1] = 1;
            //Barge
            objects[6, 7] = 1; //. . O .
            objects[7, 6] = 1; //. O . O
            objects[7, 8] = 1; //O . O .
            objects[8, 5] = 1; //. O . .
            objects[8, 7] = 1;
            objects[9, 6] = 1;
            //Pond
            objects[7, 12] = 1; //. O O .
            objects[7, 13] = 1; //O . . O
            objects[8, 11] = 1;  //O . . O
            objects[8, 14] = 1; //. O O .
            objects[9, 11] = 1;
            objects[9, 14] = 1;
            objects[10, 12] = 1;
            objects[10, 13] = 1;

            //Copy static objects
            int[,] expected = new int[20, 20];
            Array.Copy(objects, expected, objects.Length);

            //Act
            Random r = new Random();
            int rounds = r.Next(100);
            for(int roundCounter = 0; roundCounter < rounds; roundCounter++)
            {
                Program.nextGeneration(ref objects);
            }
            //Assert
            CollectionAssert.AreEqual(expected, objects);
        }

        [TestMethod]
        public void TestOscilatorsCycleTwo()
        {
            int[,] oscilators = new int[20, 20];

            //Blinker
            oscilators[0, 1] = 1;
            oscilators[1, 1] = 1;
            oscilators[2, 1] = 1;

            //Uhr
            oscilators[0, 5] = 1;
            oscilators[1, 5] = 1;
            oscilators[1, 7] = 1;
            oscilators[2, 4] = 1;
            oscilators[2, 6] = 1;
            oscilators[3, 6] = 1;

            //Kröte
            oscilators[6, 1] = 1;
            oscilators[6, 2] = 1;
            oscilators[7, 0] = 1;
            oscilators[8, 3] = 1;
            oscilators[9, 2] = 1;
            oscilators[9, 1] = 1;

            //Bipole
            oscilators[12, 0] = 1;
            oscilators[12, 1] = 1;
            oscilators[13, 0] = 1;
            oscilators[13, 1] = 1;
            oscilators[14, 2] = 1;
            oscilators[14, 3] = 1;
            oscilators[15, 2] = 1;
            oscilators[15, 3] = 1;

            //Tripole
            oscilators[12, 6] = 1;
            oscilators[12, 7] = 1;
            oscilators[13, 6] = 1;
            oscilators[14, 7] = 1;
            oscilators[14, 9] = 1;
            oscilators[15, 10] = 1;
            oscilators[16, 10] = 1;
            oscilators[16, 9] = 1;

            int[,] expectedEven = new int[oscilators.GetLength(0), oscilators.GetLength(1)];
            Array.Copy(oscilators, expectedEven, oscilators.Length);
            Random r = new Random();

            int rounds = r.Next(100);
            rounds *= 2;

            for(int roundCounter = 0; roundCounter < rounds; roundCounter++)
            {
                Program.nextGeneration(ref oscilators);
            }

            CollectionAssert.AreEqual(expectedEven, oscilators);
            Program.nextGeneration(ref oscilators);
            CollectionAssert.AreNotEqual(expectedEven, oscilators);
        }
    }
}