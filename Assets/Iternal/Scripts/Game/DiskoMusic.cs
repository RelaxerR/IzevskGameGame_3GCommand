using UnityEngine;
using System;
using System.Collections;

public class DiskoMusic : MonoBehaviour
{
    private Light _targetLight;
    [SerializeField] private float delay;
    [SerializeField] private SpriteRenderer _backPersons1;
    [SerializeField] private SpriteRenderer _backPersons2;

    private void Start () {
        _targetLight = GetComponent<Light> ();
        GameController.Instance.LevelEndedEvent += GameStoped;
        GameController.Instance.LevelStartedEvent += GameStarted;
    }
    private void OnDestroy () {
        GameController.Instance.LevelEndedEvent -= GameStoped;
        GameController.Instance.LevelStartedEvent -= GameStarted;
    }

    private void GameStarted () {
        StartCoroutine (changeLight ());
    }
    private void GameStoped (bool isWin) {
        StopAllCoroutines ();
    }

    private IEnumerator changeLight () {
        while (true)
        {
            yield return new WaitForSeconds (delay);

            var color = new Color32 (
                Convert.ToByte (UnityEngine.Random.Range (0, 255)),
                Convert.ToByte (UnityEngine.Random.Range (0, 255)),
                Convert.ToByte (UnityEngine.Random.Range (0, 255)),
                Convert.ToByte (UnityEngine.Random.Range (0, 255))
                );
            var intensivity = UnityEngine.Random.Range (0, 5);
            var range = UnityEngine.Random.Range (5, 30);

            _targetLight.intensity = intensivity;
            _targetLight.range = range;
            _targetLight.color = color;

            _backPersons1.flipX = !_backPersons1.flipX;
            _backPersons2.flipX = !_backPersons1.flipX;
        }
    }
}