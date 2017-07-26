using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // Create singleton.
    public static GameManager instance = null;

    // Private properties.
    private bool playerActive = false;
    private bool gameOver = false;

    // Public accessors.
    public bool PlayerActive
    {
        get { return playerActive; }
    }

    public bool GameOver
    {
        get { return gameOver; }
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
}
