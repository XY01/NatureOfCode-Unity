using UnityEngine;
using System.Collections;

public class Mover19 : MonoBehaviour 
{
	Vector3 		Location;
	Vector3 		Velocity;
	Vector3 		Acceleration;
	public float 	TopSpeed = 3;

	public bool 	ScaleByConstantValue = true;
	public float 	AccelerationConstantScaler = 1;


	public void Initialize( float areaWidth, float areaHeight )
	{
		Location = new Vector3 (Random.Range (-areaWidth / 2f, areaWidth / 2f), Random.Range (-areaHeight / 2f, areaHeight / 2f), 0);
		Velocity = new Vector3 (Random.Range (-1, 1), Random.Range (-1, 1), 0);
	}

	// Update is called once per frame
	void Update()
	{
		Acceleration = Random.insideUnitCircle;

		if (ScaleByConstantValue)
		{
			Acceleration *= AccelerationConstantScaler;
		}
		else
		{
			Acceleration *= Random.Range( 0f, 2f );
		}


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
