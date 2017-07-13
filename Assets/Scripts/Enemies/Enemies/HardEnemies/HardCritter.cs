using UnityEngine;
using System.Collections;

public class HardCritter : MonoBehaviour {
	public bool hide;
	public int timeBeforeHide;
	[Space(10)]
	public Vector2 hidingCheckPos = Vector2.one, emergedCheckPos = Vector2.one;
	public LayerMask targetMask = 256;

	float originalSpeed, count = 0;
	int originalDamage;
	bool rushing, playerCheck;
	Vector3 checkPos;
	Animator anim;
	EnemyMovement movement;
	EnemyAttack attack;

	void OnDrawGizmosSelected() {
		Gizmos.color = new Color(1, 0, 0, 0.5f); //red
		Gizmos.DrawCube(transform.position, hidingCheckPos);
		Gizmos.color = new Color(0, 1, 0, 0.5f); //green
		Gizmos.DrawCube(transform.position, emergedCheckPos);
	}

	void Start() {
		anim = GetComponent<Animator>();
		movement = GetComponent<EnemyMovement>();
		attack = GetComponent<EnemyAttack>();

		originalDamage = attack.damage;
		originalSpeed = movement.followSpeed;

		if(hide) {
			StartCoroutine(Hide());
			checkPos = hidingCheckPos;
		}
		else
			checkPos = emergedCheckPos;
	}

	void Update() {
		playerCheck = Physics2D.OverlapBox(transform.position, checkPos, 0, targetMask);

		if(playerCheck) {
			StartCoroutine(Emerge());
		}

		if(hide && anim.GetBool("move")) {
			if(playerCheck)
				count = 0;
			else
				count += Time.deltaTime;
			if(count > timeBeforeHide)
				StartCoroutine(Hide());
		}
	}

	IEnumerator Emerge() {
		anim.SetBool("move", true);

		yield return new WaitForSeconds(0.5f);

		movement.followSpeed = originalSpeed;
		movement.wanderSpeed = originalSpeed;

		checkPos = emergedCheckPos;
	}

	IEnumerator Hide() {
		movement.followSpeed = 0;
		movement.wanderSpeed = 0;

		yield return new WaitForSeconds(0.5f);

		anim.SetBool("move", false);

		checkPos = hidingCheckPos;
	}
}