using UnityEngine;
using System.Collections;

public class NoC_1_11_Moving101ArrayOfMovers: MonoBehaviour
{
	public Mover110 MoverPrefab;
	Mover110[] 		MoverArray;

	public Vector2 AreaSize = new Vector2( 5, 5 );

	int NumberOfMovers = 10;

	Vector3 MousePointInWorld;
	public Camera Cam;

	public float MinSteerStrength = .2f;
	public float MaxSteerStrength = .8f;

	// Use this for initialization
	void Start ()
	{
		MoverArray = new Mover110[ NumberOfMovers ];

		for (int i = 0; i < NumberOfMovers; i++) 
		{
			Mover110 newMover = Instantiate( MoverPrefab ) as Mover110;
			newMover.Initialize( AreaSize.x, AreaSize.y, Random.Range ( MinSteerStrength, MaxSteerStrength ) );
			MoverArray[i] = newMover;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		MousePointInWorld = Cam.ScreenToWorldPoint( new Vector3 (Input.mousePosition.x, Input.mousePosition.y, -Cam.transform.position.z ) );

		for (int i = 0; i < NumberOfMovers; i++)
		{
			MoverArray[i].SetSteeringTarget (MousePointInWorld);
			MoverArray[i].CheckEdges (AreaSize.x, AreaSize.y);
		}

	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube (Vector3.zero, AreaSize);
	}
}
