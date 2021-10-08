using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCP : MonoBehaviour
{
    public GameObject NewCCP;
    public float timer = 20;

    int killed = 0;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            timer = 20;

            for(int i = 0; i < Mathf.Clamp(killed, 3, 0); i++)
            {
                if(GameObject.FindGameObjectsWithTag("CCP").Length <= 16)
                {
                    GameObject child = Instantiate(NewCCP, transform.position, Quaternion.identity);
                    CCP c = child.GetComponent<CCP>();

                    c.name = "CCP Unit";
                    child.GetComponent<Entity>().speed *= UnityEngine.Random.Range(0.8f, 1.2f);
                    child.GetComponent<CircleCollider2D>().radius *= UnityEngine.Random.Range(0.8f, 1.2f);
                }
            }

            if(UnityEngine.Random.value < 0.2f) 
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {

        if(c.tag == "Citizen" && c.GetComponent<Citizen>().SC < 900)
        {
            killed++;

            Destroy(c.gameObject);
        }
    }
}
