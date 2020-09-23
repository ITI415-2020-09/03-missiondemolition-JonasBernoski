using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject prefabProjectile;
    public float velocityMult = 8f;

    [Header("Set Dynamically")]
    public GameObject Launchpoint;
    public Vector3 LaunchPos;
    public GameObject projectile;
    public bool aimingMode;

    private Rigidbody projectileRigidbody;
    // Start is called before the first frame update
    void Awake()
    {
        Transform LaunchpointTrans = transform.Find("Launchpoint");
        Launchpoint = LaunchpointTrans.gameObject;
        Launchpoint.SetActive(false);
        LaunchPos = LaunchpointTrans.position;
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
 
    void OnMouseDown()
    {
        aimingMode = true;
        projectile = Instantiate(prefabProjectile) as GameObject;
        projectile.transform.position = LaunchPos;
        projectile.GetComponent<Rigidbody>().isKinematic = true;
        projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.isKinematic = true;
    }

    void Update()
    {
        if (!aimingMode) return;
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 mouseDelta = mousePos3D - LaunchPos;

        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }
        Vector3 projPos = LaunchPos + mouseDelta;
        projectile.transform.position = projPos;

        if (Input.GetMouseButtonUp(0))
        {
            aimingMode = false;
            projectileRigidbody.isKinematic = false;
            projectileRigidbody.velocity = -mouseDelta * velocityMult;
            projectile = null;
        }
    }

}
