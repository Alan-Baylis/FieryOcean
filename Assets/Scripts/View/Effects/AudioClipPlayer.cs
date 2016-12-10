using UnityEngine;

public class AudioClipPlayer : MonoBehaviour {

    public AudioSource audioSource;

    public float randomizePitch;

    void OnEnable() {
        //TODO
        //audioSource.pitch = GameRandom.view.RandomFloat(1 - randomizePitch, 1 + randomizePitch);
        //audioSource.Play();
    }
}
