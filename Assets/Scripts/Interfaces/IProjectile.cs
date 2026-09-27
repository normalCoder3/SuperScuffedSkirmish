public interface IProjectile
{
    void Configure(float damage, float lifetime, bool persistent = false);
    void Destroy();
    void FlipY(bool flip);
    float GetDamage();
}
