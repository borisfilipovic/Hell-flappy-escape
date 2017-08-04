using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour {

    // Create singleton.
    public static GameManager instance = null;

    // Public properties.
    public bool shakeCameraEnabled = false;
    public float shakeDuration = 0.0f;
    public float shakeMagnitude = 0.0f;

    // Private properties.
    private bool playerActive = false;
    private bool gameOver = false;
    private bool gameReplay = false;
    private bool gameStarted = false;
    private GameObject player;

    [SerializeField]
    private GameObject mainMenu;

    [SerializeField]
    private GameObject playerPrefab;
    

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

    public bool GameReplay
    {
        get { return gameReplay; }
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
        Assert.IsNotNull(playerPrefab);
    }

    /******************** PRIVATE METHODS **********************/

    /// <summary>
    /// Shake main camera - Perlin noise.
    /// </summary>
    /// <returns></returns>
    IEnumerator Shake()
    {
        float elapsed = 0.0f;
        Vector3 originalCamPos = Camera.main.transform.position;
        while (elapsed < shakeDuration)
        {
            elapsed += Time.deltaTime;
            float percentComplete = elapsed / shakeDuration;
            float damper = 1.0f - Mathf.Clamp(10.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;

            x *= shakeMagnitude * damper;
            y *= shakeMagnitude * damper;

            Camera.main.transform.position = new Vector3(x, y + originalCamPos.y, originalCamPos.z);
            yield return null;

        }
        Camera.main.transform.position = originalCamPos;

    }

    IEnumerator Delay(float timeInterval)
    {
        yield return new WaitForSeconds(timeInterval);
        SetAfterPlayerCollidedState();
    }

    private void SetAfterPlayerCollidedState()
    {
        /// Set main menu state.
        MenuManager.instance.Game(GameState.replayScreen);

        /// Show main menu. 
        mainMenu.SetActive(true);

        /// Reset rock position.
        GameObject obstacle = GameObject.FindGameObjectWithTag(ConstantsManager.GetTag(ObjectTags.obstacle));
        if (obstacle != null)
        {
            Rock rockScript = obstacle.GetComponent<Rock>();
            if (rockScript != null)
            {
                /// Get random starting position coordinates.
                float randomX = Random.Range(3.75f, 4.0f);
                float randomY = Random.Range(2.0f, 8.0f);

                /// Set new starting position.
                rockScript.setStart(new Vector3(randomX, randomY, -3.2f));
            }
        }

        /// Destroy player prefab.
        Destroy(player);
    }

    /******************** PUBLIC METHODS **********************/

    public void PlayerCollided()
    {
        /// Check if camera shake is enabled.
        if (shakeCameraEnabled)
        {
            StartCoroutine(Shake());
        }        
        gameOver = true;
        playerActive = false;
        gameReplay = true;

        /// Wait for player animation to finish.
        StartCoroutine(Delay(2.0f));
    }

    public void PlayerStartedGame()
    {
        playerActive = true;
    }

    public void EnterGame()
    {
        // Hide main menu.
        mainMenu.SetActive(false);

        // Set game over flag to false-
        gameOver = false;

        // Set game started flag.
        gameStarted = true;

        // Set player active status.
        playerActive = true;

        // Instansiate player.
        player = Instantiate(playerPrefab);

        // Set players position.
        if(player != null)
        {
            Player playerScript = player.GetComponent<Player>();
            if (playerScript != null)
            {
                // Set player position.
                float randomY = Random.Range(4.0f, 6.0f);
                playerScript.setStart(new Vector3(-0.5f, randomY, -3.2f));
            }
        }

    }
}
