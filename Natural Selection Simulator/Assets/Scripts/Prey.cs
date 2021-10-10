using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : MonoBehaviour
{
    public int generation = 1;
    public float size = 1;
    public float fertility = 3;

    public GameObject offspring;

    [SerializeField] private float currentLifespan;

    CircleCollider2D CC;

    void Start()
    {
        currentLifespan = GetComponent<Entity>().defaultLifespan;
    }

    // Update is called once per frame
    void Update()
    {
        currentLifespan -= Time.deltaTime;
        
        // Produce Offspring once lifespan is over
        if(currentLifespan <= 0)
        {

            // Produce Offspring based on fertility
            for(int i = 0; i < (int) fertility; i++)
            {

                // Maximum 200 Prey
                if(GameObject.FindGameObjectsWithTag("Prey").Length <= 300) {
                    
                    // Increase to avoid crash
                    currentLifespan = 10f;

                    // Instantiate Variables
                    GameObject child = Instantiate(offspring, transform.position, Quaternion.identity);
                    Prey cPrey = child.GetComponent<Prey>();
                    Entity cEntity = child.GetComponent<Entity>();

                    // Change Name
                    child.name = $"Prey - Generation {generation + 1}";
                    
                    // Mutate Prey-Exclusive Properties
                    cPrey.generation++;
                    cPrey.fertility += (UnityEngine.Random.Range(-0.8f, 1.2f));

                    // Mutate Entity Properties
                    cEntity.speed *= UnityEngine.Random.Range(0.8f, 1.2f);
                    cEntity.defaultLifespan += UnityEngine.Random.Range(-5f, 5f);


                    // Generate the Size change values
                    float sizeChange = UnityEngine.Random.Range(0.8f, 1.2f);
                    float cSize = child.GetComponent<Prey>().size;

                    // Make sure Size is always above 0.1f the original
                    if((cSize * sizeChange) > 0.2f) {
                        cPrey.size *= sizeChange;
                        child.GetComponent<Transform>().localScale *= sizeChange;
                        //child.GetComponent<CircleCollider2D>().radius *= sizeChange;
                    }

                    cPrey.currentLifespan = cEntity.defaultLifespan;
                }
            }
            
            Destroy(gameObject);
        }
    }
}
