using UnityEngine;

public class BrightnessManager : MonoBehaviour
{
    public static BrightnessManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Usuń duplikaty
            Debug.LogWarning("Duplikat BrightnessManager został usunięty.");
            return;
        }

        Instance = this; // Ustaw instancję singletonu
        DontDestroyOnLoad(this.gameObject); // Zachowaj obiekt przy zmianie sceny
        Debug.Log("BrightnessManager singleton został zainicjalizowany: " + gameObject.name);
    }
}
