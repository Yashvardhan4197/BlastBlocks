using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonView : MonoBehaviour
{
    [SerializeField] Button levelButton;
    [SerializeField] TextMeshProUGUI levelText;

    public TextMeshProUGUI GetLevelText() => levelText;
    public Button GetLevelButton() => levelButton;
}
