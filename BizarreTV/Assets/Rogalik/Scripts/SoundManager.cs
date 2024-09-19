using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClip[] sounds;
    [SerializeField] private AudioSource audioSource;

    private bool isSoundPlaying = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(int index)
    {
        if (index >= 0 && index < sounds.Length)
        {
            StartCoroutine(PlaySoundRoutine(sounds[index]));
        }

        else
        {
            Debug.LogWarning("Ќеверный индекс или звук уже воспроизводитс€");
        }
    }

    private IEnumerator PlaySoundRoutine(AudioClip clip)
    {
        isSoundPlaying = true;
        audioSource.PlayOneShot(clip);
        float waitTime = clip.length;
        yield return new WaitForSecondsRealtime(waitTime);

    }
}