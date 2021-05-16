using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float totalHealth = 120f;
    [SerializeField] TextMeshProUGUI healthDisplay;

    private void Start()
    {
        healthDisplay.text = totalHealth.ToString();
    }

    public void healToFull()
    {
        totalHealth = 100f;
    }
    public void takeDamage(float damageTaken)
    {
        if (totalHealth > 0)
        {
            totalHealth -= damageTaken;
            healthDisplay.text = totalHealth.ToString();
        }
        if (totalHealth <= 0)
        {
            totalHealth = 0f;
            healthDisplay.text = totalHealth.ToString();
            FindObjectOfType<DeathHandler>().HandleDeath();
        }
    }
}
