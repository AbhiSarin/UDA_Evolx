using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class RollHistoryManager : MonoBehaviour
{
    [SerializeField] private Transform historyContainer;
    [SerializeField] private GameObject historyEntryPrefab;

    private Queue<GameObject> historyEntries = new Queue<GameObject>();

    private const int MAX_HISTORY = 5;

    public void AddRoll(int value)
    {
        GameObject entry = Instantiate(historyEntryPrefab, historyContainer);

        TextMeshProUGUI text = entry.GetComponent<TextMeshProUGUI>();
        text.text = "Roll: " + value;

        entry.transform.SetAsFirstSibling();

        historyEntries.Enqueue(entry);

        entry.transform.localScale = Vector3.zero;

        entry.transform
            .DOScale(1f, 0.3f)
            .SetEase(Ease.OutBack);

        if (historyEntries.Count > MAX_HISTORY)
        {
            GameObject oldest = historyEntries.Dequeue();
            Destroy(oldest);
        }
    }
}