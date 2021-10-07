using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : MonoBehaviour
{
    public int generation = 1;
    public float size = 1;
    public float defaultLifespan = 1000;
    public GameObject offspring;
    public float fertility = 3;

    public float currentLifespan;

    CircleCollider2D CC;

    void Start()
    {
        currentLifespan = defaultLifespan;
    }

    // Update is called once per frame
    void Update()
    {
        currentLifespan--;
        

        if(currentLifespan <= 0)
        {
            for(int i = 0; i < (int) fertility; i++)
            {
                currentLifespan = defaultLifespan;
                GameObject child = Instantiate(offspring, transform.position, Quaternion.identity);
                child.GetComponent<Prey>().generation++;
                child.GetComponent<Prey>().defaultLifespan += (UnityEngine.Random.value + 0.5f) * 100;
                child.GetComponent<Prey>().fertility += (UnityEngine.Random.Range(-0.5f, 0.5f));
                child.GetComponent<Entity>().speed  *= UnityEngine.Random.value + 1f;

                float sizeChange = UnityEngine.Random.Range(0.6f, 1.4f);
                float cSize = child.GetComponent<Prey>().size;
                if((cSize * sizeChange) > 0.1f) {
                    child.GetComponent<Prey>().size *= sizeChange;
                    child.GetComponent<Transform>().localScale *= sizeChange;
                    child.GetComponent<CircleCollider2D>().radius *= sizeChange;
                }
            }
            
            Destroy(gameObject);

        }
    }
}
