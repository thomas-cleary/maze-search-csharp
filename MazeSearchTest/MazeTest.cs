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
        int[,] returnedMaze = squareMaze.GenerateNewOpenMaze();
        /* This happens on object instantiation,
           But to test we call it again to get the returned maze
        */

        // Assert
        foreach (var tile in returnedMaze) // returned maze
        {
            Assert.IsTrue(tile == (int) MazeTileNum.Undiscovered);
        }

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
}