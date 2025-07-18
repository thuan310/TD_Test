using System;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour {
    [Header("Detection Settings")]
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private List<string> targetTags;
    [SerializeField] private LayerMask detectionLayer;

    public GameObject ClosestTarget { get; private set; }

    private void Update()
    {
        FindClosestTarget();
    }

    private void FindClosestTarget()
    {
        GameObject closest = null;
        float minDist = float.MaxValue;
        Vector2 origin = transform.position;

        Collider2D[] hits = Physics2D.OverlapCircleAll(origin, detectionRadius, detectionLayer);

        foreach (var tag in targetTags)
        {
            foreach (var hit in hits)
            {
                if (hit.CompareTag(tag))
                {
                    float dist = Vector2.Distance(origin, hit.transform.position);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        closest = hit.gameObject;
                    }
                }
            }

            if (closest != null)
                break;
        }

        if (closest != ClosestTarget)
        {
            ClosestTarget = closest;
        }
    }

    private bool IsTarget(Collider2D col)
    {
        foreach (var tag in targetTags)
        {
            if (col.CompareTag(tag))
                return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
