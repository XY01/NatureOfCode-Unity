using UnityEngine;
using System.Collections;

public class Mover21 : MonoBehaviour 
{
	Vector3 		Location;
	Vector3 		Velocity;
	Vector3 		Acceleration;
	public float 	Mass = 10.0f;



	public void Initialize( float XPos, float YPos, float mass )
	{
		Location = new Vector3 ( XPos, YPos, 0);
		Velocity = new Vector3 ( 0, 0, 0);
		Mass = mass;
		transform.localScale = Vector3.one * .1f * mass;
	}

	// Update is called once per frame
	void Update()
	{
		Velocity += Acceleration;
		Location += Velocity * Time.deltaTime;	

		transform.position = Location;
	}

	public void ApplyForce(Vector3 force)
	{
		force /= Mass;
		Acceleration += force;
	}

	public void CheckEdges( float areaWidth, float areaHeight ) 
	{		
		//When it reaches one edge, set Location to the other.
		if (Location.x > areaWidth/2f ) 
		{
			Location.x = areaWidth/2f;
			Velocity.x = -Velocity.x;
		}
		else if (Location.x < -areaWidth/2f)
		{
			Location.x = -areaWidth/2f;
			Velocity.x = -Velocity.x;
		}
		
		if (Location.y > areaHeight/2f) 
		{
			Location.y = areaHeight/2f;
			Velocity.y = -Velocity.y;
		} 
		else if (Location.y < -areaHeight/2f)
		{
			Location.y = -areaHeight/2f;
			Velocity.y = -Velocity.y;
		}		
	}
}
