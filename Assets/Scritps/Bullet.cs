using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	[SerializeField]
	private GameObject
		target;
	
	public float speed = 1.0f;
	
	public float lerpTime = 1.0f;
	
	float currentLerpTime;
	
	public enum Trajectory
	{
		LINEAR,
		RANDOM, 
		BERSERK
	}
	
	public Trajectory trajectory = Trajectory.LINEAR;
	
	public enum EaseStyle
	{
		EASE_OUT,
		EASE_IN,
		EXPONENTIAL,
		SMOOTHSTEP,
		SMOOTHERSTEP,
		NONE
	}
	
	public EaseStyle easeStyle = EaseStyle.EASE_IN;
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("Mob")) {
			Destroy (gameObject);
		}
	}
	
	private float CalculatedTimer ()
	{
		float t = currentLerpTime / lerpTime;
		
		switch (easeStyle) {
		case EaseStyle.EASE_OUT:
			return Mathf.Sin (t * Mathf.PI * 0.5f);
		case EaseStyle.EASE_IN:
			return 1f - Mathf.Cos (t * Mathf.PI * 0.5f);
		case EaseStyle.EXPONENTIAL:
			return t * t;
		case EaseStyle.SMOOTHSTEP:
			return t * t * (3f - 2f * t);
		case EaseStyle.SMOOTHERSTEP:
			return t * t * t * (t * (6f * t - 15f) + 10f);
		case EaseStyle.NONE:
		default:
			return t;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (target != null) {
			currentLerpTime = 0f;
			currentLerpTime += Time.deltaTime;
			if (currentLerpTime > lerpTime) {
				currentLerpTime = lerpTime;
			}
			
			if (trajectory.Equals (Trajectory.LINEAR)) {
				transform.position = Vector3.Lerp (
					transform.position, 
					target.transform.position, 
					CalculatedTimer () * speed);
			}
		}
	}
	
	public void SetTarget (GameObject theTarget)
	{
		target = theTarget;
	}
}
