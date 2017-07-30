using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour {

    // Create singleton.
    public static GameManager instance = null;

    // Private properties.
    private bool playerActive = false;
    private bool gameOver = false;
    private bool gameStarted = false;

    [SerializeField]
    private GameObject mainMenu;

    // Public accessors.
    public bool PlayerActive
    {
        get { return playerActive; }
    }

    public bool GameOver
    {
        get { return gameOver; }
    }

    public bool GameStarted
    {
        get { return gameStarted; }
    }

    void Awake() {
        // Create singleton.
        if (instance == null)
        {
            // Create singleton.
            instance = this;
        } else if (instance != this)
        {
            // There already is instance of this running so destroy this one.
            Destroy(gameObject);
        }

        // Dont destroy singleton when new scene is loaded. It will persist between scenes.
        DontDestroyOnLoad(gameObject);

        // Assert that menu is not null.
        Assert.IsNotNull(mainMenu);
    }

	/******************** PUBLIC METHODS **********************/

    public void PlayerCollided()
    {
        gameOver = true;
        playerActive = false;
    }

    public void PlayerStartedGame()
    {
        playerActive = true;
    }

    public void EnterGame()
    {
        // Hide main menu.
        mainMenu.SetActive(false);

        // Set game started flag.
        gameStarted = true;
    }
}
