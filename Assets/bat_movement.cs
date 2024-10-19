using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bat_movement : MonoBehaviour
{
   [SerializeField] private float speed = 3f;

   private Rigidbody2D playerRb;
   private Vector2 moveInput;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY);

    }

    private void FixedUpdate()
    {
       playerRb.MovePosition(playerRb.position + moveInput * speed * Time.fixedDeltaTime);
    }
}
