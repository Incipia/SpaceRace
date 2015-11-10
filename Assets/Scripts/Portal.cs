using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider2D))]
public class Portal : MonoBehaviour 
{
	public Collider2D exitPortal;

	Collider2D entranceCollider;

	// Use this for initialization
	void Start () 
	{
		entranceCollider = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//float distance = Vector3.Distance(other.transform.position, transform.position);
		other.gameObject.transform.root.transform.position = exitPortal.transform.position;
	}
}
