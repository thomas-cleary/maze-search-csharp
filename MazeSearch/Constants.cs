using System;


public static class Constants
{
    public const int NumSims = 1;
    public const int AnimationSpeed   = 25; // milliseconds
    public const int IntermissionTime = 2000; // milliseconds

    public const int NumRows = 32;
    public const int NumCols = 64;

    public const double MazeDensity = 0.36;

    public const int InvalidPosition = -1;


}


public static class MazeTileChar
{
    public const string Undiscovered     = " ";
    public const string Wall             = " ";
    public const string Goal             = "G";
    public const string CurrentPosition  = "C";
    public const string StartingPosition = "S";
    public const string Discovered       = " ";
    public const string InQueue          = "Q";

    public const string GoalInQueue      = "G";

    public const string GoalFound        = "G";
}


public enum MazeTileNum
{
    Terminal        = 1,
    Undiscovered    = 2,
    Wall            = 3,
    Goal            = 4,
    CurrentPosition = 5,
    StartingPosition = 6,
    Discovered       = 7,
    InQueue          = 8,
    GoalInQueue      = 9,
    GoalFound        = 10
}
