using UnityEngine;
using System.Collections;

public class TargetBehaviour : MonoBehaviour {

	private bool beenHit = false;

	private Animator animator;
	private GameObject parent;
	private bool activated;

	private Vector3 originalPos;


	// Use this for initialization
	void Start () {
		parent = transform.parent.gameObject;
		animator = parent.GetComponent<Animator> ();
		originalPos = parent.transform.position;
		ShowTarget ();
	}

	/// <summary>
	/// Called whenever the player clicks the object. Only works if you have a collider on the object.
	/// </summary>
	void OnMouseDown(){
		if (!beenHit && activated) {
			beenHit = true;
			animator.Play ("Flip");
			StartCoroutine (HideTarget ());
		}
	}

	public void ShowTarget(){
		if (!activated) {
			activated = true;
			beenHit = false;
			animator.Play ("Idle");

			iTween.MoveBy (parent, iTween.Hash ("y", 1.4, "easeType", "easeInOutExpo", "time", 0.5));
		}
	}

	public IEnumerator HideTarget(){
		yield return new WaitForSeconds (0.5f);

		iTween.MoveBy (parent.gameObject, iTween.Hash ("y", (originalPos.y - parent.transform.position.y), 
			"easeType", "easeOutQuad", "loopType", "none",
			"time", 0.5, "oncomplete", "OnHidden",
			"oncompletetarget", gameObject));
	}

	/// <summary>
	/// After the tween finishes, we now make sure we can be shown again.
	/// </summary>

	void OnHidden(){
		parent.transform.position = originalPos;
		activated = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
