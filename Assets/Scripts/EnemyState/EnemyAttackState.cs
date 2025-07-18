using UnityEngine;

public class EnemyAttackState : EnemyState {

    public override void Enter(Enemy enemy)
    {
        enemy.attackComponent.ToggleAttack();

    }

    public override void Exit(Enemy enemy)
    {
        enemy.attackComponent.ToggleAttack();

    }

    public override void Update(Enemy enemy)
    {
        var target = enemy.heroDetectionComponent.ClosestTarget;

        if (target == null)
        {
            enemy.ChangeState(new EnemyChaseTowerState());
            return;
        }
        if (target.CompareTag("Player"))
        {
            float distance = Vector2.Distance(enemy.transform.position, target.transform.position);
            if (distance > enemy.attackComponent.meleeRange)
            {
                enemy.ChangeState(new EnemyChaseHeroState());
            }
        }


    }

}
