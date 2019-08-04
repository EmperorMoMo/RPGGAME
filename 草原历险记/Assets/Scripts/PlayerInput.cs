using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public GameObject model;

    public string keyUp = "w";
    public string keyDown = "s";
    public string keyLeft = "a";
    public string keyRight = "d";

    public float Dup;
    public float Dright;

    private float targetDup;        //目标值Dup
    private float targetDright;     //目标值Dright
    private float velocityDup;      //过渡值Dup（SmoothDamp方法中的转换速度,SmoothDamp方法会根据最后的时间参数自行计算）
    private float velocityDright; 


    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetDup = ((Input.GetKey(keyUp) ? 1.0f : 0.0f) - (Input.GetKey(keyDown) ? 1.0f : 0.0f));
        targetDright = ((Input.GetKey(keyLeft) ? 1.0f : 0.0f) - (Input.GetKey(keyRight) ? 1.0f : 0.0f));
        Vector3 walk = new Vector3(targetDup, targetDright, 0);
        model.transform.position = walk;

    }
}
