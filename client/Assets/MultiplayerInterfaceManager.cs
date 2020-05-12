using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MultiplayerInterfaceManager : MonoBehaviour
{
    public TextMeshProUGUI serverAddress;

    public void OnJoinButton()
    {
        GetComponent<Client>().Connect(serverAddress.text);
    }
}
