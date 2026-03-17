using TMPro;
using UnityEngine;
using DG.Tweening;

public class UIEquationView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI multiplierText;
    [SerializeField] private TextMeshProUGUI totalText;

    private int currentPoints;
    private int currentMultiplier;
    private int currentTotal;

    public void UpdateEquation(int points, int multiplier, int total)
    {
        AnimateNumber(pointsText, currentPoints, points, x => currentPoints = x);
        AnimateNumber(multiplierText, currentMultiplier, multiplier, x => currentMultiplier = x);
        AnimateNumber(totalText, currentTotal, total, x => currentTotal = x);

        HighlightTotal();
    }

    void AnimateNumber(TextMeshProUGUI text, int startValue, int targetValue, System.Action<int> setter)
    {
        DOVirtual.Int(startValue, targetValue, 0.5f, x =>
        {
            setter(x);
            text.text = x.ToString();
        })
        .SetEase(Ease.OutCubic);
    }

    void HighlightTotal()
    {
        totalText.transform.DOPunchScale(Vector3.one * 0.3f, 0.35f, 8, 0.6f);

        totalText.DOColor(Color.yellow, 0.15f).SetLoops(2, LoopType.Yoyo);
    }
}