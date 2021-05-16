using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody myBody;
    private float lifeTimer = 2f;
    private float timer;
    private bool hasHitSomething = false;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody>();
        Debug.Log("first" + myBody.velocity);
        transform.rotation = Quaternion.LookRotation(myBody.velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        hasHitSomething = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("velocity" + myBody.velocity);
        if (!hasHitSomething)
        {
            transform.rotation = Quaternion.LookRotation(myBody.velocity);
        }
    }

    private void Stick()
    {
        myBody.constraints = RigidbodyConstraints.FreezeAll;
    }
}
