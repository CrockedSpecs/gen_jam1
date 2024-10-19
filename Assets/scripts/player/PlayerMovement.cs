using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;// Velocidad de movimiento
    public Rigidbody2D rb; // Referencia al Rigidbody2D
    public bool onFloor,
                jumpBool, 
                facingRight = true;
    Animator animatorPlayer;

    private Vector2 movement; // Almacena la dirección del movimiento
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        animatorPlayer = GetComponent<Animator>();
    }
    void Update()
    {
        // Obtener la entrada del jugador
        movement.x = Input.GetAxisRaw("Horizontal"); // Flechas o A/D
        

    }

    void FixedUpdate()
    {
        
        rb.AddForce(movement * moveSpeed *Time.deltaTime);
        animatorPlayer.SetFloat("speed", Mathf.Abs(movement.x));
        if (onFloor)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
                jumpBool = true;
                animatorPlayer.SetBool("jump", jumpBool);
                animatorPlayer.SetBool("onFloor", onFloor);
            }
            if (Input.GetKey(KeyCode.J))
            {
                animatorPlayer.SetTrigger("shoot");
            }
            if (movement.x > 0 && !facingRight)
            {
                Flip();
            }
            else if(movement.x < 0 && facingRight)
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight; // Cambiar la dirección
        Vector3 scale = transform.localScale; // Obtener la escala actual del objeto
        scale.x *= -1; // Invertir el valor de la escala en el eje X
        transform.localScale = scale; // Aplicar la nueva escala
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            onFloor = true;
            animatorPlayer.SetBool("onFloor", onFloor);

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            onFloor = false;
            animatorPlayer.SetBool("onFloor", onFloor);

        }
    }
}


