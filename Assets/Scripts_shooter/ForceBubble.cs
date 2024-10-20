
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForceBubble : MonoBehaviour
{
    private Player_shooter player;
    private Vector3 offset;

    void Start()
    {
    
        player = FindObjectOfType<Player_shooter>();
     
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        transform.position = player.transform.position + offset;
    }


}
