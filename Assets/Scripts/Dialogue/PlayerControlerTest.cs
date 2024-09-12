using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerControlerTest : MonoBehaviour
{
    [Header("Movement Params")]
    public float runSpeed = 6.0f;
    public float jumpSpeed = 20.0f;

    // components attached to player
    private BoxCollider coll;
    private Rigidbody rb;

    // other
    private bool isGrounded = true;

    private bool isDebugTime = false;

    private void Awake()
    {
        coll = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }


        HandleHorizontalMovement();

        HandleJumping();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void HandleHorizontalMovement()
    {
        Vector3 moveDirection = new Vector3(
            InputManager.GetInstance().GetMoveDirection().x, 
            0, 
            InputManager.GetInstance().GetMoveDirection().z);

        rb.velocity = new Vector3(
            moveDirection.x * runSpeed,
            rb.velocity.y, 
            moveDirection.z * runSpeed); // Actualizamos la velocidad del rigidbody a un Vector3

        if (isDebugTime) { 
            Debug.Log("PlayerControlerTest movement: " + InputManager.GetInstance().GetMoveDirection());
        }
    }

    private void HandleJumping()
    {
        bool jumpPressed = InputManager.GetInstance().GetJumpPressed();   
        if (isDebugTime)
        {
            Debug.Log("PlayerControlerTest jumping: " + InputManager.GetInstance().GetJumpPressed());
            Debug.Log("Funca: " + jumpPressed + "\nisGrounded: " + isGrounded);
        }

        if (isGrounded && jumpPressed)
        {
            isGrounded = false;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Reiniciar la velocidad vertical antes de aplicar el salto
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse); // Aplicar una fuerza instantánea hacia arriba
        }
    }
}

