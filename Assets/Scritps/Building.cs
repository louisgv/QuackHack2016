using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour
{

	public float health;
	public float construction_speed;
	public float attack_damage;
	public float attack_speed;
	public float attack_distance;
	public float last_attack_time = 0;
	public Mob target;
	
	public Bullet bullet_prefab;
	
	public BuildingSound m_buildingSound;
	
	// Use this for initialization
	void Start ()
	{
		m_buildingSound = GetComponent<BuildingSound> ();
	}

	void attack ()
	{
		// Iterate through NPCs, select those within attack distance, attack is attack speed has refreshed
		Mob[] attackableTargets = FindObjectsOfType (typeof(Mob)) as Mob[];
		foreach (Mob attackable_target in attackableTargets) {
			if (Vector3.Distance (attackable_target.transform.position, transform.position) < attack_distance) {
				Debug.Log ("Mob within range");
				target = attackable_target;
				
				if (last_attack_time + attack_speed < Time.time) {
					m_buildingSound.PlayAttackSound ();
					// Instantiate a bullet, then assign the target to it.
					Bullet bullet_instance = Instantiate (bullet_prefab) as Bullet;
					
					bullet_instance.SetTarget (target.gameObject);
					
					bullet_instance.easeStyle = Bullet.EaseStyle.EXPONENTIAL;
		
					attackable_target.takeDamage (attack_damage);
					
					last_attack_time = Time.time;
					
				}
			}
		} 
	}

	void onDie ()
	{
		// Change stats and sprite
		construction_speed = 0;
		attack_damage = 0;
		attack_speed = 0;
		attack_distance = 0;
	}

	public void takeDamage (float damage)
	{
		// Called when an NPC attacks
		Debug.Log ("Building took damage");

		if (health > 0) {
			health -= damage;
		} else if (health <= 0) {
			m_buildingSound.PlayDestructionSound ();
			onDie ();
		}

	}
	
	void Update ()
	{
		attack ();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
//		Debug.Log (health);

	}

}