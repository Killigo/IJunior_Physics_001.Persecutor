using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 3;

    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_characterController != null)
        {
            Vector3 playerInput = new Vector3(Input.GetAxis(Horizontal), 0, Input.GetAxis(Vertical));

            Vector3 position = playerInput * Time.deltaTime * _speed + Physics.gravity;

            if (_characterController.isGrounded)
            {
                _characterController.Move(position);
            }
            else
            {
                _characterController.Move(_characterController.velocity + Physics.gravity * Time.deltaTime);
            }

            if (playerInput != Vector3.zero)
            {
                transform.LookAt(transform.position + playerInput);
            }
        }
    }
}
