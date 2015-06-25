using UnityEngine;
using System.Collections;

[RequireComponent (typeof (LineRenderer))]
public class NoC_1_3_VectorSubtraction : MonoBehaviour
{
	public Camera 	Cam;
	public Vector3 	Center = Vector3.zero;
	Vector3 		MousePointInWorld;
	Vector3 		VectorToMouse;

	LineRenderer Line;

	// Use this for initialization
	void Start ()
	{
		Line = gameObject.GetComponent< LineRenderer > ();
		Line.SetVertexCount (2);
	}
	
	// Update is called once per frame
	void Update () 
	{
		MousePointInWorld = Cam.ScreenToWorldPoint( new Vector3 (Input.mousePosition.x, Input.mousePosition.y, -Cam.transform.position.z ) );
		VectorToMouse = MousePointInWorld - Center;

		Line.SetPosition(0, Center);
		Line.SetPosition(1, Center + VectorToMouse );
	}
}
