using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIAPI : MonoBehaviour {
  public Slider slider;
  public Text waypoints;
  string waypointType;
  public Text hero;
  string movementType;
  public Text eggs;
  int eggCount;
  public Text enemies;
  int enemyCount;
  public Text rapidFire;
  string fireRate;

  // Start is called before the first frame update
  void Start() {
    waypointType = "Sequenced";
    movementType = "WASD";
    eggCount = 0;
    enemyCount = 0;
    fireRate = "Off";

  }

  // Update is called once per frame
  void Update() {
    FillSlider();
    waypoints.text = waypointType;
    hero.text = movementType;
    eggs.text = eggCount.ToString();
    enemies.text = enemyCount.ToString();
    rapidFire.text = fireRate;
  }

  /// 
  /// Slider
  /// 
  // fills the UI slider 
  void FillSlider() {
    slider.value += 1f * Time.deltaTime;
  }

  // set the max value to egg fire rate
  public void SetSliderMax(float fireRate) {
    slider.maxValue = fireRate;
  }

  // set slider value
  public void SetSlider(float value) {
    slider.value = value;
  }

  // get the current slider value
  public float GetSliderValue() {
    return slider.value;
  }

  ///
  /// Waypoints
  /// 
  public void SetWaypoint(string str) {
    waypointType = str;
  }

  ///
  /// Hero Movement
  ///
  public void SetHeroMovement(string str) {
    movementType = str;
  }

  ///
  /// Egg
  ///
  // increase count
  public void IncEgg() {
    eggCount++;
  }

  // decrease count
  public void DecEgg() {
    if (eggCount > 0) {
      eggCount--;
    } else {
      eggCount = 0;
    }
  }

  ///
  /// Enemies
  /// 
  public void IncEnemies() {
    enemyCount++;
  }

  ///
  /// RapidFire
  /// 
  public void RapidFire(bool on) {
    if (on) {
      fireRate = "On";
    } else {
      fireRate = "Off";
    }
  }
}
