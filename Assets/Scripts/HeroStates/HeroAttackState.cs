using UnityEngine;

public class HeroAttackState : HeroState
{
    public override void Enter(Hero hero)
    {
        hero.attackComponent.ToggleAttack();

    }

    public override void Exit(Hero hero)
    {
        hero.attackComponent.ToggleAttack();

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
        if (!hero.detectionComponent.ClosestTarget)
        {
            hero.ChangeState(new HeroIdleState());
        }


    }
}
