using System;
using Mindmagma.Curses;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        IntPtr screen = NCurses.InitScreen(); // Not sure if this is needed as member variable

        if(!Display.InitColors())
        {
            Display.End("Colour Pairs Could Not Be Initialised. Ending...");
        }

        MazeSearchSim();   

        Display.End("Thanks for watching! Goodbye...");
    }


    private static void MazeSearchSim()
    {
        for (int i = 0; i < 10; i++)
        {
            Maze maze = new Maze(Constants.NumRows, Constants.NumCols);
            maze.Setup(Constants.MazeDensity);

            Maze unsearchMaze = maze.DeepCopy();

            Display.DisplayMaze(maze);
            Display.MySleep(Constants.IntermissionTime);
        }
    }

}
