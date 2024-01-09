using Unity.VisualScripting;
using UnityEngine;

public class GameObjectCharacterController : MonoBehaviour
{
    private CharacterController _characterController;

    public string EntityID { get; set; } = "";

    private float _movespeed = 2f;

    private float _vertical;
    private float _horizontal;
    private Vector3 _moveDirection;

    private Vector3 _positionLast;
    private Quaternion _rotationLast;
    private Vector3 _scaleLast;


    public Vector3 PositionTarget { get; set; }

    public bool IsCurrent { get; set; } = false;
    public bool IsLerp { get; set; } = false;

    private void Start()
    {
        if (IsCurrent)
        {
            _characterController = transform.AddComponent<CharacterController>();
        }
    }

    private void FixedUpdate()
    {
        if (IsCurrent) 
        {
            _vertical = Input.GetAxis("Vertical");
            _horizontal = Input.GetAxis("Horizontal");
            _moveDirection = new Vector3(_horizontal, 0f, _vertical);

            _moveDirection = transform.TransformDirection(_moveDirection) * _movespeed;

            _characterController.Move(_moveDirection * _movespeed * Time.deltaTime);
        }
    }

    private void Update()
    {
        if (!string.IsNullOrEmpty(EntityID) && IsCurrent)
        {
            if (transform.position != _positionLast)
            {
                OperationKeyMoveSystem.Instance.AddOperationMove(transform.position);
                _positionLast = transform.position;
            }
        }
        if (!IsCurrent)
        {
            if (PositionTarget != transform.position)
            {
                Vector3 dir = PositionTarget - transform.position;
                if (IsLerp)
                {
                    transform.position = Vector3.MoveTowards(transform.position, PositionTarget, _movespeed * Time.deltaTime);
                }
                else
                {
                    transform.position = PositionTarget;
                }
            }
        }
    }
}
