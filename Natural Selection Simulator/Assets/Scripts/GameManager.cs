using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject prey;
    public GameObject pred;
    public Text text;

    private int index = 0;

    void Start()
    {
        for(int i = 0; i < 5; i++) {
            Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0,Screen.width), Random.Range(0,Screen.height), Camera.main.farClipPlane/2));
            Instantiate(pred, screenPosition, Quaternion.identity);
        }

        for(int i = 0; i < 50; i++) {
            Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0,Screen.width), Random.Range(0,Screen.height), Camera.main.farClipPlane/2));
            Instantiate(prey, screenPosition, Quaternion.identity);
        }
    }

    void FixedUpdate()
    {
        index++;

        if(index >= 20)
        {
            index = 0;

            float predAmt = 0;
            float preyAmt = 0;

            float vision = 0;
            float lifespan = 0;
            float speed = 0;

            float size = 0;
            float fertility = 0; 
            float preyLifespan = 0;
            float preySpeed = 0;

            foreach(GameObject pred in (GameObject.FindGameObjectsWithTag("Predator")))
            {
                predAmt++;
                vision += pred.GetComponent<CircleCollider2D>().radius;
                lifespan += pred.GetComponent<Predator>().defaultLifespan;
                speed += pred.GetComponent<Entity>().speed;
            }

            foreach(GameObject prey in (GameObject.FindGameObjectsWithTag("Prey")))
            {
                preyAmt++;
                size += prey.GetComponent<Prey>().size;
                fertility += prey.GetComponent<Prey>().fertility;
                preyLifespan += prey.GetComponent<Prey>().defaultLifespan;
                preySpeed += prey.GetComponent<Entity>().speed;
            }

            text.text = $"Predators: {predAmt} | Prey: {preyAmt}"
            + $"\n\n- Predators - \nAverage Vision Radius: {Mathf.Round(vision / predAmt * 100) / 100} \nAverage Lifespan: {Mathf.Round(lifespan / predAmt * 100) / 100} \nAverage Speed: {Mathf.Round(speed / predAmt * 100) / 100} \n\n" + 
            $"- Prey - \nAverage Size: {Mathf.Round(size / preyAmt * 100) / 100} \nAverage Fertility: {Mathf.Round(fertility / preyAmt * 100) / 100} \nAverage Lifespan: {Mathf.Round(preyLifespan / preyAmt * 100) / 100} \nAverage Speed {Mathf.Round(preySpeed / preyAmt * 100) / 100}";
        }
    }
}
