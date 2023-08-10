using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    public static playerMovement instance;
    public SceneTransitionManager transitionManager;

    public int enemyHealth;
    public int enemyAttack;

    public GameObject enemyRenderer;
    public GameObject playerRenderer;

    public float moveSpeed;

    public Rigidbody2D rb;

    Vector2 movement;

    void Start()
    {
        moveSpeed = 5f;
        this.gameObject.tag = "Player";
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveSpeed * Time.deltaTime * movement);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            moveSpeed = 0f;
            collision.GetComponent<SpriteRenderer>().enabled = true;
            enemyRenderer = collision.gameObject;
            playerRenderer = this.gameObject;

            instance = this;
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(collision.gameObject);
            StartCoroutine(InitiateCombat());
        }
    }

    IEnumerator InitiateCombat()
    {
        yield return new WaitForSeconds(1);
        transitionManager.FadeToScene(1);
    }
}
