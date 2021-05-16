using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrenade : MonoBehaviour
{
    public float delay = 3f;
    public float blastRadius = 5f;
    public float explosionForce = 700f;
    [SerializeField] AudioClip grenadeExplosionSound;

    public GameObject explosionEffect;
    bool hasExploded = false;
    float countdown;


    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            // make sure it only explodes once
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        // play sound
        FindObjectOfType<AudioSource>().PlayOneShot(grenadeExplosionSound);

        // Show effect
        Instantiate(explosionEffect, transform.position, transform.rotation);

        // Get nearby objects, add forces + damage them
        Collider[] objectsInBlast= Physics.OverlapSphere(transform.position, blastRadius);
        foreach (Collider obj in objectsInBlast)
        {
            // Kill all zombies we collide with
            if (obj.gameObject.tag == "Zombie")
            {
                obj.gameObject.GetComponent<EnemyHealth>().takeDamage(100f);
            }
            if (obj.gameObject.tag == "Player")
            {
                obj.gameObject.GetComponent<PlayerHealth>().takeDamage(30f);
            }
        }

        // Remove grenade
        Destroy(gameObject);
    }
}
