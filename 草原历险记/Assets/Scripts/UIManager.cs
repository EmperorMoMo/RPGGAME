using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class UIManager : MonoBehaviour {
    public Slider lifeSlider;
    public Slider powerSlider;
    public Slider monSlider;
    public Text lifePackText;
    public Text powerPackText;
    public Transform startLifePoint;
    public Transform startPowerPoint;
    public Transform endPoint;
    public GameObject MonsterState;
    public GameObject lifeMoveEffect;
    public GameObject powerMoveEffect;

    static float targetLifeValue = 1f;
    static float targetPowerValue = 1f;
    static float targetMonValue = 1f;
    static float monMaxLife;

    static Transform _startLifePoint;
    static Transform _startPowerPoint;
    static GameObject _lifeMoveEffect;
    static GameObject _powerMoveEffect;
    static GameObject _mon;

    static GameObject pmf;
    static GameObject lmf;

    public static List<GameObject> enemys = new List<GameObject>();
    public Transform player;
    void Start() {
        _startLifePoint = startLifePoint;
        _startPowerPoint = startPowerPoint;
        _lifeMoveEffect = lifeMoveEffect;
        _powerMoveEffect = powerMoveEffect;
        _mon = MonsterState;

    }

    void Update() {
        if (targetLifeValue != lifeSlider.value) {
            lifeSlider.value = Mathf.Lerp(lifeSlider.value, targetLifeValue, 0.2f);
        }

        if (targetPowerValue != powerSlider.value) {
            powerSlider.value = Mathf.Lerp(powerSlider.value, targetPowerValue, 0.2f);
        }

        if (targetMonValue != monSlider.value) {
            monSlider.value = Mathf.Lerp(monSlider.value, targetMonValue, 0.2f);
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

        ClosestDistance(player);
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

    public static void BindMonstarUI(float currentLife, float maxLife) {
        monMaxLife = maxLife;
        targetMonValue = currentLife / monMaxLife;
        _mon.SetActive(true);
    }

    public static void UpdataMonLifeUI(float currentLife) {
        targetMonValue = currentLife / monMaxLife;
    }

    public static void UnboundUI() {
        
            _mon.SetActive(false);
        
    }

    static void ClosestDistance(Transform _player) {
        float closest = 100f;
        float distance = 0f;
        int index = -1;

        if (enemys.Count == 0) {
            return;
        }

        for (int i = 0; i < enemys.Count; i++) {
            distance = Vector3.Distance(_player.position, enemys[i].transform.position);
            if (distance < closest) {
                closest = distance;
                    index = i;
            }
        }

        
        if (Vector3.Distance(enemys[index].transform.position, _player.position) > 10 || enemys[index].GetComponent<EBunny_Status>().health == 0) {
            if (_mon.gameObject.activeSelf != false) {
                UnboundUI();
                closest = 100f;
                distance = 0f;
                index = -1;
                Debug.Log("解绑");
            }
        } else {
            BindMonstarUI(enemys[index].GetComponent<EBunny_Status>().health, 100);
            UpdataMonLifeUI(enemys[index].GetComponent<EBunny_Status>().health);
        }
    }
}
