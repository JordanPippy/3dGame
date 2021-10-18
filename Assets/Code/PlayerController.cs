using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player, playerCamera;
    private Rigidbody playerRb;
    private float side, forward, didJump;
    private float speed, jumpForce, rotationRate;
    private float rotateX, rotateY;
    private float cameraMax, currentRotation;
    private Vector3 jump, targetPosition;
    private bool canJump;
    void Start()
    {
        side = 0; forward = 0; didJump = 0; rotationRate = 50.0f;
        cameraMax = 10.0f;
        currentRotation = 0;
        speed = 10.0f;
        jumpForce = 2.0f;
        canJump = true;
        jump = new Vector3(0, 2.0f, 0);
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        playerCamera = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        playerRb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
            side = 1;
        if (Input.GetKey(KeyCode.LeftArrow))
            side = -1;
        if (Input.GetKey(KeyCode.UpArrow))
            forward = 1;
        if (Input.GetKey(KeyCode.DownArrow))
            forward = -1;
        if (Input.GetKey(KeyCode.Space) && canJump)
            didJump = 1;

        rotateX = Input.GetAxis("Horizontal");
        rotateY = Input.GetAxis("Vertical") / -50;
    }

    void FixedUpdate()
    {
        if (side != 0)
            movePlayer(speed, player.transform.right * side);
        if (forward != 0)
            movePlayer(speed, player.transform.forward * forward);
        if (didJump == 1)
            playerJump();
        

        doRotation();

        side = 0;
        forward = 0;
        didJump = 0;
        rotateX = 0;
    }

    private void doRotation()
    {
        //Player rotation
        player.transform.Rotate(0, rotateX * rotationRate * Time.deltaTime, 0);

        //camera rotation
        if (currentRotation >= cameraMax*2 && rotateY > 0) // Look further down than up.
            return;
        if (currentRotation <= -cameraMax && rotateY < 0)
            return;

        currentRotation += rotateY;
        playerCamera.transform.Rotate(rotateY, 0, 0);
        
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "jumpReset")
            canJump = true;
    }

    private void playerJump()
    {
        playerRb.AddForce(jumpForce * jump, ForceMode.Impulse);
        canJump = false;
    }

    private void movePlayer(float speed, Vector3 direction)
    {
        player.transform.position += speed * Time.deltaTime * direction;
    }
}
