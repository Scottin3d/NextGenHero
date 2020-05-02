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

  // fire rate cool down
  float fireRate = 1f;
  bool rapidFire;

  // public components
  public Transform EggFireSpawn;
  public GameObject EggPreFab;

  private void Start() {
    //uiapi = GameObject.Find("Canvas").GetComponent<UIAPI>();
    rapidFire = false;

    uiapi.SetSliderMax(fireRate);
    //slider.maxValue = RespawnTime;
    uiapi.SetSlider(fireRate);
    //slider.value = RespawnTime;

  }

  // Update is called once per frame
  void Update() {
    // shoot projectile
    if (Input.GetKeyDown(KeyCode.Space)) {
      if (!rapidFire) {
        if (uiapi.GetSliderValue() == fireRate) {
          ProcessEggSpwan();
        }
      } else {
        ProcessEggSpwan();
      }
    }

    // rapid fire -- no wait for egg firing
    if (Input.GetKeyDown(KeyCode.R)) {
      if (rapidFire) {
        rapidFire = false;
      } else {
        rapidFire = true;
      }
      uiapi.RapidFire(rapidFire);
    }
  }

  // instantiate prefab of projectile, set spawn time to 0
  private void ProcessEggSpwan() {
    Instantiate(EggPreFab, EggFireSpawn.position, EggFireSpawn.rotation);
    uiapi.SetSlider(0);
    
  }
}
