using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBunny_Status : MonoBehaviour
{
    public float health = 100;
    private bool isDead = false;
    private Animation animation;
    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ApplyDamage(float damage)
    {
        if (health <= 0)
            return;
        health -= damage;
        animation.Play("WolfBaby-TakeDamage2");
        if (health<=0&&!isDead)
        {
            health = 0;
            isDead=true;
            StartCoroutine(Die());
        }
    }
    IEnumerator Die()
    {
        animation.Stop();
        animation.Play("WolfBaby-Death");
        Destroy(this.GetComponent<EBunny_AIController>());
        yield return new WaitForSeconds(2.0f);
        Destroy(this.gameObject);
    }
    public bool IsDead()
    {
        return isDead;
    }
}
