using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] Sun sun;
    [SerializeField] AudioClip nightaudio;
    [SerializeField] AudioClip dayaudio;
    [SerializeField] AudioClip gameoveraudio;

    AudioSource nightAudioPlayer;
    AudioSource dayAudioPlayer;
    AudioSource gameOverAudioPlayer;


    // Start is called before the first frame update
    void Start()
    {

        nightAudioPlayer = GameObject.Find("Night Audio").GetComponent<AudioSource>();
        dayAudioPlayer = GameObject.Find("Day Audio").GetComponent<AudioSource>();
        gameOverAudioPlayer = GameObject.Find("Game Over Audio").GetComponent<AudioSource>();
        dayAudioPlayer.clip = dayaudio;
        nightAudioPlayer.clip = nightaudio;
        gameOverAudioPlayer.clip = gameoveraudio;
    }

    // Update is called once per frame
    void Update()
    {
        if (sun.isDay)
        {
            if (nightAudioPlayer.isPlaying)
            {
                nightAudioPlayer.Pause();
            }
            if (!dayAudioPlayer.isPlaying)
            {
                dayAudioPlayer.Play();
            }
        }
        if (!sun.isDay)
        {
            if (dayAudioPlayer.isPlaying)
            {
                dayAudioPlayer.Pause();
            }
            if (!nightAudioPlayer.isPlaying)
            {
                nightAudioPlayer.Play();
            }
        }
    }

    public void PlayGameOver()
    {
        nightAudioPlayer.Pause();
        dayAudioPlayer.Pause();
        gameOverAudioPlayer.Play();
    }
}
