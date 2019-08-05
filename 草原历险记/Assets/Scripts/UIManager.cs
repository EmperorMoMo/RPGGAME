using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
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
}
