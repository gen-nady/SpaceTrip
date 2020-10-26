using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Очки")]
    public int Hs, Coins, ShipNum;
    [Header("Визуал")]
    public Image[] Ships;
    public Image Selector;
    public Color[] SelectorColors;
    public Color[] ShipColors;
    public bool[] ShipUnlock;
    [Header("Цены")]
    public GameObject PriceInCoins, PriceInDollars;
    [Header("Кнопка начала игры")]
    public string[] PlayButtonText;
    public Text PlayBtnT;
    public Color[] PlayBtnColors;
    [Header("Очки(тектовые поля)")]
    public Text Coins_T, Hs_T;
    [Header("Музыка")]
    public Color[] SoundColors;
    public Text SoundBtn;
    public bool SoundEnable;
    void Start()
    {
        PlayerPrefs.SetInt("Play1", 1);
        Time.timeScale = 1;
        //проверяем был ли звук выключен или включен для смены кнопки
        if (AudioListener.volume == 0)
        {
            SoundBtn.color = SoundColors[1];
            SoundEnable = false;
        }
        else
        {
            SoundBtn.color = SoundColors[0];
            SoundEnable = true;
        }
        //заполняем результаты прошлых игр
        Hs = PlayerPrefs.GetInt("HS", 0);
        Coins = PlayerPrefs.GetInt("coins", 0);
        Hs_T.text = "HIGHSCORE: " + Hs.ToString();
        Coins_T.text = " " + Coins.ToString();
        //проверяем купленный корабли
        if (PlayerPrefs.GetInt("ship2") == 1)
        {
            ShipUnlock[1] = true;
        }
        else
        {
            ShipUnlock[1] = false;
        }
        if (PlayerPrefs.GetInt("ship3") == 1)
        {
            ShipUnlock[2] = true;
        }
        else
        {
            ShipUnlock[2] = false;
        }
        ChangeShip(1);
    }
    //работа со звуком, включение и выключение
    public void Sound()
    {
        if (SoundEnable == true)
        {
            AudioListener.volume = 0;
            SoundBtn.color = SoundColors[1];
            SoundEnable = false;
            return;
        }
        if (SoundEnable == false)
        {
            AudioListener.volume = 1;
            SoundBtn.color = SoundColors[0];
            SoundEnable = true;
            return;
        }
    }
    //выбор корабля
    public void ChangeShip(int num)
    {
        switch (num)
        {
            case 1:
                Selector.transform.position = Ships[0].transform.position; //передвижение заднего плана
                Ships[0].color = ShipColors[0];
                Ships[1].color = ShipColors[1];   //затемнение не выбранных кораблей
                Ships[2].color = ShipColors[1];
                ShipNum = 0;
                PlayBtnT.text = PlayButtonText[0];  //изменение надписи кнопки начала игры
                PlayBtnT.color = PlayBtnColors[0];
                PriceInCoins.SetActive(false);
                PriceInDollars.SetActive(false);
                Selector.GetComponent<Image>().color = SelectorColors[0];
                break;
            case 2:
                Selector.transform.position = Ships[1].transform.position;
                Ships[0].color = ShipColors[1];
                Ships[1].color = ShipColors[0];
                Ships[2].color = ShipColors[1];
                ShipNum = 1;
                PriceInDollars.SetActive(false);
                //проверка на то, был ли кулпен товар
                if (ShipUnlock[1] == false)
                {
                    Selector.GetComponent<Image>().color = SelectorColors[1];
                    PriceInCoins.SetActive(true);
                    PlayBtnT.text = PlayButtonText[1];
                    PlayBtnT.color = PlayBtnColors[1];
                }
                else
                {
                    Selector.GetComponent<Image>().color = SelectorColors[0];
                    PriceInCoins.SetActive(false);
                    PlayBtnT.text = PlayButtonText[0];
                    PlayBtnT.color = PlayBtnColors[0];
                }
                break;
            case 3:
                Selector.transform.position = Ships[2].transform.position;
                Ships[0].color = ShipColors[1];
                Ships[1].color = ShipColors[1];
                Ships[2].color = ShipColors[0];
                ShipNum = 2;
                PriceInCoins.SetActive(false);
                if (ShipUnlock[2] == false)
                {
                    Selector.GetComponent<Image>().color = SelectorColors[1];
                    PriceInDollars.SetActive(true);
                    PlayBtnT.text = PlayButtonText[1];
                    PlayBtnT.color = PlayBtnColors[1];
                }
                else
                {
                    Selector.GetComponent<Image>().color = SelectorColors[0];
                    PriceInDollars.SetActive(false);
                    PlayBtnT.text = PlayButtonText[0];
                    PlayBtnT.color = PlayBtnColors[0];
                }
                break;
        }
    }
    [System.Obsolete]
    //начало игры или происходит покупка корабля
    public void PlayBtn()
    {
        if (ShipNum == 0)
        {
            LoadLvlInfo();
        }
        if (ShipNum == 1)
        {
            if (ShipUnlock[1] == false)
            {
                if (Coins >= 300)
                {
                    Coins -= 300;
                    ShipUnlock[1] = true;
                    PlayerPrefs.SetInt("ship2", 1);
                    PlayerPrefs.SetInt("coins", Coins);
                    Coins_T.text = Coins.ToString();
                    ChangeShip(2);
                }
            }
            else
            {
                LoadLvlInfo();
            }
        }
        if (ShipNum == 2)
        {
            if (ShipUnlock[2] == false)
            {
                if (Coins >= 1000)
                {
                    Coins -= 1000;
                    ShipUnlock[2] = true;
                    PlayerPrefs.SetInt("ship3", 1);
                    PlayerPrefs.SetInt("coins", Coins);
                    Coins_T.text = Coins.ToString();
                    ChangeShip(3);
                }
            }
            else
            {
                LoadLvlInfo();
            }
        }
        PlayerPrefs.SetInt("ship", ShipNum);
    }
    [System.Obsolete]
    void LoadLvlInfo()
    {
        PlayerPrefs.SetInt("Play1", 0);
        PlayerPrefs.SetInt("Play2", 0);
        PlayerPrefs.SetInt("Play3", 1);
        Application.LoadLevel("Play");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
