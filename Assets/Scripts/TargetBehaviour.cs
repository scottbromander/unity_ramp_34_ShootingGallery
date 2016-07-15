using UnityEngine;
using System.Collections;

public class TargetBehaviour : MonoBehaviour {

	private bool beenHit = false;

	private Animator animator;
	private GameObject parent;
	private bool activated;

	private Vector3 originalPos;

	public float moveSpeed = 1f;
	public float frequency = 5f;
	public float magnitude = 0.1f;

	void Awake(){
		GameController._instance.targets.Add (this);
	}

	// Use this for initialization
	void Start () {
		parent = transform.parent.gameObject;
		animator = parent.GetComponent<Animator> ();
		originalPos = parent.transform.position;
		//ShowTarget ();
	}

	/// <summary>
	/// Called whenever the player clicks the object. Only works if you have a collider on the object.
	/// </summary>
	void OnMouseDown(){
		if (!beenHit && activated) {
			beenHit = true;
			animator.Play ("Flip");

			StopAllCoroutines ();

			StartCoroutine (HideTarget ());
		}
	}

	public void ShowTarget(){
		if (!activated) {
			activated = true;
			beenHit = false;
			animator.Play ("Idle");

			iTween.MoveBy (parent, iTween.Hash ("y", 1.4, "easeType", "easeInOutExpo", "time", 0.5,
				"oncomplete", "OnShown", "oncompletetarget", gameObject));
		}
	}

	void OnShown(){
		StartCoroutine ("MoveTarget");
	}
		
	public IEnumerator HideTarget(){
		yield return new WaitForSeconds (0.5f);

		iTween.MoveBy (parent.gameObject, iTween.Hash ("y", (originalPos.y - parent.transform.position.y), 
			"easeType", "easeOutQuad", "loopType", "none",
			"time", 0.5, "oncomplete", "OnHidden",
			"oncompletetarget", gameObject));
	}

	IEnumerator MoveTarget(){
		var relativeEndPos = parent.transform.position;

		if (transform.eulerAngles == Vector3.zero) {
			relativeEndPos.x = 6;
		} else {
			relativeEndPos.x = -6;
		}

		var movementTime = Vector3.Distance (parent.transform.position, relativeEndPos) * moveSpeed;
		var pos = parent.transform.position;
		var time = 0f;

		while (time < movementTime) {
			time += Time.deltaTime;
			pos += parent.transform.right * Time.deltaTime * moveSpeed;
			parent.transform.position = pos + (parent.transform.up *
				Mathf.Sin (Time.time * frequency) * magnitude);

			yield return new WaitForSeconds (0);
		}

		StartCoroutine (HideTarget ());
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
