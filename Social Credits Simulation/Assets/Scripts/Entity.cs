using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float speed = 80f;
    private Rigidbody2D rb;

    public float timer = 1000;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(UnityEngine.Random.value > 0.96)
        {
            rb.AddForce(UnityEngine.Random.insideUnitCircle.normalized * speed);
        }
    }


}
