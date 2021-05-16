using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] DialogueTrigger instructions;
    [SerializeField] Canvas nextLevelCanvas;
    [SerializeField] Canvas pauseCanvas;
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] Camera weaponCam;
    [SerializeField] Camera playerCam;
    private void Start()
    {
        resumeUI();
        pauseCanvas.enabled = false;
        nextLevelCanvas.enabled = false;
        Invoke("TriggerInstructions", 3f);
        
 

    }

    public void pauseUI()
    {
        Cursor.visible = true; // show cursor
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        weaponCam.enabled = false;
        playerCam.enabled = false;
    }

    public void resumeUI()
    {
        Cursor.visible = false; // show cursor
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        weaponCam.enabled = true;
        playerCam.enabled = true;
        Debug.Log("UI RESUMED");
    }
    public void ReloadGame()
    {
        SceneManager.LoadScene(0);
    }
    public void TriggerInstructions()
    {
        Cursor.visible = true; // show cursor
        Cursor.lockState = CursorLockMode.None;
        instructions.TriggerDialogue();
        // pause the game until user has finished reading instructions
        Time.timeScale = 0f;

    }
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        if (nextLevelCanvas.enabled == false)
        {
            // idk i have a weird bug wehere when i skip dialogue then pres jump
            // it will skip level. So this for now ig
            return;
        }
        Debug.Log("next level");
        // once user presses N on the "beat level" dialogue
        // this is called
        nextLevelCanvas.enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LevelComplete()
    {
        pauseUI();
        nextLevelCanvas.enabled = true;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOverCanvas.enabled)
        {
            if (!pauseCanvas.enabled)
            {
                // if pausing game
                pauseUI();
            } else
            {
                // if unpausing
                resumeUI();
            }
            pauseCanvas.enabled = !pauseCanvas.enabled;
        }
        if (Input.GetKeyDown(KeyCode.I) && !nextLevelCanvas.enabled && !gameOverCanvas.enabled)
        {
            instructions.TriggerDialogue();
        }
        if (SceneManager.GetActiveScene().buildIndex == 1 && Time.timeSinceLevelLoad == 1200f)
        {
            // ten minutes
            LevelComplete();
        }
        if (SceneManager.GetActiveScene().buildIndex == 2 && Time.timeSinceLevelLoad == 1800f)
        {
            // ten minutes
            LevelComplete();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
