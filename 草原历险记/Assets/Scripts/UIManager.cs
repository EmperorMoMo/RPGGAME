using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class UIManager : MonoBehaviour {
    public Slider lifeSlider;
    public Slider powerSlider;
    public Text lifePackText;
    public Text powerPackText;
    public Transform startLifePoint;
    public Transform startPowerPoint;
    public Transform endPoint;
    public GameObject lifeMoveEffect;
    public GameObject powerMoveEffect;

    public static float targetLifeValue = 1f;
    public static float targetPowerValue = 1f;

    static Transform _startLifePoint;
    static Transform _startPowerPoint;
    static GameObject _lifeMoveEffect;
    static GameObject _powerMoveEffect;

    static GameObject lmf;
    static GameObject pmf;
    void Start() {
        _startLifePoint = startLifePoint;
        _startPowerPoint = startPowerPoint;
        _lifeMoveEffect = lifeMoveEffect;
        _powerMoveEffect = powerMoveEffect;
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

        if (lmf != null) {
            lmf.transform.position = Vector3.MoveTowards(lmf.transform.position, endPoint.position, 80 * Time.deltaTime);
        }

        if (pmf != null) {
            pmf.transform.position = Vector3.MoveTowards(pmf.transform.position, endPoint.position, 80 * Time.deltaTime);
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

    public static void PlayLifeEffect() {
        lmf = GameObject.Instantiate(_lifeMoveEffect, _startLifePoint);
    }

    public static void PlayPowerEffect() {
        pmf = GameObject.Instantiate(_powerMoveEffect, _startPowerPoint);
    }

}
