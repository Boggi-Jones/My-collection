using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    Swipe,
    Hit,
    Grunt,
    Bone,
    Tumble,
    TumbleAir,
    Step,
    Eat,
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip[] swipeSounds;
    public AudioClip[] hitSounds;
    public AudioClip[] gruntSounds;
    public AudioClip[] boneSounds;
    public AudioClip[] tumbleSounds;
    public AudioClip[] tumbleAirSounds;
    public AudioClip[] stepSounds;
    public AudioClip[] eatSounds;

    public AudioClip deathSound;
    public AudioClip guardSound;
    public AudioClip jumpSound;
    public AudioClip jumpSound2;
    public AudioClip slideSound;
    public AudioClip completeSound;
    public AudioClip powerUpSound;
    public AudioClip powerDownSound;

    public AudioSource swipePlayer;
    public AudioSource hitPlayer;
    public AudioSource gruntPlayer;
    public AudioSource bonePlayer;
    public AudioSource tumblePlayer;
    public AudioSource stepPlayer;
    public AudioSource eatPlayer;
    public AudioSource powerPlayer;
    public AudioSource checkpointPlayer;

    public AudioSource soloPlayer;
    private float soloVolume;

    public AudioSource BGPlayer;
    private float BGVolume;

    public AudioSource agroPlayer;

    public AudioSource heartPlayer;

    public bool fading = false;

    private void Awake()
    {
        instance = this;
        soloVolume = soloPlayer.volume;
    }

    public void PlayDeath()
    {
        soloPlayer.clip = deathSound;
        soloPlayer.volume = soloVolume;
        soloPlayer.Play();
    }

    public void PlayGuard()
    {
        StopSolo();
        soloPlayer.clip = guardSound;
        soloPlayer.volume = soloVolume;
        soloPlayer.Play();
    }

    public void PlayJump(bool grounded)
    {
        if (grounded)
        {
            soloPlayer.clip = jumpSound;
            soloPlayer.volume = soloVolume/2f;
        }
        else
        {
            soloPlayer.clip = jumpSound2;
            soloPlayer.volume = soloVolume;
        }
        soloPlayer.Play();
    }

    public void PlaySlide()
    {
        soloPlayer.clip = slideSound;
        soloPlayer.volume = soloVolume;
        soloPlayer.Play();
    }

    public void PlayPower(bool up)
    {
        powerPlayer.clip = up ? powerUpSound : powerDownSound;
        powerPlayer.Play();
    }

    public void PlayComplete()
    {
        soloPlayer.clip = completeSound;
        soloPlayer.volume = soloVolume;
        soloPlayer.Play();

        StartCoroutine(FadeOutBGM());
        HeartVolume(0);
    }

    public void StopSolo()
    {
        soloPlayer.Stop();
    }

    public bool PlayingSlide()
    {
        return soloPlayer.isPlaying && soloPlayer.clip == slideSound;
    }

    public void HeartVolume(float volume)
    {
        if (!heartPlayer.isPlaying)
            heartPlayer.Play();
        heartPlayer.volume = volume;
    }

    public void PlayCheckpoint()
    {
        checkpointPlayer.Play();
    }

    public IEnumerator FadeOutBGM()
    {
        float time = 0;
        float startValue = BGPlayer.volume;

        while (time < 1)
        {
            BGPlayer.volume = Mathf.Lerp(startValue, 0f, time);
            time += 0.005f;
            yield return null;
        }
        BGPlayer.volume = 0f;
        BGPlayer.Stop();
    }

    public IEnumerator FadeAgro(float endValue, float duration)
    {
        fading = true;
        float time = 0;
        float startValue = agroPlayer.volume;

        while (time < duration)
        {
            agroPlayer.volume = Mathf.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        fading = false;
        agroPlayer.volume = endValue;
    }

    public void PlaySound(SoundType type)
    {
        AudioClip[] clips;
        AudioSource player;
        switch (type)
        {
            case SoundType.Swipe:
                clips = swipeSounds;
                player = swipePlayer;
                break;
            case SoundType.Hit:
                clips = hitSounds;
                player = hitPlayer;
                break;
            case SoundType.Grunt:
                clips = gruntSounds;
                player = gruntPlayer;
                break;
            case SoundType.Bone:
                clips = boneSounds;
                player = bonePlayer;
                break;
            case SoundType.Tumble:
                clips = tumbleSounds;
                player = tumblePlayer;
                break;
            case SoundType.TumbleAir:
                clips = tumbleAirSounds;
                player = tumblePlayer;
                break;
            case SoundType.Step:
                clips = stepSounds;
                player = stepPlayer;
                break;
            case SoundType.Eat:
                clips = eatSounds;
                player = eatPlayer;
                break;
            default:
                clips = swipeSounds;
                player = swipePlayer;
                break;
        }
        player.clip = clips[(int)UnityEngine.Random.Range(0, clips.Length)];
        player.Play();
    }

    public float PlayPos(SoundType soundType)
    {
        AudioSource player = swipePlayer;
        switch (soundType)
        {
            case SoundType.Swipe:
                player = swipePlayer;
                break;
            case SoundType.Hit:
                player = hitPlayer;
                break;
            case SoundType.Grunt:
                player = gruntPlayer;
                break;
            case SoundType.Bone:
                player = bonePlayer;
                break;
            case SoundType.Tumble:
                player = tumblePlayer;
                break;
            case SoundType.Step:
                player = stepPlayer;
                break;
        }

        return player.time > 0 ? player.time : Mathf.Infinity;
    }


}
