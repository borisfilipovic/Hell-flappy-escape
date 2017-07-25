using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // Private
    Animator anim;

	// Use this for initialization
	void Start () {
        // Get animator.
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            anim.Play("Jump");
        }
	}
}
