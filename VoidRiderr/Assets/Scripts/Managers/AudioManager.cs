using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayAudio(AudioSource audio)
    {
        audio.Play();
    }

    public void InstanceParticles(Transform spawnPos, GameObject particle)
    {
        Instantiate(particle, spawnPos);
    }
}
