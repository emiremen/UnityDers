using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Enemy : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 2f;
    private Vector3 nextPosition;
    private bool isTakipEdiyorMu = false;

    private Transform target;

    void Start()
    {
        nextPosition = pointB.position;
    }

    void Update()
    {
        Move();
        EnemyFlipFace();

        RaycastHit2D raycastinDegdigiObje;

        if (GetComponent<SpriteRenderer>().flipX == true)
        {
            Debug.DrawRay(transform.position + new Vector3(-1, 0, 0), -transform.right * 4, Color.green);
            raycastinDegdigiObje = Physics2D.Raycast(transform.position + new Vector3(-1, 0, 0), -transform.right, 4);

        }
        else
        {
            Debug.DrawRay(transform.position + new Vector3(1, 0, 0), transform.right * 4, Color.green);
            raycastinDegdigiObje = Physics2D.Raycast(transform.position + new Vector3(1, 0, 0), transform.right, 4);

        }

        if (raycastinDegdigiObje.collider != null && raycastinDegdigiObje.transform.tag == "Player")
        {
            isTakipEdiyorMu = true;
            target = raycastinDegdigiObje.transform;
            nextPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
        }
        else
        {
            isTakipEdiyorMu = false;
        }
    }

    private void EnemyFlipFace()
    {
        if (transform.position.x > nextPosition.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void Move()
    {
        if (!isTakipEdiyorMu)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(nextPosition.x, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);
            if (transform.position.x == nextPosition.x)
            {
                nextPosition = (nextPosition == pointA.position) ? pointB.position : pointA.position;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(nextPosition.x, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        }

    }

    void OnTriggerEnter2D(Collider2D enemininDegdigiNesne)
    {
        if (enemininDegdigiNesne.gameObject.tag == "Player")
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // void OnCollisionEnter2D(Collision2D enemininDegdigiNesne)
    // {
    //     if (enemininDegdigiNesne.gameObject.tag == "Player")
    //     {
    //         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //     }
    // }
}
