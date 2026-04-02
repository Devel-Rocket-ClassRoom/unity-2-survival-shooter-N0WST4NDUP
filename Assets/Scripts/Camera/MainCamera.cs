using UnityEngine;

public class MainCamera : MonoBehaviour, IFollowable
{
    public Vector2 Distance;

    private void Start()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void Follow(Vector3 position)
    {
        var newPos = position;
        newPos.z += Distance.x;
        newPos.y += Distance.y;

        transform.localPosition = newPos;
        transform.LookAt(position);
    }
}