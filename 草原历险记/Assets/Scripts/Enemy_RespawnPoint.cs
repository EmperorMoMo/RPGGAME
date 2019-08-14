using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_RespawnPoint : MonoBehaviour
{
    public float spawnRange = 40;//怪物出生点范围
    public GameObject enemy;//要初始化的怪物
    private Transform target;//目标
    private GameObject currentEnemy;//当前的怪物
    private bool isOutsideRange = true;//是否在怪物出生点的外面
    private Vector3 distanceToPlayer;//怪物与角色的距离
    // Start is called before the first frame update

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = transform.position - target.position;
        if(distanceToPlayer.magnitude<spawnRange)//说明主角已经进入到怪物出生点范围
        {
            if (!currentEnemy) {
           currentEnemy = Instantiate(enemy,transform.position, transform.rotation) as GameObject;

            //******
            //Author By Liu 
            UIManager.enemys.Add(currentEnemy);
            Debug.Log(UIManager.enemys.Count);
            //******
            }

            isOutsideRange = false;
        }
        else
        {
            if(currentEnemy)

                for (int i = 0; i < UIManager.enemys.Count; i++) {
                    if (UIManager.enemys[i] == currentEnemy) {
                        UIManager.enemys.Remove(currentEnemy);
                        Debug.Log("Far And Remove");
                    }
                }

                Destroy(currentEnemy);
        }
        isOutsideRange = true;
    }
}
