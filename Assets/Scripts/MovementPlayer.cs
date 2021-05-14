using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    public float speed = 10f;

    public Animator animator;

    [SerializeField]
    public Rigidbody rBody;
    private Vector3 moveDir;
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Si no esta cortando
        if (!gameObject.GetComponent<PickUpObject>().hasToCut) {
            float xDir = Input.GetAxis("Horizontal");
            float zDir = Input.GetAxis("Vertical");

            moveDir = new Vector3(xDir, 0.0f, zDir) * speed;
            transform.LookAt(transform.position + new Vector3(moveDir.x, 0, moveDir.z));

            animator.SetFloat("MovX", xDir);
            animator.SetFloat("MovZ", zDir);
        }
    }

    private void FixedUpdate()
    {
        rBody.velocity = moveDir;
    }
}
