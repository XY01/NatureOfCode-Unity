using UnityEngine;
using System.Collections;

public class NoC_1_10_Moving101AcceleratingTowardMouse: MonoBehaviour
{
	public Mover110 MoverObject;
	public Vector2 AreaSize = new Vector2( 5, 5 );

	Vector3 MousePointInWorld;
	public Camera Cam;

	[Range(0,1)]
	public float MinSteerStrength = .2f;
	[Range(0, 1)]
	public float MaxSteerStrength = .8f;

	// Use this for initialization
	void Start ()
	{
		MoverObject.Initialize (AreaSize.x, AreaSize.y, Random.Range ( MinSteerStrength, MaxSteerStrength ));
	}
	
	// Update is called once per frame
	void Update () 
	{
		MousePointInWorld = Cam.ScreenToWorldPoint( new Vector3 (Input.mousePosition.x, Input.mousePosition.y, -Cam.transform.position.z ) );

		MoverObject.SetSteeringTarget (MousePointInWorld);

		MoverObject.CheckEdges (AreaSize.x, AreaSize.y);

	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube (Vector3.zero, AreaSize);
	}
}
