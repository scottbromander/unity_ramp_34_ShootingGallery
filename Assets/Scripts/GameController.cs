﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	public static GameController _instance;

	[HideInInspector]
	public List<TargetBehaviour> targets = new List<TargetBehaviour>();

	void Awake() {
		_instance = this;
	}

	// Use this for initialization
	void Start () {
		StartCoroutine ("SpawnTargets");
	}

	void SpawnTarget(){
		int index = Random.Range (0, targets.Count);
		TargetBehaviour target = targets [index];

		target.ShowTarget ();
	}

	IEnumerator SpawnTargets(){
		yield return new WaitForSeconds (1.0f);

		while (true) {
			int numOfTargets = Random.Range (1, 4);

			for (int i = 0; i < numOfTargets; i++) {
				SpawnTarget ();
			}

			yield return new WaitForSeconds(Random.Range(0.5f * numOfTargets, 2.5f));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}