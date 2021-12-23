
public class Search
{

    public static bool Astar(Maze maze)
    {
        string searchType = "A Star";

        PriorityQueue<(int, int), double>  priorityQueue = new PriorityQueue<(int, int), double>();
        Dictionary<(int, int), (int, int)> cameFrom      = new Dictionary<(int, int), (int, int)>();
        Dictionary<(int, int), int>        costSoFar     = new Dictionary<(int, int), int>();

        priorityQueue.Enqueue((maze.startingPosition.row, maze.startingPosition.column), 0);
        costSoFar[maze.startingPosition] = 0;

        (int row, int col) invalidNode = (Constants.InvalidPosition, Constants.InvalidPosition);
        (int row, int col) previousNode = invalidNode;

        bool goalFound = false;
        int movesMade  = 0;

        while (priorityQueue.Count > 0 && !goalFound)
        {
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

            (int row, int col) currentNode = priorityQueue.Dequeue();
            
            if (maze.maze[currentNode.row, currentNode.col] == (int) MazeTileNum.GoalInQueue)
            {
                goalFound = true;
            }

            int currentNodeTileType;
            // Mark the dequeued node as the current position on the maze if it is not the goal
            if (maze.maze[currentNode.row, currentNode.col] != (int) MazeTileNum.GoalInQueue)
            {
                currentNodeTileType = (int) MazeTileNum.CurrentPosition;
            }
            else
            {
                currentNodeTileType = (int) MazeTileNum.GoalFound;
            }

            maze.maze[currentNode.row, currentNode.col] = currentNodeTileType;
            maze.currentPosition = (currentNode.row, currentNode.col);
            
            movesMade++;

            Display.DisplayMaze(maze, searchType, movesMade);

            if (goalFound)
            {
                // the search is complete
                break;
            }

            foreach ((int row, int col) neighbour in maze.GetUndiscoveredNeighbours())
            {
                int newCost = costSoFar[currentNode] + GetCost(currentNode, neighbour);

                if (!costSoFar.TryGetValue(neighbour, out int value) || newCost < costSoFar[neighbour])
                {
                    costSoFar[neighbour] = newCost;
                    double priority = newCost + GetHeuristic(neighbour, maze.goal);

                    priorityQueue.Enqueue(neighbour, priority);
                    
                    // if the neighbour is not the goal change its state to InQueue
                    // TODO: else goalInQueue = true and update Display.DisplayMaze() to change goal colour
                    if (maze.maze[neighbour.row, neighbour.col] != (int) MazeTileNum.Goal)
                    {
                        maze.maze[neighbour.row, neighbour.col] = (int) MazeTileNum.InQueue;
                    }
                    else
                    {
                        maze.maze[neighbour.row, neighbour.col] = (int) MazeTileNum.GoalInQueue;
                    }

                    Display.DisplayMaze(maze, searchType, movesMade);
                }
            }
            previousNode = currentNode;
        }
        return goalFound;
    }


    public static void BFS(Maze maze)
    {
        SearchMaze(maze, "Breadth First Search");
    }


    public static void DFS(Maze maze)
    {
        SearchMaze(maze, "Depth First Search");
    }


    private static int GetCost((int, int) node, (int, int) neighbour)
    {
        return 1;
    }

    private static double GetHeuristic((int row, int col) node, (int row, int col) goal)
    {
        double dx = Math.Abs(node.row - goal.row);
        double dy = Math.Abs(node.col - goal.col);

        double tieBreaker = (1.0 + 0.0005);

        return (dx + dy) * tieBreaker;
    }


    private static bool SearchMaze(Maze maze, string searchType)
    {
        LinkedList<(int, int)> linkedList = new LinkedList<(int, int)>();

        // was originally using Queue for BFS and Stack for DFS
        linkedList.AddFirst((maze.startingPosition.row, maze.startingPosition.column));

        (int row, int col) invalidNode = (Constants.InvalidPosition, Constants.InvalidPosition);
        (int row, int col) previousNode = invalidNode;

        bool goalFound = false;

        // track if goal is in queue to change its colour
        int movesMade = 0;

        while (linkedList.Count() > 0 && !goalFound)
        {

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

            (int row, int col) currentNode = linkedList.First.Value;
            linkedList.RemoveFirst();

            if (maze.maze[currentNode.row, currentNode.col] == (int) MazeTileNum.GoalInQueue)
            {
                goalFound = true;
            }

            int currentNodeTileType;
            // Mark the dequeued node as the current position on the maze if it is not the goal
            if (maze.maze[currentNode.row, currentNode.col] != (int) MazeTileNum.GoalInQueue)
            {
                currentNodeTileType = (int) MazeTileNum.CurrentPosition;
            }
            else
            {
                currentNodeTileType = (int) MazeTileNum.GoalFound;
            }

            maze.maze[currentNode.row, currentNode.col] = currentNodeTileType;
            maze.currentPosition = (currentNode.row, currentNode.col);
            
            movesMade++;

            Display.DisplayMaze(maze, searchType, movesMade);

            if (goalFound)
            {
                // the search is complete
                break;
            }

            // If we have not found the goal, add the unvisited tiles surrounding the current node to the queue
            foreach ((int row, int col) neighbour in maze.GetUndiscoveredNeighbours())
            {

                if (searchType.StartsWith("Breadth"))
                {
                    linkedList.AddLast(neighbour);
                }
                else if (searchType.StartsWith("Depth"))
                {
                    linkedList.AddFirst(neighbour);
                }

                // if the neighbour is not the goal change its state to InQueue
                // TODO: else goalInQueue = true and update Display.DisplayMaze() to change goal colour
                if (maze.maze[neighbour.row, neighbour.col] != (int) MazeTileNum.Goal)
                {
                    maze.maze[neighbour.row, neighbour.col] = (int) MazeTileNum.InQueue;
                }
                else
                {
                    maze.maze[neighbour.row, neighbour.col] = (int) MazeTileNum.GoalInQueue;
                }

                Display.DisplayMaze(maze, searchType, movesMade);
            }

            previousNode = currentNode;
        }
        return goalFound;
    }
}