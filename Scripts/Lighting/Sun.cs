using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    [SerializeField] float speedOfDayCycle = 1.0f;
    [SerializeField] float speedOfNightCycle = 0.5f;
    Light light;
    public bool isDay;
    [SerializeField] AudioClip rooster;
    AudioSource audiosource;
    public PlayerHealth ph;
    public Material daySkyBox;
    public Material nightSkyBox;
    /*
    [SerializeField] TextMeshProUGUI timeText;

    [SerializeField] float lastTime = 270f;
    [SerializeField] float totalTimeElapsed = 0f;
    [SerializeField] bool isIncreasing = true;*/
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        audiosource = GetComponent<AudioSource>();
        isDay = true;
        ph = FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDay)
        {
            transform.RotateAround(Vector3.zero, Vector3.right, speedOfDayCycle * Time.deltaTime);
        } else
        {
            transform.RotateAround(Vector3.zero, Vector3.right, speedOfNightCycle * Time.deltaTime);
        }
        
        transform.LookAt(Vector3.zero);
        float deg = transform.eulerAngles.x;

        /*
        // so 1 degree corresponds to 4 min
        // 15 deg to 1 hour, 360 deg 1 day

        

        if (Mathf.Abs(lastTime - deg) > 1)
        {
            lastTime = deg;
            Debug.Log("lasttime is now " + lastTime);
        } else
        {
            totalTimeElapsed += Mathf.Abs(lastTime - deg);
            lastTime = deg;
        }
        Debug.Log(deg + " elapsed: " + totalTimeElapsed);

        totalTimeElapsed %= 360; // 1 day = 720 deg
        string timeString = "";
        float minutesPassed = 4f * totalTimeElapsed;
        int minutes = Mathf.FloorToInt(minutesPassed);
        int numHours = minutes / 60;
        int numMinExtra = minutes % 60;

        // I calculated that starting time is 6:46
        numHours += 18;
        if (numHours >= 0 && numHours < 12)
        { // am
            if (numHours == 0)
            {
                numHours = 12;
            }
            timeString += numHours.ToString() + ":" + numMinExtra.ToString() + "am";
        } else
        {
            // pm
            if (numHours != 12)
            {
                numHours %= 12;
            }
            timeString += numHours.ToString() + ":" + numMinExtra.ToString() + "pm";
        }
        
        timeText.text = timeString;*/

        // starts at -90 (3am), goes to 0 (9am), up to 90 (3pm), down to 0 (9pm), down to -90 (3am)

        
        if (deg >= 0 && deg <= 180) // day 9am to 9pm
        {
            if (!isDay)
            {
                // night to morning transition
                audiosource.PlayOneShot(rooster);
                // heal player to full health...
                ph.healToFull();
                RenderSettings.skybox = daySkyBox;
                RenderSettings.fog = false;
            }
            isDay = true;
            light.enabled = true;
        } else
        {
            if (isDay)
            {
                // morning to night transition
                RenderSettings.skybox = nightSkyBox;
                RenderSettings.fog = true;
            }
            isDay = false;
            light.enabled = false;
        }

        


    }
}
