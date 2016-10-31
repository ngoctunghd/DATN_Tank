using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Chat : Photon.MonoBehaviour {
    public static Chat instance;
    public List<string> messages = new List<string>();

    private int chatHeight = (int)100;
    private Vector2 scrollPos = Vector2.zero;
    private string chatInput = "";
    private float lastUnfocusTime = 0;


    void Awake()
    {
        MakeInstance();
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void OnGUI()
    {
        GUI.SetNextControlName("");

//        GUILayout.BeginArea(new Rect(0, Screen.height - chatHeight, Screen.width, chatHeight));
        GUILayout.BeginArea(new Rect(Screen.width - 400, Screen.height - chatHeight, 400, chatHeight));

        //Show scroll list of chat messages
        scrollPos = GUILayout.BeginScrollView(scrollPos);
        GUI.color = Color.blue;
        
        for (int i = messages.Count - 1; i >= 0; i--)
        {
            GUILayout.Label(messages[i]);
            GUI.skin.label.fontSize = 30;
        }
        GUILayout.EndScrollView();
        GUI.color = Color.white;

        //Chat input
        GUILayout.BeginHorizontal();
        GUI.SetNextControlName("ChatField");
        chatInput = GUILayout.TextField(chatInput, GUILayout.MinWidth(300), GUILayout.MinHeight(65));
        Debug.Log(chatInput);
        GUI.skin.textField.fontSize = 28;     
        

        if (Event.current.type == EventType.keyDown && Event.current.character == '\n')
        {
            if (GUI.GetNameOfFocusedControl() == "ChatField")
            {
                SendChat(PhotonTargets.All);
                lastUnfocusTime = Time.time;
                GUI.FocusControl("");
                GUI.UnfocusWindow();
            }
            else
            {
                if (lastUnfocusTime < Time.time - 0.1f)
                {
                    GUI.FocusControl("ChatField");
                }
            }
        }

        if (GUILayout.Button("SEND", GUILayout.Height(60), GUILayout.Width(80) ))
            SendChat(PhotonTargets.All);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }


    public static void AddMessage(string text)
    {
        instance.messages.Add(text);
        if (instance.messages.Count > 15)
            instance.messages.RemoveAt(0);
    }


    [PunRPC]
    void SendChatMessage(string text, PhotonMessageInfo info)
    {
        AddMessage("[" + info.sender + "] " + text);
    }

    void SendChat(PhotonTargets target)
    {
        if (chatInput != "")
        {
            photonView.RPC("SendChatMessage", target, chatInput);

            chatInput = "";
        }
    }

    void SendChat(PhotonPlayer target)
    {
        if (chatInput != "")
        {
            chatInput = "[PM] " + chatInput;
            photonView.RPC("SendChatMessage", target, chatInput);
            chatInput = "";
        }
    }

    void OnLeftRoom()
    {
        this.enabled = false;
    }

    void OnJoinedRoom()
    {
        this.enabled = true;
    }
    void OnCreatedRoom()
    {
        this.enabled = true;
    }

}
