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

    public float rollSpeed = 6.0f;//物体的移动速度
    public float rotateSpeed = 4.0f;//物体的旋转速度
    public float gravity = 20.0f;
    public float jumpSpeed = 8.0f;
    public float duckSpeed = 3.0f;
    public float timer = 0f;

    public CharacterController controller;
    public Animator anim;

    private bool inputEnable = true;
    private bool isGround = true;
    private bool isJump = true;
    private bool isRun = true;
    private bool isWalk = true;

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
                if (isWalk == true)
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

                            if (timer % 50 == 0 && isRun == true&&Input.GetButton("Vertical"))
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
                    if (Input.GetKeyDown(keyB)&&PlayerState.GetCurrentPower()!=0)
                    {
                        anim.SetBool("IsJump",true);
                        moveDirection.y = jumpSpeed;
                        isWalk = false;
                        PlayerState.ChangePower(-10.0f);
                    }
                    else
                    {
                        anim.SetBool("IsJump",false);
                        isWalk = true;
                    }
                }

                if (Input.GetKey(keyC))
                {
                    anim.SetBool("IsDuck", true);
                    rollSpeed = duckSpeed;
                    controller.height = 1;
                    controller.center = new Vector3(0, 0.79f, 0);
                    isJump = false;
                    isRun = false;
                }
                else if (Input.GetKeyUp(keyC))
                {
                    anim.SetBool("IsDuck", false);
                    rollSpeed = 6.0f;
                    controller.height = 2;
                    controller.center = new Vector3(0, 1.1f, 0);
                    isJump = true;
                    isRun = true;
                }
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;

        CollisionFlags flags = controller.Move(moveDirection * Time.deltaTime);
        controller.transform.Rotate(rotateDirection * Time.deltaTime, rotateSpeed);
        isGround = ((flags & CollisionFlags.Below) != 0);//判断是否在地面，如果在地面，isGround为真，否则为假
    }

    void Update()
    {
        if (PlayerState.GetCurrentLife() == 0)
        {
            StartCoroutine(Die());
        }

        IEnumerator Die()
        {
            inputEnable = false;
            anim.SetBool("IsDie",true);
            yield return StartCoroutine(waitForDie());
            HideCharacter();
            yield return StartCoroutine(waitForOneSeconds());
            if (CheckPoint.isActivept)
            {
                controller.transform.position = CheckPoint.isActivept.transform.position;
                controller.transform.position = new Vector3(controller.transform.position.x, controller.transform.position.y + 0.5f, controller.transform.position.z);
            }
            ShowCharacter();
            PlayerState.ChangeLife(PlayerState.GetMaxLife());
        }

        IEnumerator waitForDie()
        {
            yield return new WaitForSeconds(3.5f);
        }

        IEnumerator waitForOneSeconds()
        {
            yield return new WaitForSeconds(1.0f);
            inputEnable = true;
            anim.SetBool("IsDie", false);
        }
    }

    void HideCharacter()
    {
        GameObject.FindGameObjectWithTag("Body").GetComponent<SkinnedMeshRenderer>().enabled = false;
        GameObject.FindGameObjectWithTag("Wheels").GetComponent<SkinnedMeshRenderer>().enabled = false;
    }

    void ShowCharacter()
    {
        GameObject.FindGameObjectWithTag("Body").GetComponent<SkinnedMeshRenderer>().enabled = true;
        GameObject.FindGameObjectWithTag("Wheels").GetComponent<SkinnedMeshRenderer>().enabled = true;
    }

}

