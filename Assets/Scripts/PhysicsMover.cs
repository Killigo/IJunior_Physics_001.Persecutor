using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsMover : MonoBehaviour
{
    [SerializeField] private float _speed = 2;

    private Vector3 _normal;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        Vector3 directionAlongSurface = Project(direction.normalized);
        Vector3 offset = directionAlongSurface.normalized * (_speed * Time.deltaTime);

        if (Time.deltaTime > 0)
        {
            _rigidbody.velocity = offset / Time.deltaTime;
        }
        else
        {
            Debug.LogError("Time.deltaTime is zero. Cannot assign velocity.");
            return;
        }

        Vector3 bottomPosition = new Vector3(transform.position.x, transform.position.y - 0.1f - transform.localScale.y / 2, transform.position.z);
        Collider collider = Physics.OverlapSphere(bottomPosition, 0.5f).FirstOrDefault();

        if (collider == null || collider.gameObject == gameObject)
        {
            _rigidbody.velocity += Physics.gravity;
        }
    }

    private Vector3 Project(Vector3 direction)
    {
        direction = direction - Vector3.Dot(direction, _normal) * _normal;

        return direction;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _normal = collision.contacts[0].normal;
    }

    private void OnDrawGizmos()
    {
        Vector3 bottomPosition = new Vector3(transform.position.x, transform.position.y - transform.localScale.y / 2, transform.position.z);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(bottomPosition, 0.7f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + _normal * 3);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Project(transform.forward) * 3);
    }
}
