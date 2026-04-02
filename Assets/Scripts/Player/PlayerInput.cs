using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public readonly string VerticalAxis = "Vertical";
    public readonly string HorizontalAxis = "Horizontal";
    public readonly string FireAxis = "Fire1";


    public float Vertical { get; private set; }
    public float Horizontal { get; private set; }
    public float MoveNorm => Mathf.Max(Mathf.Abs(Vertical), Mathf.Abs(Horizontal));
    public bool Fire { get; private set; }
    public Vector3 Aim
    {
        get
        {
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new(Vector3.up, Vector3.zero);

            if (plane.Raycast(cameraRay, out float rayDistance))
            {
                return cameraRay.GetPoint(rayDistance);
            }

            return Vector3.zero;
        }
    }

    private void Update()
    {
        Vertical = Input.GetAxis(VerticalAxis);
        Horizontal = Input.GetAxis(HorizontalAxis);
        Fire = Input.GetButton(FireAxis);
        // Debug.Log($"Vertical: {Vertical}, Horizontal: {Horizontal}");
    }

}
