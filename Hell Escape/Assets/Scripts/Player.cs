using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour {

    // Public.
    [SerializeField]
    private float jumpForce = 100.0f;
    [SerializeField]
    private AudioClip sfxJump;
    [SerializeField]
    private AudioClip sfxDeath;

    // Private.
    private Animator anim;
    private Rigidbody rigidBody;
    private bool jump = false;
    private AudioSource audioSource;
    private string obstacleTag;
    private string jumpAnimationName;

    void Awake()
    {
        // Check if objects are really here. Defensive programming.
        Assert.IsNotNull(sfxJump);
        Assert.IsNotNull(sfxDeath);
    }

	// Use this for initialization
	void Start () {
        // Get animator.
        anim = GetComponent<Animator>();

        // Get rigid body.
        rigidBody = GetComponent<Rigidbody>();

        // Get audio source.
        audioSource = GetComponent<AudioSource>();

        // Get obstacle tag.
        obstacleTag = ConstantsManager.GetTag(ObjectTags.obstacle);

        // Get jump animation name.
        jumpAnimationName = ConstantsManager.GetAnimationName(Animations.jump);
	}
	
	// Update is called once per frame
	void Update () {
        // If game over then do not allow player controlls.
        if (GameManager.instance.GameOver) { return; }

        // Check if mouse button was clicked.
        if (!GameManager.instance.GameOver && GameManager.instance.GameStarted )
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Set player is active in game manager.
                GameManager.instance.PlayerStartedGame();

                // Play jump animation.
                anim.Play(jumpAnimationName);

                // Set gravity to true so he starts falling.
                rigidBody.useGravity = true;

                // Set jump flag to true.
                jump = true;

                // Play jump sound.
                audioSource.PlayOneShot(sfxJump);
            }
        }
	}

    void FixedUpdate() {
        // Check if user jumped.
        if (jump == true)
        {
            // Reset jump flag.
            jump = false;

            // Reset velocity so he stops moving. He can jump again now.
            rigidBody.velocity = new Vector2(0, 0); // Reset velocity so he can jump again.

            // Push player character straight up into the air.
            rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode.Impulse);
        }        
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check collision.
        if (collision.gameObject.tag == obstacleTag)
        {
            // If player hits obstacle let obstacle force/bounce him back a little.
            rigidBody.AddForce(new Vector2(-50, 20), ForceMode.Impulse);

            // Turn collisions off so he won't hit other obstacles.
            rigidBody.detectCollisions = false;

            // Play death sound.
            audioSource.PlayOneShot(sfxDeath);

            // Notify game manager that player collided with obstacle.
            GameManager.instance.PlayerCollided();
        }
    }
}
