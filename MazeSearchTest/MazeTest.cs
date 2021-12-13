using NUnit.Framework;
using System;

namespace MazeSearchTest;

public class MazeTests
{
    [SetUp]
    public void Setup()
    {}

    [Test]
    public void Maze_WithCorrectDimensions_MazeHasExpectedDimensions()
    {
        // Arrange
        int numRows = 32;
        int numCols = 64;

        // Act
        Maze maze = new Maze(numRows, numCols);

        // Assert
        Assert.AreEqual(numRows, maze.numRows);
        Assert.AreEqual(numCols, maze.numCols);
    }

    [Test]
    public void GenerateNewOpenMaze_WithSquareMaze_ReturnsUndiscoveredMaze()
    {
        // Arrange
        int dimSize = 8;
        Maze squareMaze = new Maze(dimSize, dimSize);

        // Act
        squareMaze.GenerateNewOpenMaze();


        // Assert
        foreach (var tile in squareMaze.maze) // member variable
        {
            Assert.AreEqual(tile, (int) MazeTileNum.Undiscovered);
        }
    }

    [Test]
    public void AddWalls_WithDensity100_MazeShouldBeAllWall()
    {
        // Arrange
        double density = 1.0;
        int dimSize    = 4;
        Maze testMaze  = new Maze(dimSize, dimSize);

        // Act
        testMaze.AddWalls(density);

        // Assert
        foreach (var tile in testMaze.maze) // member variable
        {
            Assert.AreEqual(tile, (int) MazeTileNum.Wall);
        }
    }

    [Test]
    public void AddWalls_WithDensity0_MazeShouldBeAllUndiscovered()
    {
        // Arrange
        double density = 0.0;
        int dimSize    = 4;
        Maze testMaze  = new Maze(dimSize, dimSize);

        // Act
        testMaze.AddWalls(density);

        // Assert
        foreach (var tile in testMaze.maze) // member variable
        {
            Assert.AreEqual(tile, (int) MazeTileNum.Undiscovered);
        }
    }

    [Test]
    public void AddWalls_WithDensity50_MazeShouldBeHalfWalls()
    {
        // Arrange
        double density = 0.5;
        int dimSize    = 4;

        int expectedNumWalls = (int) ((dimSize * dimSize) * density);
        int wallsFound       = 0;

        Maze testMaze  = new Maze(dimSize, dimSize);

        // Act
        testMaze.AddWalls(density);

        // Assert
        foreach (var tile in testMaze.maze) // member variable
        {
            if (tile == (int) MazeTileNum.Wall)
            {
                wallsFound++;
            }
        }
        Assert.AreEqual(wallsFound, expectedNumWalls);

    }

    [Test]
    public void AddWalls_WithDensity30_MazeShouldBeHalfWalls()
    {
        // Arrange
        double density = 0.3; // Density used in original python program
        int dimSize    = 4;

        int expectedNumWalls = (int) ((dimSize * dimSize) * density);
        int wallsFound       = 0;

        Maze testMaze  = new Maze(dimSize, dimSize);

        // Act
        testMaze.AddWalls(density);

        // Assert
        foreach (var tile in testMaze.maze) // member variable
        {
            if (tile == (int) MazeTileNum.Wall)
            {
                wallsFound++;
            }
        }
        Assert.AreEqual(wallsFound, expectedNumWalls);
    }

    [Test]
    public void AddGoal_WithMazeDensity30_ReturnedGoalSameAsActual()
    {
        // Arrange
        double density = 0.3;
        int numRows    = 16;
        int numCols    = numRows * 2;

        (int row, int col) foundGoal = (-1, -1);

        Maze testMaze = new Maze(numRows, numCols);
        testMaze.AddWalls(density);

        // Act
        (int row, int col) returnedGoal = testMaze.AddGoal();
 
        // Assert
        for (int rowNum = 0; rowNum < numRows; rowNum++)
        {
            for (int colNum = 0; colNum < numCols; colNum++)
            {
                var currTile = testMaze.maze[rowNum, colNum];
                if (currTile  == (int) MazeTileNum.Goal)
                {
                    foundGoal = (rowNum, colNum);
                }
            }
            if (foundGoal.row > -1 || foundGoal.col > -1) break; // Found the goal already
        }

        // Assert
        Assert.AreEqual(foundGoal, returnedGoal);

    }

    [Test]
    public void AddGoal_WithMazeDensity100_ThrowsInvalidOperationExceptionAndMazeHasNoGoal()
    {
        // Arrange
        double density = 1.0;
        int numRows    = 16;
        int numCols    = numRows * 2;

        Maze testMaze = new Maze(numRows, numCols);
        testMaze.AddWalls(density);

        // Act / Assert
        Assert.Throws<InvalidOperationException>(() => testMaze.AddGoal());
    }
}
