using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool hasTarget;
    private RaycastHit hit;
    private Movement movement;
    private float currentDistanceFromEnemy;
    private float currentCooldown = 0f;

    public float maxDistanceFromEnemy = 3f;
    public float maxCooldown = 1f;
    public Status status;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<Movement>();
        status = GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTarget();
        Prepare();
        Execute();
        UpdateCooldown();
    }

    private void UpdateCooldown()
    {
        if (currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    private void Execute()
    {
        if (hasTarget
            && movement.agent.isStopped
            && currentCooldown <= 0f)
        {
            status.currentState = StateEnum.ATTACKING;
            transform.LookAt(hit.transform);
            DealDamage();
            ResetCooldown();
        }
    }

    private void ResetCooldown()
    {
        currentCooldown = maxCooldown;
    }

    private void DealDamage()
    {
        Status targetStatus = hit.transform.gameObject.GetComponent<Status>();
        float damage = status.currentAttack * (100 / (100 + targetStatus.currentDefense));

        targetStatus.AddHealth(-damage);
    }

    private void Prepare()
    {
        currentDistanceFromEnemy = hasTarget ? Vector3.Distance(hit.transform.position, transform.position) : maxDistanceFromEnemy;
        movement.agent.isStopped = currentDistanceFromEnemy < maxDistanceFromEnemy;
    }

    private void CheckTarget()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasTarget = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)
                && TagEnum.ENEMY.Equals(hit.transform.gameObject.tag);
        }

        if (hasTarget && hit.transform == null)
        {
            hasTarget = false;
            movement.agent.destination = transform.position;
            status.currentState = StateEnum.IDLE;
        }
    }
}

