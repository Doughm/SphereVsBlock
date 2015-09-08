using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform bulletPrefab;
    private int bulletCounter = 0;
    private const int fireRate = 20;
    private int fireCoolDown = 0;
    private float speed = 0.3f;
    private GameObject[] bullets = new GameObject[10];

    // Use this for initialization
    void Start()
    {
        this.tag = "PlayerObject";
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i] = Instantiate(bulletPrefab.gameObject, new Vector3(), Quaternion.Euler(90, 0, 0)) as GameObject;
            bullets[i].tag = "BulletPlayer";
            bullets[i].SetActive(false);
        }
    }

    //updates at a regular interval
    void FixedUpdate()
    {
        if (Game.instance.isPaused == false)
        {
            fireCoolDown++;
            checkInput();
        }
    }

    //updates input
    private void checkInput()
    {
        if (Input.GetAxisRaw("Left") == 1)
        {
            if (transform.position.x >= -4.5)
            {
                transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
            }
        }
        if (Input.GetAxisRaw("Right") == 1)
        {
            if (transform.position.x <= 4.5)
            {
                transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
            }
        }
        if (Input.GetAxisRaw("Down") == 1)
        {
            if (transform.position.z >= -24.5)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed);
            }
        }
        if (Input.GetAxisRaw("Up") == 1)
        {
            if (transform.position.z <= 24.5)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);
            }
        }
        if (Input.GetButton("Fire") == true)
        {
            fireBullet();
        }
    }

    //fires the bullet
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

    //collision detection
    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "BulletEnemy")
        {
            Game.instance.addToLives(-1);
            collider.gameObject.SetActive(false);
        }
    }

    //destroys all bullets
    public void destroyBullets()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i].SetActive(false);
        }
    }
}
