using UnityEngine;
using System.Collections;

public class Camera_Controller : MonoBehaviour {
	public float move_speed;
	private Vector3 last_position;
	private Vector3 camera_position;
	public Vector3 scene_limit_right;
	public Vector3 scene_limit_left;
	public Camera camera;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			last_position = Input.mousePosition;
			//Debug.Log ("last_position (mouse)");
			//Debug.Log (last_position);
		}

		if (Input.GetMouseButton (0)) {
			//Debug.Log("camera position");
			//Debug.Log (camera_position);
			Vector3 change = Input.mousePosition - last_position;
			camera_position = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.nearClipPlane));
			//Debug.Log (camera_position);
			if (camera.transform.position.x < scene_limit_right.x && camera.transform.position.x > scene_limit_left.x) {
				camera.transform.Translate (Vector3.left * move_speed * change.x);
			}

			/*if (camera.transform.position.x >= scene_limit_right.x) {
				//allow movement left only
				if (Input.mousePosition.x < camera_position.x) {
					camera.transform.Translate (Vector3.left * move_speed * change.x);
				}
			}
			if (camera.transform.position.x <= scene_limit_left.x) {
				//allow movement right only
				if (Input.mousePosition.x > camera_position.x) {
					camera.transform.Translate (Vector3.left * move_speed * change.x);
				}
			}*/
			last_position = Input.mousePosition;
        }

	}
}
