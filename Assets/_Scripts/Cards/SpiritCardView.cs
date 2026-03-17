using UnityEngine;
using DG.Tweening;

public class SpiritCardView : MonoBehaviour
{
    [SerializeField] private SpiritCardData cardData;

    [SerializeField] CanvasGroup glow;
    [SerializeField] ParticleSystem activationParticles;
    [SerializeField] AudioSource canv;

    public void Activate()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOScale(1.25f, 0.2f).SetEase(Ease.OutBack));

        seq.Append(transform.DOScale(1f, 0.2f));

        glow.DOFade(1, 0.2f).SetLoops(2, LoopType.Yoyo);

        activationParticles.Play();

        canv.resource = cardData.clip;
        canv.Play();
    }

    public bool MatchesCard(SpiritCardData data)
    {
        return data == cardData;
    }
}