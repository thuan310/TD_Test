using UnityEngine;

public class Arrow : MonoBehaviour {
    [SerializeField] private float flightDuration = 1.0f;
    [SerializeField] private float maxHeight = 2.0f;

    private Vector3 start;
    private float timeElapsed = 0f;
    private bool launched = false;

    private int damage = 0;
    private Transform targetTransform;
    private Stats targetStat;
    private GameObject attacker;

    public void Launch(GameObject targetObject, int damage, GameObject attacker)
    {

        if (targetObject == null) return;
        this.attacker = attacker;
        start = transform.position;
        targetTransform = targetObject.transform;
        targetStat = targetObject.GetComponent<Stats>();
        this.damage = damage;

        timeElapsed = 0f;
        launched = true;
    }

    private void Update()
    {
        if (!launched || targetTransform == null) return;

        timeElapsed += Time.deltaTime;
        float t = Mathf.Clamp01(timeElapsed / flightDuration);

        // Get latest position of the moving target
        Vector3 currentEnd = targetTransform.position;

        // Parabolic arc
        Vector3 horizontal = Vector3.Lerp(start, currentEnd, t);
        float arcY = Mathf.Sin(Mathf.PI * t) * maxHeight;
        Vector3 arcOffset = Vector3.up * arcY;
        Vector3 currentPos = horizontal + arcOffset;

        transform.position = currentPos;

        // Rotate toward movement direction
        if (t < 1f)
        {
            Vector3 nextT = Vector3.Lerp(start, currentEnd, t + 0.01f);
            float nextArcY = Mathf.Sin(Mathf.PI * (t + 0.01f)) * maxHeight;
            Vector3 nextPos = nextT + Vector3.up * nextArcY;
            Vector3 dir = (nextPos - currentPos).normalized;
            if (dir != Vector3.zero)
                transform.right = dir;
        }

        // Apply damage at end of flight
        if (t >= 1f)
        {
            if (targetStat != null)
            {
                targetStat.TakeDamage(damage, attacker);
            }

            Destroy(gameObject);
        }
    }
}
