using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMoveable
{
    public readonly int HashMove = Animator.StringToHash("Move");

    public float MoveSpeed = 5f;

    private Animator _playerAnimator;
    private PlayerInput _playerInput;
    private Rigidbody _playerRigidbody;

    private IFollowable _camera;

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
        _playerInput = GetComponent<PlayerInput>();
        _playerRigidbody = GetComponent<Rigidbody>();

        _camera = GameObject.FindGameObjectWithTag("MainCamera")
                            .GetComponent<IFollowable>();
    }

    private void FixedUpdate()
    {
        OnMove();

        transform.LookAt(_playerInput.Aim);
        _camera.Follow(_playerRigidbody.position);
    }

    private void Update()
    {
        transform.LookAt(_playerInput.Aim);
        _playerAnimator.SetFloat(HashMove, _playerInput.MoveNorm);
    }

    public void OnMove()
    {
        var moveOffset = Vector3.zero;
        moveOffset.z = _playerInput.Vertical * MoveSpeed * Time.deltaTime;
        moveOffset.x = _playerInput.Horizontal * MoveSpeed * Time.deltaTime;
        _playerRigidbody.MovePosition(_playerRigidbody.position + moveOffset);
    }
}