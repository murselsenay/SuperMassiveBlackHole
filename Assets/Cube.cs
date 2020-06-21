using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public GameObject hole;
    public float force;
    private bool isMagnetic=false;

    void Update()
    {
        if (isMagnetic)
        {
            GetComponent<Rigidbody>().AddForce((hole.transform.position - transform.position) * force * Time.smoothDeltaTime);
        }
     
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Hole")
        {
            isMagnetic = true;
            
        }
      
        
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Hole")
        {
            isMagnetic = false;
        }

    }
}
