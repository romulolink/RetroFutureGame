﻿using UnityEngine;
using System.Collections;

public class CharFloat : MonoBehaviour {
	Rigidbody2D rigidBody2D;
	CharStatus charStatus;

	// Use this for initialization
	void Start () {
		rigidBody2D = GetComponent<Rigidbody2D> ();
		charStatus = GetComponent<CharStatus> ();
	}

	void FixedUpdate () {
		//This could later be changed to so that when an upgrade is obtained, this part of the script is enabled for the player.
		if (charStatus.InAir() && rigidBody2D.velocity.y <= -1f && (Input.GetButton ("Leaf") || Input.GetAxis("Leaf") > 0)) {
			rigidBody2D.velocity = new Vector2 (rigidBody2D.velocity.x, -1f);
		}
	}
}