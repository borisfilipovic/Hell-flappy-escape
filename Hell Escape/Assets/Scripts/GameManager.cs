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

    /******************** PUBLIC METHODS **********************/

    public void PlayerCollided()
    {
        /// Check if came shake is enabled.
        if (shakeCameraEnabled)
        {
            StartCoroutine(Shake());
        }        
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
