using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    public Animator anim;

   [SerializeField] private float playerSpeed = 2.0f;
   [SerializeField] private float jumpHeight = 1.0f;
   [SerializeField] private float gravityValue = -9.81f;

    public float Rspeed;
    private Transform cameraM;
   
    public InputActions ip;
    public Vector2 movement;

    private void OnEnable()
    {
        ip = new InputActions();
        ip.Player.Enable();
    }
    private void OnDisable()
    {
        ip.Player.Disable();
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        cameraM = Camera.main.transform;
        controller = gameObject.GetComponent<CharacterController>();
      
    }

    public float walkanimSpeed;
    void Update()
    {
        movement = ip.Player.Movement.ReadValue<Vector2>();
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(movement.x, 0, movement.y).normalized;
        move = cameraM.forward * move.z + cameraM.right * move.x;
        move.y = 0;
        controller.Move(move * Time.deltaTime * playerSpeed);

        walkanimSpeed = move.sqrMagnitude;
        anim.SetFloat("walk", walkanimSpeed, 5f, Time.deltaTime);
       

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if(move.magnitude>=0f)
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg+cameraM.eulerAngles.y;
            Debug.Log(targetAngle);
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation , Time.deltaTime * Rspeed);
        }
    }

   


}




