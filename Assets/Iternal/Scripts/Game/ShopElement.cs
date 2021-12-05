using UnityEngine;
using UnityEngine.UI;

public class ShopElement : MonoBehaviour
{
    [SerializeField] private Button _priceBtn;
    [SerializeField] private int _musicId;
    [SerializeField] private int _price;

    private string _musicsKey = "Musics";

    void Start()
    {
        CheckMusic ();
    }

    private void CheckMusic () {
        if (!PlayerPrefs.HasKey (_musicsKey)) PlayerPrefs.SetString (_musicsKey, "T_F_F_F_F_F");
        _priceBtn.GetComponentInChildren<Text> ().text = _price.ToString ();
        var data = PlayerPrefs.GetString (_musicsKey).Split ('_');
        if (data[_musicId] == "T") _priceBtn.interactable = false;
    }

    public void BuyButtonClick () {
        if (!GameVariables.Instance.TrySpendMoney (_price)) return;

        var data = PlayerPrefs.GetString (_musicsKey).Split ('_');

        data[_musicId] = "T";
        var txt = "";
        foreach(string item in data)
        {
            txt += $"{item}_";
        }

        PlayerPrefs.SetString (_musicsKey, txt);
        CheckMusic ();
    }
}