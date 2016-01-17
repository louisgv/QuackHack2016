using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour
{
	public float health;
	public float move_speed;
	public float attack_speed;
	public float attack_damage;
	public float attack_distance;
	public float last_attack_time = 0;
	public Building target;
	public GameObject goal;

	private Animator m_animator;
	
	private MobSound m_mobSound;
	
	// Use this for initialization
	void Start ()
	{
		goal = GameObject.FindGameObjectWithTag ("Goal");
		m_animator = GetComponent<Animator> ();
		m_mobSound = GetComponent<MobSound> ();
	}

	void attack ()
	{
		// Iterate through buildings, select those within attack distance, attack is attack speed has refreshed
		// Detect if target is within range
		Building[] attackableTargets = FindObjectsOfType (typeof(Building)) as Building[];
		foreach (Building attackable_target in attackableTargets) {
			//Debug.Log (attackable_target);
			if (Vector3.Distance (attackable_target.transform.position, transform.position) < attack_distance) {
				target = attackable_target;
				if (last_attack_time + attack_speed < Time.time) {
					m_mobSound.PlayAttack ();
					
					attackable_target.takeDamage (attack_damage);
					
					last_attack_time = Time.time;
				}
			}
		} 
	}

	void onDie ()
	{
		// Change stats and sprite
		move_speed = 0;
		attack_damage = 0;
		attack_speed = 0;
		attack_distance = 0;
	}

	public void takeDamage (float damage)
	{
		// Called when a Building attacks
		if (health > 0) {
			health -= damage;
		} else if (health <= 0) {
			m_mobSound.PlayDeathSound ();
			m_animator.SetBool ("IsDead", true);
			onDie ();
		}
	}

	void move ()
	{
		transform.position = Vector3.MoveTowards (transform.position, goal.transform.position, move_speed * Time.deltaTime);
	}
	// Update is called once per frame
	void Update ()
	{
		move ();
		attack ();
	}
}
