using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbAttacher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static List<int> randomLimbSetup(GameObject baseLimb, Rigidbody bodyToAttach, int RandomNumber, bool IsCopy = false)
    {

        //back side - bottom left 
        if (RandomNumber == 1)
        {
            

            float xTransform = bodyToAttach.transform.position.x + (bodyToAttach.transform.localScale.x / 2) + (baseLimb.transform.localScale.x / 2);
            float yTransform = bodyToAttach.transform.position.y + (baseLimb.transform.localScale.y / 2) - (bodyToAttach.transform.localScale.y / 2);
            float zTransform = bodyToAttach.transform.position.z + (bodyToAttach.transform.localScale.z / 2)  + (baseLimb.transform.localScale.z /2);
            baseLimb.transform.position = new Vector3(xTransform, yTransform, zTransform);
            List<int> potentialFutureLimbs = new List<int>()
            {
                1,2,3,4,5,8,9,17,18
            };
            return potentialFutureLimbs;


        }
        //bottom right
        if (RandomNumber == 2)
        {
            float xTransform = bodyToAttach.transform.position.x + (bodyToAttach.transform.localScale.x / 2) + (baseLimb.transform.localScale.x / 2);
            float yTransform = bodyToAttach.transform.position.y + (baseLimb.transform.localScale.y / 2) - (bodyToAttach.transform.localScale.y / 2);
            baseLimb.transform.position = new Vector3(xTransform, yTransform, bodyToAttach.transform.position.z);
            List<int> potentialFutureLimbs = new List<int>()
            {
                1,2,3,4,5,15,6,16,7,17,8,18,9
            };
            return potentialFutureLimbs;
        }
        // right side- out to the back
        if (RandomNumber == 3)
        {
            if (IsCopy == false)
            {
                baseLimb.transform.localScale = new Vector3(baseLimb.transform.localScale.x, baseLimb.transform.localScale.z, baseLimb.transform.localScale.y);
            }
            float xTransform = bodyToAttach.transform.position.x + (bodyToAttach.transform.localScale.x / 2) + (baseLimb.transform.localScale.x / 2);
            float zTransform = bodyToAttach.transform.position.z - (bodyToAttach.transform.localScale.z / 2) + (baseLimb.transform.localScale.z / 2);
            baseLimb.transform.position = new Vector3(xTransform, bodyToAttach.transform.position.y, zTransform);
            List<int> potentialFutureLimbs = new List<int>()
            {
                1,2,3,4,5,6,15,7,16,8,17,9,18
            };
            return potentialFutureLimbs;
        }
        // right side out the front
        if (RandomNumber == 4)
        {
            if (IsCopy == false)
            {
                baseLimb.transform.localScale = new Vector3(baseLimb.transform.localScale.x, baseLimb.transform.localScale.z, baseLimb.transform.localScale.y);
            }
            float xTransform = bodyToAttach.transform.position.x + (bodyToAttach.transform.localScale.x / 2) + (baseLimb.transform.localScale.x / 2);
            float zTransform = bodyToAttach.transform.position.z + (bodyToAttach.transform.localScale.z / 2) - (baseLimb.transform.localScale.z / 2);
            baseLimb.transform.position = new Vector3(xTransform, bodyToAttach.transform.position.y, zTransform);
            List<int> potentialFutureLimbs = new List<int>()
            {
                1,2,3,4,5,6,15,7,16,8,17,9,18
            };
            return potentialFutureLimbs;
        }
        // Add to right side
        if (RandomNumber == 5)
        {
            if (IsCopy == false)
            {
                baseLimb.transform.localScale = new Vector3(baseLimb.transform.localScale.y, baseLimb.transform.localScale.x, baseLimb.transform.localScale.z);
            }
            float xTransform = bodyToAttach.transform.position.x + (bodyToAttach.transform.localScale.x / 2) + (baseLimb.transform.localScale.x / 2);
            baseLimb.transform.position = new Vector3(xTransform, bodyToAttach.transform.position.y, bodyToAttach.transform.position.z);
            List<int> potentialFutureLimbs = new List<int>()
            {
                1,2,3,4,5,6,15,7,16,8,17,9,18
            };
            return potentialFutureLimbs;
        }
        // Add to top on right side comign forwards
        if (RandomNumber == 6)
        {
            if (IsCopy == false)
            {

                baseLimb.transform.localScale = new Vector3(baseLimb.transform.localScale.x, baseLimb.transform.localScale.z, baseLimb.transform.localScale.y);
            }
            float xTransform = bodyToAttach.transform.position.x + (bodyToAttach.transform.localScale.x / 2) - (baseLimb.transform.localScale.x / 2);
            float yTransform = bodyToAttach.transform.position.y + (bodyToAttach.transform.localScale.y / 2) + (baseLimb.transform.localScale.y / 2);
            float zTransform = bodyToAttach.transform.position.z + (bodyToAttach.transform.localScale.z / 2) - (baseLimb.transform.localScale.z / 2);
            baseLimb.transform.position = new Vector3(xTransform, yTransform, zTransform);


            List<int> potentialFutureLimbs = new List<int>()
            {
               1,2,11,3,12,4,13,5,14,6,15,7,16
            };
            return potentialFutureLimbs;
        }
        //Add to top on right side going backwards
        if (RandomNumber == 7)
        {
            if (IsCopy == false)
            {

                baseLimb.transform.localScale = new Vector3(baseLimb.transform.localScale.x, baseLimb.transform.localScale.z, baseLimb.transform.localScale.y);
            }
            float xTransform = bodyToAttach.transform.position.x + (bodyToAttach.transform.localScale.x / 2) - (baseLimb.transform.localScale.x / 2);
            float yTransform = bodyToAttach.transform.position.y + (bodyToAttach.transform.localScale.y / 2) + (baseLimb.transform.localScale.y / 2);
            float zTransform = bodyToAttach.transform.position.z - (bodyToAttach.transform.localScale.z / 2) + (baseLimb.transform.localScale.z / 2);
            baseLimb.transform.position = new Vector3(xTransform, yTransform, zTransform);

            List<int> potentialFutureLimbs = new List<int>()
            {
                1,2,11,3,12,4,13,5,14,6,15,7,16
            };
            return potentialFutureLimbs;
        }
        //Add to bottom right side going backwards
        if (RandomNumber == 8)
        {
            if (IsCopy == false)
            {

                baseLimb.transform.localScale = new Vector3(baseLimb.transform.localScale.x, baseLimb.transform.localScale.z, baseLimb.transform.localScale.y);
            }
            float xTransform = bodyToAttach.transform.position.x + (bodyToAttach.transform.localScale.x / 2) - (baseLimb.transform.localScale.x / 2);
            float yTransform = bodyToAttach.transform.position.y - (bodyToAttach.transform.localScale.y / 2) - (baseLimb.transform.localScale.y / 2);
            float zTransform = bodyToAttach.transform.position.z - (bodyToAttach.transform.localScale.z / 2) + (baseLimb.transform.localScale.z / 2);
            baseLimb.transform.position = new Vector3(xTransform, yTransform, zTransform);
            List<int> potentialFutureLimbs = new List<int>()
            {
                1,10,2,3,12,4,13,5,14,8,17,9,18
            };
            return potentialFutureLimbs;
        }
        //Add to bottom right side going forwards
        if (RandomNumber == 9)
        {
            if (IsCopy == false)
            {

                baseLimb.transform.localScale = new Vector3(baseLimb.transform.localScale.x, baseLimb.transform.localScale.z, baseLimb.transform.localScale.y);
            }
            float xTransform = bodyToAttach.transform.position.x + (bodyToAttach.transform.localScale.x / 2) - (baseLimb.transform.localScale.x / 2);
            float yTransform = bodyToAttach.transform.position.y - (bodyToAttach.transform.localScale.y / 2) - (baseLimb.transform.localScale.y / 2);
            float zTransform = bodyToAttach.transform.position.z + (bodyToAttach.transform.localScale.z / 2) - (baseLimb.transform.localScale.z / 2);
            baseLimb.transform.position = new Vector3(xTransform, yTransform, zTransform);

            List<int> potentialFutureLimbs = new List<int>()
            {
                1,10,2,3,12,4,13,5,14,8,17,9,18
            };
            return potentialFutureLimbs;
        }
        //top left
        if (RandomNumber == 10)
        {
            float xTransform = bodyToAttach.transform.position.x - (bodyToAttach.transform.localScale.x / 2) - (baseLimb.transform.localScale.x / 2);
            float yTransform = bodyToAttach.transform.position.y + (bodyToAttach.transform.localScale.y / 2) - (baseLimb.transform.localScale.y / 2);
            baseLimb.transform.position = new Vector3(xTransform, yTransform, bodyToAttach.transform.position.z);
            List<int> potentialFutureLimbs = new List<int>()
            {
                10,11,12,13,14,17,8,9,18
            };
            return potentialFutureLimbs;
        }
        //bottom left
        if (RandomNumber == 11)
        {
            float xTransform = bodyToAttach.transform.position.x - (bodyToAttach.transform.localScale.x / 2) - (baseLimb.transform.localScale.x / 2);
            float yTransform = bodyToAttach.transform.position.y - (bodyToAttach.transform.localScale.y / 2) + (baseLimb.transform.localScale.y / 2);
            baseLimb.transform.position = new Vector3(xTransform, yTransform, bodyToAttach.transform.position.z);
            List<int> potentialFutureLimbs = new List<int>()
            {
                10,11,12,13,14,15,6,16,7
            };
            return potentialFutureLimbs;
        }
        // left side out to the back
        if (RandomNumber == 12)
        {
            if (IsCopy == false)
            {
                baseLimb.transform.localScale = new Vector3(baseLimb.transform.localScale.x, baseLimb.transform.localScale.z, baseLimb.transform.localScale.y);

            }
            float xTransform = bodyToAttach.transform.position.x - (bodyToAttach.transform.localScale.x / 2) - (baseLimb.transform.localScale.x / 2);
            float zTransform = bodyToAttach.transform.position.z - (bodyToAttach.transform.localScale.z / 2) + (baseLimb.transform.localScale.z / 2);
            baseLimb.transform.position = new Vector3(xTransform, bodyToAttach.transform.position.y, zTransform);

            List<int> potentialFutureLimbs = new List<int>()
            {
                10,11,12,13,14,15,6,16,7,17,18
            };
            return potentialFutureLimbs;
        }
        //left side out the front
        if (RandomNumber == 13)
        {
            if (IsCopy == false)
            {

                baseLimb.transform.localScale = new Vector3(baseLimb.transform.localScale.x, baseLimb.transform.localScale.z, baseLimb.transform.localScale.y);
            }
            float xTransform = bodyToAttach.transform.position.x - (bodyToAttach.transform.localScale.x / 2) - (baseLimb.transform.localScale.x / 2);
            float zTransform = bodyToAttach.transform.position.z + (bodyToAttach.transform.localScale.z / 2) - (baseLimb.transform.localScale.z / 2);
            baseLimb.transform.position = new Vector3(xTransform, bodyToAttach.transform.position.y, zTransform);

            List<int> potentialFutureLimbs = new List<int>()
            {
                10,11,12,13,14,15,16,17,8,18,9
            };
            return potentialFutureLimbs;
        }
        // Add to left side
        if (RandomNumber == 14)
        {
            if (IsCopy == false)
            {

                baseLimb.transform.localScale = new Vector3(baseLimb.transform.localScale.y, baseLimb.transform.localScale.x, baseLimb.transform.localScale.z);
            }
            float xTransform = bodyToAttach.transform.position.x - (bodyToAttach.transform.localScale.x / 2) - (baseLimb.transform.localScale.x / 2);
            baseLimb.transform.position = new Vector3(xTransform, bodyToAttach.transform.position.y, bodyToAttach.transform.position.z);
            List<int> potentialFutureLimbs = new List<int>()
            {
                10,11,12,13,14,15,16,17,8,18,9
            };
            return potentialFutureLimbs;
        }
        //Add to top on left side coming forward
        if (RandomNumber == 15)
        {
            if (IsCopy == false)
            {

                baseLimb.transform.localScale = new Vector3(baseLimb.transform.localScale.x, baseLimb.transform.localScale.z, baseLimb.transform.localScale.y);
            }
            float xTransform = bodyToAttach.transform.position.x - (bodyToAttach.transform.localScale.x / 2) + (baseLimb.transform.localScale.x / 2);
            float yTransform = bodyToAttach.transform.position.y + (bodyToAttach.transform.localScale.y / 2) + (baseLimb.transform.localScale.y / 2);
            float zTransform = bodyToAttach.transform.position.z + (bodyToAttach.transform.localScale.z / 2) - (baseLimb.transform.localScale.z / 2);
            baseLimb.transform.position = new Vector3(xTransform, yTransform, zTransform);

            List<int> potentialFutureLimbs = new List<int>()
            {
                10,11,12,13,14,15,6,16,7
            };
            return potentialFutureLimbs;
        }
        //Add to top on left side going backward
        if (RandomNumber == 16)
        {
            if (IsCopy == false)
            {

                baseLimb.transform.localScale = new Vector3(baseLimb.transform.localScale.x, baseLimb.transform.localScale.z, baseLimb.transform.localScale.y);
            }
            float xTransform = bodyToAttach.transform.position.x - (bodyToAttach.transform.localScale.x / 2) + (baseLimb.transform.localScale.x / 2);
            float yTransform = bodyToAttach.transform.position.y + (bodyToAttach.transform.localScale.y / 2) + (baseLimb.transform.localScale.y / 2);
            float zTransform = bodyToAttach.transform.position.z - (bodyToAttach.transform.localScale.z / 2) - (baseLimb.transform.localScale.z / 2);
            baseLimb.transform.position = new Vector3(xTransform, yTransform, zTransform);
            List<int> potentialFutureLimbs = new List<int>()
            {
                10,11,12,13,14,15,16,7
            };
            return potentialFutureLimbs;
        }
        //Add to bottom on left side going backwards
        if (RandomNumber == 17)
        {
            if (IsCopy == false)
            {

                baseLimb.transform.localScale = new Vector3(baseLimb.transform.localScale.x, baseLimb.transform.localScale.z, baseLimb.transform.localScale.y);
            }
            float xTransform = bodyToAttach.transform.position.x - (bodyToAttach.transform.localScale.x / 2) + (baseLimb.transform.localScale.x / 2);
            float yTransform = bodyToAttach.transform.position.y - (bodyToAttach.transform.localScale.y / 2) - (baseLimb.transform.localScale.y / 2);
            float zTransform = bodyToAttach.transform.position.z - (bodyToAttach.transform.localScale.z / 2) + (baseLimb.transform.localScale.z / 2);
            baseLimb.transform.position = new Vector3(xTransform, yTransform, zTransform);
            List<int> potentialFutureLimbs = new List<int>()
            {
                10,11,12,13,14,17,8,9,18
            };
            return potentialFutureLimbs;
        }
        // Add to bottom on left side going forwards
        if (RandomNumber == 18)
        {
            if (IsCopy == false)
            {

                baseLimb.transform.localScale = new Vector3(baseLimb.transform.localScale.x, baseLimb.transform.localScale.z, baseLimb.transform.localScale.y);
            }
            float xTransform = bodyToAttach.transform.position.x - (bodyToAttach.transform.localScale.x / 2) + (baseLimb.transform.localScale.x / 2);
            float yTransform = bodyToAttach.transform.position.y - (bodyToAttach.transform.localScale.y / 2) - (baseLimb.transform.localScale.y / 2);
            float zTransform = bodyToAttach.transform.position.z + (bodyToAttach.transform.localScale.z / 2) - (baseLimb.transform.localScale.z / 2);
            baseLimb.transform.position = new Vector3(xTransform, yTransform, zTransform);
            List<int> potentialFutureLimbs = new List<int>()
            {
                10,11,12,13,14,8,17,9,18
            };
            return potentialFutureLimbs;
        }
        else
        {
            Debug.Log("number not assigned to a potential limb");
            return null;
        }
    }

    public static List<int> randomLimbSetup2(GameObject baseLimb, Rigidbody bodyToAttach, int RandomNumber, bool IsCopy = false)
    {
         //back side - bottom left
        if (RandomNumber == 1)
        {

            float xTransform = bodyToAttach.transform.position.x + (bodyToAttach.transform.localScale.x / 2) + (baseLimb.transform.localScale.x / 2);
            float yTransform = bodyToAttach.transform.position.y + (baseLimb.transform.localScale.y / 2) - (bodyToAttach.transform.localScale.y / 2);
            float zTransform = bodyToAttach.transform.position.z + (bodyToAttach.transform.localScale.z / 2)  + (baseLimb.transform.localScale.z /2);
            baseLimb.transform.position = new Vector3(xTransform, yTransform, zTransform);
            List<int> potentialFutureLimbs = new List<int>()
            {
                1,2,3,4,5,8,9,17,18
            };
            return potentialFutureLimbs;
        }
        return null;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
