using UnityEngine;
using System.Collections;

public class GeneticObject_Circle : GeneticObject
{
	protected override void UpdateGeneExpression()
	{
		transform.localScale = Vector3.one * m_DNA.m_Genes[ 0 ].ScaleFrom01( 0.01f, 2 );
		//transform.localScale = Vector3.one *  Utils.NormalizedToRange( m_DNA.m_Genes[ 0 ], 0.01f, 2 );
		transform.SetLocalX( m_DNA.m_Genes[ 1 ].ScaleFrom01( -2, 2 ) );
		transform.SetLocalY( m_DNA.m_Genes[ 2 ].ScaleFrom01( -2, 2 ) );
		//transform.SetLocalX( Utils.NormalizedToRange( m_DNA.m_Genes[ 1 ], -2, 2 ) );
		//transform.SetLocalY( Utils.NormalizedToRange( m_DNA.m_Genes[ 2 ], -2, 2 ) );
	}

	public override void AssesFitness( )
	{
		float distanceFromCenter;
		float differenceInRadius;		

		distanceFromCenter = 1 - ( Mathf.Clamp01( Vector3.Distance( transform.localPosition, Vector3.zero ) / 2 ) ) ; 
		differenceInRadius = 1 - Mathf.Abs( 1 - transform.localScale.x );
		
		float fitness = ( distanceFromCenter + differenceInRadius ) / 2f;
		
		if( GeneticTester.Instance.m_FitnessEvaluation == GeneticTester.FitnessEvaluation.Linear )
			m_Fitness = fitness;
		else
			m_Fitness = fitness * fitness;

	}
}
