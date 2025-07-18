using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyChaseHeroState : EnemyState
{

    public override void Enter(Enemy enemy)
    {
        enemy.SetAnimation("run");
    }

    public override void Update(Enemy enemy)
    {
        var target = enemy.heroDetectionComponent.ClosestTarget;

        if (target == null || !target.CompareTag("Player"))
        {
            enemy.ChangeState(new EnemyChaseTowerState());
            return;
        }

        enemy.enemyChaseHeroComponent.MoveTowardHero();

        float distance = Vector2.Distance(enemy.transform.position, target.transform.position);
        if (distance < enemy.attackComponent.meleeRange)
        {
            enemy.ChangeState(new EnemyAttackState());
        }
    }
}
