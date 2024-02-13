using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AutoGun : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _reloadDelay;

    public Transform target;

    private void Start()
    {
        StartCoroutine(StartShooting());
    }

    private IEnumerator StartShooting()
    {
        var shooting = enabled;
        var delay = new WaitForSeconds(_reloadDelay);

        while (shooting)
        {
            var directionToTarget = GetDirectionToTarget();
            var bullet = CreateBullet(directionToTarget);
            SetBulletDirectionAndSpeed(directionToTarget, bullet);

            yield return delay;
        }
    }

    private Vector3 GetDirectionToTarget()
    {
        return (target.position - transform.position).normalized;
    }

    private GameObject CreateBullet(Vector3 directionToTarget)
    {
        return Instantiate(_bulletPrefab, transform.position + directionToTarget, Quaternion.identity);
    }

    private void SetBulletDirectionAndSpeed(Vector3 directionToTarget, GameObject bullet)
    {
        var bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.transform.up = directionToTarget;
        bulletRigidbody.velocity = directionToTarget * _bulletSpeed;
    }
}