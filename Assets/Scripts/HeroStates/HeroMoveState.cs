using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroMoveState : HeroState
{
    public override void Enter(Hero hero)
    {
        hero.SetAnimation("run");
    }

    public override void HandleInput(Hero hero, HeroInput input)
    {
        if (input.move == Vector2.zero)
        {
            hero.ChangeState(new HeroIdleState());
        }
        else if (input.skillPressed && hero.skillComponent.IsReady)
        {
            hero.ChangeState(new HeroSkillState());
        }
        else
        {
            hero.movementComponent.Move(input.move);
        }
    }
}
