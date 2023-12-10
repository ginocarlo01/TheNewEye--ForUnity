using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cherryCount;

    public static UIManager instance;

    private void Awake() { instance = this; }

    public void UpdateCherryCount(int value) { cherryCount.text = "x" + value.ToString(); }

}
