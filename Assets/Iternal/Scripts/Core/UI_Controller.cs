using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] private GameObject MenuPanel;
    [SerializeField] private GameObject GamePanel;
    [SerializeField] private GameObject GameWinPanel;
    [SerializeField] private GameObject GameLoosePanel;
    [SerializeField] private GameObject ShopPanel;
    [SerializeField] private Toggle BeerToggle;
    [SerializeField] private Toggle VodkaToggle;
    [SerializeField] private Text MoneyText;
    [SerializeField] private Text LevelText;
    [SerializeField] private Slider LevelProgressSlider;

    private void Start () {
        DisactivePanels ();
        MenuPanel.SetActive (true);

        GameController.Instance.LevelStartedEvent += LevelStarted;
        GameController.Instance.LevelEndedEvent += LevelEnded;
        LevelController.Instance.LevelProgressAddedEvent += AddProgress;
    }
    private void OnDestroy () {
        GameController.Instance.LevelStartedEvent -= LevelStarted;
        GameController.Instance.LevelEndedEvent -= LevelEnded;
        LevelController.Instance.LevelProgressAddedEvent -= AddProgress;
    }

    private void LevelStarted () {
        MenuPanel.SetActive (false);
        GamePanel.SetActive (true);

        MoneyText.text = GameVariables.Instance._moneyCount.ToString () + "$";
        LevelText.text = "Уровень: " + GameVariables.Instance._levelNumber.ToString ();
    }
    private void LevelEnded(bool win) {
        GamePanel.SetActive (false);

        if (win)
            GameWinPanel.SetActive (true);
        else
            GameLoosePanel.SetActive (true);
    }

    private void DisactivePanels () {
        MenuPanel.SetActive (true);
        GamePanel.SetActive (false);
        GameWinPanel.SetActive (false);
        GameLoosePanel.SetActive (false);
        ShopPanel.SetActive (false);
    }

    public void DrinkChanged () {
        if (BeerToggle.isOn)
            LevelController.Instance.BeerChosed = true;
        else
            LevelController.Instance.BeerChosed = false;
    }

    public void StartButtonClick () {
        GameController.Instance.GameStart ();
    }
    public void PauseButtonClick () {

    }
    public void ShopButtonClick () {
        MenuPanel.SetActive (false);
        ShopPanel.SetActive (true);
    }
    public void RestartGameButtonClick () {
        GameController.Instance.RestartGame ();
    }
    public void NextLevelButtonClick () {
        GameController.Instance.NextLevel ();
    }
    public void MenuButtonClick () {
        DisactivePanels ();
    }

    private void AddProgress (float val) {
        LevelProgressSlider.value = val;
    }
}