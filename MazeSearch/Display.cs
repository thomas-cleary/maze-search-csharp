using Mindmagma.Curses;

public class Display
{

    public static void AddChar(char ch)
    {
        NCurses.AttributeSet(NCurses.ColorPair(1)); // Make these numbers an enum
        NCurses.AddChar(ch);
    }


    public static void AddChar(char ch, short colorPair)
    {
        NCurses.AttributeSet(NCurses.ColorPair(colorPair));
        NCurses.AddChar(ch);
    }

    public static void AddString(string str)
    {
        NCurses.AttributeSet(NCurses.ColorPair(1)); // Make these numbers an enum
        NCurses.AddString(str);
    }


    public static void AddString(string str, short colorPair)
    {
        NCurses.AttributeSet(NCurses.ColorPair(colorPair));
        NCurses.AddString(str);
    }


    public static void DisplayMaze(Maze maze)
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

                    case MazeTileNum.CurrentPosition:
                        AddString(MazeTileChar.CurrentPosition, (short) MazeTileNum.CurrentPosition);
                        break;

                    case MazeTileNum.StartingPosition:
                        AddString(MazeTileChar.StartingPosition, (short) MazeTileNum.StartingPosition);
                        break;

                    case MazeTileNum.Discovered:
                        AddString(MazeTileChar.Discovered, (short) MazeTileNum.Discovered);
                        break;

                    case MazeTileNum.InQueue:
                        AddString(MazeTileChar.InQueue, (short) MazeTileNum.InQueue);
                        break;

                    case MazeTileNum.GoalInQueue:
                        AddString(MazeTileChar.GoalInQueue, (short) MazeTileNum.GoalInQueue);
                        break;

                    case MazeTileNum.GoalFound:
                        AddString(MazeTileChar.GoalFound, (short) MazeTileNum.GoalFound);
                        break;

                    default:
                        throw new ArgumentException(String.Format("{0} is not a valid TileNum", tile));
                }
            }
            AddString("\n");
        }
        MyRefresh();
    }


    public static void End(string message)
    {
        NCurses.Erase();
        NCurses.AddString("Colour Pairs Could Not Be Initialised. Ending...");
        MyRefresh();
        NCurses.EndWin();
    }


    public static bool InitColors()
    {
        if (NCurses.HasColors())
        {
            NCurses.StartColor();

            NCurses.InitPair((short) MazeTileNum.Terminal,         CursesColor.WHITE,  CursesColor.BLACK);   
            NCurses.InitPair((short) MazeTileNum.Undiscovered,     CursesColor.BLACK,  CursesColor.WHITE);   
            NCurses.InitPair((short) MazeTileNum.Wall,             CursesColor.RED,    CursesColor.RED);     
            NCurses.InitPair((short) MazeTileNum.Goal,             CursesColor.RED,    CursesColor.YELLOW);  
            NCurses.InitPair((short) MazeTileNum.CurrentPosition,  CursesColor.YELLOW, CursesColor.BLUE);    
            NCurses.InitPair((short) MazeTileNum.StartingPosition, CursesColor.YELLOW, CursesColor.BLACK);   
            NCurses.InitPair((short) MazeTileNum.Discovered,       CursesColor.BLACK,  CursesColor.GREEN);   
            NCurses.InitPair((short) MazeTileNum.InQueue,          CursesColor.BLACK,  CursesColor.MAGENTA); 
            NCurses.InitPair((short) MazeTileNum.GoalInQueue,      CursesColor.YELLOW,  CursesColor.MAGENTA);
            NCurses.InitPair((short) MazeTileNum.GoalFound,        CursesColor.YELLOW,  CursesColor.BLUE);

            return true;
        }
        return false;
    }

    
    public static void MyRefresh()
    {
        NCurses.Nap(Constants.AnimationSpeed);
        NCurses.Refresh();
    }

    public static void MySleep(int milliseconds)
    {
        // Thread.Sleep(milliseconds);
        NCurses.Nap(milliseconds);
    }
}