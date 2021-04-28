using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    FRONT,
    BACK,
    LEFT,
    RIGHT
}

public class movementPUG : MonoBehaviour
{
    public const float PI = 3.1415926535897931f;
    public float speedMovement = (PI /180f) * 90f;
    public float speedRotation = 30f;
    private State stateObj;

    // Start is called before the first frame update
    void Start()
    {
        stateObj = State.FRONT;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        /*switch (stateObj) {
            case FRONT:
                break;
            case BACK:
                break;
            case FRONT:
                break;
            case FRONT:
                break;
        }*/


        if (Input.GetKey("w"))
        {
            stateObj = State.BACK;
            pos.z += speedMovement * Time.deltaTime;
            transform.position = pos;

        }
        if (Input.GetKey("a"))
        {
            stateObj = State.LEFT;
            pos.x -= speedMovement * Time.deltaTime;
            transform.position = pos;
            transform.Rotate(Vector3.up * speedMovement);
        }
        if (Input.GetKey("s"))
        {
            stateObj = State.FRONT;
            pos.z -= speedMovement * Time.deltaTime;
            transform.position = pos;
        }
        if (Input.GetKey("d"))
        {
            stateObj = State.RIGHT;
            pos.x += speedMovement * Time.deltaTime;
            transform.position = pos;
            transform.Rotate(-Vector3.up * speedMovement);
        }      


        
    }
}
