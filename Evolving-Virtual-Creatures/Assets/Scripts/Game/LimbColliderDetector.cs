using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbColliderDetector : MonoBehaviour
{

    public void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.GetComponent<Limb>() != null)
        {
            Limb collidedLimb = col.gameObject.GetComponent<Limb>();
            collidedLimb.isGrounded = true;
        }
    }
    public void OnCollisionExit(Collision col)
    {
        if(col.gameObject.GetComponent<Limb>() != null)
        {
            Limb collidedLimb = col.gameObject.GetComponent<Limb>();
            collidedLimb.isGrounded = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
