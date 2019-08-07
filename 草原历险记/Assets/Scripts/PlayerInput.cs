using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    public string keyUp = "w";
    public string keyDown = "s";
    public string keyLeft = "a";
    public string keyRight = "d";

    public string keyA;
    public string keyB;
    public string keyC;
    
    //public float Dup;
    //public float Dright;
    //public float Dmag;
    //public Vector3 Dvec;

    //public bool run = true;

    //private float targetUp;
    //private float targetRight;
    //private float velocityDup;
    //private float velocityDright;

    public float rollSpeed = 6.0f;//物体的移动速度
    public float rotateSpeed = 4.0f;//物体的旋转速度
    public float gravity = 20.0f;
    public float jumpSpeed = 8.0f;
    public float duckSpeed = 3.0f;
    public float timer = 0f;

    public CharacterController controller;
    public Animator anim;
    public GameObject model;

    private bool inputEnable = true;
    private bool isGround = true;
    private bool isJump = true;
    private bool isRun = true;

    private Vector3 rotateDirection = Vector3.zero;
    private Vector3 moveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer++;
        if (isGround)
        {
            if (inputEnable == true)
            {
                float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");
                moveDirection = new Vector3(0, 0, v);
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= rollSpeed;

                rotateDirection = Vector3.Lerp(rotateDirection, new Vector3(0, h, 0), 1f);
                float Dmag = Mathf.Sqrt(v * v);
                float target = (Input.GetKey(keyA) ? 2.0f : 1.0f);
                anim.SetFloat("forward", Dmag * Mathf.Lerp(anim.GetFloat("forward"), target, 0.25f));


                if (timer % 50 == 0 && rollSpeed == 6.0f)
                {
                    PlayerState.ChangePower(10.0f);
                    Debug.Log(PlayerState.GetCurrentPower().ToString());
                }

                if (PlayerState.GetCurrentPower() >= 30)
                {
                    isRun = true;
                }

                if (isRun)
                {
                    if (Input.GetKey(keyA) && PlayerState.GetCurrentPower() > 0)
                    {
                        rollSpeed = Mathf.Lerp(rollSpeed, 12.0f, 1.0f);

                        if (timer % 50 == 0 && isRun == true)
                        {
                            PlayerState.ChangePower(-10.0f);
                            Debug.Log(PlayerState.GetCurrentPower().ToString());
                        }
                    }
                    else if (PlayerState.GetCurrentPower() == 0 || Input.GetKeyUp(keyA))
                    {
                        rollSpeed = Mathf.Lerp(rollSpeed, 6.0f, 1.0f);
                        isRun = false;
                    }
                }
            }

            if (isJump)
            {
                if (Input.GetKeyDown(keyB))
                {
                    moveDirection.y = jumpSpeed;
                    inputEnable = false;
                    PlayerState.ChangePower(-10.0f);
                }
                else
                {
                    inputEnable = true;
                }
            }

            if (Input.GetKey(keyC))
            {
                rollSpeed = duckSpeed;
                controller.height = 1;
                controller.center = new Vector3(0, 0.79f, 0);
                isJump = false;
                isRun = false;
            }
            else if (Input.GetKeyUp(keyC))
            {
                rollSpeed = 6.0f;
                controller.height = 2;
                controller.center = new Vector3(0, 1.1f, 0);
                isJump = true;
                isRun = true;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;

        CollisionFlags flags = controller.Move(moveDirection * Time.deltaTime);
        controller.transform.Rotate(rotateDirection * Time.deltaTime, rotateSpeed);
        isGround = ((flags & CollisionFlags.Below) != 0);//判断是否在地面，如果在地面，isGround为真，否则为假
    }

    //void Update()
    //{
    //    targetUp = (Input.GetKey(keyUp) ? 1.0f : 0.0f) - (Input.GetKey(keyDown) ? 1.0f : 0.0f);
    //    targetRight = (Input.GetKey(keyRight) ? 1.0f : 0.0f) - (Input.GetKey(keyLeft) ? 1.0f : 0.0f);
    //    Dup = Mathf.SmoothDamp(Dup, targetUp, ref velocityDup, 0.1f);
    //    Dright = Mathf.SmoothDamp(Dright, targetRight, ref velocityDright, 0.1f);

    //    Vector2 tempDAxis = SquareToCircle(new Vector2(Dright, Dup));
    //    float Dright2 = tempDAxis.x;
    //    float Dup2 = tempDAxis.y;

    //    Dmag = Mathf.Sqrt((Dup * Dup) + (Dright * Dright));
    //    Dvec = Dright * transform.right + Dup * transform.forward;


    //    run = Input.GetKey(keyA);


    //}

    //////修复斜着跑速度变快的BUG
    //private Vector2 SquareToCircle(Vector2 input)
    //{
    //    Vector2 output = Vector2.zero;

    //    output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
    //    output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);

    //    return output;
    //}

}
