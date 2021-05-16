using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTaker : MonoBehaviour
{

    [SerializeField] Canvas impactCanvas;
    [SerializeField] float lengthOnScreen = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        impactCanvas.enabled = false;
    }

    public void ShowDamageImage()
    {
        StartCoroutine(ShowBlood());
    }

    IEnumerator ShowBlood()
    {
        impactCanvas.enabled = true;
        yield return new WaitForSeconds(lengthOnScreen); // this is how long the image stays on screen
        impactCanvas.enabled = false;
    }
}
