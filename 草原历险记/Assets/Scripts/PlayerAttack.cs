using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PlayerAttack : MonoBehaviour
{
    public PlayerInput pi;
    public Animator anim;
    public UIManager UI;

    public float attackTime = 1.0f;
    public Vector3 attackPosition = new Vector3(0, 1, 0);
    public float attackRadius = 3.0f;

    private float damage = 0f;
    private float timer = 0.0f;
    private bool isBusy = false;
    private Vector3 ourLocation;
    private GameObject[] enemies;

    // Start is called before the first frame update
    void Awake()
    {
        pi = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        UI = GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        damage=random(damage);



        if (!isBusy && Input.GetKeyDown(KeyCode.J) && timer > attackTime)
        {
            StartCoroutine(DidAttack());
            isBusy = true;
            timer = 0;
        }
    }

    IEnumerator DidAttack()
    {
        anim.SetBool("IsAttack",true);
        yield return new WaitForSeconds(1.0f);
        anim.SetBool("IsAttack",false);

        ourLocation = transform.TransformPoint(attackPosition);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            EBunny_Status enemyStatus = enemy.GetComponent<EBunny_Status>();
            if (enemyStatus == null)
            {
                continue;
            }

            if (Vector3.Distance(enemy.transform.position, ourLocation) < attackRadius)
            {
                enemyStatus.ApplyDamage(damage);
            }
        }

        isBusy = false;
    }

    private float random(float value)
    {
        var seed = Guid.NewGuid().GetHashCode();
        Random r=new Random(seed);
        value = r.Next(30, 50);
        return value;
    }

}
