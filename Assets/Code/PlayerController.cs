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
    private Vector3 cameraOffset;
    private float rotateX, rotateY;
    private Vector3 jump;
    private bool canJump;
    void Start()
    {
        side = 0; forward = 0; didJump = 0; rotationRate = 50.0f;
        speed = 10.0f;
        jumpForce = 2.0f;
        canJump = true;
        jump = new Vector3(0, 2.0f, 0);
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        playerCamera = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        playerRb = player.GetComponent<Rigidbody>();

        cameraOffset = playerCamera.transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
            side = 1;
            //movePlayer(speed, player.transform.right);
        if (Input.GetKey(KeyCode.LeftArrow))
            side = -1;
            //movePlayer(speed, -player.transform.right);
        if (Input.GetKey(KeyCode.UpArrow))
            forward = 1;
            //movePlayer(speed, player.transform.forward);
        if (Input.GetKey(KeyCode.DownArrow))
            forward = -1;
            //movePlayer(speed, -player.transform.forward);
        if (Input.GetKey(KeyCode.Space) && canJump)
            didJump = 1;
            //playerJump();
        rotateX = Input.GetAxis("Horizontal");
        rotateY = Input.GetAxis("Vertical");
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
        pinCameraToPlayer();



        side = 0;
        forward = 0;
        didJump = 0;
        rotateX = 0;
    }

    private void doRotation()
    {
        player.transform.Rotate(0, rotateX * rotationRate * Time.deltaTime, 0);
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

    private void pinCameraToPlayer()
    {
        cameraOffset = Quaternion.AngleAxis(rotateX * rotationRate * Time.deltaTime, Vector3.up) * cameraOffset;

        playerCamera.transform.position = player.transform.position + cameraOffset;

        Vector3 targetPosition = new Vector3( player.transform.position.x, 
                                        playerCamera.transform.position.y, 
                                        player.transform.position.z ) ;
        playerCamera.transform.LookAt(targetPosition);
        
        //float ang = Vector3.Angle(playerCamera.transform.forward, player.transform.position - playerCamera.transform.position);
        //print(ang);
        /*
        playerCamera.transform.rotation = new Quaternion(
                                        playerCamera.transform.rotation.x,
                                        playerCamera.transform.rotation.y + ang,
                                        playerCamera.transform.rotation.z,
                                        playerCamera.transform.rotation.w);
                                        */
        //playerCamera.transform.rotation = Quaternion.AngleAxis(ang, Vector3.up);
                                        
        //playerCamera.transform.rotation = Quaternion.AngleAxis(rotateY * rotationRate * Time.deltaTime, Vector3.right);
        //Vector3 yRotate = playerCamera.transform.right * rotateY;
        //playerCamera.transform.rotation = Quaternion.AngleAxis(rotateY, playerCamera.transform.right);
    }
}
