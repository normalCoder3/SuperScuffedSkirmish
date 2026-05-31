public interface IProjectile
{
    void Configure(float damage, float lifetime, float speed = 0);
    void Destroy();
    float GetDamage();
}
