using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverMenu;
    [SerializeField] Camera playerCam;
    [SerializeField] Camera weaponCam;
    [SerializeField] Image blood;
    

    private void Start()
    {
        // turn off the game over canvas when game first starts
        gameOverMenu.enabled = false;

    }

    public void HandleDeath()
    {
        gameOverMenu.enabled = true;
        blood.enabled = false;
        GetComponentInChildren<Weapon>().enabled = false; // disable shooting when in game over screen
        Time.timeScale = 0.0f; // stop time
        
        weaponCam.enabled = false;
        playerCam.enabled = false;

        FindObjectOfType<WeaponSwitcher>().enabled = false; // no cycling thru guns

        Cursor.visible = true; // show cursor
        Cursor.lockState = CursorLockMode.None;

        FindObjectOfType<MusicPlayer>().PlayGameOver();
        // FindObjectOfType<SceneLoader>().ReloadLevel();
    }
}
