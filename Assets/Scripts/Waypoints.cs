using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour {
  public Sprite[] sprites;

  [SerializeField]
  SpawnPoints spawnPoints;
  [SerializeField]

  public GameObject waypointPrefab;

  int numberOfWaypoints = 6;
  [SerializeField]
  List<GameObject> waypoints;

  bool hideWaypoints;

  // Start is called before the first frame update
  void Start() {
    spawnPoints = GameObject.Find("SpawnPoints").GetComponent<SpawnPoints>();
    hideWaypoints = false;
    waypoints = new List<GameObject>();
    InitializeWaypoints();
  }

  private void Update() {
    if (Input.GetKeyDown(KeyCode.H)) {
      foreach (GameObject point in waypoints) {
        point.GetComponent<SpriteRenderer>().enabled = hideWaypoints;
      }

      if (hideWaypoints) {
        hideWaypoints = false;
      } else {
        hideWaypoints = true;
      }
    }
  }

  // initialize waypoints
  void InitializeWaypoints() {
    for (int i = 0; i < numberOfWaypoints; i++) {
      GenerateWaypoint(sprites[i]);
    }
  }

  // generate waypoint
  void GenerateWaypoint(Sprite sprite) {
    Vector3 spawnPos = spawnPoints.GetRNGWaypointSpawn();
    GameObject waypoint = Instantiate(waypointPrefab);
    waypoint.transform.position = spawnPos;
    waypoint.GetComponent<SpriteRenderer>().sprite = sprite;

    waypoints.Add(waypoint);
    waypoint.transform.SetParent(this.transform);
  }

  public GameObject GetWaypoint(int waypointIndex) {
    return waypoints[waypointIndex];
  }

  public GameObject GetRNGWaypoint() {
    int rng = Random.Range(0, numberOfWaypoints - 1);
    return waypoints[rng];
  }

  public int GetNumberOfWaypoints() {
    return waypoints.Count;
  }

}
