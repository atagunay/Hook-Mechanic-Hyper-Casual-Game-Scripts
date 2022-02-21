using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    float progress = 0;
    Slider slider;
    public static bool isJump = false;

    [SerializeField]
    private GameObject playerPrefab;

    float time = 0;

    public static float slideMultiply = 0;

    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();

    }

    public void TapButton()
    {
        Debug.Log("tap");
       
        progress = progress + 5.5f;
        slideMultiply = slideMultiply + 0.035f;
        PlayerController.rotateVector = PlayerController.rotateVector + new Vector3(-1, 0, 0);
    }

    private void Update()
    {
        if(slider.value >= 99 || time >= 5)
        {
            

            time = time + Time.deltaTime;
           

            PlayerController player = playerPrefab.GetComponent<PlayerController>();
            player.Jump();
            gameObject.transform.parent.gameObject.SetActive(false);
            Destroy(gameObject);

        }
        else
        {
            progress = progress - 0.1f;
            slider.value = progress;

            time = time + Time.deltaTime;
  
      

        }
    }

}
