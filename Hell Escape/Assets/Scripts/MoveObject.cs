using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour {

    // Public properties.
    [SerializeField]
    float objectSpeed = 1f;

    // Private properties.
    private float resetPosition = -42.0f;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Move object to the left.
        transform.Translate(Vector3.left * (objectSpeed * Time.deltaTime));

        if (transform.localPosition.x <= resetPosition) {
            Vector3 newPosition = new Vector3(66.0f, transform.position.y, transform.position.z);
            transform.position = newPosition;
        }
    }
}
