using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour {
  
  public UIAPI uiapi;
  public SpawnPoints spawnPoints;
  public Waypoints waypoints;

  [SerializeField]
  List<GameObject> enemies;

  public GameObject enemyPrefab;

  int numberOfEnemiesInScene = 0;
  int maxNumberOfEnemies = 10;
  int enemieCount = 0;

  bool flightOrder;
  [SerializeField]
  float enemySpeed;

  Transform headingOne;
  // Start is called before the first frame update
  void Start() {
    spawnPoints = GameObject.Find("SpawnPoints").GetComponent<SpawnPoints>();
    //spawnPoints = (SpawnPoints)Resources.Load("Assets/Prefabs/SpawnPoints");
    waypoints = GameObject.Find("Waypoints").GetComponent<Waypoints>();
    //waypoints = (Waypoints)Resources.Load("Assets/Prefabs/Waypoints");
    uiapi = GameObject.Find("Canvas").GetComponent<UIAPI>();
    //uiapi = UI.GetComponent<UIAPI>();


    enemies = new List<GameObject>();
    maxNumberOfEnemies = 10;
    headingOne = waypoints.GetWaypoint(0).transform;
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
  }

  void SpawnEnemy() {
    Debug.Log("Spawning enemy");
    Vector3 spawnPos = spawnPoints.GetRNGWaypointSpawn(false);
    GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);


    //enemy.GetComponent<EnemyBehavior>().SetHeading(headingOne);
    enemies.Add(enemy);
    enemy.transform.SetParent(this.transform);
    enemy.name = "Enemy " + enemieCount;
    enemieCount++;
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

  public Waypoints GetWaypoinmts() {
    return waypoints;
  }

  public EnemiesController GetEnemiesController() {
    return this;
  }
}
