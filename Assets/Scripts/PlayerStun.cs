using UnityEngine;

public class PlayerStun : MonoBehaviour 
{
	public float stunDuration = 0.3f;
	public float stunStrength = 2.0f;
	public bool deactivateParticlesOnStun;
	public Vector2 newPlayerMaxVelocity = new Vector2(1, 10);

	Transform trans;

	void Start()
	{
		trans = transform;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		StunPlayer(other.gameObject);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		StunPlayer(other.gameObject);
	}

	void StunPlayer(GameObject other)
	{
		if (isActiveAndEnabled)
		{
			MovePlayer movePlayer = other.GetComponent<MovePlayer>();
			if (movePlayer != null)
			{
				Vector2 flingDirection = (Vector2)(other.transform.position - trans.position);	
				flingDirection.Normalize();
				movePlayer.addImpulse(flingDirection * stunStrength);
				movePlayer.Stun(stunDuration, deactivateParticlesOnStun);
				movePlayer.SetMaxVelocity(newPlayerMaxVelocity);
			}
			else
			{
				Debug.Log("Colliding object doesn't have a MovePlayer script");
			}
		}
	}
}
