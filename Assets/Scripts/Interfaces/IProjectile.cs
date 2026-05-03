using UnityEngine;

public interface IProjectile
{
    void ConfigureProjectile(float damage, float lifetime, float speed);
    void Destroy();
    float GetDamage();
}
