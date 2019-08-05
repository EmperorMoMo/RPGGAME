using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Slider lifeSlider;
    public Slider powerSlider;
    public static Text lifePackText;
    public static Text powerPackText;

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

    //public static void UI_UpdataLifePack(int value) {
    //    lifePackText.text = value.ToString();
    //}

    //public static void UI_UpdataPowerPack(int value) {
    //    powerPackText.text = value.ToString();
        
    //}
}
