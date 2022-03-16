using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    GameManager gm;
    public GameObject GameManagerGo;
    public GameObject PlayerBullet;
    public GameObject bulletPos1;
    public GameObject bulletPos2;
    public GameObject Explosinship;
    public float speed;

    public Text LiveUIText;

    const int MaxLives = 3;
    int lives;

    public void Init()
    {
        lives = MaxLives;
        LiveUIText.text = lives.ToString();

        transform.position = new Vector2(0, 0);

        gameObject.SetActive(true);
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Fire Bullet//
        if (Input.GetKeyDown("space"))
        {
            GameObject bullet01 = (GameObject)Instantiate(PlayerBullet);
            bullet01.transform.position = bulletPos1.transform.position;

            GameObject bullet02 = (GameObject)Instantiate(PlayerBullet);
            bullet02.transform.position = bulletPos2.transform.position;
        }

        //MoveMente Player//
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        

        Vector2 direction = new Vector2(x, y).normalized;

        Move(direction);    

    }

    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - 0.225f;
        min.x = min.x + 0.225f;

        max.y = max.y - 0.285f;
        min.y = min.y + 0.285f;

        Vector2 pos = transform.position;

        pos += direction * speed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.tag == "EnemyShipTag") || (collision.tag == "EnemyBulletTag"))
        {
            PlayExplosion();
            lives--;
            LiveUIText.text = lives.ToString();

            if(lives == 0)
            {
                FindObjectOfType<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
                
                gameObject.SetActive(false);
                //Destroy(gameObject);
            }
            
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(Explosinship);

        explosion.transform.position = transform.position;
    }
}
