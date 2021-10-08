using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject CCP;
    public GameObject Citizen;
    public Text text;

    private int index = 0;

    void Start()
    {
        for(int i = 0; i < 2; i++) {
            Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0,Screen.width), Random.Range(0,Screen.height), Camera.main.farClipPlane/2));
            Instantiate(CCP, screenPosition, Quaternion.identity);
        }

        for(int i = 0; i < 30; i++) {
            Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0,Screen.width), Random.Range(0,Screen.height), Camera.main.farClipPlane/2));
            GameObject n = Instantiate(Citizen, screenPosition, Quaternion.identity);
            n.GetComponent<Citizen>().SC = UnityEngine.Random.Range(0, 1400);
        }
    }

    void FixedUpdate()
    {
        index++;

        if(index >= 20)
        {
            index = 0;

            float CCPAmt = 0;
            float CitAmt = 0;

            float SC = 0;
            float speed = 0;
            float altruism = 0;
            float vision = 0;

            float CCPVision = 0;
            float CCPSpeed = 0;

            foreach(GameObject cit in (GameObject.FindGameObjectsWithTag("Citizen")))
            {
                CitAmt++;
                vision += cit.GetComponent<CircleCollider2D>().radius;
                SC += cit.GetComponent<Citizen>().SC;
                altruism += cit.GetComponent<Citizen>().altruism;
                speed += cit.GetComponent<Entity>().speed;
            }

            foreach(GameObject CPP in (GameObject.FindGameObjectsWithTag("CCP")))
            {
                CCPAmt++;

                CCPVision += CCP.GetComponent<CircleCollider2D>().radius;
                CCPSpeed += CCP.GetComponent<Entity>().speed;
            }

            text.text = $"Citizens: {CitAmt} | CCP: {CCPAmt}"
            + $"\n\n- Citizens - \nAverage Social Credits: {Mathf.Round(SC / CitAmt * 100) / 100} \nAverage Vision Radius: {Mathf.Round(vision / CitAmt * 100) / 100} \nAverage Altruism: {Mathf.Round(altruism / CitAmt * 100) / 100} \nAverage Speed: {Mathf.Round(speed / CitAmt * 100) / 100} \n\n" + 
            $"- CCP - \nAverage Vision Radius: {Mathf.Round(CCPVision / CCPAmt * 100) / 100} \nAverage Speed {Mathf.Round(CCPSpeed / CCPAmt * 100) / 100}";
        }
    }
}
