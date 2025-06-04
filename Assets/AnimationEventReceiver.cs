using Julien.Script;
using UnityEngine;

public class AnimationEventReceiver : MonoBehaviour
{
    public void PlaySound(AudioClip clip)
    {
        SoundManager.Instance.PlaySound(gameObject, SoundManager.Instance.StepsPlayer);
    }
}
