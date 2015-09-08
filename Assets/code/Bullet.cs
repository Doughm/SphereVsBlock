using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool isForward = true;
    private float speed = 1;
    private int timer = 50;
    private int ticker = 0;

    //updates at a regular interval
    void FixedUpdate()
    {
        if (Game.instance.isPaused == false)
        {
            ticker++;
            if (isForward == true)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed);
            }
            if (ticker >= timer)
            {
                gameObject.SetActive(false);
            }
        }
    }

    //run when the object is turned on
    void OnEnable()
    {
        ticker = 0;
    }

    //sets if backwards or forwards
    public void setIsForward(bool setDirection)
    {
        isForward = setDirection;
    }

    //sets the bullet speed
    public void setSpeed(float bulletSpeed)
    {
        speed = bulletSpeed;
    }

    //sets the timer amount
    public void setTimer(int turnOffTime)
    {
        timer = turnOffTime;
    }
}
