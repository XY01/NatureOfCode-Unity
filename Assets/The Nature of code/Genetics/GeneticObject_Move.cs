using UnityEngine;
using System.Collections;

public class GeneticObject_Move : GeneticObject
{
	float rotationMin = -90;
	float rotationMax = -90;

	float m_Velocity;
	float m_Dampening;

	public override void InitializeDNA()
	{
		m_Velocity = 	m_DNA.m_Genes[ 2 ].ScaleFrom01( .01f, 6f  );
		m_Dampening = 	m_DNA.m_Genes[ 3 ].ScaleFrom01( .8f, .99f );
	}

	protected override void UpdateGeneExpression()
	{
		m_Velocity *= m_Dampening;

		transform.Translate( m_DNA.m_Genes[ 0 ].ScaleFrom01( -1f, 1f ) * m_Velocity * Time.deltaTime,
		                    m_DNA.m_Genes[ 1 ].ScaleFrom01( -1f, 1f ) * m_Velocity * Time.deltaTime,
		                    0 );
	}

	public override void AssesFitness( )
	{
		float distanceFromCenter = 1 - ( Mathf.Clamp01( Vector3.Distance( transform.localPosition, Vector3.zero ) / 4 ) ) ; 
		
		float fitness = distanceFromCenter;
		
		if( GeneticTester.Instance.m_FitnessEvaluation == GeneticTester.FitnessEvaluation.Linear )
			m_Fitness = fitness;
		else
			m_Fitness = fitness * fitness;
		
	}
}
