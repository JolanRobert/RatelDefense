public enum DamageType {
    RAW,
    PHYSICAL,
    MAGICAL
}

public interface IDamageable {
    public void TakeDamage(float amount, DamageType damageType);
    public void Die();
}
