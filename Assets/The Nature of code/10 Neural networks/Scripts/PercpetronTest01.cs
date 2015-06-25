using UnityEngine;
using System.Collections;

public class PercpetronTest01 : MonoBehaviour {

	Perceptron ptron;

	//2,000 training points
	Trainer[] training = new Trainer[2000];
	int count = 0;
	
	//The formula for a line
	float f(float x) 
	{
		return 3*x+2;
	}

	public float m_Width = 4;
	public float m_Height = 4;
	
	void Start() 
	{
		ptron = new Perceptron(3);
		
		//Make 2,000 training points.
		for (int i = 0; i < training.Length; i++) 
		{
			float x = Random.Range(-m_Width/2,m_Width/2);
			float y = Random.Range(-m_Height/2,m_Height/2);
			//Is the correct answer 1 or -1?
			int answer = 1;
			if (y < f(x)) answer = -1;
			training[i] = new Trainer(x, y, answer);
		}
	}
	
	
	void Update() 
	{		
		ptron.train(training[count].inputs, training[count].answer);
		//For animation, we are training one point at a time.
		count = (count + 1) % training.Length;
		

	}

	void OnDrawGizmos()
	{
		for (int i = 0; i < count; i++) 
		{
			int guess = ptron.feedforward(training[i].inputs);
			//Show the classification—no fill for -1, black for +1.
			Color col = Color.blue;
			if (guess > 0) col = Color.blue;
			else           col = Color.red;
			
			// Draw result
			//ellipse(training[i].inputs[0], training[i].inputs[1], 8, 8);
			Gizmos.color = col;
			Gizmos.DrawWireSphere( new Vector3( training[i].inputs[0], training[i].inputs[1], 0 ), .05f );
		}
	}
}
