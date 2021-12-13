public class Maze
{
    public int[,] maze;
    public int    numRows;
    public int    numCols;

    public Maze(int numRows, int numCols)
    {
        this.numRows = numRows;
        this.numCols = numCols;

        maze = GenerateNewMaze();
    }

    public int[,] GenerateNewMaze()
    {
       int[,] newMaze = new int[numRows, numCols]; 

       for (int rowNum = 0; rowNum < numRows; rowNum++)
       {
           for (int colNum = 0; colNum < numCols; colNum++)
           {
               newMaze[rowNum, colNum] = (int) MazeTileNum.Undiscovered;
           }
       }
       return newMaze;
    }
}