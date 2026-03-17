using UnityEngine;

public class GameCalculator : MonoBehaviour
{
    public int Points { get; private set; }
    public int Multiplier { get; private set; } = 10;
    public int Total { get; private set; }

    [SerializeField] private SpiritCardData[] spiritCards;

    public System.Action<int, int, int> OnEquationChanged;
    public System.Action<SpiritCardData> OnSpiritActivated;

    public void ProcessDiceResult(int diceValue)
    {
        Points = diceValue;
        Multiplier = 10;

        ApplySpiritCards(diceValue);

        CalculateTotal();
    }

    void ApplySpiritCards(int diceValue)
    {
        foreach (var card in spiritCards)
        {
            if (card.triggerDiceValue != diceValue)
                continue;

            ActivateCard(card);
        }
    }

    void ActivateCard(SpiritCardData card)
    {
        switch (card.cardType)
        {
            case SpiritCardType.MultiplierOverride:
                Multiplier = card.value;
                break;

            case SpiritCardType.AddPoints:
                Points += card.value;
                break;
        }

        OnSpiritActivated?.Invoke(card);
    }

    void CalculateTotal()
    { 
        Total = Points * Multiplier;

        OnEquationChanged?.Invoke(Points, Multiplier, Total);
    }
}
