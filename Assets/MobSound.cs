using UnityEngine;
using System.Collections;

public class MobSound : SoundPlayer
{
	
	public void PlayAttack ()
	{
		PlayClip (0);
	}
	
	public void PlayDeathSound ()
	{
		PlayClip (1);
	}
	
}
