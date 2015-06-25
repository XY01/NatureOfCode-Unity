using UnityEngine;
using System.Collections;

public class Mover17 : MonoBehaviour 
{
	Vector3 Location;
	Vector3 Velocity;

	public void Initialize( float areaWidth, float areaHeight )
	{
		Location = new Vector3 (Random.Range (-areaWidth / 2f, areaWidth / 2f), Random.Range (-areaHeight / 2f, areaHeight / 2f), 0);
		Velocity = new Vector3 (Random.Range (-2, 2), Random.Range (-2, 2), 0);
	}

	// Update is called once per frame
	void Update()
	{
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
