using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health = 100f;
    bool isDead = false;

    public void takeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");
        health -= damage;
        if (health <= 0)
        {
            if (isDead)
            {
                return; // basically only do the animation once after he's died
            }
            isDead = true;
            GetComponent<Animator>().SetTrigger("Dead");
            GetComponent<Animator>().SetBool("Attack", false);
            GetComponent<Animator>().ResetTrigger("Move");
        }
    }

    public bool IsDead()
    {
        return isDead;
    }
}
