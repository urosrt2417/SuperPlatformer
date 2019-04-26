using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public string scene = "RandomlyGenerated";

    private GameObject map;
    private GameObject player;

    void Start()
    {
        map = GameObject.FindGameObjectWithTag("map");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        int playerY = (int)player.transform.position.y;
        int lowestPoint = map.GetComponent<MapGenerator>().lowestPoint;
        if (playerY < lowestPoint)
        {
            Debug.Log(player.transform.position.y);
            Die();
        }
    }

    void Die()
    {
        SceneManager.LoadScene(scene);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "meanGrass")
        {
            Die();
        }
    }
}
