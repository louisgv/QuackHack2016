using UnityEngine;
using System.Collections;

public class BuildingSound : SoundPlayer
{
	public void PlayAttackSound ()
	{
		PlayClip (0);
	}

	public void PlayConstructionSound ()
	{
		PlayClip (1);
	}
	
	public void PlayDestructionSound ()
	{
		PlayClip (2);
	}
}
