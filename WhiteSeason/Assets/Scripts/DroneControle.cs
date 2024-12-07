using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneControle : MonoBehaviour
{
    private Rigidbody RB;
    private Vector3 moveDirection;

    [SerializeField]
    private float verticalSpeed;
    [SerializeField]
    private float horizontalSpeed;
    private void Awake()
    {
        RB = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = CheckInputNormalized();
    }


    private void FixedUpdate()
    {
        Mooving();
    }

    Vector3 CheckInputNormalized()
    {
        Vector3 currentDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentDirection += Vector3.up;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            currentDirection += Vector3.down;
        }

        if (Input.GetAxisRaw("Horizontal")!= 0)
        {
            currentDirection.x += Input.GetAxisRaw("Horizontal");
        }

        if (Input.GetAxisRaw("Vertical") != 0)
        {
            currentDirection.z += Input.GetAxisRaw("Vertical");
        }

        return currentDirection.normalized;
    }

    private void Mooving()
    {
        Vector3 addedForce = new Vector3(moveDirection.x * horizontalSpeed, moveDirection.y * verticalSpeed, moveDirection.z * horizontalSpeed);
        RB.AddForce(addedForce,ForceMode.Impulse);
        addedForce = Vector3.zero;
    }
}
