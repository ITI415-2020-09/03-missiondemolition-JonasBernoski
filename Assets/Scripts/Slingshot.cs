using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public GameObject Launchpoint;
    // Start is called before the first frame update
    void Awake()
    {
        Transform LaunchpointTrans = transform.Find("Launchpoint");
        Launchpoint = LaunchpointTrans.gameObject;
        Launchpoint.SetActive(false);
    }
    void OnMouseEnter()
    {
        //print("Slingshot:OnMouseEnter()");
        Launchpoint.SetActive(true);
    }

    // Update is called once per frame
    void OnMouseExit()
    {
        // print("Slingshot:OnMouseExit()"); 
        Launchpoint.SetActive(false);
    }
}
