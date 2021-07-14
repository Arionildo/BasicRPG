using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private bool hasTarget;
    private float currentCooldown = 0f;
    private GameObject target;

    public float maxCooldown = 1.5f;
    public Status status;

    private void Start()
    {
        status = GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
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
            && currentCooldown <= 0f)
        {
            status.currentState = StateEnum.ATTACKING;
            transform.LookAt(target.gameObject.transform);
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
        Status targetStatus = target.GetComponent<Status>();
        float damage = status.currentAttack * (100 / (100 + targetStatus.currentDefense));

        targetStatus.AddHealth(-damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TagEnum.PLAYER.Equals(other.gameObject.tag))
        {
            hasTarget = true;
            target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (TagEnum.PLAYER.Equals(other.gameObject.tag))
        {
            hasTarget = false;
            target = null;
        }
    }
}
