""" show different search alogorithms """

import curses
import time
import random
import copy

from curses import wrapper

from bfs import *
from dfs import *
from astar import *
from random_search import *
from maze import *
from constants import *


def end(screen):
    time.sleep(3)
    screen.erase()
    screen.addstr("Goodbye\n")
    screen.refresh()
    time.sleep(1)


def main(args):
    screen = curses.initscr()

    curses.init_pair(1, curses.COLOR_BLACK, curses.COLOR_CYAN)
    curses.init_pair(2, curses.COLOR_BLACK, curses.COLOR_MAGENTA)
    curses.init_pair(3, curses.COLOR_BLACK, curses.COLOR_WHITE)
    curses.init_pair(4, curses.COLOR_BLACK, curses.COLOR_GREEN)
    curses.init_pair(5, curses.COLOR_RED, curses.COLOR_RED)
    curses.init_pair(6, curses.COLOR_RED, curses.COLOR_YELLOW)
    curses.init_pair(7, curses.COLOR_YELLOW, curses.COLOR_CYAN)
    curses.init_pair(8, curses.COLOR_YELLOW, curses.COLOR_MAGENTA)
    curses.init_pair(9, curses.COLOR_YELLOW, curses.COLOR_BLACK)

    for i in range(NUM_RUNS):
        screen.erase()

        search_map = get_maze()
        at_x, at_y = get_current_pos(search_map)

        #random_traverse(screen, copy.deepcopy(search_map), at_x, at_y)
        #time.sleep(2)

        bfs(screen, copy.deepcopy(search_map), at_x, at_y)
        time.sleep(2)

        dfs(screen, copy.deepcopy(search_map), at_x, at_y)
        time.sleep(2)

        goal_x, goal_y = get_goal(search_map)
        astar(screen, copy.deepcopy(search_map), at_x, at_y, goal_x, goal_y)

        time.sleep(2)

    end(screen)


if __name__ == "__main__":
    wrapper(main)