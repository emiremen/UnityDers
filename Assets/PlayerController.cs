using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float ziplamaGucu = 5;
    private Rigidbody2D rb;
    public CameraFollow cameraScript;
    Vector3 karakterinNeKadarOnundeOlsun;
    public Animator animator;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        karakterinNeKadarOnundeOlsun = new Vector3(3.5f, cameraScript.kameraUzakligi.y, cameraScript.kameraUzakligi.z);
    }

    void Update()
    {
        Move();
        Jump();
        CameraOffset();
        FlipPlayerFace();
    }

    private void FlipPlayerFace()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void CameraOffset()
    {
        float move = Input.GetAxis("Horizontal");
        if (move > 0)
        {
            karakterinNeKadarOnundeOlsun = new Vector3(3.5f, cameraScript.kameraUzakligi.y, cameraScript.kameraUzakligi.z);
            cameraScript.kameraUzakligi = Vector3.Lerp(cameraScript.kameraUzakligi, karakterinNeKadarOnundeOlsun, 0.01f);
        }
        else if (move < 0)
        {
            karakterinNeKadarOnundeOlsun = new Vector3(-3.5f, cameraScript.kameraUzakligi.y, cameraScript.kameraUzakligi.z);
            cameraScript.kameraUzakligi = Vector3.Lerp(cameraScript.kameraUzakligi, karakterinNeKadarOnundeOlsun, 0.01f);
        }
    }

    private float Move()
    {
        float move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(move * 10, rb.velocity.y);
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isIdle", false);
        }else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", true);
        }
        return move;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, ziplamaGucu);
        }
    }

}
