using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    float mouseSpeed = 7;
    float mouseY = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mouseY += Input.GetAxis("Mouse Y") * mouseSpeed;

        mouseY = Mathf.Clamp(mouseY, -55.0f, 55.0f);
        transform.localEulerAngles = new Vector3(-mouseY, 0, 0);
    }
}
