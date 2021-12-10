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
    public void GetNewMaze_WithSquareMaze_ReturnsUndiscoveredMaze()
    {
        // Arrange
        int dimSize = 8;
        Maze squareMaze = new Maze(dimSize, dimSize);

        // Act
        int[,] returnedMaze = squareMaze.GetNewMaze();

        // Assert
        foreach (var tile in returnedMaze)
        {
            Assert.IsTrue(tile == (int) MazeTileNum.Undiscovered);
        }
    }
}