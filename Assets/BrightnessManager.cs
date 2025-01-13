using UnityEngine;

public class BrightnessManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject); // Zachowaj obiekt przy zmianie sceny
        Debug.Log("Obiekt został zachowany: " + gameObject.name);
    }
}
