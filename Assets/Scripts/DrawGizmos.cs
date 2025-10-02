using UnityEngine;

public class DrawGizmos : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public Color gizmoColor = Color.red;
    public float sphereRadius = 0.2f;

    void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = gizmoColor;

            // Vẽ đường thẳng giữa hai điểm
            Gizmos.DrawLine(pointA.position, pointB.position);

            // Vẽ hình cầu tại mỗi điểm
            Gizmos.DrawSphere(pointA.position, sphereRadius);
            Gizmos.DrawSphere(pointB.position, sphereRadius);
        }
    }
}