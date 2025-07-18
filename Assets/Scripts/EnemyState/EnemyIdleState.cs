using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public override void Enter(Enemy enemy)
    {
        enemy.SetAnimation("idle");
    }



    //public override void Update(Enemy enemy)
    //{
    //    if (hero.detectionComponent.ClosestTarget)
    //    {
    //        hero.ChangeState(new HeroAttackState());
    //    }
    //}
}
