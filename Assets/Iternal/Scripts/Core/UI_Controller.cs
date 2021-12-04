using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] private GameObject MenuPanel;
    [SerializeField] private GameObject GamePanel;
    [SerializeField] private GameObject GameWinPanel;
    [SerializeField] private GameObject GameLoosePanel;
    [SerializeField] private Toggle BeerToggle;
    [SerializeField] private Toggle VodkaToggle;

    private void Awake () {
        GameController.Instance.LevelStartedEvent += LevelStarted;
        GameController.Instance.LevelEndedEvent += LevelEnded;
    }
    private void Start () {
        DisactivePanels ();
    }
    private void OnDestroy () {
        GameController.Instance.LevelStartedEvent -= LevelStarted;
        GameController.Instance.LevelEndedEvent -= LevelEnded;
    }

    private void LevelStarted () {
        MenuPanel.SetActive (false);
        GamePanel.SetActive (true);
    }
    private void LevelEnded(bool win) {
        GamePanel.SetActive (false);

        if (win)
            GameWinPanel.SetActive (true);
        else
            GamePanel.SetActive (true);
    }

    private void DisactivePanels () {
        MenuPanel.SetActive (false);
        GamePanel.SetActive (false);
        GameWinPanel.SetActive (false);
        GamePanel.SetActive (false);
    }

    public void DrinkChanged (bool beer) {
        
    }
}