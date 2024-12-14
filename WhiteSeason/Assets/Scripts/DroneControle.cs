using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneControle : MonoBehaviour
{
    /*    private Transform cam;

        private CharacterController characterController;
        [SerializeField]
        private float speed = 6f;
        [SerializeField]
        private float turnSmoothTime = 0.1f;
        float turnSmoothVelocity;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            cam = Camera.main.transform;
        }

        private void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if(direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                characterController.Move(moveDirection.normalized * speed * Time.deltaTime);

            }
        }*/




    private Rigidbody RB;
    private Vector3 moveDirection;
    private float rotarionDirection;
    private Vector3 angularVelocity;
    bool isInFlyMode;

    [SerializeField]
    private float verticalSpeed;
    [SerializeField]
    private float horizontalSpeed;
    [SerializeField]
    private float rotationVelosity;

    [SerializeField]
    private float flyGravityScale;

    private void Awake()
    {
        RB = GetComponent<Rigidbody>();
        isInFlyMode = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        angularVelocity = new Vector3(0, rotationVelosity, 0);
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = CheckMooveInputNormalized();
        rotarionDirection = CheckRotationInputNormalized();
    }


    private void FixedUpdate()
    {
        Mooving();
        Rotate();

        if (isInFlyMode)
        {
            EnableFlyGravity();
        }
        else
        {
            DisableFlyGravity();
        }
    }

    Vector3 CheckMooveInputNormalized()
    {
        Vector3 currentDirection = Vector3.zero;


        currentDirection = transform.right * Input.GetAxisRaw("Horizontal") + Input.GetAxisRaw("Vertical") * transform.forward;


        if (Input.GetKey(KeyCode.Space))
        {
            currentDirection += Vector3.up;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            currentDirection += Vector3.down;
        }

        /*        if (Input.GetAxisRaw("Horizontal") != 0)
                {
                    currentDirection.x += Input.GetAxisRaw("Horizontal");
                }

                if (Input.GetAxisRaw("Vertical") != 0)
                {
                    currentDirection.z += Input.GetAxisRaw("Vertical");
                }*/

        return currentDirection.normalized;
    }

    float CheckRotationInputNormalized()
    {
        float currentRotationDir = 0f;

        if (Input.GetKey(KeyCode.E))
        {
            currentRotationDir = 1f;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            currentRotationDir = -1f;
        }

        return currentRotationDir;
    }

    private void Mooving()
    {
        Vector3 addedForce = new Vector3(moveDirection.x * horizontalSpeed, moveDirection.y * verticalSpeed, moveDirection.z * horizontalSpeed);
        RB.AddForce(addedForce, ForceMode.Impulse);
        addedForce = Vector3.zero;
    }

    private void Rotate()
    {
        float addedRotation = rotarionDirection;

        Quaternion deltaRotation = Quaternion.Euler(angularVelocity * Time.fixedDeltaTime * addedRotation);
        RB.MoveRotation(RB.rotation * deltaRotation);
    }

    private void EnableFlyGravity()
    {
        RB.useGravity = false;
        RB.AddForce(new Vector3(0,-flyGravityScale,0)*Time.fixedDeltaTime, ForceMode.Impulse);
    }

    private void DisableFlyGravity()
    {
        RB.useGravity = true;
    }
}
