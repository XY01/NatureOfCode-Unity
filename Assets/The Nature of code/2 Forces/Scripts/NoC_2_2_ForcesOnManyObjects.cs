using UnityEngine;
using System.Collections;

public class NoC_2_2_ForcesOnManyObjects: MonoBehaviour
{
	public Vector2 	AreaSize = new Vector2( 5, 5 );

	public Vector3 	WindForce = new Vector3 (.01f, 0 );
	public Vector3 	Gravity = new Vector3 ( 0, 0.1f );

	public Mover21 	MoverPrefab;
	Mover21[]		Movers;

	public int 		NumberOfMovers = 10;

	// Use this for initialization
	void Start ()
	{
		Movers = new Mover21[ NumberOfMovers ];

		for (int i = 0; i < Movers.Length; i++)
		{
			Mover21 newMover = Instantiate( MoverPrefab ) as Mover21;
			newMover.Initialize ( -AreaSize.x / 2f, AreaSize.y/2f, Random.Range( 2f, 8f ) );

			Movers[i] = newMover;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		for (int i = 0; i < Movers.Length; i++) 
		{
			Movers[i].ApplyForce (WindForce);
			Movers[i].ApplyForce (Gravity);
			Movers[i].CheckEdges (AreaSize.x, AreaSize.y);
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube (Vector3.zero, AreaSize);
	}
}
