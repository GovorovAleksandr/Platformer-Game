using UnityEngine;

public static class FloatRounder
{
    private const int ORDER = 10;
    private const int PLACES = 2;

    public static float Round(float number)
    {
        return Mathf.Round(number * Mathf.Pow(ORDER, PLACES)) / Mathf.Pow(10, PLACES);
    }
}