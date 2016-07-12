using UnityEngine;
using System.Collections;

public class TargetBehaviour : MonoBehaviour {

	private bool beenHit = false;
	private Animator animator;
	private GameObject parent;

	// Use this for initialization
	void Start () {
		parent = transform.parent.gameObject;
		animator = parent.GetComponent<Animator> ();
	}

	/// <summary>
	/// Called whenever the player clicks the object. Only works if you have a collider on the object.
	/// </summary>
	void OnMouseDown(){
		if (!beenHit) {
			beenHit = true;
			animator.Play ("Flip");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
