using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject prey;
    public GameObject pred;
    public Text text;

    private float index = 1;

    void Start()
    {
        InstantiateEntities();
        UpdateText();
    }

    void FixedUpdate()
    {
        index -= Time.deltaTime;

        // Every 20 frames, Update the info
        if(index <= 0)
        {
            index = 3;

           UpdateText();
        }
    }

    public void Refresh()
    {
        foreach(GameObject pred in (GameObject.FindGameObjectsWithTag("Predator")))
        {
            Destroy(pred);
        }

        foreach(GameObject prey in (GameObject.FindGameObjectsWithTag("Prey")))
        {
            Destroy(prey);
        }

        InstantiateEntities();
        Invoke("UpdateText", 0.1f);
    }

    private void InstantiateEntities()
    {
        // Instantiate Predators
        for(int i = 0; i < 5; i++) {
            Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0,Screen.width), Random.Range(0,Screen.height), Camera.main.farClipPlane/2));
            Instantiate(pred, screenPosition, Quaternion.identity);
        }

        // Instantiate Prey
        for(int i = 0; i < 60; i++) {
            Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0,Screen.width), Random.Range(0,Screen.height), Camera.main.farClipPlane/2));
            Instantiate(prey, screenPosition, Quaternion.identity);
        }
    }

    private void UpdateText()
    {
         // Create Variables to calculate average later
            float predAmt = 0;
            float preyAmt = 0;

            float gen = 0;
            float vision = 0;
            float lifespan = 0;
            float speed = 0;

            float preyGen = 0;
            float size = 0;
            float fertility = 0; 
            float preyLifespan = 0;
            float preySpeed = 0;

            // Iterate through the predators and add their property to the sum
            foreach(GameObject pred in (GameObject.FindGameObjectsWithTag("Predator")))
            {
                Entity e = pred.GetComponent<Entity>();

                predAmt++;
                gen += pred.GetComponent<Predator>().generation;
                vision += pred.transform.localScale.x;
                lifespan += e.defaultLifespan;
                speed += e.speed / 40;
            }

            // Iterate through the predators and add their property to the sum
            foreach(GameObject prey in (GameObject.FindGameObjectsWithTag("Prey")))
            {
                Prey p = prey.GetComponent<Prey>();
                Entity e = prey.GetComponent<Entity>();

                preyAmt++;
                preyGen += p.generation;
                size += prey.transform.localScale.x;
                fertility += p.fertility;
                preyLifespan += e.defaultLifespan;
                preySpeed += e.speed / 40;
            }

            text.text = $"Predators: {predAmt} | Prey: {preyAmt}"
            + $"\n\n- Predators - \nAverage Generation: {Mathf.Round(gen / predAmt * 100) / 100} \nAverage Vision Radius: {Mathf.Round(vision / predAmt * 100) / 100}"
            + $"\nAverage Lifespan: {Mathf.Round(lifespan / predAmt * 100) / 100} \nAverage Speed: {Mathf.Round(speed / predAmt * 100) / 100} \n\n"
            + $"- Prey - \nAverage Generation: {Mathf.Round(preyGen / preyAmt * 100) / 100} \nAverage Size: {Mathf.Round(size / preyAmt * 100) / 100}"
            + $"\nAverage Fertility: {Mathf.Round(fertility / preyAmt * 100) / 100} \nAverage Lifespan: {Mathf.Round(preyLifespan / preyAmt * 100) / 100} \nAverage Speed {Mathf.Round(preySpeed / preyAmt * 100) / 100}";
    }
}
