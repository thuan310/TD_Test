using UnityEngine;

public class EnemyChaseTowerState : EnemyState
{
    public override void Enter(Enemy enemy)
    {
        enemy.SetAnimation("run");
    }

    public override void Update(Enemy enemy)
    {
        enemy.enemyChaseWallComponent.MoveTowardWall();

        if (enemy.heroDetectionComponent.ClosestTarget != null )
        {
            if (enemy.heroDetectionComponent.ClosestTarget.CompareTag("Player"))
            {
                enemy.ChangeState(new EnemyChaseHeroState());

            }
            else if (enemy.heroDetectionComponent.ClosestTarget.CompareTag("Wall"))
            {
                float distance = Vector2.Distance(enemy.transform.position, enemy.enemyChaseWallComponent.closetPoint);
                if (distance < enemy.attackComponent.meleeRange)
                {
                    enemy.ChangeState(new EnemyAttackState());
                }
            }
        }

    }
}
