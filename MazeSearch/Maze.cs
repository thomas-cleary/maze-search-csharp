
public class Maze
{
    public int[,] maze;
    public int    numRows;
    public int    numCols;
    

    public (int row, int column) goal;
    public (int row, int column) currentPosition;

    public (int row, int column) startingPosition;

    private Random rand;


    public Maze(int numRows, int numCols)
    {
        this.numRows = numRows;
        this.numCols = numCols;

        rand = new Random();

        GenerateNewOpenMaze();

    }


    /// <summary> Add the starting position of the search to the maze </summary>
    public void AddCurrentPosition()
    {
        this.currentPosition = AddTileToRandomUndiscovered(MazeTileNum.CurrentPosition);
        this.startingPosition = currentPosition;
    }


    /// <summary> Add a goal point at a random location in the maze for the search alogirthm to find </summary>
    public void AddGoal()
    {
        this.goal = AddTileToRandomUndiscovered(MazeTileNum.Goal);
    }


    private (int, int) AddTileToRandomUndiscovered(MazeTileNum tileNum)
    {
        bool undiscoveredFound = IsUndiscoveredTile();

        if (!undiscoveredFound)
        {
            throw new InvalidOperationException("There is no available tile to place the goal onto.");
        }

        int randRow;
        int randCol;
        // Find a random undiscovered tile to make the goal of the search
        do
        {
            randRow = rand.Next(this.numRows);
            randCol = rand.Next(this.numCols);
        }
        while (!(this.maze[randRow, randCol] == (int) MazeTileNum.Undiscovered));

        this.maze[randRow, randCol] = (int) tileNum;

        return (randRow, randCol);
    }


    /// <summary> Add walls to this.maze to meet the specified density </summary>
    public void AddWalls(double density)
    {
        int numTiles      = this.numRows * this.numCols;
        int numWallsToAdd = (int) (numTiles * density); // cast truncates while Convert.ToIntX() would round
        int wallsAdded    = 0;

        while (wallsAdded < numWallsToAdd)
        {
            var randRow = this.rand.Next(0, this.numRows);
            var randCol = this.rand.Next(0, this.numCols);

            if (this.maze[randRow, randCol] == (int) MazeTileNum.Undiscovered)
            {
                this.maze[randRow, randCol] = (int) MazeTileNum.Wall;
                wallsAdded++;
            }
        }
    }


    /// <summary> Return a deep copy of this maze object </summary>
    public Maze DeepCopy()
    {
        Maze copy = new Maze(this.numRows, this.numCols);
        copy.startingPosition = this.startingPosition;
        copy.currentPosition  = this.currentPosition;
        copy.goal             = this.goal;
        Array.Copy(this.maze, copy.maze, this.maze.Length);

        return copy;
    }


    /// <summary> Set this.maze to a new maze with no Wall tiles inserted </summary>
    public void GenerateNewOpenMaze()
    {
       int[,] newMaze = new int[numRows, numCols]; 

       for (int rowNum = 0; rowNum < numRows; rowNum++)
       {
           for (int colNum = 0; colNum < numCols; colNum++)
           {
               newMaze[rowNum, colNum] = (int) MazeTileNum.Undiscovered;
           }
       }
       this.maze = newMaze;
    }


    /// <summary> Return all (x, y) locations in this.maze that are Undiscovered and neighbours of Current Location
    public List<(int, int)> GetUndiscoveredNeighbours()
    {
        List<(int, int)> undiscoveredNeighbours = new List<(int, int)>();

        // If goal already on the queue it will be dequeued before we reach the extra additions
        int[] visitable = {(int) MazeTileNum.Undiscovered, (int) MazeTileNum.Goal};

        int numDirections = 4;
        for (int direction = 1; direction <= numDirections; direction++)
        {
            if (direction == 1) // Left
            {
                if (!(this.currentPosition.column - 1 < 0))
                {
                    if (visitable.Contains(this.maze[this.currentPosition.row, this.currentPosition.column - 1]))
                    {
                        undiscoveredNeighbours.Add((this.currentPosition.row, this.currentPosition.column - 1));
                    }
                }
            }

            else if (direction == 2) // Up
            {
                if (!(this.currentPosition.row - 1 < 0))
                {
                    if (visitable.Contains(this.maze[this.currentPosition.row - 1, this.currentPosition.column]))
                    {
                        undiscoveredNeighbours.Add((this.currentPosition.row - 1, this.currentPosition.column));
                    }
                }
            }

            else if (direction == 3) // Right
            {
                if (!(this.currentPosition.column + 1 >= this.numCols))
                {
                    if (visitable.Contains(this.maze[this.currentPosition.row, this.currentPosition.column + 1]))
                    {
                        undiscoveredNeighbours.Add((this.currentPosition.row, this.currentPosition.column + 1));
                    }
                }
            }

            else // Down
            {
                if (!(this.currentPosition.row + 1 >= this.numRows))
                {
                    if (visitable.Contains(this.maze[this.currentPosition.row + 1, this.currentPosition.column]))
                    {
                        undiscoveredNeighbours.Add((this.currentPosition.row + 1, this.currentPosition.column));
                    }
                }
            }
        }

        return undiscoveredNeighbours;
    }


    /// <summary> Check if there is an undiscovered tile in the maze </summary>
    public bool IsUndiscoveredTile()
    {
        // Check that maze has an available tile to place the goal on
        for (int rowNum = 0; rowNum < this.numRows; rowNum++)
        {
            for (int colNum = 0; colNum < this.numCols; colNum++)
            {
                if (this.maze[rowNum, colNum] == (int) MazeTileNum.Undiscovered)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void Setup(double density)
    {
        AddWalls(density);
        AddGoal();
        AddCurrentPosition();
    }


}