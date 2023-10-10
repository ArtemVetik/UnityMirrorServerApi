using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private Button _disconnectButton;
    [SerializeField] private TMP_Text _infoText;

    private void OnEnable()
    {
        _disconnectButton.onClick.AddListener(NetworkManager.singleton.StopClient);
    }

    private void OnDisable()
    {
        _disconnectButton.onClick.RemoveListener(NetworkManager.singleton.StopClient);
    }

    private void Start()
    {
        var serverInfo = JsonUtility.ToJson(ServerInfo.Server);
        _infoText.text = serverInfo;
    }
}
