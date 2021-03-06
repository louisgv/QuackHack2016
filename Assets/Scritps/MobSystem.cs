﻿using UnityEngine;
using System.Collections;

public class MobSystem : MonoBehaviour
{
	
	public Mob mobPrefab;
	
	public int health = 2;
	
	public float speed = 5.0f;
	
	float timer;
	
	public float delayTime = 5.0f;

	public int reward = 10;
	
	// Use this for initialization
	void Start ()
	{
		SpawnMob ();
	}
	
	void SpawnMob ()
	{
		Mob mobInstance = Instantiate (mobPrefab, transform.position, transform.rotation) as Mob;
		
		mobInstance.transform.SetParent (transform);
		
		mobInstance.health = health;
		
		mobInstance.move_speed = speed;

		mobInstance.reward = reward;
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;
		if (timer > delayTime) {
			SpawnMob ();
			
			timer = 0;
		}
	}
	
}
