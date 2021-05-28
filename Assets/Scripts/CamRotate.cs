using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public float sensitivity = 200;
    Vector3 angles;
    Vector3 lastAngle;

    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        angles.x = transform.eulerAngles.x;
        angles.y = transform.eulerAngles.y;

        lastAngle = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, sensitivity * Time.deltaTime);

        transform.forward = Vector3.Lerp(transform.forward, target.forward, sensitivity * Time.deltaTime);

        Vector3 temp = Input.mousePosition - lastAngle;
        //float h = Input.GetAxis("Mouse Y");
        //float v = Input.GetAxis("Mouse X");
        angles.x += -temp.y * sensitivity * Time.deltaTime;
        //angles.y += temp.x * sensitivity * Time.deltaTime;

        angles.x = Mathf.Clamp(angles.x, -60, 60);
        //transform.eulerAngles = angles;

        lastAngle = Input.mousePosition;
    }
}
