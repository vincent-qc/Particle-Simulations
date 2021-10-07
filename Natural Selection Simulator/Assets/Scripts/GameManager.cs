using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject prey;
    public GameObject pred;

    void Start()
    {
        for(int i = 0; i < 8; i++) {
            Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0,Screen.width), Random.Range(0,Screen.height), Camera.main.farClipPlane/2));
            Instantiate(pred, screenPosition, Quaternion.identity);
        }

        for(int i = 0; i < 50; i++) {
            Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0,Screen.width), Random.Range(0,Screen.height), Camera.main.farClipPlane/2));
            Instantiate(prey, screenPosition, Quaternion.identity);
        }
    }
}
