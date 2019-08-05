using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static Slider lifeSlider;
    public static Slider powerSlider;
    public static Text lifePackText;
    public static Text powerPackText;

    private static float maxLife;
    private static float maxPower;
    void Start() { 
    
    }
    public void ButtonPack_Click(string pack) {
        switch (pack) { 
            case "Life":
                Debug.Log("Life");
                UI_ChangeLife(0);
                break;
            case "Power":
                Debug.Log("Power");
                UI_ChangePowerUI(0);
                break;
            default:
                Debug.Log("other");
                break;
        }
    }

    void UI_ChangeLife(int value) { 
    
    }

    void UI_ChangePowerUI(int value) { 
    
    }

    public static void UI_UpdataLifeValue(float currentLife) {
        lifeSlider.value = currentLife / maxLife;
    }

    public static void UI_UpdataPowerValue(float currentPower) {
        powerSlider.value = currentPower / maxPower;
    }

    public static void UI_UpdataLifePack(int value) {
        lifePackText.text = value.ToString();
    }

    public static void UI_UpdataPowerPack(int value) {
        powerPackText.text = value.ToString();
        
    }
}
