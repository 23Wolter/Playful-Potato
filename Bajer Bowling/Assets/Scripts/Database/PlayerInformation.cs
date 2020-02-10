using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerInformation
{
    private static string[] playerTypes = { "AI", "AI", "AI", "AI" };

    public static void SetPlayerType(int index, string type)
    {
        playerTypes[index] = type;
    }

    public static string[] GetPlayerTypes()
    {
        return playerTypes;
    }
}
