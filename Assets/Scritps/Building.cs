using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour
{

	public TowerData.TowerDefinition definition;

	public float health;
	public float construction_speed{get{return definition.construction_speed;}}
	public float attack_damage{get{return definition.attack_damage;}}
	public float attack_speed{get{return definition.attack_speed;}}
	public float attack_distance{get{return definition.attack_distance;}}
	public float resource_cost{get{return (float)definition.resource_cost;}}
	public Sprite sprite_built{get{return definition.sprite_built;}}


	public float last_attack_time = 0;
	public bool construction;
	public float construction_time;



	public SpriteRenderer m_spriteRenderer;           // holds the sprite object of under construction model


	public Mob target;
	
	public Bullet bullet_prefab;
	
	private BuildingSound m_buildingSound;
	
	// Use this for initialization
	void Start ()
	{
		health = definition.health;
		construction_time = definition.construction_time;
		m_buildingSound = GetComponent<BuildingSound> ();
	}

	void attack ()
	{
		// Iterate through NPCs, select those within attack distance, attack is attack speed has refreshed
		Mob[] attackableTargets = FindObjectsOfType (typeof(Mob)) as Mob[];
		foreach (Mob attackable_target in attackableTargets) {
			if (Vector3.Distance (attackable_target.transform.position, transform.position) < attack_distance) {
				//Debug.Log ("Mob within range");
				target = attackable_target;
				
				if (last_attack_time + attack_speed < Time.time) {
					m_buildingSound.PlayAttackSound ();
					// Instantiate a bullet, then assign the target to it.
					Bullet bullet_instance = Instantiate (bullet_prefab, transform.position, transform.rotation) as Bullet;
					
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
		GameObject.Destroy (gameObject);
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
		construction_time += 0.01f;
		
		attack ();
		
		while (construction_time < construction_speed) {
			construction = false;
		}
		if (construction) {
			// change the sprite
			m_spriteRenderer.sprite = sprite_built;
			attack ();
		}
	}
	

}