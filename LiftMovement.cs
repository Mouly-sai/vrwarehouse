using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LiftMovement : MonoBehaviour
{
    //public GameObject Platform;
    public float speed = 5f;
    public Transform Target, Home;
    bool Up = false;
    bool Down = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            Up = true;
            if (Up == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);
            }
        }
        Up = false;
        if (Input.GetKey(KeyCode.K))
        {
            Down = true;
            if (Down == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, Home.position, speed * Time.deltaTime);
            }
        }
        Down = false;
    }
}