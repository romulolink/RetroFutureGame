﻿using UnityEngine;
using System.Collections;

public class CharStatus : MonoBehaviour {
	public bool onGround, onLeftWall, onRightWall, onPlatform, isMirrored, isFloating, onSurface;
	public bool megaPunch, chargedMegaPunch;
	public float velocityX, velocityY;

	void Update() {
		velocityX = GetComponent<Rigidbody2D> ().velocity.x;
		velocityY = GetComponent<Rigidbody2D>().velocity.y;
	}

	/**
	 * Returns true if the player is in the air.
	 */
	public bool InAir() {
		return (!onGround && !onLeftWall && !onRightWall && !onPlatform && !onSurface);
	}

	public bool IsMegaPunching() {
		return (megaPunch || chargedMegaPunch);
	}

	public void NoLongerMegaPunching() {
		megaPunch = false;
		chargedMegaPunch = false;
	}
}