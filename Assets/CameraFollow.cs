using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position,player.transform.position,0.2f);
    }
}
