using UnityEngine;
using System.Collections;

public class Mover21 : MonoBehaviour 
{
	Vector3 		Location;
	public Vector3 	Velocity;
	Vector3 		Acceleration;
	public float 	Mass = 1;

	//--- ADDED FOR TUTE 2.5
	public bool		_InLiquidTrigger = false;
	float			_LiquidDragCoefficient = .1f;


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
		if (_InLiquidTrigger)
			ApplyDrag(_LiquidDragCoefficient);

		Velocity += Acceleration;
		Location += Velocity * Time.deltaTime;	

		transform.position = Location;

		Acceleration = Vector3.zero;
	}

	public void ApplyForce(Vector3 force)
	{
		force /= Mass;
		Acceleration += force;
	}

    void ApplyDrag(float dragCoefficient)
    {
		float speed = Velocity.magnitude;
		float dragMagnitude = dragCoefficient * speed * speed;

		Vector3 drag = -Velocity.normalized;
		drag *= dragMagnitude;

		ApplyForce(drag);
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

    private void OnTriggerEnter(Collider other)
    {
		// 6 is the index of the 'Liquid trigger' layer
        if(other.gameObject.layer == 6)
        {
			_InLiquidTrigger = true;
		}
    }

	private void OnTriggerExit(Collider other)
	{
		// 6 is the index of the 'Liquid trigger' layer
		if (other.gameObject.layer == 6)
		{
			_InLiquidTrigger = false;
		}
	}
}
