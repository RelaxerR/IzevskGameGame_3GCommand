using UnityEngine;
using UnityEngine.UI;

public class PersonController : MonoBehaviour
{
    private float _money;
    private float _energy;

    [SerializeField] private float _energyLost;
    [SerializeField] private Slider _enegryBar;
    [SerializeField] private Slider _moneyBar;

    private void FixedUpdate () {
        _energy -= _energyLost;
        UpdateBars ();
    }
    private void OnMouseDown () {
        float money = 0;
        float energy = 0;
        LevelController.Instance.GetDrink (out money, out energy);

        _money -= money;
        _energy += energy;
    }
    
    private void UpdateBars () {
        _enegryBar.value = _energy;
        _moneyBar.value = _money;
    }
}