using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    CharacterController character;

    float mouseX = 0;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();    
    }

    // Update is called once per frame
    void Update()
    {
        playerMove();
        mouseX += Input.GetAxis("Mouse X") * 7;
        transform.eulerAngles = new Vector3(0, mouseX, 0);
    }

    void playerMove()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");


        Vector3 move = new Vector3(moveX, 0, moveZ);

        character.Move(transform.TransformDirection(move) * Time.deltaTime * 7);

    }
}
