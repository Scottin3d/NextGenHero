using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Instantiates a gameobject as the projectile when the fire button is pressed
/// </summary>
public class FireEgg : MonoBehaviour {
  // UI slider for interaction
  public Slider slider;
  // fire rate cool down
  private float RespawnTime = 1f;

  // public components
  public Transform EggFireSpawn;
  public GameObject EggPreFab;

  private void Awake() {
    slider.maxValue = RespawnTime;
    slider.value = RespawnTime;

  }

  // Update is called once per frame
  void Update() {
    // shoot projectile
    if (Input.GetKeyDown(KeyCode.Space)) {
      if (slider.value == RespawnTime) {
        ProcessEggSpwan();
        
      }
    }
    FillSlider();
  }

  // instantiate prefab of projectile, set spawn time to 0
  private void ProcessEggSpwan() {
    Instantiate(EggPreFab, EggFireSpawn.position, EggFireSpawn.rotation);
    slider.value = 0;
  }

  // fills the UI slider 
  void FillSlider() {
      slider.value += 1f * Time.deltaTime;
  }
}
