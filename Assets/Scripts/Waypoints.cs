using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour {
  public Sprite[] sprites;
  public SpawnPoints spawnPoints;
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
        point.GetComponent<BoxCollider2D>().enabled = hideWaypoints;

      }
      hideWaypoints = !hideWaypoints;
      /*
      if (hideWaypoints) {
        hideWaypoints = false;
      } else {
        hideWaypoints = true;
      }
      */
    }
  }

  // initialize waypoints
  void InitializeWaypoints() {
    for (int i = 0; i < numberOfWaypoints; i++) {
      GameObject waypoint = GenerateWaypoint(sprites[i]);
      waypoint.name = "Waypoint " + i;
      waypoints.Add(waypoint);
    }
  }

  // generate waypoint
  GameObject GenerateWaypoint(Sprite sprite) {
    Vector3 spawnPos = spawnPoints.GetRNGWaypointSpawn(true);
    GameObject waypoint = Instantiate(waypointPrefab);
    waypoint.transform.position = spawnPos;
    waypoint.GetComponent<SpriteRenderer>().sprite = sprite;

    
    waypoint.transform.SetParent(this.transform);
    return waypoint;
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
