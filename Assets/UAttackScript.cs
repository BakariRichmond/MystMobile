using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UAttackScript : MonoBehaviour {
	public Rigidbody rb;
	[SerializeField] public int Power;
	[SerializeField] public bool DestroyOnContact;
	[SerializeField] private string Direction;
	[SerializeField] private string DeadlyTag;
	[SerializeField] public float Speed;
	[SerializeField] public float DestroyAfter;
	[SerializeField] public string MoveLeftOn1;
	[SerializeField] public string MoveLeftOn2;
	[SerializeField] public string MoveLeftOn3;

	[SerializeField] public string MoveRightOn1;
	[SerializeField] public string MoveRightOn2;
	[SerializeField] public string MoveRightOn3;

	[SerializeField] public string MoveBackOn1;
	[SerializeField] public string MoveBackOn2;
	[SerializeField] public string MoveBackOn3;

	[SerializeField] public string MoveForwardOn1;
	[SerializeField] public string MoveForwardOn2;
	[SerializeField] public string MoveForwardOn3;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();

		Destroy (gameObject, DestroyAfter);
		if (Direction == "forward") {
			rb.velocity = transform.TransformDirection (Vector3.forward * Speed);
		} else if (Direction == "back") {
			rb.velocity = transform.TransformDirection (Vector3.back * Speed);
		} else if (Direction == "left") {
			rb.velocity = transform.TransformDirection (Vector3.left * Speed);
		} else if (Direction == "right") {
			rb.velocity = transform.TransformDirection (Vector3.right * Speed);
		}
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter (Collider other) {

		if (other.gameObject.name == MoveRightOn1 | other.gameObject.name == MoveRightOn2 | other.gameObject.name == MoveRightOn3) {

			rb.velocity = transform.TransformDirection (Vector3.right * Speed);
		}

		if (other.gameObject.name == MoveLeftOn1 | other.gameObject.name == MoveLeftOn2 | other.gameObject.name == MoveLeftOn3) {

			rb.velocity = transform.TransformDirection (Vector3.left * Speed);
		}

		if (other.gameObject.name == MoveForwardOn1 | other.gameObject.name == MoveForwardOn2 | other.gameObject.name == MoveForwardOn3) {

			rb.velocity = transform.TransformDirection (Vector3.forward * Speed);
		}
		if (other.gameObject.name == MoveBackOn1 | other.gameObject.name == MoveBackOn2 | other.gameObject.name == MoveBackOn3) {

			rb.velocity = transform.TransformDirection (Vector3.back * Speed);
		}
		if (other.gameObject.tag == DeadlyTag) {
			if(DestroyOnContact){
			Destroy (gameObject);
			}
		}

	}
}