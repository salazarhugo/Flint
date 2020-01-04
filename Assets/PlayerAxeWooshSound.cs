using UnityEngine;

public class PlayerAxeWooshSound : MonoBehaviour
{
    #region Private Fields

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] woosh_Sounds;

    #endregion

    #region Public Methods
    void PlayWooshSound()
    {
        audioSource.clip = woosh_Sounds[Random.Range(0, woosh_Sounds.Length)];
        audioSource.Play();
    }

    #endregion
}