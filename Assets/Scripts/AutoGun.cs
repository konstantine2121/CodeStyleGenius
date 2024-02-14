using System.Collections;
using UnityEngine;

public class AutoGun : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Rigidbody _bulletPrefab;
    [SerializeField] private float _reloadDelay;
    [SerializeField] private Transform _target;

    public Transform Target 
    { 
        get => _target; 
        set => _target = value; 
    }

    private void Start()
    {
        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        var shooting = enabled;
        var delay = new WaitForSeconds(_reloadDelay);

        while (shooting)
        {
            Vector3 directionToTarget = GetDirectionToTarget();
            Rigidbody bullet = CreateBullet(directionToTarget);
            SetBulletDirection(directionToTarget, bullet);

            yield return delay;
        }
    }

    private Vector3 GetDirectionToTarget()
    {
        return Target != null ?
            (Target.position - transform.position).normalized : 
            Vector3.zero;
    }

    private Rigidbody CreateBullet(Vector3 directionToTarget)
    {
        return Instantiate(_bulletPrefab, transform.position + directionToTarget, Quaternion.identity);
    }

    private void SetBulletDirection(Vector3 directionToTarget, Rigidbody bulletRigidbody)
    {
        bulletRigidbody.transform.up = directionToTarget;
        bulletRigidbody.velocity = directionToTarget * _bulletSpeed;
    }
}