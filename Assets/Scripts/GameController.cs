using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public static GameController _instance;

	private float timeLeft = 50;
	public Text timeText;

	[HideInInspector]
	public List<TargetBehaviour> targets = new List<TargetBehaviour>();

	 
	void Awake() {
		_instance = this;

		timeLeft = 50;
		timeText.text = timeLeft.ToString();
	}

	// Use this for initialization
	void Start () {
		iTween.ValueTo (gameObject, iTween.Hash (
			"from", timeLeft,
			"to", 0,
			"time", timeLeft,
			"onupdatetarget", gameObject,
			"onupdate", "tweenUpdate",
			"oncomplete", "GameComplete"
		));

		StartCoroutine ("SpawnTargets");
	}

	void GameComplete(){
		StopCoroutine ("SpawnTargets");
		timeText.color = Color.black;
		timeText.text = "GAME OVER";
	}

	void tweenUpdate(float newValue){
		timeLeft = newValue;
		if (timeLeft > 10) {
			timeText.text = timeLeft.ToString("#");
		} else {
			timeText.color = Color.red;
			timeText.text = timeLeft.ToString ("#.0");
		}
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
