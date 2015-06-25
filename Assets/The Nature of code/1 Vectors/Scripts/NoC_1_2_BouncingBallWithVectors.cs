using UnityEngine;
using System.Collections;

public class NoC_1_2_BouncingBallWithVectors : MonoBehaviour 
{
	public GameObject Sphere;

	Vector2 Location;
	Vector2 Velocity;

	public float BounceAreaWidth = 5;
	public float BounceAreaHeight = 5;

	void Start()
	{
		float randomXLocation = Random.Range (-BounceAreaWidth / 2f, BounceAreaWidth / 2f);
		float randomYLocation = Random.Range (-BounceAreaWidth / 2f, BounceAreaWidth / 2f);
		Location = new Vector2( randomXLocation, randomYLocation );

		float randomXVelocity = Random.Range( -4f, 4f );
		float randomYVelocity = Random.Range( -4f, 4f );
		Velocity = new Vector2( randomXVelocity, randomYVelocity );
	}

	void Update ()
	{
		Location = Location + ( Velocity * Time.deltaTime );

		
		//Check for bouncing.
		if ((Location.x > BounceAreaWidth / 2f ) || ( Location.x < -BounceAreaWidth / 2f)) 
		{
			Velocity.x = Velocity.x * -1;
		}

		if ((Location.y > BounceAreaHeight / 2f ) || ( Location.y < -BounceAreaHeight / 2f)) 
		{
			Velocity.y = Velocity.y * -1;
		}

		Sphere.transform.position = new Vector3 (Location.x, Location.y, 0);
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube (Vector3.zero, new Vector3 (BounceAreaWidth, BounceAreaHeight, 0));
	}
}
