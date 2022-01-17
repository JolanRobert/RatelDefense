using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour {
    
    public enum GameState {
        PLANIFICATION,
        PLAY 
    }
    
    [Header("Variables")]
    public int baseLife = 10;
    public float gold = 100;
    public int currentWave = 1;

    [Header("Managers")]
    [SerializeField] private WaveManager waveManager;
    public UIManager uiManager;

    [Header("Planification")]
    [SerializeField]
    public Camera planifCam;
    [SerializeField] private Transform planifCamPosition;
    
    [SerializeField] public GameObject selectedPrefabTower;
    public GameObject selectedTower;
    
    [SerializeField] private GameObject player;

    public GameState state = GameState.PLANIFICATION;
    public static GameManager instance;
    
    private bool nextWave;

    private void Awake()
    {
        instance = this;
    }
    
    private void Start() {
        StartCoroutine(GameLoop());
    }

    void Update() {
        uiManager.UpdateUI(currentWave,gold,baseLife);
        if (baseLife <= 0) 
            Failed();
    }
    

    private IEnumerator GameLoop()
    {
        while (true) {
            if (state == GameState.PLANIFICATION)
                yield return StartCoroutine(Planification());
            
            if (state == GameState.PLAY)
                yield return StartCoroutine(Wave());
        }
    }

    private IEnumerator Wave() {
        waveManager.StartWave(currentWave);
        while (waveManager.inWave) {
            yield return null;
        }
    }

    private IEnumerator Planification()
    {
        uiManager.DisplayDelete();
        if (selectedPrefabTower != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Ray ray = planifCam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit)) 
                {
                    if (hit.transform.CompareTag("Ground"))
                    {
                        yield return Instantiate(selectedPrefabTower, hit.point + new Vector3(0,1,0), Quaternion.identity);
                        gold -= selectedPrefabTower.GetComponent<Tower>().towerSO.goldCost;
                        selectedPrefabTower = null;
                    }
                }
            }
        }

        yield return null;
    }

    private IEnumerator Tween(bool toPlayer) {
        if (toPlayer) {
            var tween = planifCam.transform.DOMove(player.transform.GetChild(0).transform.position, 1f);
            planifCam.transform.DORotate(player.transform.GetChild(0).transform.rotation.eulerAngles, 1f);
            
            while (tween.IsPlaying()) {
                yield return null;
            }
            
            planifCam.gameObject.SetActive(false);
        }

        else {
            var tween = planifCam.transform.DOMove(planifCamPosition.position, 1f);
            planifCam.transform.DORotate(planifCamPosition.rotation.eulerAngles, 1f);
        }
    }
    
    public void NextWave() {
        player.SetActive(true);
        state = GameState.PLAY;
        uiManager.WaveUI();
        
        selectedPrefabTower = null;
        StartCoroutine(Tween(true));
    }

    public void EndWave() {
        player.SetActive(false);
        state = GameState.PLANIFICATION;
        uiManager.PlanificationUI();

        planifCam.gameObject.SetActive(true);
        StartCoroutine(Tween(false));
        currentWave++;
    }
    
    private void Failed() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void DeleteSelectedTower()
    {
        gold += selectedTower.GetComponent<Tower>().towerSO.goldCost*0.6f;
        Destroy(selectedTower);
    }

    public void UpgradeTower()
    {
        if (selectedTower.GetComponent<Tower>().towerSO == null) return;
        if (selectedTower.GetComponent<Tower>().towerSO.nextTowerSO.goldCost>gold) return;
        selectedTower.GetComponent<Tower>().towerSO = selectedTower.GetComponent<Tower>().towerSO.nextTowerSO;
        gold -= selectedTower.GetComponent<Tower>().towerSO.goldCost;
    }
}
