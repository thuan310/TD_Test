using UnityEngine;
using UnityEngine.InputSystem;

public class HeroIdleState : HeroState
{
    public override void Enter(Hero hero)
    {
        hero.SetAnimation("idle");
    }

    public override void HandleInput(Hero hero, HeroInput input)
    {
        if (input.move != Vector2.zero)
        {
            hero.ChangeState(new HeroMoveState());
        }
        else if (input.skillPressed && hero.skillComponent.IsReady)
        {
            hero.ChangeState(new HeroSkillState());
        }

    }

    public override void Update(Hero hero)
    {
        if (hero.detectionComponent.ClosestTarget)
        {
            hero.ChangeState(new HeroAttackState());
        }
    }
}
