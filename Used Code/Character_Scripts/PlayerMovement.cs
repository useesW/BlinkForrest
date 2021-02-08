using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{


    public float moveSpeed = 5f;
    bool isdead = false;

    public Rigidbody2D rb;
    public Camera cam;

    private int count;

    Vector2 movement;
    Vector2 mousePos;

 
    // Input
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");

        movement.y = Input.GetAxisRaw("Vertical");

       // mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    // Movement
    void FixedUpdate()
    {
        if (isdead)
        {
            return;
        }
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
       // Vector2 lookDir = mousePos - rb.position;
       // float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
       // rb.rotation = angle;
    }

    public void OnDeath()
    {
        isdead = true;
    }
   
  
}
