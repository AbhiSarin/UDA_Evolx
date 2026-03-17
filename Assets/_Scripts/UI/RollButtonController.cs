using UnityEngine;
using UnityEngine.UI;

public class RollButtonController : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] DiceRoller diceRoller;


    [System.Obsolete]
    void Start()
    {
        button.onClick.AddListener(Roll);
    }

    [System.Obsolete]
    void Roll()
    {
        button.interactable = false;

        diceRoller.RollDice();

        Invoke(nameof(EnableButton), 2f);
    }

    void EnableButton()
    {
        button.interactable = true;
    }
}
