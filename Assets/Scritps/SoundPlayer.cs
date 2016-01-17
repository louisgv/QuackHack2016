using UnityEngine;
using System.Collections;

public class SoundPlayer : MonoBehaviour
{
	public AudioClip[] clips;
	private AudioSource m_audioSource;
	
	public void Start ()
	{
		m_audioSource = GetComponent<AudioSource> ();
	}
	
	protected void PlayClip (int index)
	{
		m_audioSource.clip = clips [index];
		m_audioSource.Play ();
	}
	
}
