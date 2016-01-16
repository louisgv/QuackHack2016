using UnityEngine;
using System.Collections;

public class scene : MonoBehaviour {
	public Vector3 position_now;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		position_now = transform.position;
		Debug.Log (position_now);
	}
}
