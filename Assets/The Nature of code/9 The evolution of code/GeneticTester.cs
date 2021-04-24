using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Genetic test.
/// - Tests a group of genertic objects for fitness, then breeds them
/// </summary>
public class GeneticTester : MonoBehaviour 
{
	static 			GeneticTester m_Instance { get; set; }
	public static 	GeneticTester Instance{ get { return m_Instance; } }

	public enum BreedingMethod		// The breeding method determines how the genes are splices together
	{
		Mix,
		RandomParent,
	}

	public enum FitnessEvaluation	// The fitness evaluation can be exponential or linear. With an exponential evaluation, the more fit the object, the more it is favored 
	{
		Linear,
		Exponential,
	}

	public BreedingMethod 		m_BreedingMethod = 		BreedingMethod.RandomParent;
	public FitnessEvaluation 	m_FitnessEvaluation = 	FitnessEvaluation.Exponential;

	public GeneticObject 		m_GeneticObjectPrefab; 										// Make generic genetic object 
	List< GeneticObject > 		m_CurrentPopulation = 	new List<GeneticObject>();			// The current popluation of genetic objects
	List< GeneticObject > 		m_SelectedPopulation = 	new List<GeneticObject>();			// The genetic objects selected for breeding

	public int 					m_PopulationCount = 		100;							// The amount of genetic objects in the population, higher amounts leads to greater variety						
	public float 				m_MatingPoolPercentage = 	.5f;							// The percentage of genetic objects that are selected for reproduction		
	public float 				m_MutationRate = 			.1f;							// The rate of mutation of genes. Mutation is important to introduce variation into a gene pool

	int 						m_Generation = 0;					// The current generation index
	float 						m_TotalFitness;						// The total fitness of all genetic objects in the current population
	float 						m_FitnessMin;						// The minimum fitness level
	float 						m_FitnessMax;						// The maximum fitness level

	public bool 				m_PauseOnSelectionPhase = true;		// Flag to pause at the selection stage so you can see the selected object
	bool 						m_SelectionPhase = false;			// Flag set when in the selection phase
	
	//public GUIText 				m_GUIText;							// GUIText for displaying the output


	void Awake()
	{
		m_Instance = this;
	}

	void Start()
	{
		CreateInitialPopulation();						// Create the initial population of genetic objects
	}

	void Update()
	{
		if( m_PauseOnSelectionPhase )
		{
			if( !m_SelectionPhase && Input.GetKeyDown( KeyCode.Return ) )
			{
				AssessFitness();
				SelectMatingPool();
				m_SelectionPhase = true;
			}
			else if( Input.GetKeyDown( KeyCode.Return ) )
			{
				BreedMatingPool();
				m_SelectionPhase = false;
			}
		}
		else
		{
			if( Input.GetKeyDown( KeyCode.Return ) )
			{
				AssessFitness();
				SelectMatingPool();
				BreedMatingPool();
			}
		}

		//m_GUIText.text = 
		//		"Generation: " + m_Generation + "\n" +
		//		"Selection Phase: " + m_SelectionPhase + "\n" +
		//		"Population: " + m_PopulationCount + "\n" +
		//		"Fitness range: " + m_FitnessMin.ToDoubleDecimalString() + "/" + m_FitnessMax.ToDoubleDecimalString() + "\n" +
		//		"Mating pool %: " + m_MatingPoolPercentage + "\n" +
		//		"Mutation Rate: " + m_MutationRate + "\n";

	}

	void CreateInitialPopulation()
	{
		for( int i = 0; i < m_PopulationCount; i++ )										// For the population count
		{
			GeneticObject gCircle = ( GeneticObject )Instantiate( m_GeneticObjectPrefab );	// Create new genetic object from the prefab
			gCircle.Initialize();															// Initialize the new object
			m_CurrentPopulation.Add( gCircle );												// Add it to the current population list
		}
	}

	// assess fitness
	void AssessFitness()
	{
		for( int i = 0; i < m_CurrentPopulation.Count; i++ )	// For the population count
		{
			m_CurrentPopulation[ i ].AssesFitness();			// Call the assess fitness method
		}
	}




	//Select mating pool
	void SelectMatingPool()
	{
		m_TotalFitness = 0;																		// Reset total fitness
		m_CurrentPopulation =  m_CurrentPopulation.OrderBy( x => x.m_Fitness ).ToList();		// Order the current population list by fitness ( using LINQ )

		m_FitnessMin = m_CurrentPopulation[ 0 ].m_Fitness;										// Set the minimum fitness as first in the list ordered by fitness
		m_FitnessMax = m_CurrentPopulation[ m_CurrentPopulation.Count - 1 ].m_Fitness;			// Set the minimum fitness as last in the list ordered by fitness

		int startIndex = (int)( m_PopulationCount * m_MatingPoolPercentage );					// Find start index to start the selection from, based on the mating pool percentage
		startIndex = m_PopulationCount - startIndex;

		for( int i = startIndex; i < m_PopulationCount; i++ )									// From the start index to the population count
		{
			m_SelectedPopulation.Add( m_CurrentPopulation[ i ] );								// Add selected to the selected population list
			m_TotalFitness += m_CurrentPopulation[ i ].m_Fitness;								// Sum total fitness
		}

		for( int i = 0; i < m_SelectedPopulation.Count; i++ )
		{
			if( i == 0 )
			{
				m_SelectedPopulation[ i ].m_SelectionRangeStart = 0;													// Set the start of the selection range of the object
			}
			else
			{
				m_SelectedPopulation[ i ].m_SelectionRangeStart = m_SelectedPopulation[ i - 1 ].m_SelectionRangeEnd;	// Set the start of the selection range of the object
			}

			float percentageOfTotalFitness = m_SelectedPopulation[ i ].m_Fitness / m_TotalFitness;										// Find the percentage of total fitness that this object represents
			m_SelectedPopulation[ i ].m_SelectionRangeEnd = m_SelectedPopulation[ i ].m_SelectionRangeStart + percentageOfTotalFitness;	// Use this fitness percentage to set the end range for this objects selection value

			m_CurrentPopulation.Remove( m_SelectedPopulation[ i ] );																	// Remove the object from the currnet popluation list
		}

		for( int i = 0; i < startIndex; i++ )
		{
			Destroy( m_CurrentPopulation[ i ].gameObject );																				// Destroy all objects left in the current popluation (those unselected for breeding )	
		}
	}

	void BreedMatingPool()
	{
		List< GeneticObject > newGeneration = new List<GeneticObject>();	// Create a list of genetic objects for the new generation

		for( int i = 0; i < m_PopulationCount; i++ )						// Create offspring up to the population count so we have a stable population
		{
			float rand1 = Random.Range( 0f, 1f );							// Generate a random number to select the first parent
			float rand2 = Random.Range( 0f, 1f );							// Generate a random number to select the second parent
			
			GeneticObject parent1 = m_SelectedPopulation[ 0 ];
			GeneticObject parent2 = m_SelectedPopulation[ 1 ];


			for( int j = 0; j < m_SelectedPopulation.Count; j++ )																		// Find both parents
			{
				if( rand1 > m_SelectedPopulation[ j ].m_SelectionRangeStart && rand1 < m_SelectedPopulation[ j ].m_SelectionRangeEnd )	// If the random number is within the bounds of the objects selection range
				{
					parent1 = m_SelectedPopulation[ j ];																				// Select as parent 1
				}
				
				if( rand2 > m_SelectedPopulation[ j ].m_SelectionRangeStart && rand2 < m_SelectedPopulation[ j ].m_SelectionRangeEnd )	// If the random number is within the bounds of the objects selection range
				{
					parent2 = m_SelectedPopulation[ j ];																				// Select as parent 1
				}
			}		

			newGeneration.Add( Breed( parent1, parent2 ) );																				// Add the bred parents offspring to the new generation		
		}

		for( int i = 0; i < m_SelectedPopulation.Count; i++ )
		{
			Destroy( m_SelectedPopulation[ i ].gameObject );																			// Destroy all parents so only the children remain		
		}
		
		m_CurrentPopulation.Clear();								// Clear the current population list
		m_SelectedPopulation.Clear();								// Clear the selected population list
		
		m_CurrentPopulation = newGeneration.ToList();				// Make the current population the new generation

		m_Generation++;												// Increment the generation count
	}


	// Breeds two parents together
	GeneticObject Breed( GeneticObject parent1, GeneticObject parent2 )
	{
		GeneticObject childObject = ( GeneticObject )Instantiate( m_GeneticObjectPrefab );		// Create a child object
		childObject.Initialize();																// Initialize the child object

		if( m_BreedingMethod == BreedingMethod.Mix )											// Depending on the breeding method, call the appropriate DNA breeding funtion
		{
			childObject.m_DNA.Mix( parent1.m_DNA, parent2.m_DNA );
		}
		else
		{
			childObject.m_DNA.RandomSplice( parent1.m_DNA, parent2.m_DNA );
		}

		childObject.m_DNA.Mutate( m_MutationRate );												// Check for mutations

		childObject.InitializeDNA();

		return childObject;
	}
}
