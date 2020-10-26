using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public bool Pause;
    public bool SounEnable;
    public GameObject PausePanel;
    public Color[] SoundColors;
    public Text SoundBtn;
    public void Start()
    {
        //изменение кнопки звука 
        if (AudioListener.volume == 0)
        {
            SoundBtn.color = SoundColors[1];
            SounEnable = false;
        }
        else
        {
            SoundBtn.color = SoundColors[0];
            SounEnable = true;
        }
    }
    //кнопка паузы
    public void PauseM()
    {
        if (Pause == true)
        {
            PausePanel.SetActive(false);
            Time.timeScale = 1;
            Pause = false;
            return;
        }
        if (Pause == false)
        {
            PausePanel.SetActive(true);
            Time.timeScale = 0f;
            Pause = true;
            return;
        }
    }
    //загрузка сцены главного меню
    [System.Obsolete]
    public void MainMenuBtn()
    {
        Application.LoadLevel("MainMenu");
    }
    //выключение и выключение звука
    public void Sound()
    {
        if (SounEnable == true)
        {
            AudioListener.volume = 0;
            SoundBtn.color = SoundColors[1];
            SounEnable = false;
            return;
        }
        if (SounEnable == false)
        {
            AudioListener.volume = 1;
            SoundBtn.color = SoundColors[0];
            SounEnable = true;
            return;
        }
    }
}
