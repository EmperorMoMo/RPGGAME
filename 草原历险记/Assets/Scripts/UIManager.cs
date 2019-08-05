using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour {
    public Slider lifeSlider;
    public Slider powerSlider;
    public Text lifePackText;
    public Text powerPackText;

    public static float targetLifeValue = 1f;
    public static float targetPowerValue = 1f;
    public static int lifePackCount = 2;
    public static int PowerPackCount = 2;
    void Start() {
    }

    void Update() {
        if (targetLifeValue != lifeSlider.value) {
            lifeSlider.value = Mathf.Lerp(lifeSlider.value, targetLifeValue, 0.2f);
        }

        if (targetPowerValue != powerSlider.value) {
            powerSlider.value = Mathf.Lerp(powerSlider.value, targetPowerValue, 0.2f);
        }
        //Convert.ToInt32(lifePackText.text.ToString())
    }
    public void ButtonPack_Click(string pack) {
        switch (pack) { 
            case "Life":
                Debug.Log("Life");
                //UI_ChangeLife(0);
                break;
            case "Power":
                Debug.Log("Power");
                //UI_ChangePowerUI(0);
                break;
            default:
                Debug.Log("other");
                break;
        }
    }

    public static void UI_UpdataLifeValue(float currentLife) {
        targetLifeValue = currentLife / PlayerState.GetMaxLife();
    }

    public static void UI_UpdataPowerValue(float currentPower) {
        targetPowerValue = currentPower / PlayerState.GetMaxPower();
    }

    public static void UI_UpdataLifePack(int value) {
        lifePackCount = value;
    }

    public static void UI_UpdataPowerPack(int value) {
        PowerPackCount = value;
    }
}
