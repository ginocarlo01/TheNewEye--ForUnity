using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cherryCount;

    public static UIManager instance;

    private void Awake() { instance = this; }

    #region ObserverSubscription
    private void OnEnable()
    {
        PlayerCollider.cherriesQtyChanged += UpdateCherryCount;
    }

    private void OnDisable()
    {
        PlayerCollider.cherriesQtyChanged -= UpdateCherryCount;
    }
    #endregion

    public void UpdateCherryCount(int value) {
        if(cherryCount != null)
        {
            cherryCount.text = "x" + value.ToString();
        }
        
    }

}
