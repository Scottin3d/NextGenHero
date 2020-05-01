﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour {
  [SerializeField]
  UIAPI uiapi;
  [SerializeField]
  SpawnPoints spawnPoints;
  [SerializeField]
  Waypoints waypoints;

  [SerializeField]
  List<GameObject> enemies;

  public GameObject enemyPrefab;

  int numberOfEnemiesInScene = 0;
  int maxNumberOfEnemies = 10;

  bool flightOrder;
  [SerializeField]
  float enemySpeed;

  // Start is called before the first frame update
  void Start() {
    spawnPoints = GameObject.Find("SpawnPoints").GetComponent<SpawnPoints>();
    waypoints = GameObject.Find("Waypoints").GetComponent<Waypoints>();
    uiapi = GameObject.Find("Canvas").GetComponent<UIAPI>();

    enemies = new List<GameObject>();
    maxNumberOfEnemies = 10;

  }

  // Update is called once per frame
  void Update() {
    enemySpeed = uiapi.GetEnemySpeed();
    numberOfEnemiesInScene = enemies.Count;
    if (numberOfEnemiesInScene < maxNumberOfEnemies) {
      SpawnEnemy();
    }

    // change flight navigation order
    if (Input.GetKeyDown(KeyCode.J)) {
      if (flightOrder) {
        flightOrder = false;
        uiapi.SetWaypoint("Random");
      } else {
        flightOrder = true;
        uiapi.SetWaypoint("Sequenced");
      }
    }

    if (Input.GetKeyDown(KeyCode.X)) {
      SpawnEnemy();
    }
  }

  void SpawnEnemy() {
    Debug.Log("Spawning enemy");
    Vector3 spawnPos = spawnPoints.GetRNGSpawn();
    GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

    enemies.Add(enemy);
    enemy.transform.SetParent(this.transform);
  }

  public void DestroyEnemy(GameObject obj) {
    uiapi.IncEnemies();
    enemies.Remove(obj);
    Destroy(obj);
  }

  public bool FlightOrder() {
    return flightOrder;
  }

  public float GetEnemySpeed() {
    return enemySpeed;
  }
}