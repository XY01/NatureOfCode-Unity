using UnityEngine;
using System.Collections;


public class NoC_2_5_FluidResistance : MonoBehaviour
{
	public Vector2 	AreaSize = new Vector2( 5, 5 );

	public Vector3 	WindForce = new Vector3 (1, 0 );
	public Vector3 	Gravity = new Vector3 ( 0, -9.81f);

	public Mover21 	MoverPrefab;
	Mover21[]		Movers;

	public int 		NumberOfMovers = 10;

	public float	FrictionCoefficient = .1f;
	float			FrictionNormal = 1;						// Assuming a normal of 1 for now as per the tute

	// Use this for initialization
	void Start ()
	{
		Movers = new Mover21[ NumberOfMovers ];

		for (int i = 0; i < Movers.Length; i++)
		{
			Mover21 newMover = Instantiate( MoverPrefab ) as Mover21;
			newMover.Initialize ( Random.Range(-AreaSize.x * .4f, AreaSize.x * .4f), AreaSize.y/2f, Random.Range( 1f, 4f ) );

			Movers[i] = newMover;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		float frictionMag = FrictionCoefficient * FrictionNormal;
	

		for (int i = 0; i < Movers.Length; i++) 
		{
			Vector3 friction = -(Movers[i].Velocity).normalized;
			friction *= frictionMag;

			Movers[i].ApplyForce(friction);
			Movers[i].ApplyForce ( WindForce * Time.deltaTime);
			Movers[i].ApplyForce ( Gravity * Movers[i].Mass * Time.deltaTime);
			Movers[i].CheckEdges ( AreaSize.x, AreaSize.y );
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube (Vector3.zero, AreaSize);
	}
}
