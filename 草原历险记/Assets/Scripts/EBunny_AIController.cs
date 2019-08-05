using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBunny_AIController : MonoBehaviour
{
    public float damage = 1.0f;//怪物伤害值
    public float attackMoveSpeed = 5.0f;//攻击时的移动速度
    public float attackDistance = 15;//巡逻范围
    public float walkSpeed = 3.0f;
    public float rotateSpeed=30;//转向速度
    public float attackRadius = 5.0f;//怪物攻击范围
    public Transform target;
    public bool isAttacking = false;
    public float directionTraveltime = 2.0f;//怪物转方向的时间间隔
    public float idleTime = 1.5f;
    public Vector3 attackPosition = new Vector3(0, 1, 0); 
    private float timeToNewDirection = 0;//计时器
    private Vector3 distanceToPlayer;
    private CharacterController characterController;
    private Animation animation;
    private float lastAttackTime=0;
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
        animation["EBunny_Hit"].layer = 3;
        animation["EBunny_Attack"].layer = 1;
        StartCoroutine (InitEnemy());
    }
    IEnumerator InitEnemy()
    {
        while (true)
        {
            yield return StartCoroutine( Idle());
            yield return StartCoroutine(Attack());
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
            yield return null;
        }
    }
    IEnumerator Attack()
    {
        isAttacking = true;
        animation.Play("EBunny_Attack");
        transform.LookAt(target);
        Vector3 direction = transform.TransformDirection(Vector3.forward * attackMoveSpeed);
        characterController.SimpleMove(direction);
        bool lostSight = false;
        while(!lostSight)
        {
            Vector3 location = transform.TransformPoint(attackPosition) - target.position;//计算玩家的距离
            if(Time.time>lastAttackTime+2.0f&&location.magnitude<attackRadius)
            {
                target.SendMessage("ApplyDamage",damage);
                lastAttackTime = Time.time;
            }
            if(location.magnitude>attackRadius)
            {
                lostSight = true;
                yield break;
            }
            yield return null;
        }
        isAttacking = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
