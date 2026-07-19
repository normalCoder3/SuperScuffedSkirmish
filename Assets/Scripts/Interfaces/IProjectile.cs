public interface IProjectile
{
    void Configure(float damage, float lifetime, float speed = 0, bool persistent = false);
    void Destroy();
    float GetDamage();
}
