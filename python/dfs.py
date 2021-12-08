""" dfs function """

from constants import *
from maze import *

BLINK = True

def print_map(screen, search_map, num_moves, goal_found=False, goal_in_queue=False):
    global BLINK

    for row in search_map:
        for char in row:

            if char == CURR_POS:
                if not goal_found:
                    screen.addch(CURR_POS, curses.color_pair(1))
                else:
                    screen.addch(CURR_POS, curses.color_pair(7))

            elif char == IN_QUEUE:
                screen.addch(IN_QUEUE, curses.color_pair(2))

            elif char == UNDISCOVERED:
                screen.addch(UNDISCOVERED, curses.color_pair(3))

            elif char == DISCOVERED:
                screen.addch(DISCOVERED, curses.color_pair(4))

            elif char == WALL:
                screen.addch(WALL, curses.color_pair(5))

            elif char == GOAL:
                if BLINK:
                    screen.addch(GOAL, curses.color_pair(9))
                elif goal_in_queue:
                    screen.addch(GOAL, curses.color_pair(8))
                else:
                    screen.addch(GOAL, curses.color_pair(6))
                BLINK = not BLINK

            elif char == START:
                screen.addch(START, curses.color_pair(9))

        screen.addch("\n")

    for _ in range(NUM_COLS):
        screen.addch("-")

    screen.addch("\n")

    for char in "Depth First Search\n":
        screen.addch(char)

    for _ in range(NUM_COLS):
        screen.addch("-")
    
    screen.addch("\n")

    for char in "Nodes Searched: ":
        screen.addch(char)
    for char in str(num_moves):
        screen.addch(char)

    screen.addch("\n")

    for _ in range(NUM_COLS):
        screen.addch("-")

    screen.addch("\n")


def get_unvisited_neighbours(search_map, at_x, at_y, goal_in_queue=False):
    neighbours = []

    directions = list(range(1, 4+1))

    visitable = [UNDISCOVERED]

    if not goal_in_queue:
        visitable.append(GOAL)

    for direction in directions:
        if direction == 1: # left
            if not at_x - 1 < 0:
                if search_map[at_y][at_x-1] in visitable:
                    neighbours.append((at_x-1, at_y))

        elif direction == 2: # up
            if not at_y - 1 < 0:
                if search_map[at_y-1][at_x] in visitable:
                    neighbours.append((at_x, at_y-1)) 

        elif direction == 3: # right
            if not at_x + 1 > len(search_map[0]) - 1:
                if search_map[at_y][at_x+1] in visitable:
                    neighbours.append((at_x+1, at_y))

        elif direction == 4: #down
            if not at_y + 1 > len(search_map) - 1:
                if search_map[at_y+1][at_x] in visitable:
                    neighbours.append((at_x, at_y+1)) 

    return neighbours


def dfs(screen, search_map, at_x, at_y):
    start_x = at_x
    start_y = at_y

    queue = []

    queue.append((at_x, at_y))

    previous_node = None

    goal_found = False
    goal_in_queue = False

    moves_made = 0
    
    while len(queue) > 0 and not goal_found:
        if previous_node is not None:
            search_map[previous_node[1]][previous_node[0]] = DISCOVERED

        current_node = queue.pop(0)
        cur_x, cur_y = current_node

        if search_map[cur_y][cur_x] == GOAL:
            goal_found = True

        search_map[cur_y][cur_x] = CURR_POS

        if previous_node is not None:
            search_map[start_y][start_x] = START
            moves_made += 1

        print_map(screen, search_map, moves_made, goal_found, goal_in_queue)
        screen.refresh()
        time.sleep(ANIMATION_SPEED)
        screen.erase()

        if goal_found:
            break

        # we want the one we find first in get neighbours to be added to the front of the queue
        neighbours = list(reversed(get_unvisited_neighbours(search_map, cur_x, cur_y, goal_in_queue=goal_in_queue)))

        for neighbour in neighbours:
            queue = [neighbour] + queue
            if search_map[neighbour[1]][neighbour[0]] != GOAL:
                search_map[neighbour[1]][neighbour[0]] = IN_QUEUE
            else:
                goal_in_queue = True

            print_map(screen, search_map, moves_made, goal_found, goal_in_queue)
            screen.refresh()
            time.sleep(ANIMATION_SPEED)
            screen.erase()

        previous_node = current_node