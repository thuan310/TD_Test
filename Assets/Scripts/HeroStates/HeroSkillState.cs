using UnityEngine;

public class HeroSkillState : HeroState
{
    private bool animationFinished = false;

    public override void Enter(Hero hero)
    {
        hero.skillComponent.UseSkill();
    }

    public override void Update(Hero hero)
    {
        var animState = hero.animator.GetCurrentAnimatorStateInfo(0);

        if (animState.IsName("skill") && animState.normalizedTime >= 1f && !animationFinished)
        {
            animationFinished = true;

        }
    }

    public override void HandleInput(Hero hero, HeroInput input)
    {
        if (animationFinished)
        {
            if (input.move == Vector2.zero)
            {
                hero.ChangeState(new HeroIdleState());
            }
            else
            {
                hero.ChangeState(new HeroMoveState());
            }
        }

    }

}
