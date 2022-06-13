using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float movementSpeed = 10f;
    public float rotationSpeed = 10f;

    Vector3 Vec;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            transform.position += Vector3.up * movementSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.X))
        {
            transform.position += -Vector3.up * movementSpeed  * Time.deltaTime;
        }
        

        //Rotation
        float rotation =0;
        if (Input.GetKey (KeyCode.Q))
            rotation -= rotationSpeed;
        if (Input.GetKey (KeyCode.E))
            rotation += rotationSpeed;
        transform.Rotate (rotation, 0, 0);

        //test to see
        Vec = transform.localPosition;
        Vec.y += Input.GetAxis("Jump") * Time.deltaTime * movementSpeed;
        Vec.x += Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        Vec.z += Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;
        transform.localPosition = Vec;
    }
}
