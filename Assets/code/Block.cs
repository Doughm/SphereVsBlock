using UnityEngine;

public class Block : MonoBehaviour
{
    public Transform bulletPrefab;
    private bool isGoingLeft;
    private GameObject[] bullets = new GameObject[10];
    private const int fireRate = 40;
    private int fireCoolDown = 0;
    private int bulletCounter = 0;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i] = Instantiate(bulletPrefab.gameObject, new Vector3(), Quaternion.Euler(90, 0, 0)) as GameObject;
            bullets[i].tag = "BulletEnemy";
            bullets[i].GetComponent<Bullet>().setIsForward(false);
            bullets[i].GetComponent<Bullet>().setSpeed(0.5f);
            bullets[i].GetComponent<Bullet>().setTimer(100);
            bullets[i].SetActive(false);
        }
    }

    //updates at a regular interval
    void FixedUpdate()
    {
        if (Game.instance.isPaused == false)
        {
            fireCoolDown++;
            moveBlock();
            changeDirection();
            fireBullet();
        }
    }

    //moves the block
    private void moveBlock()
    {
        if (isGoingLeft == true)
        {
            transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z - 0.3f);
        }
        else
        {
            transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z - 0.3f);
        }
    }

    //fires the blocks bullet
    private void fireBullet()
    {
        if (fireCoolDown >= fireRate)
        {
            bullets[bulletCounter].SetActive(true);
            bullets[bulletCounter].transform.position = transform.position;
            bulletCounter++;
            fireCoolDown = 0;
            if (bulletCounter > bullets.Length - 1)
            {
                bulletCounter = 0;
            }
        }
    }

    //changes direction of the block
    private void changeDirection()
    {
        if (transform.position.x <= -4.2)
        {
            isGoingLeft = false;
        }
        else if (transform.position.x >= 4.2)
        {
            isGoingLeft = true;
        }

        if (transform.position.z < -24)
        {
            gameObject.SetActive(false);
        }
    }

    //run when the object is turned on
    void OnEnable()
    {
        if (Random.Range(0, 2) == 0)
        {
            isGoingLeft = true;
        }
        else
        {
            isGoingLeft = false;
        }
        fireCoolDown = Random.Range(0, fireRate);
    }

    //collision detection
    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "BulletPlayer")
        {
            collider.gameObject.SetActive(false);
            Game.instance.addToScore(10);
            gameObject.SetActive(false);
            Game.instance.checkNewLife();
        }
        else if (collider.gameObject.tag == "PlayerObject")
        {
            Game.instance.addToLives(-1);
            gameObject.SetActive(false);
        }
    }

    //destroys all bullets
    public void destroyBullets()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if (bullets[i] != null)
            {
                bullets[i].SetActive(false);
            }
        }
    }
}
