using System;
using Mindmagma.Curses;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        IntPtr screen = NCurses.InitScreen(); // Not sure if this is needed as member variable

        if(!InitColors())
        {
            End("Colour Pairs Could Not Be Initialised. Ending...");
        }

        MazeSearchSim();   

        End("Thanks for watching! Goodbye...");
    }


    private static void AddChar(char ch)
    {
        NCurses.AttributeSet(NCurses.ColorPair(1)); // Make these numbers an enum
        NCurses.AddChar(ch);
    }


    private static void AddChar(char ch, short colorPair)
    {
        NCurses.AttributeSet(NCurses.ColorPair(colorPair));
        NCurses.AddChar(ch);
    }

    private static void AddString(string str)
    {
        NCurses.AttributeSet(NCurses.ColorPair(1)); // Make these numbers an enum
        NCurses.AddString(str);
    }


    private static void AddString(string str, short colorPair)
    {
        NCurses.AttributeSet(NCurses.ColorPair(colorPair));
        NCurses.AddString(str);
    }


    private static void DisplayMaze(Maze maze)
    {
        NCurses.Erase();

        for (int rowNum = 0; rowNum < maze.numRows; rowNum++)
        {
            for (int colNum = 0; colNum < maze.numCols; colNum++)
            {
                MazeTileNum tile = (MazeTileNum) maze.maze[rowNum, colNum];

                switch (tile)
                {
                    case MazeTileNum.Undiscovered:
                        AddString(MazeTileChar.Undiscovered, (short) MazeTileNum.Undiscovered);
                        break;

                    case MazeTileNum.Wall:
                        AddString(MazeTileChar.Wall, (short) MazeTileNum.Wall);
                        break;

                    case MazeTileNum.Goal:
                        AddString(MazeTileChar.Goal, (short) MazeTileNum.Goal);
                        break;

                    default:
                        throw new ArgumentException(String.Format("{0} is not a valid TileNum", tile));
                }
            }
            AddString("\n");
        }
        MyRefresh();
    }


    private static void End(string message)
    {
        NCurses.Erase();
        NCurses.AddString("Colour Pairs Could Not Be Initialised. Ending...");
        MyRefresh();
        NCurses.EndWin();
    }


    private static bool InitColors()
    {
        if (NCurses.HasColors())
        {
            NCurses.StartColor();

            NCurses.InitPair(1, CursesColor.WHITE, CursesColor.BLACK);    // Terminal
            NCurses.InitPair(2, CursesColor.BLACK, CursesColor.WHITE);    // Undiscovered
            NCurses.InitPair(3, CursesColor.RED, CursesColor.RED);        // Wall

            return true;
        }
        return false;
    }


    private static void MazeSearchSim()
    {
        for (int i = 0; i < 10; i++)
        {
            Maze maze = new Maze(Constants.NumRows, Constants.NumCols);
            maze.AddWalls(Constants.MazeDensity);
            DisplayMaze(maze);
            maze.GenerateNewOpenMaze();
            DisplayMaze(maze);
        }
        MySleep(5000);
    }

    private static void MyRefresh()
    {
        NCurses.Nap(Constants.AnimationSpeed);
        NCurses.Refresh();
    }

    private static void MySleep(int milliseconds)
    {
        // Thread.Sleep(milliseconds);
        NCurses.Nap(milliseconds);
    }

}
