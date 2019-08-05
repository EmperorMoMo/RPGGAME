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
    private static int powerPack = 2;

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

    public static int GetLifePack() {
        return lifePack;
    }

    public static int GetPowerPack() {
        return powerPack;
    }
    public static void ChangeLife(float value) {
        currentLife = Mathf.Clamp(currentLife + value, 0, maxLife);
        UIManager.UI_UpdataLifeValue(currentLife);
    }

    public static void ChangePower(float value) {
        currentPower = Mathf.Clamp(currentPower + value, 0, maxPower);
        UIManager.UI_UpdataPowerValue(currentPower);
    }

    public static void ClickLifePack() {
        if (lifePack > 0 && currentLife != maxLife) {
            ChangeLife(20f);
            lifePack--;
        }
    }

    public static void ClickPowerPack() {
        if (powerPack > 0 && currentPower != maxPower) {
            ChangePower(20f);
            powerPack--;
        }
    }
}
