using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hero : MonoBehaviour
{
    [HideInInspector] public Animator animator;

    public HeroState state;

    //Components
    [HideInInspector] public Movement movementComponent;
    [HideInInspector] public IAttackComponent attackComponent;
    [HideInInspector] public Detection detectionComponent;
    [HideInInspector] public Stats statsComponent;
    [HideInInspector] public HeroSkill skillComponent;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movementComponent = GetComponent<Movement>();
        attackComponent = GetComponent<IAttackComponent>();
        detectionComponent = GetComponent<Detection>();
        statsComponent = GetComponent<Stats>();
        skillComponent = GetComponent<HeroSkill>();
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
        state = new HeroIdleState();
        state.Enter(this);
    }

    void Update()
    {
        Debug.Log(state.GetType().ToString());

        Vector2 move = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;

        bool skill = Input.GetKeyDown(KeyCode.Q);

        HeroInput input = new HeroInput(move, skill);

        state.HandleInput(this, input);
        state.Update(this);
    }

    public void ChangeState(HeroState newState)
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
        ChangeState(new HeroDieState());
    }
}
