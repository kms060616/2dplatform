using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
        public float moveSpeed = 3f;
    public float jumpForce = 3f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private Animator pAni;

    private  bool isGiant = false;

    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pAni = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        if (isGiant)
        {
            if (moveInput < 0)
                transform.localScale = new Vector3(2f, 2f, 1f);

            if (moveInput > 0)
                transform.localScale = new Vector3(-2f, 2f, 1f);
        }
        else
        {
            if (moveInput < 0)
                transform.localScale = new Vector3(1f, 1f, 1f);

            if (moveInput > 0)
                transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //점프 코드
            pAni.SetTrigger("Jump");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (collision.CompareTag("Finish"))
        {
            collision.GetComponent<LevelObject>().MoveToNextLevel();
        }
        
        if (collision.CompareTag("Enemy"))
        {
            if (isGiant)
                Destroy(collision.gameObject);
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (collision.CompareTag("Item"))
        {
            isGiant = true;
            Destroy(collision.gameObject);
        }
    }

    
}
