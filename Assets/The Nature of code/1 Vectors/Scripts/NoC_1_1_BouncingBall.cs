using UnityEngine;
using System.Collections;

public class NoC_1_1_BouncingBall : MonoBehaviour 
{
	public GameObject Sphere;

	public float X = 1;
	public float Y = 1;
	public float XSpeed = 1;
	public float YSpeed = 3.3f;

	public float BounceAreaWidth = 5;
	public float BounceAreaHeight = 5;

	void Update ()
	{
		X = X + XSpeed * Time.deltaTime;
		Y = Y + YSpeed * Time.deltaTime;
		
		//Check for bouncing.
		if ((X > BounceAreaWidth / 2f ) || (X < -BounceAreaWidth / 2f)) 
		{
			XSpeed = XSpeed * -1;
		}

		if ((Y > BounceAreaHeight / 2f ) || (Y < -BounceAreaHeight / 2f)) 
		{
			YSpeed = YSpeed * -1;
		}

		Sphere.transform.position = new Vector3 (X, Y, 0);
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube (Vector3.zero, new Vector3 (BounceAreaWidth, BounceAreaHeight, 0));
	}
}
