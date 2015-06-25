using UnityEngine;
using System.Collections;

public class Mover18 : MonoBehaviour 
{
	Vector3 Location;
	Vector3 Velocity;
	Vector3 Acceleration;
	public float 	TopSpeed = 10;

	public void Initialize( float areaWidth, float areaHeight )
	{
		Location = new Vector3 (Random.Range (-areaWidth / 2f, areaWidth / 2f), Random.Range (-areaHeight / 2f, areaHeight / 2f), 0);
		Velocity = new Vector3 (Random.Range (-1, 1), Random.Range (-1, 1), 0);

		Acceleration = new Vector3 (Random.Range (-0.001f, 0.01f), Random.Range (-0.001f, 0.01f), 0);
	}

	// Update is called once per frame
	void Update()
	{
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
