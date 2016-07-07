﻿using UnityEngine;
using System.Collections;

public class PickUpableItem : MonoBehaviour {
	public float HoldPositionX = 0.341f, HoldPositionY = -0.332f;
	bool beingHeld;


	void FixedUpdate() {
		if (beingHeld) {
			this.gameObject.transform.localPosition = new Vector2 (HoldPositionX, HoldPositionY);
		}
	}

	public void PickedUp(GameObject player) {
		this.gameObject.transform.SetParent (player.transform);
		this.gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
		beingHeld = true;
	}
}
