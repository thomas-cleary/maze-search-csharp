
public static class Search
{
    public static void BFS(Maze maze)
    {
        Queue<(int, int)> queue = new Queue<(int, int)>();

        queue.Enqueue((maze.startingPosition.row, maze.startingPosition.column));

    }
}