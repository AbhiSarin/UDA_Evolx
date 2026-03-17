using UnityEngine;

public enum SpiritCardType
{
    MultiplierOverride,
    AddPoints
}

[CreateAssetMenu(fileName = "SpiritCardData", menuName = "Scriptable Objects/SpiritCardData")]
public class SpiritCardData : ScriptableObject
{
    public string cardName;

    public SpiritCardType cardType;

    public int triggerDiceValue;

    public int value;

    public AudioClip clip;

    [TextArea]
    public string description;
}
