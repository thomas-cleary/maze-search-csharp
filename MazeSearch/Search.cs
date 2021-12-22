
public static class Search
{
    public static void BFS(Maze maze, bool debug)
    {
        Queue<(int, int)> queue = new Queue<(int, int)>();

        queue.Enqueue((maze.startingPosition.row, maze.startingPosition.column));

        (int row, int col) invalidNode = (Constants.InvalidPosition, Constants.InvalidPosition);
        (int row, int col) previousNode = invalidNode;

        bool goalFound = false;

        // track if goal is in queue to change its colour
        int movesMade = 0;

        while (queue.Count() > 0 && !goalFound)
        {
            if (debug)
            {
                foreach ((int, int) node in queue)
                {
                    Console.WriteLine(String.Format("Moves made {0}: ", movesMade));
                    Console.WriteLine(node);
                }
                Console.WriteLine("\n");
            }

            if (previousNode != invalidNode)
            {
                // Mark tile we just moved from as Discovered
                maze.maze[previousNode.row, previousNode.col] = (int) MazeTileNum.Discovered;

                if (movesMade > 0)
                {
                    // If we have moved off the starting position mark it as the starting position tile
                    maze.maze[maze.startingPosition.row, maze.startingPosition.column] = (int) MazeTileNum.StartingPosition;
                }
            }
            
            // Get the next tile on the maze we will move to
            (int row, int col) currentNode = queue.Dequeue();

            if (maze.maze[currentNode.row, currentNode.col] == (int) MazeTileNum.Goal)
            {
                goalFound = true;
            }

            // Mark the dequeued node as the current position on the maze
            maze.maze[currentNode.row, currentNode.col] = (int) MazeTileNum.CurrentPosition;
            maze.currentPosition = (currentNode.row, currentNode.col);
            
            movesMade++;

            if (!debug)
            {
                Display.DisplayMaze(maze);
            }

            if (goalFound)
            {
                // the search is complete
                break;
            }

            // If we have not found the goal, add the unvisited tiles surrounding the current node to the queue
            foreach ((int row, int col) neighbour in maze.GetUndiscoveredNeighbours())
            {
                queue.Enqueue(neighbour);
                
                // if the neighbour is not the goal change its state to InQueue
                // TODO: else goalInQueue = true and update Display.DisplayMaze() to change goal colour
                if (maze.maze[neighbour.row, neighbour.col] != (int) MazeTileNum.Goal)
                {
                    maze.maze[neighbour.row, neighbour.col] = (int) MazeTileNum.InQueue;
                }

                if (!debug)
                {
                    Display.DisplayMaze(maze);
                }
            }

            previousNode = currentNode;

        }

    }
}