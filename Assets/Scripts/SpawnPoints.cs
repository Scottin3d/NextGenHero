using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour {
  [SerializeField]
  private int numberOfSpawns = 10;
  Vector3[] spawnPoints;
  bool[] spawnFilled;
  // Start is called before the first frame update
  void Start() {
    spawnPoints = new Vector3[numberOfSpawns];
    spawnFilled = new bool[numberOfSpawns];
  }

  // Update is called once per frame
  void Update() {

  }

  void InitializeSpawns(int numOfSpawns) {
    for (int i = 0; i < numOfSpawns; i++) {
      spawnPoints[i] = GenerateSpawn();
      spawnFilled[i] = false;
    }
  }


  Vector3 GenerateSpawn() {
    float spawnY = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, 
                                Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
    float spawnX = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, 
                                Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

    Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);
    return spawnPosition;
  }
}
