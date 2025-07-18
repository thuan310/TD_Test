public abstract class EnemyState {
    public virtual void Enter(Enemy enemy) { }
    public virtual void Update(Enemy enemy) { }
    public virtual void Exit(Enemy enemy) { }
}