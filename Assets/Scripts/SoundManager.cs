using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameObject sfx;
    public AudioClip[] audioClips;
    //вопспроизводим звуки
    public void PlaySound(int soundNum)
    {
        GameObject s = Instantiate(sfx, Vector2.zero, Quaternion.identity) as GameObject;
        AudioSource aS = s.GetComponent<AudioSource>();
        aS.clip = audioClips[soundNum];
        aS.Play();
        Destroy(s, audioClips[soundNum].length);
    }
}
