using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    public float rollSpeed = 6.0f;//物体的移动速度
    public float rotateSpeed = 4.0f;//物体的旋转速度
    public float gravity = 20.0f;
    public CharacterController controller;

    private bool isGround = true;
    private Vector3 rotateDirection = Vector3.zero;
    private Vector3 moveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGround)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            moveDirection = new Vector3(h, 0, v);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= rollSpeed;

            if (h > 0)
            {
                rotateDirection = Vector3.Lerp(rotateDirection, new Vector3(0, 1, 0), 0.5f);
            }
            else if (h < 0)
            {
                rotateDirection = Vector3.Lerp(rotateDirection, new Vector3(0, -1, 0), 0.5f);
            }
            else
            {
                rotateDirection = new Vector3(0, 0, 0);
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;

        CollisionFlags flags = controller.Move(moveDirection * Time.deltaTime);
        controller.transform.Rotate(rotateDirection * Time.deltaTime, rotateSpeed);
        isGround = ((flags & CollisionFlags.Below) != 0);//判断是否在地面，如果在地面，isGround为真，否则为假
    }
}
