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

    Vector3 lastPos;
    public float rotSpeed = 5;
    float angle;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        cc = GetComponent<CharacterController>();
        angle = transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = Input.mousePosition - lastPos;
        angle += temp.x * rotSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(0, angle, 0);
        lastPos = Input.mousePosition;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // Animator Controller 의 Speed 파라미터에 값 할당
        anim.SetFloat("Speed", v);
        anim.SetFloat("Direction", h);

        Vector3 dir = new Vector3(h, 0, v);
        dir = Camera.main.transform.TransformDirection(dir);

        if(cc.isGrounded)
        {
            yVelocity = 0;
        }

        if(Input.GetButtonDown("Jump"))
        {
            yVelocity = jumpPower;
            anim.SetTrigger("Jump");
        }

        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;
        cc.Move(dir * speed * Time.deltaTime);
    }

    
}
