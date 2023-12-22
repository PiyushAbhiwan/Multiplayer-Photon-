using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;



public class Counter : MonoBehaviour
{
    public Button clickBtn;
    public TMP_Text counterTxt;
    private int randomNum;
    private int drawChance1 = 10, drawChance2 = 10;
    private int sum1,sum2;

    // Start is called before the first frame update
    void Start()
    {
        clickBtn.onClick.AddListener(ClickCounter);
        
    }

   public void ClickCounter()
    {
        
        
            if (PhotonNetwork.LocalPlayer.IsMasterClient) {
                     if (drawChance1 > 0)
                     {
                         randomNum = Random.Range(0, 10);
                         counterTxt.text = randomNum.ToString();
                         sum1 += randomNum;
                         Debug.Log(PhotonNetwork.LocalPlayer.NickName + " Clicked");
                         drawChance1--;
                     }
                     else
                     {
                        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " sum is Clicked "+ sum1);
                     }
            }
            else
            {
                     if (drawChance2 > 0)
                     {
                         randomNum = Random.Range(0, 10);
                         counterTxt.text = randomNum.ToString();
                         sum2 += randomNum;
                         Debug.Log(PhotonNetwork.LocalPlayer.NickName + " Clicked");
                              drawChance2--;
                     }
                     else
                     {
                         Debug.Log(PhotonNetwork.LocalPlayer.NickName + " sum is Clicked " + sum2);
                     }
        }
    }
       
    }

