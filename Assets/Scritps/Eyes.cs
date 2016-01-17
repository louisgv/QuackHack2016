using UnityEngine;
using System.Collections;

public class Eyes : MonoBehaviour
{
	
	public GameObject target;
	
	public GameObject leftEye;
	
	public GameObject rightEye;
	
	public GameObject leftPupil;
	
	public GameObject rightPupil;
	
	public float eyeRadius;
	
	public float pupilRadius;
	
	public float speed = 2.0f;
	
	public enum EyesState
	{
		TRACKING,
		IDLE, 
		CLOSED
	}
	
	public EyesState state = EyesState.TRACKING;
	
	Vector3 CalculateFinalPupilPosition (Vector3 pupilPosition, Vector3 eyePosition)
	{
		Vector3 distanceToTarget = target.transform.position - pupilPosition;
		
		distanceToTarget = Vector3.ClampMagnitude (distanceToTarget, eyeRadius - pupilRadius);
		
		return eyePosition + distanceToTarget;
	}
	
	void MovePupils ()
	{
		if (state.Equals (EyesState.TRACKING) && target != null) {
			Vector3 finalLeftPupilPosition = CalculateFinalPupilPosition (leftPupil.transform.position, leftEye.transform.position);
		
			Vector3 finalRightPupilPosition = CalculateFinalPupilPosition (rightPupil.transform.position, rightEye.transform.position);
		
			leftPupil.transform.position = Vector3.Lerp (leftPupil.transform.position, finalLeftPupilPosition, Time.fixedDeltaTime * speed);
		
			rightPupil.transform.position = Vector3.Lerp (rightPupil.transform.position, finalRightPupilPosition, Time.fixedDeltaTime * speed);
		} else {
			leftPupil.transform.localPosition = rightPupil.transform.localPosition = Vector3.zero;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Isolate then move this in to a different script. Or just make the Template type
		if (transform.parent.GetComponent<Building> ().target != null)
			target = transform.parent.GetComponent<Building> ().target.gameObject;
		MovePupils ();
	}
}
