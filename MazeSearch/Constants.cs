using System;


public static class Constants
{
    public const int NumSims = 10;
    public const int AnimationSpeed   = 1000; // milliseconds
    public const int IntermissionTime = 2000; // milliseconds

    public const int NumRows = 16;
    public const int NumCols = 32;

    public const double MazeDensity = 0.3;

}


public static class MazeTileChar
{
    public const string Undiscovered = " ";
    public const string Wall         = " ";
    public const string Goal         = "G";
}


public enum MazeTileNum
{
    Terminal     = 1,
    Undiscovered = 2,
    Wall         = 3,

    Goal         = 4
}
