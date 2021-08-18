using UnityEngine;
using UnityEngine.UI;

public class OptionView : MonoBehaviour
{
    [SerializeField] private Button closeButton;

    public Button CloseButton
    {
        get => closeButton;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }
}
