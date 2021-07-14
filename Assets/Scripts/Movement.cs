using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    private RaycastHit hit;
    private Vector3 previousPosition;

    public NavMeshAgent agent;
    public AnimationController animationController;
    public Status status;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animationController = GetComponent<AnimationController>();
        agent.updateRotation = false;
        previousPosition = transform.position;
        status = GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToClick();
        CheckMovement();
    }

    private void CheckMovement()
    {
        if (IsRunning())
        {
            transform.LookAt(agent.destination + Vector3.up);
            status.currentState = StateEnum.RUNNING;
        }
        else if (!IsAttackingEnemy())
        {
            status.currentState = StateEnum.IDLE;
        }

        previousPosition = transform.position;
    }

    private bool IsAttackingEnemy()
    {
        return StateEnum.ATTACKING.Equals(status.currentState);
    }

    private void MoveToClick()
    {
        if (Input.GetMouseButtonDown(0)
            && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            agent.destination = hit.point;
        }
    }

    private bool IsRunning()
    {
        return (transform.position - previousPosition).magnitude > 0.01f;
    }
}