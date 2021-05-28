using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5;
    public float gravity = -20;
    public float jumpPower = 5;
    
    float yVelocity = 0;


    CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        dir = Camera.main.transform.TransformDirection(dir);

        if(cc.isGrounded)
        {
            yVelocity = 0;
        }

        if(Input.GetButtonDown("Jump"))
        {
            yVelocity = jumpPower;
        }

        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;
        cc.Move(dir * speed * Time.deltaTime);
    }

    
}
