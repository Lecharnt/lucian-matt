using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class connectUI : MonoBehaviour
{
    [SerializeField] private Button hostButtion;
    [SerializeField] private Button clientButtion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hostButtion.onClick.AddListener(houstButtionOnClick);
        clientButtion.onClick.AddListener(clientButtionOnCLick);
    }

    private void houstButtionOnClick()
    {
        NetworkManager.Singleton.StartHost();
    }
    private void clientButtionOnCLick()
    {
        NetworkManager.Singleton.StartClient();
    }
}
