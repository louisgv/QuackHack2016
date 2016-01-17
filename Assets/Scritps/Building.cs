using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {

	public float health;
	public float construction_speed;
	public float attack_damage;
	public float attack_speed;
	public float attack_distance;
    public float resource_cost;
	public float last_attack_time = 0;
	public Mob target;

	// Use this for initialization
	void Start () {
		
	}

	void attack() {
		// Iterate through NPCs, select those within attack distance, attack is attack speed has refreshed
		Mob[] attackableTargets = FindObjectsOfType (typeof(Mob)) as Mob[] ;
		foreach (Mob attackable_target in attackableTargets) {
			//Debug.Log (attackable_target);
			if (Vector3.Distance (target.transform.position, transform.position) < attack_distance) {
				//Debug.Log ("Building within range");
				if (last_attack_time+attack_speed < Time.time) {
					attackable_target.takeDamage (attack_damage);
					last_attack_time = Time.time;
				}
			}
		} 
	}

	void onDie() {
        // Change stats and sprite
        Debug.Log("onDie called");
        GameObject.Destroy(gameObject);
	}

	public void takeDamage(float damage) {
		// Called when an NPC attacks
		if (health > 0) {
			health -= damage;
		}
        if (health <= 0) {
            Debug.Log("health is zero");
			onDie ();
		}

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Debug.Log(health);
	}

}