using UnityEngine;

public class DiceFace : MonoBehaviour
{
    public int value;

    public Vector3 localNormal = Vector3.up;

    public Vector3 GetWorldNormal()
    {
        return transform.TransformDirection(localNormal);
    }
}
