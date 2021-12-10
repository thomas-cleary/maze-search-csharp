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
            for (int colNum = 0; colNum < maze.numRows; colNum++)
            {
                MazeTileNum tile = (MazeTileNum) maze.maze[rowNum, colNum];

                switch (tile)
                {
                    case MazeTileNum.Undiscovered:
                        AddString(MazeTileChar.Undiscovered, (short) MazeTileNum.Undiscovered);
                        break;
                }
            }
            AddString("\n");
        }

        NCurses.Refresh();
    }


    private static void End(string message)
    {
        NCurses.Erase();
        NCurses.AddString("Colour Pairs Could Not Be Initialised. Ending...");
        NCurses.Refresh();
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
        Maze maze = new Maze(Constants.NumRows, Constants.NumCols);
        DisplayMaze(maze);
        Sleep(3000);
        
    }

    private static void Sleep(int milliseconds)
    {
        Thread.Sleep(milliseconds);
    }

}
