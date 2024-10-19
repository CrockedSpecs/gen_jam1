using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;// Velocidad de movimiento
    public Rigidbody2D rb; // Referencia al Rigidbody2D
    public bool onFloor,
                jumpBool,
                shootBool = false,
                facingRight = true;
    Animator animatorPlayer;
    public GameObject bullet;

    [SerializeField] private AudioClip shootAudioclip, jumpAudioClip, moveAudioClip;
    [SerializeField] private bool isFloor;
    [SerializeField] private int energy, lifes;

    private Vector2 movement; // Almacena la dirección del movimiento
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        animatorPlayer = GetComponent<Animator>();

        energy = 5;
        lifes = 3;

    }
    void Update()
    {
        // Obtener la entrada del jugador
        movement.x = Input.GetAxisRaw("Horizontal"); // Flechas o A/D
        shoot();

    }

    void FixedUpdate()
    {
        
        rb.AddForce(movement * moveSpeed *Time.deltaTime);
        animatorPlayer.SetFloat("speed", Mathf.Abs(movement.x));
        if (onFloor && isFloor)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AudioManager.instance.PlaySFX(jumpAudioClip);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
                jumpBool = true;
                onFloor = false;
                isFloor = false;
                animatorPlayer.SetBool("jump", jumpBool);
                animatorPlayer.SetBool("onFloor", onFloor);
            }
            //if (Input.GetKey(KeyCode.J))
            //{
            //    animatorPlayer.SetTrigger("shoot");
            //    shootBool = true;
            //}
            if (movement.x > 0 && !facingRight)
            {
                AudioManager.instance.PlaySFX(moveAudioClip);
                transform.rotation = Quaternion.Euler(0,0,0);
                Debug.Log("True" + facingRight);
                facingRight = !facingRight;
            }
            else if(movement.x < 0 && facingRight)
            {
                AudioManager.instance.PlaySFX(moveAudioClip);
                transform.rotation = Quaternion.Euler(0, 180, 0);
                Debug.Log("False" + facingRight);
                facingRight = !facingRight;
            }
        }
    }
    void shoot()
    {
        if(Input.GetKeyDown(KeyCode.J) && onFloor && energy > 0)
        {
            energy--;
            GameManager.instance.ChangeEnergy(energy, false);

            AudioManager.instance.PlaySFX(shootAudioclip);
            animatorPlayer.SetTrigger("shoot");
            if (!facingRight)
            {
                Instantiate(bullet, transform.position, Quaternion.Euler(0, 180, 0));
            }
            else
            {
                Instantiate(bullet, transform.position, Quaternion.identity);
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

            //Is he really on the ground or did he hit a wall?
            if (isFloor)
            {
                animatorPlayer.SetBool("onFloor", onFloor);
            }
        }
    }
    /*
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            onFloor = false;
            animatorPlayer.SetBool("onFloor", onFloor);

        }
    }
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            //Is he really falling?
            if (!isFloor)
            {
                isFloor = true;
            }
        }
    }
    /*
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isFloor = false;
        }
    }
    */
}


