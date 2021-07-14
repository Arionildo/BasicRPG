using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    private Status status;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        status = GetComponent<Status>();
    }

    private void Update()
    {
        AnimateBasedOnState(status.currentState);
    }

    private void AnimateBasedOnState(StateEnum currentState)
    {
        switch (currentState)
        {
            case StateEnum.ATTACKING:
                Attack();
                break;
            case StateEnum.DEAD:
                Die();
                break;
            case StateEnum.IDLE:
                Idle();
                break;
            case StateEnum.RUNNING:
                Run();
                break;
            default:
                Debug.LogError("Nenhuma animação foi escolhida para ser executada");
                break;
        }
    }

    private void Die()
    {
        StopAnimations();
        animator.SetBool(AnimationEnum.DIE, true);
    }

    public void Idle()
    {
        StopAnimations();
        animator.SetBool(AnimationEnum.IDLE, true);
    }

    public void Run()
    {
        StopAnimations();
        animator.SetBool(AnimationEnum.RUN, true);
    }

    public void Attack()
    {
        StopAnimations();
        animator.SetBool(AnimationEnum.ATTACK, true);
    }

    private void StopAnimations()
    {
        animator.SetBool(AnimationEnum.ATTACK, false);
        animator.SetBool(AnimationEnum.DIE, false);
        animator.SetBool(AnimationEnum.IDLE, false);
        animator.SetBool(AnimationEnum.RUN, false);
    }
}
