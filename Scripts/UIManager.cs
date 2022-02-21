using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject startPanel;

    [SerializeField]
    private GameObject tapPanel;

    [SerializeField]
    private GameObject winPanel;

    [SerializeField]
    private GameObject losePanel;

    [SerializeField]
    private TMPro.TextMeshProUGUI cmText;

    [SerializeField]
    private GameObject SCENE;

    public static int cm;

    private void Start()
    {
        cm = 0;
    }
    // Start is called before the first frame update

    public void StartTheGame()
    {
        startPanel.SetActive(false);
        PlayerController.flag = 1;
    }

    public void StartTapAllert()
    {
        tapPanel.SetActive(true);

    }

    public void WinPanel()
    {
        winPanel.SetActive(true);
        winPanel.GetComponent<Image>().DOFade(0.6f, 1);
    }

    public void LosePanel()
    {
        losePanel.SetActive(true);
        losePanel.GetComponent<Image>().DOFade(0.6f, 2);
    }

    public void IncraseCM()
    {
        cm = cm + 1;
        cmText.text = cm.ToString() + " FT";
        cmText.fontSize = cmText.fontSize + 0.2f;
    }

    public void DecreaseCM()
    {
        cm = PlayerController.linkedList.Count - 1;
        cmText.text = cm.ToString() + " FT";

        int temp = PlayerController.size - cm;

        cmText.fontSize = (float)( cmText.fontSize - temp * 0.2f);
        //Debug.Log(cmText.fontSize);

        PlayerController.size = cm;
    }

    public void Restart()
    {
        DOTween.KillAll();
        Destroy(SCENE);
        SceneManager.LoadScene(0);
    }

}
