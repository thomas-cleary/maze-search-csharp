using System;
using Mindmagma.Curses;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {

        // Debug_BFS();
        IntPtr screen = NCurses.InitScreen(); // Not sure if this is needed as member variable

        if(!Display.InitColors())
        {
            Display.End("Colour Pairs Could Not Be Initialised. Ending...");
        }

        MazeSearchSim();   

        Display.End("Thanks for watching! Goodbye...");
    }


    public static void Debug_BFS()
    {
        // Arrange
        double density = 0.0;
        int numRows = 4;
        int numCols = numRows;

        Maze testMaze = new Maze(numRows, numCols);
        testMaze.Setup(density);

        Search.BFS(testMaze, true);
    }


    private static void MazeSearchSim()
    {
        for (int i = 0; i < Constants.NumSims; i++)
        {
            Maze maze = new Maze(Constants.NumRows, Constants.NumCols);
            maze.Setup(Constants.MazeDensity);

            Maze unsearchedMaze = maze.DeepCopy();
            Search.BFS(unsearchedMaze, false);
            
            unsearchedMaze = maze.DeepCopy();
            Search.DFS(unsearchedMaze, false);

            Display.MySleep(Constants.IntermissionTime);
        }
    }

}
