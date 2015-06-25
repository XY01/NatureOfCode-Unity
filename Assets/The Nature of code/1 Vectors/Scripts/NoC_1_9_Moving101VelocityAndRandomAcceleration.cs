using UnityEngine;
using System.Collections;

public class NoC_1_9_Moving101VelocityAndRandomAcceleration: MonoBehaviour
{
	public Mover19 MoverObject;
	public Vector2 AreaSize = new Vector2( 5, 5 );

	// Use this for initialization
	void Start ()
	{
		MoverObject.Initialize (AreaSize.x, AreaSize.y);
	}
	
	// Update is called once per frame
	void Update () 
	{
		MoverObject.CheckEdges (AreaSize.x, AreaSize.y);
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube (Vector3.zero, AreaSize);
	}
}
