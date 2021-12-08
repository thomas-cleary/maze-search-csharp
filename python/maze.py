""" generate map """

import random
import curses
import time

from constants import *



def get_maze():
    search_map = [[UNDISCOVERED] * NUM_COLS for _ in range(NUM_ROWS)]

    at_x = random.randint(0, NUM_COLS-1)
    at_y = random.randint(0, NUM_ROWS-1)
    # at_x = len(search_map[0]) - 1
    # at_y = 0
    search_map[at_y][at_x] = CURR_POS

    add_walls(search_map, density=DENSITY)
    add_goal(search_map)

    return search_map



def get_current_pos(maze):
    for y, row in enumerate(maze):
        for x, col in enumerate(row):
            if col == CURR_POS:
                return (x, y)


def get_goal(maze):
    for y, row in enumerate(maze):
        for x, col in enumerate(row):
            if col == GOAL:
                return (x, y)



def add_walls(search_map, density=0.1):
    num_rows = len(search_map)
    num_cols = len(search_map[0])

    num_nodes = num_rows * num_cols

    num_walls = int(num_nodes * density)

    walls_added = 0

    while walls_added < num_walls:
        rand_row = random.randint(0, num_rows-1)
        rand_col = random.randint(0, num_cols-1)

        if search_map[rand_row][rand_col] == UNDISCOVERED:
            search_map[rand_row][rand_col] = WALL
            walls_added += 1


def add_goal(search_map):
    num_rows = len(search_map)
    num_cols = len(search_map[0])

    goal_added = False

    while not goal_added:
        rand_row = random.randint(0, num_rows-1)
        rand_col = random.randint(0, num_cols-1)

        if search_map[rand_row][rand_col] != CURR_POS:
            search_map[rand_row][rand_col] = GOAL

            goal_added = True