using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Gun GunToEquip;
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        // Debug.Log($"Fire: {_playerInput.Fire}");
        if (_playerInput.Fire == true)
        {
            GunToEquip.Fire();
        }
    }
}