using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBunny_AIController : MonoBehaviour
{
    public float attackDistance = 15;//巡逻范围
    public float walkSpeed = 3.0f;
    public float rotateSpeed=30;//转向速度
    public Transform target;
    public float directionTraveltime = 2.0f;//怪物转方向的时间间隔
    public float idleTime = 1.5f;
    private float timeToNewDirection = 0;//计时器
    private Vector3 distanceToPlayer;
    private CharacterController characterController;
    private Animation animation;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animation = GetComponent<Animation>();
        if (!target)
        {
            target = GameObject.FindWithTag("Player").transform;
        }
        animation.wrapMode = WrapMode.Loop;
        animation["EBunny_Death"].wrapMode = WrapMode.Once;
        animation["EBunny_Death"].layer = 5;
        animation["EBunny_Attack"].layer = 1;
        animation["EBunny_Hit"].layer = 3;
        InitEnemy();
    }
    void InitEnemy()
    {
        while (true)
        {
            StartCoroutine( Idle());
            Attack();
        }
    }
    IEnumerator Idle()
    {
        while(true)
        {
            if(Time.time>timeToNewDirection)//到了转换方向的时间
            {
                yield return new WaitForSeconds(idleTime);//等待idleTime时间，模拟怪物在思考下一步的方向
                //通过产生随机值让怪物随机转方向
                if(Random.value>0.5)
                    transform.Rotate(new Vector3(0, 5, 0), rotateSpeed);
                else
                    transform.Rotate(new Vector3(0, -5, 0), rotateSpeed);
                timeToNewDirection = Time.time + directionTraveltime;
            }
            Vector3 walkForward = transform.TransformDirection(Vector3.forward);
            characterController.SimpleMove(walkForward * walkSpeed);
            distanceToPlayer = transform.position - target.position;//算出与目标的距离
            if(distanceToPlayer.magnitude<attackDistance)//如果两者之间的距离小于巡逻的距离，意味着主角已经进入到怪物的攻击范围
            {
                yield break;
            }
        }
        yield return null;
    }
    void Attack()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
