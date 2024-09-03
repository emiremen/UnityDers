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

    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;

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
        //CameraOffset();
        FlipPlayerFace();
        PlayShootAnim();
    }

    void PlayShootAnim()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("hitShoot");
            //StartCoroutine(Shoot());
            Invoke("Shoot", 0.8f / 1.8f); // 0.8 saniye bekledikten sonra Shoot fonksiyonunu çalıştırır.
        }
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (spriteRenderer.flipX)
        {
            rb.velocity = transform.right * -bulletSpeed;
        }
        else
        {
            rb.velocity = transform.right * bulletSpeed;
        }
    }

    private void FlipPlayerFace()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            spriteRenderer.flipX = false;
            bulletSpawnPoint.localPosition = new Vector3(bulletSpawnPoint.localPosition.x, bulletSpawnPoint.localPosition.y, bulletSpawnPoint.localPosition.z);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            spriteRenderer.flipX = true;
            bulletSpawnPoint.localPosition = new Vector3(-bulletSpawnPoint.localPosition.x, bulletSpawnPoint.localPosition.y, bulletSpawnPoint.localPosition.z);
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
        }
        else
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
            rb.velocity = new Vector2(rb.velocity.x, ziplamaGucu * Time.deltaTime);
        }
    }

}
