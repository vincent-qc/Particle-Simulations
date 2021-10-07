using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : MonoBehaviour
{
    public int generation = 1;
    public float defaultLifespan = 1000;
    public GameObject offspring;

    public float kills = 0;
    public float currentLifespan;

    public CircleCollider2D CC;

    void Start()
    {
        CC = GetComponent<CircleCollider2D>();
        currentLifespan = defaultLifespan;

        float scale = GetComponent<CircleCollider2D>().radius * 3;
        transform.localScale = new Vector3(scale, scale, 1);
    }

    // Update is called once per frame
    void Update()
    {
        currentLifespan--;
        

        if(currentLifespan <= 0)
        {
            if(kills > 6) kills = 6;

            for(int i = 0; i < (kills / 2); i++)
            {
                currentLifespan = defaultLifespan;
                GameObject child = Instantiate(offspring, transform.position, Quaternion.identity);
                child.GetComponent<CircleCollider2D>().radius *= UnityEngine.Random.Range(0.8f, 1.2f);
                child.GetComponent<Predator>().generation++;
                child.GetComponent<Predator>().defaultLifespan += (UnityEngine.Random.value + 0.5f) * 100;
                child.GetComponent<Entity>().speed  *= UnityEngine.Random.value + 1f;
                float scale = GetComponent<CircleCollider2D>().radius * 3;
                child.transform.localScale = new Vector3(scale, scale, 1);
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
