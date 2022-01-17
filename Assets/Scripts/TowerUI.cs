using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{

    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private Tower tower;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button button;
    
    void Start()
    {
        text.text = tower.towerSO.goldCost.ToString();
    }

    void Update()
    {
        if (tower.towerSO.goldCost > GameManager.instance.gold)
        {
            text.color = Color.red;
            button.enabled = false;
        }
        else
        {
            text.color = Color.white;
            button.enabled = true;
        }
    }
    
    
    public void SelectTower()
    {
        GameManager.instance.selectedPrefabTower = towerPrefab;
    }
}
