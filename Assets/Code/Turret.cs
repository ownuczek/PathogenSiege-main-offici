using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float bps = 1f; // Bullets per second
    [SerializeField] private float maxTargetDistance = 10f; // Maksymalny zasiêg na którym wie¿a mo¿e znaleŸæ cel

    private Transform target;
    private float timeUntilFire;

    private void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            target = null; // Jeœli cel jest poza zasiêgiem, ustaw go na null
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / bps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }

    private void FindTarget()
    {
        // U¿ywamy CircleCastAll, aby znaleŸæ wszystkie obiekty w zasiêgu
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);

        if (hits.Length > 0)
        {
            Transform closestTarget = null;
            float closestDistance = float.MaxValue;

            // Sortujemy wrogów po odleg³oœci
            foreach (var hit in hits)
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance && distance <= maxTargetDistance)
                {
                    closestTarget = hit.transform;
                    closestDistance = distance;
                }
            }

            // Jeœli znaleziono cel, przypisujemy go
            if (closestTarget != null)
            {
                target = closestTarget;
            }
        }
    }

    private bool CheckTargetIsInRange()
    {
        // U¿ywamy kwadratu odleg³oœci do porównania
        return (target.position - transform.position).sqrMagnitude < targetingRange * targetingRange;
    }

    private void RotateTowardsTarget()
    {
        // Obliczamy k¹t miêdzy wie¿¹ a celem
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
