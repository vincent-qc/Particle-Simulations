using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : MonoBehaviour
{
    public int generation = 1;
    public GameObject offspring;

    [SerializeField] private float kills = 0;
    [SerializeField] private float currentLifespan;

    public CircleCollider2D CC;

    void Start()
    {
        CC = GetComponent<CircleCollider2D>();
        currentLifespan = GetComponent<Entity>().defaultLifespan;

        //float scale = GetComponent<CircleCollider2D>().radius * 2;
        //transform.localScale = new Vector3(scale, scale, 1);
    }

    // Update is called once per frame
    void Update()
    {
        currentLifespan -= Time.deltaTime;
        
        // Produce Offspring once lifespan is over
        if(currentLifespan <= 0)
        {
            // Set max number of kills
            if(kills > 16) kills = 16;

            for(int i = 0; i < Mathf.Clamp(kills * 40f / GetComponent<Entity>().speed, 0, 3); i++)
            {

                // Maximum 200 Predators
                if(GameObject.FindGameObjectsWithTag("Predator").Length <= 150)
                {

                    // Increase to avoid crash
                    currentLifespan = 10;

                    // Instantiate Variables
                    GameObject child = Instantiate(offspring, transform.position, Quaternion.identity);
                    Predator cPredator = child.GetComponent<Predator>();
                    Entity cEntity = child.GetComponent<Entity>();
                    
                    // Change Name
                    child.name = $"Predator - Generation {generation + 1}";


                    // Mutate Predator-Exclusive Properties
                    cPredator.generation++;
                    cPredator.kills = 0;


                    // Mutate Entity Properties
                    cEntity.speed  *= UnityEngine.Random.Range(0.8f, 1.2f);
                    cEntity.defaultLifespan += UnityEngine.Random.Range(-5f, 5f);
                    

                    // Mutate Vision
                    //child.GetComponent<CircleCollider2D>().radius *= UnityEngine.Random.Range(0.6f, 1.4f);
                    //float scale = GetComponent<CircleCollider2D>().radius * 2;
                    child.transform.localScale *= UnityEngine.Random.Range(0.85f, 1.15f);

                    cPredator.currentLifespan = cEntity.defaultLifespan;
                }
            }
            
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Prey")
        {
            Destroy(other.gameObject);
            kills++;
        }
    }
}
