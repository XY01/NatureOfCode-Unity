using UnityEngine;
using System.Collections;

public class Mover110 : MonoBehaviour 
{
	Vector3 		Location;
	Vector3 		Velocity;
	Vector3 		Acceleration;
	public float 	TopSpeed = 3;

	Vector3 		SteeringTarget;
	public float 	SteeringStrength = .5f;


	public void Initialize( float areaWidth, float areaHeight, float steeringStrength )
	{
		Location = new Vector3 (Random.Range (-areaWidth / 2f, areaWidth / 2f), Random.Range (-areaHeight / 2f, areaHeight / 2f), 0);
		Velocity = new Vector3 (Random.Range (-1, 1), Random.Range (-1, 1), 0);
		SteeringStrength = steeringStrength;
	}

	public void SetSteeringTarget( Vector3 target )
	{
		SteeringTarget = target;
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 direction = SteeringTarget - Location;
		direction = direction.normalized;
		direction *= SteeringStrength;

		Acceleration = direction;

		Velocity += Acceleration;
		Velocity = Vector3.ClampMagnitude (Velocity, TopSpeed);

		Location += Velocity * Time.deltaTime;	

		transform.position = Location;
	}

	public void CheckEdges( float areaWidth, float areaHeight ) 
	{		
		//When it reaches one edge, set Location to the other.
		if (Location.x > areaWidth/2f ) 
		{
			Location.x = -areaWidth/2f;
		}
		else if (Location.x < -areaWidth/2f)
		{
			Location.x = areaWidth/2f;
		}
		
		if (Location.y > areaHeight/2f) 
		{
			Location.y = -areaHeight/2f;
		} 
		else if (Location.y < -areaHeight/2f)
		{
			Location.y = areaHeight/2f;
		}		
	}
}
