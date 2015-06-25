using UnityEngine;
using System.Collections;

[RequireComponent (typeof (LineRenderer))]
public class NoC_1_5_VectorMagnitude : MonoBehaviour
{
	public Camera 	Cam;
	public Vector3 	Center = Vector3.zero;
	Vector3 		MousePointInWorld;
	Vector3 		VectorToMouse;

	LineRenderer MouseLine;

	public Vector3 	MagnitudeLineStart = new Vector3 (0, -2, 0);
	public LineRenderer MagnitudeLine;

	// Use this for initialization
	void Start ()
	{
		MouseLine = gameObject.GetComponent< LineRenderer > ();
		MouseLine.SetVertexCount (2);

		MagnitudeLine.SetVertexCount (2);
	}
	
	// Update is called once per frame
	void Update () 
	{
		MousePointInWorld = Cam.ScreenToWorldPoint( new Vector3 (Input.mousePosition.x, Input.mousePosition.y, -Cam.transform.position.z ) );

		VectorToMouse = MousePointInWorld - Center;
		float magnitudeOfVectorToMouse = VectorToMouse.magnitude;

		MouseLine.SetPosition(0, Center);
		MouseLine.SetPosition(1, Center + VectorToMouse );

		MagnitudeLine.SetPosition(0, MagnitudeLineStart);
		MagnitudeLine.SetPosition(1, MagnitudeLineStart + new Vector3( magnitudeOfVectorToMouse, 0, 0 ) );
	}
}
