using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToFollow : MonoBehaviour {

	public GameObject targetToFollow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.position = targetToFollow.transform.position - new Vector3(0,-3,10);
	}
}
