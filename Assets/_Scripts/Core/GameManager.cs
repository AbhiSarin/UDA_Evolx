using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] DiceRoller diceRoller;
    [SerializeField] GameCalculator calculator;
    [SerializeField] UIEquationView equationView;
    [SerializeField] SpiritCardView[] cardViews;
    [SerializeField] private RollHistoryManager historyManager;


    void OnEnable()
    {
        diceRoller.OnRollComplete += OnDiceRolled;
        calculator.OnEquationChanged += equationView.UpdateEquation;
        calculator.OnSpiritActivated += ActivateCardView;
    }

    void OnDisable()
    {
        diceRoller.OnRollComplete -= OnDiceRolled;
        calculator.OnEquationChanged -= equationView.UpdateEquation;
        calculator.OnSpiritActivated -= ActivateCardView;
    }

    void Start()
    {
      
    }

    void OnDiceRolled(int result)
    {
        calculator.ProcessDiceResult(result);
        historyManager.AddRoll(result);
    }

    void ActivateCardView(SpiritCardData card)
    {
        foreach (var view in cardViews)
        {
            if (view.MatchesCard(card))
                view.Activate();
        }
    }

    [System.Obsolete]
    public void Roll()
    {
        diceRoller.RollDice();
    }
}
