using UnityEngine;

public class TurretScript : MonoBehaviour
{
    private float duration = 5f;
    void Start()
    {
        Destroy(gameObject, duration);
    }
}
