using Mindmagma.Curses;

var Screen = NCurses.InitScreen();
NCurses.NoDelay(Screen, true);
NCurses.NoEcho();

for (var i = 'a'; i < 'h'; i++)
{
    NCurses.Erase();
    NCurses.AddChar(i);
    System.Threading.Thread.Sleep(500);
    NCurses.Refresh();
}
System.Threading.Thread.Sleep(3000);
NCurses.EndWin();