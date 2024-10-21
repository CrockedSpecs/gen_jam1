using System.Collections;
using System.Collections.Generic;
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

    [SerializeField]
    private AudioClip shootAudioclip,
                                        jumpAudioClip,
                                        moveAudioClip,
                                        damageAudioClip,
                                        winAudioClip,
                                        gameOverAudioClip,
                                        reloadEnergyAudioClip;
    [SerializeField] private bool isFloor, isEnergyEmpty, playerInmunity;
    [SerializeField] private int energy, lifes;

    private Vector2 movement; // Almacena la dirección del movimiento
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        animatorPlayer = GetComponent<Animator>();

        energy = 5;
        isEnergyEmpty = true;
        lifes = 3;

        InvokeRepeating("GetEnergy", 0, 5);
    }
    void Update()
    {
        // Obtener la entrada del jugador
        movement.x = Input.GetAxisRaw("Horizontal"); // Flechas o A/D
        shoot();

        if (lifes == 0)
        {
            AudioManager.instance.PlaySFX(gameOverAudioClip);
            GameManager.instance.GameOver();
            Destroy(gameObject);
        }

    }

    void FixedUpdate()
    {
        rb.AddForce(movement * moveSpeed * Time.deltaTime);
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
                transform.rotation = Quaternion.Euler(0, 0, 0);
                facingRight = !facingRight;
            }
            else if (movement.x < 0 && facingRight)
            {
                AudioManager.instance.PlaySFX(moveAudioClip);
                transform.rotation = Quaternion.Euler(0, 180, 0);
                facingRight = !facingRight;
            }
        }
    }
    void shoot()
    {
        if (Input.GetKeyDown(KeyCode.J) && energy > 0)
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
        else if(energy == 0 && isEnergyEmpty)
        {
            isEnergyEmpty = false;
            StartCoroutine("GetEnergy");
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

        else if (collision.gameObject.CompareTag("Limit") && lifes > 0)
        {
            onFloor = true;
            isFloor = true;
            animatorPlayer.SetBool("onFloor", onFloor);
        }
        else if (collision.gameObject.CompareTag("Enemy") && lifes > 0 && !playerInmunity)
        {
            playerInmunity = true;
            AudioManager.instance.PlaySFX(damageAudioClip);
            lifes--;
            GameManager.instance.ChangeLife(lifes, false);
            StartCoroutine("PlayerInmunity");
        }
    }
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

        

        else if (collision.gameObject.CompareTag("Finish") && lifes > 0)
        {
            AudioManager.instance.PlaySFX(winAudioClip);
            GameManager.instance.ChanceScene("ProceduralLevel");
            GameManager.instance.PassLevel();
        }
    }


    IEnumerator GetEnergy()
    { 
        GameManager.instance.ChangeEnergy(energy, true);
        yield return new WaitForSeconds(5);
        AudioManager.instance.PlaySFX(reloadEnergyAudioClip);
        yield return new WaitForSeconds(0.1f);
        energy = 5;
        isEnergyEmpty = true;
    }

    IEnumerator PlayerInmunity()
    {
        yield return new WaitForSeconds(1);
        playerInmunity = false;
    }
}