public class Maze
{
    /// <remarks> Indexing is array[column, row] to match ncurses display output </remarks>
    public int[,] maze;
    public int    numRows;
    public int    numCols;

    public Maze(int numRows, int numCols)
    {
        this.numRows = numRows;
        this.numCols = numCols;

        GenerateNewOpenMaze();
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

    /// <summary> Add walls to this.maze to meet the specified density </summary>
    public void AddWalls(double density)
    {
        int numTiles      = this.numRows * this.numCols;
        int numWallsToAdd = (int) (numTiles * density); // cast truncates while Convert.ToIntX() would round
        int wallsAdded    = 0;

        var rand = new Random();

        while (wallsAdded < numWallsToAdd)
        {
            var randRow = rand.Next(0, this.numRows);
            var randCol = rand.Next(0, this.numCols);

            if (this.maze[randRow, randCol] == (int) MazeTileNum.Undiscovered)
            {
                this.maze[randRow, randCol] = (int) MazeTileNum.Wall;
                wallsAdded++;
            }
        }
    }
}