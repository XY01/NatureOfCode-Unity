using UnityEngine;
using System.Collections;

[RequireComponent (typeof (LineRenderer))]
public class NoC_1_6_NormalizingAVector : MonoBehaviour
{
	public Camera 	Cam;
	public Vector3 	Center = Vector3.zero;
	Vector3 		MousePointInWorld;
	Vector3 		VectorToMouse;

	LineRenderer MouseLine;


	// Use this for initialization
	void Start ()
	{
		MouseLine = gameObject.GetComponent< LineRenderer > ();
		MouseLine.SetVertexCount (2);
	}
	
	// Update is called once per frame
	void Update () 
	{
		MousePointInWorld = Cam.ScreenToWorldPoint( new Vector3 (Input.mousePosition.x, Input.mousePosition.y, -Cam.transform.position.z ) );

		VectorToMouse = MousePointInWorld - Center;
		Vector3 VectorToMouseNormalized = VectorToMouse.normalized;

		MouseLine.SetPosition(0, Center);
		MouseLine.SetPosition(1, Center + VectorToMouseNormalized );
	}
}
