using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraDampValue = 0.5f;

    private PlayerInput Pi;
    private GameObject cameraHandle;
    private GameObject camera;

    private Vector3 cameraDampVelocity;

    void Awake()
    {
        cameraHandle = transform.parent.gameObject;
        camera = Camera.main.gameObject;
        
    }

    void FixedUpdate()
    {
        camera.transform.position =
            Vector3.SmoothDamp(camera.transform.position, transform.position, ref cameraDampVelocity, cameraDampValue);
        //camera.transform.eulerAngles = transform.eulerAngles;//这里会使镜头左右快速移动的时候颤抖，看着很不舒服，改成下面
        camera.transform.LookAt(cameraHandle.transform.position);//让镜头看向它的父物体cameraHandle
    }

    void Update()
    {
        Cursor.visible = false;
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            Cursor.visible = true;
        }
    }

    //public Transform target;//目标物体
    //public float distance = 10.0f;//距离
    //public float height = 5.0f;//高度
    //public float heightDamping = 2.0f;//摄像机高度上进行调整时候的阻尼
    //public float rotationDamping = 3.0f;//摄像机旋转进行调整时候的阻尼
    //public float distanceDampingX = 0.5f;//摄像机X轴上进行调整时候的阻尼
    //public float distanceDampingZ = 0.2f;//摄像机Z轴上进行调整时候的阻尼
    //public float camSpeed = 2.0f;//摄像机移动的速度

    //public bool smoothed = true;//摄像机是否平滑

    //private float wantedRotationAngle;//摄像机要到达的角度值
    //private float wantedHeight;//摄像机要到达的高度
    //private float wantedDistanceZ;//摄像机要到达的Z轴位置
    //private float wantedDistanceX;//摄像机要到达的X轴位置

    //private float currentRotationAngle;//摄像机当前的角度值
    //private float currentHeight;//摄像机当前的高度
    //private float currentDistanceZ;//摄像机当前的Z轴位置
    //private float currentDistanceX;//摄像机当前的X轴位置

    //private Quaternion currentRotation;

    //void Update()
    //{
    //    if (!target)
    //    {
    //        return;
    //    }

    //    wantedRotationAngle = target.eulerAngles.y;//取得当前摄像机要到达的角度值
    //    wantedHeight = target.position.y + height;//取得摄像机要到达高度
    //    wantedDistanceZ = target.position.z - distance;//取得摄像机要到达的Z轴位置
    //    wantedDistanceX = target.position.x - distance;//取得摄像机要到达的X轴位置

    //    currentRotationAngle = transform.eulerAngles.y;//取得当前摄像机的角度值、位置信息
    //    currentHeight = transform.position.y;
    //    currentDistanceZ = transform.position.z;
    //    currentDistanceX = transform.position.x;

    //    //平滑移到新的角度，置为当前角度值
    //    currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

    //    currentHeight = Mathf.LerpAngle(currentHeight, wantedHeight, heightDamping * Time.deltaTime);//平滑移到新的高度，置为当前高度
    //    currentDistanceZ = Mathf.LerpAngle(currentDistanceZ, wantedDistanceZ, distanceDampingZ * Time.deltaTime);//平滑移到新的Z轴位置，置为当前Z轴位置
    //    currentDistanceX = Mathf.LerpAngle(currentDistanceX, wantedDistanceX, distanceDampingX * Time.deltaTime);//平滑移到新的X轴位置，置为当前X轴位置

    //    currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);//从角度值转为角度

    //    transform.position -= currentRotation * Vector3.forward * camSpeed;//摄像机往目标移动

    //    transform.position = new Vector3(currentDistanceX, currentHeight, currentDistanceZ); //不断更新摄像机的位置

    //    LookAtMe();

    //}

    //void LookAtMe()
    //{
    //    if (smoothed)
    //    {
    //        Quaternion camRotation = Quaternion.LookRotation(target.position - transform.position);
    //        Quaternion.Slerp(transform.rotation, camRotation, Time.deltaTime * camSpeed);
    //    }
    //    else
    //    {
    //        transform.LookAt(target);//始终盯着物体
    //    }
    //}
}
