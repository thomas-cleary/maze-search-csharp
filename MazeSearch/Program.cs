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
        for (int i = 0; i < Constants.NumSims; i++)
        {
            Maze maze = new Maze(Constants.NumRows, Constants.NumCols);
            maze.Setup(Constants.MazeDensity);

            Maze unsearchedMaze = maze.DeepCopy();
            bool goalReachable = Search.Astar(unsearchedMaze);
            Display.MySleep(Constants.IntermissionTime);

            if (goalReachable)
            {
                unsearchedMaze = maze.DeepCopy();
                Search.BFS(unsearchedMaze);
                Display.MySleep(Constants.IntermissionTime);
                
                unsearchedMaze = maze.DeepCopy();
                Search.DFS(unsearchedMaze);
                Display.MySleep(Constants.IntermissionTime);
            }
            else{
                i--;
            }
        }
    }
}
