using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class DiceRoller : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rb;

    [Header("Roll Settings")]
    [SerializeField] private float rollForce = 6f;
    [SerializeField] private float rollTorque = 12f;

    [Header("Face Transforms (Assign in Inspector)")]
    [SerializeField] private Transform[] faceTransforms; // Size = 6

    [Header("Debug")]
    [SerializeField] private bool useDebugRoll = false;
    [SerializeField] private int debugValue = 3; // 3 or 6

    public Action<int> OnRollComplete;

    private bool rolling;

    // ----------------------------------------

    [Obsolete]
    public void RollDice()
    {
        if (rolling) return;
        StartCoroutine(RollRoutine());
    }

    [Obsolete]
    IEnumerator RollRoutine()
    {
        rolling = true;

        // Reset physics
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Apply random force & torque
        rb.AddForce(UnityEngine.Random.onUnitSphere * rollForce, ForceMode.Impulse);
        rb.AddTorque(UnityEngine.Random.onUnitSphere * rollTorque, ForceMode.Impulse);

        // Wait until dice settles
        yield return new WaitUntil(() =>
            rb.velocity.sqrMagnitude < 0.05f &&
            rb.angularVelocity.sqrMagnitude < 0.05f);

        yield return new WaitForSeconds(0.2f);

        int result = useDebugRoll ? debugValue : GetTopFace();

        // Snap visually using DOTween
        yield return SnapToFace(result);

        Debug.Log("Dice Result: " + result);

        rolling = false;
        OnRollComplete?.Invoke(result);
    }

    // ----------------------------------------
    // Detect which face is pointing up
    // ----------------------------------------
    int GetTopFace()
    {
        int bestIndex = 0;
        float maxDot = -Mathf.Infinity;

        for (int i = 0; i < faceTransforms.Length; i++)
        {
            float dot = Vector3.Dot(faceTransforms[i].up, Vector3.up);

            if (dot > maxDot)
            {
                maxDot = dot;
                bestIndex = i;
            }
        }

        return bestIndex + 1; // Convert to 1–6
    }

    // ----------------------------------------
    // Smooth snap using DOTween
    // ----------------------------------------
    IEnumerator SnapToFace(int value)
    {
        Transform targetFace = faceTransforms[value - 1];

        Quaternion targetRotation = Quaternion.FromToRotation(
            targetFace.up,
            Vector3.up
        ) * transform.rotation;

        bool completed = false;

        transform
            .DORotateQuaternion(targetRotation, 0.25f)
            .SetEase(Ease.OutBack)
            .OnComplete(() => completed = true);

        // Optional polish (tiny bounce)
        transform.DOPunchScale(Vector3.one * 0.15f, 0.2f, 6, 0.5f);

        yield return new WaitUntil(() => completed);
    }

    // ----------------------------------------
    // Debug Control (UI Buttons Friendly)
    // ----------------------------------------
    public void SetDebugRoll(bool enabled, int value)
    {
        useDebugRoll = enabled;
        debugValue = value;
    }
}