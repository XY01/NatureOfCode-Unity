using UnityEngine;
using System.Collections;


/// <summary>
/// Genetic object.
///  - An object that is controlled by DNA
///  - Made to be inherited from and extended
/// </summary>
public class GeneticObject : MonoBehaviour
{
	public DNA 		m_DNA;					// Stores the genes as 0 - 1 float values
	public int 		m_NumberOfGenes = 3;	// The number of genes the DNA has
	public float 	m_Fitness = 0;			// The fitness value that is determined once the fitness evaulation is run

	public float 	m_SelectionRangeStart;	// Start selection range for creating a weighted selection list for breeding
	public float 	m_SelectionRangeEnd;	// End selection range for creating a weighted selection list for breeding

	protected bool m_Initialized = false;	// Initialized flag

	public void Initialize()
	{
		m_DNA = new DNA( m_NumberOfGenes );	// Create DNA
		InitializeDNA();					// Init DNA
		m_Initialized = true;				// Set Init to true
	}

	public virtual void InitializeDNA()
	{

	}


	void Update()
	{
		if( !m_Initialized ) return;

		UpdateGeneExpression();			// Update the gene expression
	}

	public virtual void AssesFitness()	// Asseses the fitness of the object
	{

	}

	protected virtual void UpdateGeneExpression()	// Updates how the genes ( Genotype ) are expressed ( Phenotype )
	{

	}
}


/// <summary>
/// DNA.
///  - Stores the reference to the genes ( normalized floats 0 - 1 ) being used
///  - Holds the fitness value of those genes once it is calculated
///  - 
/// </summary>
public class DNA		
{
	public float[] m_Genes;		// Array of normalized float 0 - 1
	
	public DNA( int num )
	{
		m_Genes = new float[ num ];
		
		for( int i = 0; i < m_Genes.Length; i++ )
		{
			m_Genes[ i ] = Random.Range( 0f, 1f );
		}
	}
	
	public void Mix( DNA dna1, DNA dna2 )			// Breeding funtion that mixes the genes values ( experimental, doesn't represent nature )
	{
		for( int i = 0; i < m_Genes.Length; i++ )
		{
			m_Genes[ i ] = ( dna1.m_Genes[ i ] + dna2.m_Genes[ i ] ) / 2;
		}
	}
	
	public void RandomSplice( DNA dna1, DNA dna2 )	// Breeding funtion that selects a random parent to give each gene
	{
		for( int i = 0; i < m_Genes.Length; i++ )
		{
			if( Random.Range( 0f, 1f ) > .5f )
				m_Genes[ i ] = dna1.m_Genes[ i ];
			else
				m_Genes[ i ] = dna2.m_Genes[ i ];
		}
	}
	
	public void Mutate( float mutationRate )		// Mutation funtion that allows for variation in genes of a limited gene pool
	{
		float mutationRand;
		for( int i = 0; i < m_Genes.Length; i++ )
		{
			mutationRand = Random.Range( 0f, 1f ) * m_Genes.Length;
			
			if( mutationRand < mutationRate )
				m_Genes[ i ] = Random.Range( 0f, 1f );
		}
	}
}