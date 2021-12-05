using UnityEngine;

public class SecurityController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private Vector3 _startPos;
    private GameObject _target;
    private Vector3 _movePoint;
    private bool _lock;

    private void Start () {
        LevelController.Instance.BringStartEvent += StartMoving;
        _startPos = transform.position;
    }
    private void OnDestroy () {
        LevelController.Instance.BringStartEvent -= StartMoving;
    }
    private void Update () {
        if (_target != null) Bring ();
    }
    private void OnTriggerStay2D (Collider2D collision) {
        if (Vector3.Distance(transform.position, _target.transform.position) < 0.1f)
        {
            Take ();
            _target.gameObject.GetComponent<PersonController> ().Bring ();
        }
    }

    private void Bring () {
        if (!_lock) _movePoint = _target.transform.position;

        transform.position = Vector2.MoveTowards (transform.position,
            _movePoint,
            _moveSpeed * Time.deltaTime);
        if (_movePoint == _startPos)
            _target.transform.position = Vector2.MoveTowards (
                _target.transform.position,
                _movePoint,
                _moveSpeed * Time.deltaTime
                );

        if (_target.transform.position.x == _startPos.x && _target.transform.position.y == _startPos.y)
        {
            Destroy (_target);
            _lock = false;
            LevelController.Instance.PlayerDead ();
        }
    }
    private void Take () {
        _movePoint = _startPos;
        _lock = true;
    }

    private void StartMoving (GameObject obj) {
        if (_target != null) return;

        _target = obj;
    }
}