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
    [SerializeField] private float maxTargetDistance = 10f; // Maksymalny zasi�g na kt�rym wie�a mo�e znale�� cel

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
            target = null; // Je�li cel jest poza zasi�giem, ustaw go na null
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
        // U�ywamy CircleCastAll, aby znale�� wszystkie obiekty w zasi�gu
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);

        if (hits.Length > 0)
        {
            Transform closestTarget = null;
            float closestDistance = float.MaxValue;

            // Sortujemy wrog�w po odleg�o�ci
            foreach (var hit in hits)
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance && distance <= maxTargetDistance)
                {
                    closestTarget = hit.transform;
                    closestDistance = distance;
                }
            }

            // Je�li znaleziono cel, przypisujemy go
            if (closestTarget != null)
            {
                target = closestTarget;
            }
        }
    }

    private bool CheckTargetIsInRange()
    {
        // U�ywamy kwadratu odleg�o�ci do por�wnania
        return (target.position - transform.position).sqrMagnitude < targetingRange * targetingRange;
    }

    private void RotateTowardsTarget()
    {
        // Obliczamy k�t mi�dzy wie�� a celem
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
