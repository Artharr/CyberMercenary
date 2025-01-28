using UnityEngine;

public interface IWeapon
{
    int Level { get; set; }
    bool isActive { get; set; }
    string Name { get; set; }
    void Activate();
    void Upgrade();
}
