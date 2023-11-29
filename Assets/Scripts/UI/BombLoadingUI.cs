using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class BombLoadingUI : MonoBehaviourPunCallbacks
{
    [SerializeField] Image bombFill;
    float targetFill;

    void Awake ()
    {
        bombFill.fillAmount = 0;
        targetFill = 0.25f;
    }

    public override void OnConnectedToMaster ()
    {
        base.OnConnectedToMaster();

        targetFill = 0.5f;
    }

    public override void OnJoinedLobby ()
    {
        base.OnJoinedLobby();

        targetFill = 1f;
    }

    void Update ()
    {
        bombFill.fillAmount = Mathf.Lerp(bombFill.fillAmount, targetFill, 1.5f * Time.deltaTime);
    }
}
