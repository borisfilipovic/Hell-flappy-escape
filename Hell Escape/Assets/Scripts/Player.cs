using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // Public.
    [SerializeField]
    private float jumpForce = 100.0f;
    [SerializeField]
    private AudioClip sfxJump;

    // Private.
    private Animator anim;
    private Rigidbody rigidBody;
    private bool jump = false;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        // Get animator.
        anim = GetComponent<Animator>();

        // Get rigid body.
        rigidBody = GetComponent<Rigidbody>();

        // Get audio source.
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            anim.Play("Jump");
            rigidBody.useGravity = true;
            jump = true;
            audioSource.PlayOneShot(sfxJump);
        }
	}

    void FixedUpdate() {
        if (jump == true)
        {
            jump = false;
            rigidBody.velocity = new Vector2(0, 0); // Reset velocity so he can jump again.
            rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode.Impulse);
        }
    }
}
