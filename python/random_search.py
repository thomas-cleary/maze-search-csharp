""" look for the goal randomly """

import random

from constants import *
from maze import *


def print_map(screen, search_map, num_moves, max_moves, goal_found=False, goal_in_queue=False):
    for row in search_map:
        for char in row:

            if char == CURR_POS:
                if not goal_found:
                    screen.addch(CURR_POS, curses.color_pair(1))
                else:
                    screen.addch(CURR_POS, curses.color_pair(7))

            elif char == UNDISCOVERED:
                screen.addch(UNDISCOVERED, curses.color_pair(3))

            elif char == DISCOVERED:
                screen.addch(DISCOVERED, curses.color_pair(4))

            elif char == WALL:
                screen.addch(WALL, curses.color_pair(5))

            elif char == GOAL:
                if goal_in_queue:
                    screen.addch(GOAL, curses.color_pair(8))
                else:
                    screen.addch(GOAL, curses.color_pair(6))

            elif char == START:
                screen.addch(START, curses.color_pair(9))


        screen.addch("\n")

    for _ in range(NUM_COLS):
        screen.addch("-")
    
    screen.addch("\n")

    for char in "Random Search\n":
        screen.addch(char)

    for _ in range(NUM_COLS):
        screen.addch("-")
    
    screen.addch("\n")

    for char in "Moves: ":
        screen.addch(char)
    for char in str(num_moves):
        screen.addch(char)
    for char in " / ":
        screen.addch(char)
    for char in str(max_moves):
        screen.addch(char)
    
    screen.addch("\n")

    for _ in range(NUM_COLS):
        screen.addch("-")

    screen.addch("\n")


def random_move(search_map, at_x, at_y):

    moves = [1,2,3,4]
    random.shuffle(moves)

    moved = False

    while len(moves) > 0 and not moved:
        next_move = moves.pop()

        if next_move == 1: # left
            if at_x - 1 < 0 or search_map[at_y][at_x-1] == WALL:
                continue
            at_x -= 1

        elif next_move == 2: # up
            if at_y - 1 < 0 or search_map[at_y-1][at_x] == WALL:
                continue
            at_y -= 1

        elif next_move == 3: # right
            if at_x + 1 > len(search_map[0]) - 1 or search_map[at_y][at_x+1] == WALL:
                continue
            at_x += 1

        elif next_move == 4: #down
            if at_y + 1 > len(search_map) - 1 or search_map[at_y+1][at_x] == WALL:
                continue
            at_y += 1

        moved = True

    if moved:
        return (at_x, at_y)
    else:
        return (None, None)


def random_traverse(screen, search_map, at_x, at_y):
    SPEED = ANIMATION_SPEED // 4

    start_x = at_x
    start_y = at_y

    goal_found = False
    prev_x = None
    prev_y = None

    max_moves = NUM_ROWS * NUM_COLS


    moves_made = 0

    print_map(screen, search_map, moves_made, max_moves, goal_found=goal_found)
    screen.refresh()
    time.sleep(SPEED)
    screen.erase()





    while not goal_found and moves_made < max_moves:
        next_x, next_y = random_move(search_map, at_x, at_y)

        if next_x is None or next_y is None:
            break

        moves_made += 1

        if search_map[next_y][next_x] == GOAL:
            goal_found = True


        if search_map[at_y][at_x] != START:
            search_map[at_y][at_x]     = DISCOVERED

        search_map[start_y][start_x] = START

        search_map[next_y][next_x] = CURR_POS

        prev_x, prev_y = at_x, at_y
        at_x, at_y = next_x, next_y

        print_map(screen, search_map, moves_made, max_moves, goal_found=goal_found)
        screen.refresh()
        time.sleep(SPEED / 2)
        screen.erase()