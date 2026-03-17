using UnityEngine;

public class DebugRollController : MonoBehaviour
{
    [SerializeField] private DiceRoller diceRoller;

    public void Force3()
    {
        diceRoller.SetDebugRoll(true, 3);
    }

    public void Force6()
    {
        diceRoller.SetDebugRoll(true, 6);
    }

    public void DisableDebug()
    {
        diceRoller.SetDebugRoll(false, 0);
    }
}