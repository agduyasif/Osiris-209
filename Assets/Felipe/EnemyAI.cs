using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float detectionRange = 15f;
    public float attackRange = 4f;

    public float investigateSpeed = 3f;
    public float chaseSpeed = 6f;

    private Vector3 targetPosition;
    private bool hasTarget = false;
    private bool isUnderground = true;

    float loseTargetTimer = 0f;
    public float loseTargetDelay = 2f;

    public float patrolRadius = 5f;
    public float patrolSpeed = 2f;

    private Vector3 patrolTarget;
    private bool hasPatrolTarget = false;

    float reacquireCooldown = 0f;
    public float reacquireDelay = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (VibrationSystem.Instance != null)
        {
            VibrationSystem.Instance.OnVibration += OnVibrationHeard;
        }

        Debug.Log("Instance: " + VibrationSystem.Instance);
    }
    private void OnDisable()
    {
        if (VibrationSystem.Instance != null)
        {
            VibrationSystem.Instance.OnVibration -= OnVibrationHeard;
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (reacquireCooldown > 0f)
        {
            reacquireCooldown -= Time.deltaTime;
        } 

        float vibration = VibrationMeter.Instance.currentVibration;

        if (!hasTarget)
        {
            Patrol();
            return;
        }

        if (vibration < 30f)
        {
            loseTargetTimer += Time.deltaTime;

            if (loseTargetTimer >= loseTargetDelay)
            {
                hasTarget = false;
            }
        }
        else
        {
            loseTargetTimer = 0f;

            if (vibration < 60f)
            {
                Investigate();
            }
            else
            {
                Chase();
            }
        }

    }

    void OnVibrationHeard(Vector3 pos, float radius)
    {
        if (reacquireCooldown > 0f) return;

        Vector3 a = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 b = new Vector3(pos.x, 0, pos.z);

        float dist = Vector3.Distance(a, b);

        if (dist < radius && dist < detectionRange)
        {
            targetPosition = pos;
            hasTarget = true;

            reacquireCooldown = reacquireDelay;
            loseTargetTimer = 0f;
        }
    }

    void Investigate()
    {
        MoveUnderground(investigateSpeed);
    }

    void Chase()
    {
        MoveUnderground(chaseSpeed);

        float dist = Vector3.Distance(transform.position, targetPosition);

        if (dist < attackRange + 1f)
        {
            Attack();
        }
    }

    void MoveUnderground(float speed)
    {
        Vector3 undergroundPos = new Vector3(targetPosition.x, -1f, targetPosition.z);

        transform.position = Vector3.MoveTowards(transform.position, undergroundPos, speed * Time.deltaTime);
    }

    void Attack()
    {
        if (isUnderground)
        {
            Emerge();
        }

        Debug.Log("ATAQUE");

        VibrationMeter.Instance.currentVibration *= 0.5f;
        reacquireCooldown = 1f;

        Invoke(nameof(Hide), 1.5f);
        hasTarget = false;
    }

    void Emerge()
    {
        isUnderground = false;

        transform.position = new Vector3(transform.position.x, 2f, transform.position.z);

        GetComponent<Renderer>().material.color = Color.red;
    }

    void Hide()
    {
        isUnderground = true;

        transform.position = new Vector3(transform.position.x, -1, transform.position.z);

        GetComponent<Renderer>().material.color = Color.black;
    }

    void Patrol()
    {
        if (!hasPatrolTarget)
        {
            Vector2 randomCircle = Random.insideUnitCircle * patrolRadius;

            patrolTarget = new Vector3(transform.position.x + randomCircle.x, transform.position.y, transform.position.z + randomCircle.y);

            hasPatrolTarget = true;
        }

        transform.position = Vector3.MoveTowards(transform.position, patrolTarget, patrolSpeed * Time.deltaTime);

        float dist = Vector3.Distance(transform.position, patrolTarget);

        if (dist < 0.2f)
        {
            hasPatrolTarget = false;
        }
    }
}
