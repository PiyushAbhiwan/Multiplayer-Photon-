using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class PhotonManager : MonoBehaviourPunCallbacks , ILobbyCallbacks 
{
    
    #region Panels , InputFields and Buttons
    public GameObject connectionPanel, lobbyPanel, roomPanel , roomListPanel;
    public Button connectBtn, createRoomBtn, JoinRoomBtn, joinBtn, startBtn, leaveBtn, backToLobbyBtn;
    public TMP_Text clientstatetxt, playerName1, playerName2;
    public TMP_InputField playerName;
    public PhotonManager instance;
    #endregion

    private Dictionary<string, RoomInfo> roomListData;


    public void Start()
    {
         instance = this;
        connectBtn.onClick.AddListener(OnClickConnect);
        createRoomBtn.onClick.AddListener(OnClickRoomCreate);
        leaveBtn.onClick.AddListener(OnClickLeaveBtn);
        JoinRoomBtn.onClick.AddListener(OnClickJoinRoomBtn);
        backToLobbyBtn.onClick.AddListener(OnClickBackToLobbyBtn);

        roomListData = new Dictionary<string, RoomInfo>();
    }
    public void Update()
    {
        clientstatetxt.text = PhotonNetwork.NetworkClientState.ToString();
    }
    public void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    public void onDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    #region UIMethods

    public void OnClickConnect()
    {
        var nameText = playerName.text;
        if (!string.IsNullOrEmpty(nameText))
        {
            PhotonNetwork.LocalPlayer.NickName = nameText;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void OnClickRoomCreate()
    {
       var roomName = Random.Range(0, 1000).ToString();
        var roomOptions = new RoomOptions
        {
            MaxPlayers = 2,
            IsVisible = true
        };
        PhotonNetwork.CreateRoom(roomName, roomOptions);
       
    }
    public void OnClickLeaveBtn()
    {
        PhotonNetwork.Disconnect();
    }

    public void OnClickJoinRoomBtn()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
        roomListPanel.SetActive(true);
        lobbyPanel.SetActive(false);
        Debug.Log("Join Room Btn Clicked!!");
    }


    public void OnClickBackToLobbyBtn()
    {
        roomListPanel.SetActive(false);
        lobbyPanel.SetActive(true);
        Debug.Log("Back To Lobby Btn Clicked!!");
    }


    private void RoomJoinFromList(string roomName)
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }
        PhotonNetwork.JoinRoom(roomName);
    }
   /* private void ClearRoomList()
    {
        Debug.Log("Room list clear");
        foreach (var roomObject in roomListGameObjects.Values)
        {
            Destroy(roomObject);
        }
        roomListGameObjects.Clear();
    }*/





    #endregion



    #region MonoBehaviourPunCallbacks Callbacks
    public override void OnConnected()
    {
        Debug.Log("Connected To Internet()");
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster()");
        connectionPanel.SetActive(false);
        lobbyPanel.SetActive(true);
       
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected");
        roomPanel.SetActive(false);
        connectionPanel.SetActive(true);
        Debug.LogWarningFormat("OnDisconnected(){0}" + cause);

    }

    #endregion
    public override void OnCreatedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name + " room Created");
       
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " room Joined");
        roomPanel.SetActive(true);
        lobbyPanel.SetActive(false);
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            playerName1.text = PhotonNetwork.LocalPlayer.NickName;
        }
        else
        {
            playerName2.text = PhotonNetwork.LocalPlayer.NickName;
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (var rooms in roomList)
        {
            Debug.Log(" Room Name "+rooms.Name);
            roomListData.Add(rooms.Name, rooms);
        }
        
    }

    public override void OnJoinedLobby()
    {
        Debug.Log(" OnJoinedLobby");
    }
    public override void OnLeftLobby()
    {
        Debug.Log(" OnLeftLobby");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
    }

    public override void OnLeftRoom()
    {
        Debug.Log("  OnLeftRoom");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
    }

    


}
