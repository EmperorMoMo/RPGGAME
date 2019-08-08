using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public static CheckPoint isActivept;//当前复活点
    public CheckPoint firstPt;
    // Start is called before the first frame update
    void Start()
    {
        isActivept = firstPt;
    }
    void OnTriggerEnter()
    {
        if(isActivept!=this)
        {
            isActivept = this;
        }
        PlayerState.GetMaxLife();
        PlayerState.GetMaxPower();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
