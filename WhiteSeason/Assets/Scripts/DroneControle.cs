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
        
    }

    Vector3 CheckInput()
    {
        Vector3 currentDirection = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentDirection += Vector3.up;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            currentDirection += Vector3.down;
        }


        return currentDirection.normalized;
    }
}
