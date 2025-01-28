using System;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [HideInInspector] public float health;
    float maxHealth = 50;
    [HideInInspector]public bool isDashing = false;
    public event EventHandler<float> onDamaged;
    private GameData gameData;
    public IWeapon[] weapons;


    private void Awake()
    {
        weapons = gameObject.GetComponents<IWeapon>();
        gameData = SaveSystem.LoadGame();
        maxHealth += gameData.maxHealthLevel*10;
        health = maxHealth;
    }

    public void Damage(float damage)
    {

        if (isDashing || health < 0 || GlobalData.won) { return; }
        health -= damage;
        onDamaged?.Invoke(this, health/maxHealth);
    }
}
