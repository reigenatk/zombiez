using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerHealth target;
    [SerializeField] float damage = 30f;
    [SerializeField] private AudioClip[] zombieAttackSounds;
    AudioSource m_AudioSource;

    void Start()
    {
        // grab the playerhealth script 
        target = FindObjectOfType<PlayerHealth>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    // this method is called by an Animation Event which sits on an animation object
    // which is on the animatior on the Zombie
    public void AttackEvent()
    {
        if (target == null)
        {
            return;
        }
        target.takeDamage(damage);
        PlayAttackAudio();

        target.GetComponent<DamageTaker>().ShowDamageImage(); // show blood animation
    }

    private void PlayAttackAudio()
    {
        int n = Random.Range(1, zombieAttackSounds.Length);
        m_AudioSource.clip = zombieAttackSounds[n];
        m_AudioSource.Play();
        // move picked sound to index 0 so it's not picked next time
        zombieAttackSounds[n] = zombieAttackSounds[0];
        zombieAttackSounds[0] = m_AudioSource.clip;
    }
}
