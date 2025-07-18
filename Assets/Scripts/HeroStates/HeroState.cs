public abstract class HeroState {
    public virtual void Enter(Hero hero) { }
    public virtual void HandleInput(Hero hero, HeroInput input) { }
    public virtual void Update(Hero hero) { }
    public virtual void Exit(Hero hero) { }
}
