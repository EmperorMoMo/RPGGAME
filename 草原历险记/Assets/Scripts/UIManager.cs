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
    void Start() {
    }

    void Update() {
        if (targetLifeValue != lifeSlider.value) {
            lifeSlider.value = Mathf.Lerp(lifeSlider.value, targetLifeValue, 0.2f);
        }

        if (targetPowerValue != powerSlider.value) {
            powerSlider.value = Mathf.Lerp(powerSlider.value, targetPowerValue, 0.2f);
        }

        if (Convert.ToInt32(lifePackText.text.ToString()) != PlayerState.GetLifePack()) {
            lifePackText.text = PlayerState.GetLifePack().ToString();
        }

        if (Convert.ToInt32(powerPackText.text.ToString()) != PlayerState.GetPowerPack()) {
            powerPackText.text =  PlayerState.GetPowerPack().ToString();
        }
    }
    public void ButtonPack_Click(string pack) {
        switch (pack) { 
            case "Life":
                Debug.Log("Life");
                PlayerState.ClickLifePack();
                break;
            case "Power":
                Debug.Log("Power");
                PlayerState.ClickPowerPack();
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
}
