
public static class Search
{
    public static void BFS(Maze maze)
    {
        Queue<(int, int)> queue = new Queue<(int, int)>();

        queue.Enqueue((maze.startingPosition.row, maze.startingPosition.column));

        (int Row, int Col) previousNode = (Constants.InvalidPosition, Constants.InvalidPosition);

        bool goalFound   = false;
        bool goalInQueue = false;

        int movesMade = 0;

        while (queue.Count > 0 && !goalFound)
        {
            if (previousNode != (Constants.InvalidPosition, Constants.InvalidPosition))
            {
                maze.maze[previousNode.Row, previousNode.Col] = (int) MazeTileNum.Discovered;
            }

            (int Row, int Col) currentNode = queue.Dequeue();

            if (maze.maze[currentNode.Row, currentNode.Col] == (int) MazeTileNum.Goal)
            {
                goalFound = true;
            }

            maze.maze[currentNode.Row, currentNode.Col] = (int) MazeTileNum.CurrentPosition;

            if (movesMade > 0 && previousNode != (Constants.InvalidPosition, Constants.InvalidPosition))
            {
                return;
            }
        }

    }
}