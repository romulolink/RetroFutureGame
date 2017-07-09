﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	public float followDist, followDistUp, followDistDown, wanderDist;
	public float moveSpeed;
	
	public Vector2 groundcheckPos = Vector2.one, wallcheckPos = Vector2.one;
	public LayerMask groundcheckMask, wallcheckMask, raycastMask;

	bool grounded, wallcheck;
	int wanderDir = -1;
	float startPos;
	RaycastHit2D raycastHit;
	Rigidbody2D rb2D;
	GameObject player;

	void OnDrawGizmosSelected() {
		Gizmos.color = new Color(1, 1, 0, 0.5f); //yellow
		Gizmos.DrawCube(transform.position - new Vector3(0, groundcheckPos.y, 0), new Vector3(groundcheckPos.x, 0.1f, 0));
		Gizmos.DrawCube(transform.position - new Vector3(wallcheckPos.x * -wanderDir, 0, 0), new Vector3(0.1f, wallcheckPos.y, 0));
	}

	void Start() {
		rb2D = GetComponent<Rigidbody2D>();
		player = GameObject.Find("Char");
		startPos = transform.position.x;
	}

	void Update() {
		raycastHit = Physics2D.Raycast(transform.position, RaycastDirection(), followDist, raycastMask);
		grounded = Physics2D.OverlapBox(transform.position - new Vector3(0, groundcheckPos.y, 0), new Vector3(groundcheckPos.x, 0.1f, 0), 0, groundcheckMask);
		wallcheck = Physics2D.OverlapBox(transform.position - new Vector3(wallcheckPos.x * -wanderDir, 0, 0), new Vector3(0.1f, wallcheckPos.y, 0), 0, wallcheckMask);
	}

	void FixedUpdate() {
		if(wallcheck) {
//			if(Random.Range(0, 100) == 0)
				wanderDir *= -1;
		}

		SpriteFacing();

		if(grounded) {
			if(raycastHit) {
				if(raycastHit.transform.name == "Char") {
					rb2D.velocity += Mathf.Abs(rb2D.velocity.x) < moveSpeed ? new Vector2(moveSpeed * 0.1f * Mathf.Sign(player.transform.position.x - transform.position.x), 0) : Vector2.zero;
					wanderDir *= (int)Mathf.Sign(player.transform.position.x - transform.position.x);
				}
				else
					rb2D.velocity += Mathf.Abs(rb2D.velocity.x) < moveSpeed * 0.5f ? new Vector2(moveSpeed * 0.1f * wanderDir, 0) : Vector2.zero;
			}
			else
				rb2D.velocity += Mathf.Abs(rb2D.velocity.x) < moveSpeed * 0.5f ? new Vector2(moveSpeed * 0.1f * wanderDir, 0) : Vector2.zero;

			if(transform.position.x - startPos > wanderDist && wanderDir == 1 || startPos - transform.position.x > wanderDist && wanderDir == -1)
				wanderDir *= -1;
		}
	}

	Vector2 RaycastDirection() {
		Vector2 raycastDirection = player.transform.position - transform.position;
		if(raycastDirection.y < -followDistDown || raycastDirection.y > followDistUp || Mathf.Abs(raycastDirection.x) > followDist)
			raycastDirection = new Vector2(followDist * wanderDir, 0);
		Debug.DrawRay(transform.position, raycastDirection);
		return raycastDirection;
	}

	void SpriteFacing() {
		if(rb2D.velocity.x > 1f) {
			transform.rotation = Quaternion.Euler(0, 180, 0);
			GetComponent<Animator>().enabled = true;
		}
		else if(rb2D.velocity.x < -1f) {
			transform.rotation = Quaternion.Euler(0, 0, 0);
			GetComponent<Animator>().enabled = true;
		}
		else {
			GetComponent<Animator>().enabled = false;
		}

	}
}