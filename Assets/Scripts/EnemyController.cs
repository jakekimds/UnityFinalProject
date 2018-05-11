using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class EnemyController : MonoBehaviour{

	public float lookRadius = 10f;
	public Transform target;
	public float attackDelay = 1f;
	public GameObject smokePre;

	NavMeshAgent agent;
	bool detectedPlayer = false;

	float nextAttackTime = 0;

	Animator anim;
	

	// Use this for initialization
	void Start () {
		target = PlayerTracker.playerTransform;
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponentInChildren<Animator>();
	}

	public void detectPlayer() {
		detectedPlayer = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			if (!detectedPlayer) {
				float distance = Vector3.Distance(target.position, transform.position);
				if (distance <= lookRadius) {
					detectedPlayer = true;
				}
			}
			if (detectedPlayer) {
				float distance = Vector3.Distance(target.position, transform.position);
				agent.SetDestination(target.position);
				if (distance < agent.stoppingDistance) {
					anim.SetFloat("SpeedRate", 0, 0.05f, Time.deltaTime);
					FaceTarget();
					AttackTarget();
				} else {
					anim.SetFloat("SpeedRate", 1, 0.05f, Time.deltaTime);
				}
			}
		} else {
			detectedPlayer = false; 
		}
	}

	void FaceTarget() {
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = lookRotation;

	}

	private void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}

	void Die() {
		Instantiate(smokePre, transform.position, smokePre.transform.rotation);
		Destroy(gameObject);
	}

	void AttackTarget() {
		if (Time.time >= nextAttackTime) {
			anim.SetTrigger("Attack");
			nextAttackTime = Time.time + attackDelay;
			StatsDisplay.instance.healthFlash();
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("LightCone")) {
			Die();
		}
	}
}
