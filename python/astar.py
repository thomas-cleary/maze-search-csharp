""" functions for A* """


import math

from queue import PriorityQueue

from constants import *
from maze import *


BLINK = True

def get_neighbours(search_map, at_x, at_y, goal_in_queue=False):
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


def get_cost(node_1, node_2):
    return 1


def get_heuristic(node_1, node_2):
    # manhattan distance
    dx = abs(node_1[0] - node_2[0])
    dy = abs(node_1[1] - node_2[1])

    tie_breaker = (1.0 + 0.0005)

    return (dx + dy) * tie_breaker



def astar(screen, search_map , at_x, at_y, goal_x, goal_y):
    start_x = at_x
    start_y = at_y

    pq = PriorityQueue()
    pq.put((0, (start_x, start_y)))

    came_from = dict()
    cost_so_far = dict()

    came_from[(start_x, start_y)] = None
    cost_so_far[(start_x, start_y)] = 0

    nodes_searched = 0
    prev_node = None

    goal_found = False
    goal_in_queue = False

    while pq.qsize() > 0 and not goal_found:
        if prev_node is not None:
            search_map[prev_node[1]][prev_node[0]] = DISCOVERED


        current_node = pq.get()[1]
        cur_x, cur_y = current_node
        nodes_searched += 1
        search_map[cur_y][cur_x] = CURR_POS

        if prev_node is not None:
            search_map[start_y][start_x] = START

        if current_node == (goal_x, goal_y):
            goal_found = True
            

        print_map(screen, search_map, nodes_searched, goal_found, goal_in_queue)
        screen.refresh()
        time.sleep(ANIMATION_SPEED)
        screen.erase()

        if goal_found:
            break

        neighbours = get_neighbours(search_map, cur_x, cur_y)
        for neighbour in neighbours:
            new_cost = cost_so_far[current_node] + get_cost(current_node, neighbour)

            if neighbour not in cost_so_far or new_cost < cost_so_far[neighbour]:
                cost_so_far[neighbour] = new_cost
                priority = new_cost + get_heuristic(neighbour, (goal_x, goal_y))

                pq.put((priority, neighbour))

                if search_map[neighbour[1]][neighbour[0]] != GOAL:
                    search_map[neighbour[1]][neighbour[0]] = IN_QUEUE
                else:
                    goal_in_queue = True
                    break

                came_from[neighbour] = current_node

                print_map(screen, search_map, nodes_searched, goal_found, goal_in_queue)
                screen.refresh()
                time.sleep(ANIMATION_SPEED)
                screen.erase()

        prev_node = current_node



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

    for char in "A* Search\n":
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
