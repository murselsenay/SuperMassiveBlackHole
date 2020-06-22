using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
 
    public float force;
    private bool isMagnetic=false;
    Vector3 dir;
    void Update()
    {
        dir = Hole.positions - transform.position;
        if (isMagnetic)
        {
            GetComponent<Rigidbody>().AddForce(dir * force * Time.smoothDeltaTime);
        }
        Debug.DrawRay(transform.position, dir);
     
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Hole")
        {
            
            isMagnetic = true;
            
        }
      
        
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Hole")
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
