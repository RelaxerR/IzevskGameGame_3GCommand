using UnityEngine;
using UnityEngine.UI;

public class PersonController : MonoBehaviour
{
    private float _money;
    private float _energy;
    private bool _isSleeping;
    private bool _isBringing;
    private string _securityNameKey = "Security";

    private Vector2 MovePos;
    private SpriteRenderer _mySpriteRenderer;
    private AudioSource _myAudioSource;

    [SerializeField] private float _energyLost;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Slider _enegryBar;
    [SerializeField] private Slider _moneyBar;
    [SerializeField] private Sprite _sleepingSprite;
    [SerializeField] private GameObject _standartBody;
    [SerializeField] private GameObject _sleepingBody;

    private void Start () {
        _money = 1;
        _energy = 0.5f;
        _mySpriteRenderer = GetComponentInChildren<SpriteRenderer> ();
        GameController.Instance.LevelEndedEvent += LevelEnded;
        _myAudioSource = GetComponent<AudioSource> ();
    }
    private void Update () {
        Move ();
    }
    private void FixedUpdate () {
        if (_energy <= 0 && !_isSleeping) GetSleep ();
        _energy -= _energyLost; 

        UpdateBars ();
    }
    private void OnDestroy () {
        GameController.Instance.LevelEndedEvent -= LevelEnded;
    }
    private void OnMouseDown () {
        if (_isSleeping || _isBringing) return;

        _myAudioSource.Play ();

        float money = 0;
        float energy = 0;
        LevelController.Instance.GetDrink (out money, out energy);

        Debug.Log ($"{money} | {energy}");
        if (money > _money)
        {
            _energy -= energy;
            return;
        }

        _money -= money;
        _energy += energy;

        if (_energy > 1 || _money == 0) GetOut ();
    }
    
    private void UpdateBars () {
        _enegryBar.value = _energy;
        _moneyBar.value = _money;
    }

    private void Move () {
        if (_isSleeping || _isBringing) return;

        if (MovePos == new Vector2(transform.position.x, transform.position.y))
            SetMovePos ();

        if (MovePos.x >= transform.position.x) _mySpriteRenderer.flipX = false;
        else _mySpriteRenderer.flipX = true;

        transform.position = Vector2.MoveTowards (transform.position, MovePos, _moveSpeed * Time.deltaTime);
    }
    private void OnTriggerStay2D (Collider2D collision) {
        if (collision.gameObject.name != _securityNameKey) SetMovePos ();
    }
    private void SetMovePos () {
        MovePos = new Vector2 (Random.Range (-3, 3), Random.Range (-3, 3));
    }

    private void GetSleep () {
        _isSleeping = true;
        GetComponentInChildren<SpriteRenderer> ().sprite = _sleepingSprite;
        LevelController.Instance.PlayerDead ();
    }
    private void GetOut () {
        LevelController.Instance.BringOutPerson (gameObject);
    }
    public void Bring () {
        _isBringing = true;
    }
    private void LevelEnded (bool win) {
        if (win)
        {
            LevelController.Instance.AddSummaryMoney (Mathf.RoundToInt(_money * 100));
        }
    }
}