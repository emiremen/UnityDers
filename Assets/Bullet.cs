using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D mermininDegdigiObje)
    {
        if (mermininDegdigiObje.gameObject.tag == "Enemy")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
            StartCoroutine(ChangeEnemyColor(mermininDegdigiObje.gameObject));
        }
    }

    IEnumerator ChangeEnemyColor(GameObject enemyObject)
    {
        enemyObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(1);
        enemyObject.GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        Destroy(enemyObject);
    }
}
