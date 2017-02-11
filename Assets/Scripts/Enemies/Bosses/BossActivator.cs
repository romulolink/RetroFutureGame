﻿using UnityEngine;
using System.Collections;

public class BossActivator : MonoBehaviour {
	public string prefab;
	int startChildren;

	void Start() {
		startChildren = transform.childCount;
	}

	void Spawn() {
		if(transform.childCount == startChildren) {
			GameObject instance = Instantiate (Resources.Load (prefab), transform.position, Quaternion.identity) as GameObject;
			instance.transform.parent = transform;
	//		instance.GetComponent<SpriteRenderer> ().sortingOrder = (int)Time.timeSinceLevelLoad;
		}
	}

	public void KillExtraChild() {
		GameObject.Destroy (transform.GetChild(startChildren).gameObject);
		Debug.Log ("Prefab spawner:\nKilled the extra child.");
	}
		
	void OnTriggerEnter2D(Collider2D col) {
		if (!col.isTrigger && col.gameObject.tag.Equals("Char")) {
			Spawn ();
		}
	}
}
