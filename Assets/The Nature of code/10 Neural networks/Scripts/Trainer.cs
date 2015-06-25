using UnityEngine;
using System.Collections;

public class Trainer
{		
	//A "Trainer" object stores the inputs and the correct answer.
	public float[] inputs;
	public int answer;
	
	public Trainer(float x, float y, int a) 
	{
		inputs = new float[3];
		inputs[0] = x;
		inputs[1] = y;

		//Note that the Trainer has the bias input built into its array.
		inputs[2] = 1;
		answer = a;
	}

}
