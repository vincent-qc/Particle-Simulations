using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    private TrailRenderer tr;

    void Start()
    {
        tr = GetComponent<TrailRenderer>();
        tr.startWidth = 0.1f * (transform.parent.transform.localScale.x);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
