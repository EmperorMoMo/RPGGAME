using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerState
{
    private static float maxLife = 100;
    private static float maxPower = 100;
    private static float currentLife = 100;
    private static float currentPower = 100;
    private static int lifePack = 2;
    private static int powerPack = 1;

    public static float GetMaxLife() {
        return maxLife;
    }

    public static float GetCurrentLife() {
        return currentLife;
    }

    public static float GetMaxPower() {
        return maxPower;
    }

    public static float GetCurrentPower() {
        return currentPower;
    }
    public static void ChangeLife(float value) {
        Mathf.Clamp(currentLife += value, 0, maxLife);
    }

    public static void ChangePower(float value) {
        Mathf.Clamp(currentPower += value, 0, maxPower);
    }

}
