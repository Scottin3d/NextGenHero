using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Instantiates a gameobject as the projectile when the fire button is pressed
/// </summary>
public class FireEgg : MonoBehaviour {

  // UI slider for interaction
  public UIAPI uiapi;
  //public Slider slider;
  // fire rate cool down
  private float fireRate = 1f;

  // public components
  public Transform EggFireSpawn;
  public GameObject EggPreFab;

  private void Awake() {
    uiapi.SetSliderMax(fireRate);
    //slider.maxValue = RespawnTime;
    uiapi.SetSlider(fireRate);
    //slider.value = RespawnTime;

  }

  // Update is called once per frame
  void Update() {
    // shoot projectile
    if (Input.GetKeyDown(KeyCode.Space)) {
      if (uiapi.GetSliderValue() == fireRate) {
        ProcessEggSpwan();
      }
    }
  }

  // instantiate prefab of projectile, set spawn time to 0
  private void ProcessEggSpwan() {
    Instantiate(EggPreFab, EggFireSpawn.position, EggFireSpawn.rotation);
    uiapi.SetSlider(0);
    
  }
}
