using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : MonoBehaviour
{
    public int SC = 800;
    public float altruism = 0.5f;

    public GameObject NewCitizen;
    public float timer = 20;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            timer = 20;

            for(int i = 0; i < 2; i++)
            {
                if(GameObject.FindGameObjectsWithTag("Citizen").Length <= 200)
                {
                    GameObject child = Instantiate(NewCitizen, transform.position, Quaternion.identity);
                    Citizen c = child.GetComponent<Citizen>();

                    c.name = "Citizen";

                    c.SC = Mathf.Clamp((int) (SC * (UnityEngine.Random.Range(0.2f, 1.8f))), 0, 1400);
                    c.altruism *= UnityEngine.Random.Range(0.8f, 1.2f);
                    child.GetComponent<Entity>().speed *= UnityEngine.Random.Range(0.8f, 1.2f);
                    child.GetComponent<CircleCollider2D>().radius *= UnityEngine.Random.Range(0.8f, 1.2f);
                }
            }

            //Destroy(gameObject);
        }
    }
    
    int index = 0;
    void FixedUpdate()
    {
        if(index >= 10)
        {
            float green = Mathf.Clamp(SC, 0, 700);
            float red = Mathf.Clamp(1400 - SC, 0, 700);  

            sr.color = new Color(red / 700, green / 700, 0);
            index = 0;

            SC += (int) (Mathf.Clamp(UnityEngine.Random.Range(-10f, 9f), 0, 1400));
        }

        index++;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        Debug.Log("Triggered");

        if(c.tag == "Citizen")
        {
            if(UnityEngine.Random.value > altruism && c.GetComponent<Citizen>().SC < SC - 400)
            {
                rb.AddForce((transform.position.normalized - c.transform.position) * 2);
            } else if(c.GetComponent<Citizen>().SC < SC - 400) {
                SC -= UnityEngine.Random.Range(50, (SC - c.GetComponent<Citizen>().SC) / 3); 
            }
        }

    }
}
