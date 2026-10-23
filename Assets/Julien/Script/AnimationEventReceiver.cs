using Julien.Script;
using UnityEngine;

public class AnimationEventReceiver : MonoBehaviour
{
    public void PlaySound(AudioClip clip)
    {
        SoundManager.Instance.PlaySound(gameObject, SoundManager.Instance.StepsPlayer, 0.02f, new Vector2(0.8f, 1.2f));
    }
}
