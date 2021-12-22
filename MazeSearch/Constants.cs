using System;


public static class Constants
{
    public const int NumSims = 3;
    public const int AnimationSpeed   = 1; // milliseconds
    public const int IntermissionTime = 2000; // milliseconds

    public const int NumRows = 48;
    public const int NumCols = 96;

    public const double MazeDensity = 0.3;

    public const int InvalidPosition = -1;

}


public static class MazeTileChar
{
    public const string Undiscovered = " ";
    public const string Wall         = " ";
    public const string Goal         = "G";
    public const string CurrentPosition = "C";
    public const string StartingPosition = "S";
    public const string Discovered       = " ";
    public const string InQueue          = "Q";
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
    InQueue          = 8
}
