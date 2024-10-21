using System.Collections;
using Unity.Mathematics;
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
    public GameObject Bomb;
    public bool waitForBoom;

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

    private Vector2 movement; // Almacena la direcci�n del movimiento
    private void Start()
    {
        waitForBoom = true;
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
        LifeMonitor();
        setBomb();

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
        if (energy == 0 && !isEnergyEmpty)
        {
            isEnergyEmpty = true;
            StartCoroutine("GetEnergy");
        }
    }

    void setBomb()
    {
        if (waitForBoom)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (facingRight)
                {
                    Instantiate(Bomb, new Vector2(transform.position.x + 1, transform.position.y), quaternion.identity);
                }
                else if (!facingRight)
                {
                    Instantiate(Bomb, new Vector2(transform.position.x - 1, transform.position.y), quaternion.identity);
                }
            }
        }

    }
    void Flip()
    {
        facingRight = !facingRight; // Cambiar la direcci�n
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
            AudioManager.instance.PlaySFX(damageAudioClip);
            lifes--;
            GameManager.instance.ChangeLife(lifes, false);
            playerInmunity = true;
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
            GameManager.instance.ChangeScene("ProceduralLevel");
            GameManager.instance.PassLevel();
        }
    }

    private void LifeMonitor()
    {
        if (lifes == 0)
        {
            AudioManager.instance.PlaySFX(gameOverAudioClip);
            GameManager.instance.GameOver();
            Destroy(gameObject);
        }
    }


        IEnumerator GetEnergy()
    {
        Debug.Log("entreacargar");
        GameManager.instance.ChangeEnergy(energy, true);
        yield return new WaitForSeconds(5);
        energy = 5;
        yield return new WaitForSeconds(0.1f);
        isEnergyEmpty = false;
        AudioManager.instance.PlaySFX(reloadEnergyAudioClip);    
    }

    IEnumerator PlayerInmunity()
    {  
        yield return new WaitForSeconds(1);
        playerInmunity = false;
    }
}