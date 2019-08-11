using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerInput pi;
    public Animator anim;

    public float attackTime = 1.0f;
    public Vector3 attackPosition = new Vector3(0, 1, 0);
    public float attackRadius = 3.0f;
    public float damage = 10f;

    private float timer = 0.0f;
    private bool isBusy = false;
    private Vector3 ourLocation;
    private GameObject[] enemies;

    // Start is called before the first frame update
    void Awake()
    {
        pi = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

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
}
