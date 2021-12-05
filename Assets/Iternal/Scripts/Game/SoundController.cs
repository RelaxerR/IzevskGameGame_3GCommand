using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioClip[] _musicArray = new AudioClip[6];
    [SerializeField] private GameObject _backMusicPlayer;
    [SerializeField] private GameObject _mainMusicPlayer;

    private AudioSource backMusicAudioSource;
    private AudioSource MainMusicAudioSource;
    private string _musicsKey = "Musics";

    private void Start () {
        UnlockMusic ();
        MainMusicAudioSource = _mainMusicPlayer.GetComponent<AudioSource> ();
        backMusicAudioSource = _backMusicPlayer.GetComponent<AudioSource> ();

        GameController.Instance.LevelStartedEvent += PlayMusic;
        GameController.Instance.LevelEndedEvent += PlayBackMusic;
    }
    private void OnDestroy () {
        GameController.Instance.LevelStartedEvent -= PlayMusic;
        GameController.Instance.LevelEndedEvent -= PlayBackMusic;
    }

    private void PlayBackMusic (bool win) {
        MainMusicAudioSource.Stop ();
        backMusicAudioSource.Play ();
    }

    private void UnlockMusic () {
        if (!PlayerPrefs.HasKey (_musicsKey)) PlayerPrefs.SetString (_musicsKey, "T_F_F_F_F_F");

        var unlockedSounds = PlayerPrefs.GetString (_musicsKey).Split ('_');
        for (int i = 0; i < unlockedSounds.Length; i++)
        {
            if (unlockedSounds[i] == "F") _musicArray[i] = null;
        }
    }
    public void PlayMusic () {
        AudioClip playingClip = null;

        while (playingClip == null)
        {
            var id = Random.Range (0, _musicArray.Length);
            playingClip = _musicArray[id];
        }

        MainMusicAudioSource.clip = playingClip;

        backMusicAudioSource.Stop ();
        MainMusicAudioSource.Play ();
    }
}