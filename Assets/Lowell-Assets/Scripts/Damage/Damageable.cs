public interface Damageable {
    Team team { get; }
    void Damage(int dmg, Element element);
    void Heal(int hp);
}