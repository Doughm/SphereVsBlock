using UnityEngine;
using System.Collections.Generic;

public class BlockFactory : MonoBehaviour
{
    public Transform box;
    private int ticker = 0;
    private Vector3 boxPosition;
    private int creationSpeed = 61;
    private GameObject[] blocks = new GameObject[200];
    private int objectCounter = 0;

    // Use this for initialization
    void Start()
    {
        boxPosition.y = 1;
        boxPosition.z = 24;
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i] = Instantiate(box.gameObject, new Vector3(), Quaternion.Euler(0, 0, 0)) as GameObject;
            blocks[i].SetActive(false);
        }
    }

    //updates at a regular interval
    void FixedUpdate()
    {
        if (Game.instance.isPlayingGame == true && Game.instance.isPaused == false)
        {
            if (Game.instance.blocksLeft != 0)
            {
                ticker++;
                if (ticker == creationSpeed)
                {
                    boxPosition.x = Random.Range(-4, 4);
                    blocks[objectCounter].SetActive(true);
                    blocks[objectCounter].gameObject.transform.position = boxPosition;
                    ticker = 0;

                    objectCounter++;
                    if (objectCounter >= blocks.Length)
                    {
                        objectCounter = 0;
                    }
                    Game.instance.subtractBlocksLeft();
                }
            }
            else
            {
                for (int i = 0; i < blocks.Length; i++)
                {
                    if (blocks[i].gameObject.activeInHierarchy == true)
                    {
                        break;
                    }
                    else
                    {
                        if (i == blocks.Length - 1)
                        {
                            setSpawnSpeed(Game.instance.level);
                            Game.instance.nextLevel();
                        }
                    }
                }
            }
        }
    }

    //removes all blocks
    public void deleteAllBlocks()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].SetActive(false);
        }
    }

    //sets the spawn speed of the blocks
    public void setSpawnSpeed(int level)
    {
        creationSpeed = 61 - level;
    }

    //resets all game varables
    public void reset()
    {
        creationSpeed = 61;
        objectCounter = 0;
    }

    //destroys all bullets in all blocks
    public void destroyBlockBullets()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].GetComponent<Block>().destroyBullets();
        }
    }
}
