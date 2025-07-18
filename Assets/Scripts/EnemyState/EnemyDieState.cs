using UnityEngine;

public class EnemyDieState : EnemyState
{
    public override void Enter(Enemy enemy)
    {
        enemy.SetAnimation("die");
    }

    public override void Update(Enemy enemy)
    {
        var animState = enemy.animator.GetCurrentAnimatorStateInfo(0);

        if (animState.IsName("die") && animState.normalizedTime >= 1f)
        {
            Object.Destroy(enemy.gameObject);
        }
    }
}
