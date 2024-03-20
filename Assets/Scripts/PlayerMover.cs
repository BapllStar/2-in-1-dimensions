using System.Collections;
using UnityEngine; // Added this line
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpHeight = 2.0f;
    private bool isJumping = false;
    public LayerMask groundMask;
    private CharacterController controller;
    private Vector3 velocity; // Added this line
    public Transform groundCheck; // Added this line

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVertical;

        if (CheckGrounded() && Input.GetButton("Jump") && !isJumping) // Modified this line
        {
            isJumping = true;
            Debug.Log("Jumping");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y); // Modified this line
        }

        // Added gravity effect
        velocity.y += Physics.gravity.y * Time.deltaTime;
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -0.1f;
        }

        controller.Move((movement) * speed * Time.deltaTime + velocity * Time.deltaTime); // Modified this line

        if (isJumping && CheckGrounded())
        {
            isJumping = false;
        }
    }

    bool CheckGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, .5f, groundMask); // Modified this line
    }
}
