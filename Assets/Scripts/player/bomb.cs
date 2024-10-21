using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    private PlayerMovement player;
    [SerializeField] private AudioClip bombInstanceAudioClip, explosionAudioClip, explosionReload;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        StartCoroutine(Explosion());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Explosion()
    {
        player.waitForBoom = false; // Prevent other actions while waiting for the explosion
        GameManager.instance.ChangeBombState(false);
        AudioManager.instance.PlaySFX(bombInstanceAudioClip);
        yield return new WaitForSeconds(3f); // Wait before triggering the explosion

        CircleCollider2D collider = gameObject.transform.GetChild(0).GetComponent<CircleCollider2D>();
        SpriteRenderer spriteRenderer = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        Animator animation = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();

        // Enable the sprite and collider for the explosion effect
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        spriteRenderer.enabled = true;
        collider.enabled = true; 
        animation.enabled = true;
        AudioManager.instance.PlaySFX(explosionAudioClip);


        // Wait for a moment while the explosion effect is active
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.enabled = false;
        player.waitForBoom = true; // Allow further actions
        GameManager.instance.ChangeBombState(true);
        AudioManager.instance.PlaySFX(explosionReload);
        Destroy(gameObject);
        
    }

}
