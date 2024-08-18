using UnityEngine;

[RequireComponent(typeof(PhysicsMover))]
public class Persecutor : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private float _offset = 2f;
    private PhysicsMover _physicsMover;

    private void Awake()
    {
        _physicsMover = GetComponent<PhysicsMover>();
    }

    private void Update()
    {
        Watch();
        Follow();
    }

    private void Watch()
    {
        transform.LookAt(_target);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    private void Follow()
    {
        Vector3 direction = (_target.transform.position - transform.position);

        if (Vector3.Distance(transform.position, _target.position) > _offset)
        {
            _physicsMover.Move(direction);
        }
        else
        {
            _physicsMover.Move(Vector3.zero);
        }
    }


}
