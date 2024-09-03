using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelArea : MonoBehaviour
{
    public Vector3 levelAreaCameraOffset;
    private CameraFollow camerafollowScript;
    public float areaCameraSize;

    public List<GameObject> spawnlanacakEnemyListesi = new();
    public List<GameObject> spawnlanmisEnemyListesi = new();
    bool isEnemiesSpawned = false;

    void Start()
    {
        camerafollowScript = Camera.main.GetComponent<CameraFollow>();
        SpawnEnemies();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            camerafollowScript.target = transform;
            camerafollowScript.kameraUzakligi = levelAreaCameraOffset;
            StartCoroutine(ChangeCameraSize(areaCameraSize));
        }
    }

    IEnumerator ChangeCameraSize(float newSize)
    {
        while (Camera.main.orthographicSize != newSize)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, newSize, 0.001f);
            yield return null;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            camerafollowScript.target = other.transform;
            StartCoroutine(ChangeCameraSize(5));
            camerafollowScript.kameraUzakligi = camerafollowScript.playerKameraUzakligi;

            SpawnEnemies();
        }
    }

    void SpawnEnemies()
    {
        if (isEnemiesSpawned == true)
        {
            for (int i = 0; i < spawnlanacakEnemyListesi.Count; i++)
            {
                if (spawnlanacakEnemyListesi[i] != spawnlanmisEnemyListesi[i] && spawnlanmisEnemyListesi[i] == null)
                {
                    GameObject enemy = Instantiate(spawnlanacakEnemyListesi[i], transform.position, Quaternion.identity);
                    spawnlanmisEnemyListesi[i] = enemy;
                    enemy.transform.SetParent(transform);
                }
            }
        }
        else
        {
            for (int i = 0; i < spawnlanacakEnemyListesi.Count; i++)
            {
                GameObject enemy = Instantiate(spawnlanacakEnemyListesi[i], transform.position, Quaternion.identity);
                spawnlanmisEnemyListesi.Add(enemy);
                enemy.transform.SetParent(transform);
            }
            isEnemiesSpawned = true;
        }
    }
}