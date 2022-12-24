using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;
    private bool jumpKeyPressed;
    private float horizontalInput;
    private Rigidbody rigidBodyComponent;
    private int superJumpsRemaining;
    

    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            jumpKeyPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate(){

        rigidBodyComponent.velocity = new Vector3(horizontalInput, rigidBodyComponent.velocity.y, 0);

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0) {
            return;
        }

        if(jumpKeyPressed){

            float jumpPower = 5f;

            if (superJumpsRemaining > 0) 
            {
                jumpPower *= 2;
                superJumpsRemaining--;
            }

            rigidBodyComponent.AddForce(Vector3.up *jumpPower, ForceMode.VelocityChange);
            jumpKeyPressed = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer ==7) {
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }
    }
}
