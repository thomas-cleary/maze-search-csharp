using NUnit.Framework;
using System;

namespace MazeSearchTest;

public class MazeTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Maze_WithCorrectDimensions_MazeHasExpectedDimensions()
    {
        // Arrange
        int numRows = 32;
        int numCols = 64;

        // Act
        Maze maze = new Maze(numRows, numCols);

        // Assert
        Assert.IsTrue(numRows == maze.numRows);
        Assert.IsTrue(numCols == maze.numCols);
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
            Assert.IsTrue(tile == (int) MazeTileNum.Undiscovered);
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
            Assert.IsTrue(tile == (int) MazeTileNum.Wall);
        }

    }
}