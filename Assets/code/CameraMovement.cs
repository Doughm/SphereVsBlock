using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float cameraDistance = 10;

    // Update is called once per frame
    void Update()
    {
        if(Game.instance.isPlayingGame == true)
        {
            this.gameObject.transform.position = new Vector3(Game.instance.getPlayerPosition().x,
                                                             Game.instance.getPlayerPosition().y + cameraDistance,
                                                             Game.instance.getPlayerPosition().z - cameraDistance);
        }
    }
}
