using UnityEngine;

public class HeroDieState : HeroState
{
    public override void Enter(Hero hero)
    {
        hero.SetAnimation("die");
    }
}
