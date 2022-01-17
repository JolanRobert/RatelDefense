using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    
    
    [SerializeField] private GameObject planification;
    [SerializeField] private GameObject wave;

    [SerializeField] private GameObject towerMenu;
    [SerializeField] private TextMeshProUGUI deleteText;
    [SerializeField] private TextMeshProUGUI upgradeText;
    
    [SerializeField] private TextMeshProUGUI waveUI;
    [SerializeField] private TextMeshProUGUI goldUI;
    [SerializeField] private TextMeshProUGUI baseLifeUI;
    [SerializeField] private TextMeshProUGUI playerHealthUI;
    
    
    public void UpdateUI(int currentWave, float gold, int baseLife) {
        waveUI.text = "Wave : " + currentWave;
        goldUI.text = "Gold : " + gold;
        baseLifeUI.text = "Base Life : " + baseLife;
    }
    
    public void UpdatePlayerHealth(float health) {
        playerHealthUI.text = "HP : " + health;
    }

    public void WaveUI() {
        wave.SetActive(true);
        planification.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PlanificationUI() {
        wave.SetActive(false);
        planification.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void DisplayDelete()
    {
        Ray ray = GameManager.instance.planifCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!towerMenu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (Physics.Raycast(ray, out hit)) 
                {
                    if (hit.transform.GetComponent<Tower>())
                    {
                        GameManager.instance.selectedTower = hit.transform.gameObject;
                        upgradeText.text = "MAX";
                        if(hit.transform.GetComponent<Tower>().towerSO.nextTowerSO!=null)
                            upgradeText.text = "Upgrade (" +hit.transform.GetComponent<Tower>().towerSO.nextTowerSO.goldCost+")";
                        
                        deleteText.text = "Delete (" +
                            hit.transform.GetComponent<Tower>().towerSO.goldCost * 0.6f + ")";
                        
                        towerMenu.transform.position = GameManager.instance.planifCam.WorldToScreenPoint(hit.transform.position);
                        towerMenu.SetActive(true);
                    }
                }
            }
        }
        /*else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (Physics.Raycast(ray, out hit)) {
                    if (hit.transform.gameObject == GameManager.instance.selectedTower) return;
                    if(hit.transform.gameObject != towerMenu)
                    {
                        towerMenu.SetActive(false);
                        GameManager.instance.selectedTower = null;
                    }
                }
            }
        }*/
    }
}
