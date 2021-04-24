using UnityEngine;
using System.Collections;

public class NoC_2_1_Forces: MonoBehaviour
{
	public Mover21 MoverObject;
	public Vector2 AreaSize = new Vector2( 5, 5 );

	public Vector3 WindForce = new Vector3 (.01f, 0 );
	public Vector3 Gravity = new Vector3 ( 0, 0.1f );

	// Use this for initialization
	void Start ()
	{
		MoverObject.Initialize (MoverObject.transform.position.x, MoverObject.transform.position.y, 5f );
	}
	
	// Update is called once per frame
	void Update () 
	{
		MoverObject.ApplyForce (WindForce * Time.deltaTime);
		MoverObject.ApplyForce (Gravity * Time.deltaTime);
		MoverObject.CheckEdges (AreaSize.x, AreaSize.y);
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube (Vector3.zero, AreaSize);
	}
}
