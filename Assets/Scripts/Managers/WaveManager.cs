using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyName {
    CACTUS,
    CHARIOT,
    MAGE
}

public class WaveManager : MonoBehaviour {

    public static WaveManager instance;
    
    public bool inWave;
    private int repeatWave = 1;
    
    [SerializeField] private List<EnemyKey> enemyKeys = new List<EnemyKey>();
    [SerializeField] private GameObject enemySpawn;

    void Awake() {
        instance = this;
    }

    void Update() {
        if (inWave) {
            if (transform.childCount == 0) EndWave();
        }
    }

    public void StartWave(int waveID) {
        inWave = true;
        if (waveID == 1) Wave1();
        else if (waveID == 2) Wave2();
        else if (waveID == 3) Wave3();
        else StartCoroutine(Wave4());
    }

    private void Wave1() {
        SpawnEnemy(EnemyName.CACTUS,enemySpawn.transform.position);
    }
    
    private void Wave2() {
        SpawnEnemy(EnemyName.CHARIOT,enemySpawn.transform.position);
    }
    
    private void Wave3() {
        SpawnEnemy(EnemyName.MAGE,enemySpawn.transform.position);
    }
    
    private IEnumerator Wave4() {
        int tmp = repeatWave;
        while (tmp > 0) {
            SpawnEnemy(EnemyName.CACTUS,enemySpawn.transform.position);
            SpawnEnemy(EnemyName.CHARIOT,enemySpawn.transform.position);
            SpawnEnemy(EnemyName.MAGE,enemySpawn.transform.position);
            tmp--;
            yield return new WaitForSeconds(3);
        }
        
        repeatWave++;
    }

    private void EndWave() {
        inWave = false;
        GameManager.instance.EndWave();
    }

    [Serializable]
    private class EnemyKey {
        public GameObject prefab;
        public EnemyName enemyName;
    }
    
    public void SpawnEnemy(EnemyName enemyName, Vector3 position) {
        GameObject enemy = Instantiate(GetPrefabEnemy(enemyName), position, Quaternion.identity,transform);
        enemy.transform.position = position;
    }
    
    private GameObject GetPrefabEnemy(EnemyName enemyName) {
        foreach (EnemyKey key in enemyKeys) {
            if (key.enemyName == enemyName) return key.prefab;
        }
        throw new Exception("Enemy doesn't exists");
    }
}
