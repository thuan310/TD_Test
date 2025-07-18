using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public Animator animator;

    public EnemyState state;

    //Components
    [HideInInspector] public EnemyChaseWall enemyChaseWallComponent;
    [HideInInspector] public EnemyChaseHero enemyChaseHeroComponent;
    [HideInInspector] public Detection heroDetectionComponent;
    [HideInInspector] public Stats statsComponent;
    [HideInInspector] public EnemyAttack attackComponent;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyChaseWallComponent = GetComponent<EnemyChaseWall>();
        enemyChaseHeroComponent = GetComponent<EnemyChaseHero>();
        heroDetectionComponent = GetComponent<Detection>();
        statsComponent = GetComponent<Stats>();
        attackComponent = GetComponent<EnemyAttack>();
    }

    private void OnEnable()
    {
        statsComponent.OnDeath += HandleDeath;
    }

    private void OnDisable()
    {
        statsComponent.OnDeath -= HandleDeath;
    }

    void Start()
    {
        state = new EnemyChaseTowerState();
        state.Enter(this);
    }

    void Update()
    {
       

        state.Update(this);
    }

    public void ChangeState(EnemyState newState)
    {
        state?.Exit(this);
        state = newState;
        state.Enter(this);
    }

    public void SetAnimation(string name)
    {
        animator.Play(name);
    }


    private void HandleDeath(GameObject attacker)
    {
        EventManager.instance.enemyKillEvent.EnemyKilled(attacker);
        ChangeState(new EnemyDieState());
    }
}
