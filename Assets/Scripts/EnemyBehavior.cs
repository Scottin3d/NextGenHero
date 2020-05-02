using UnityEngine;
using System.Collections;

/// <summary>
/// This script contols the behaviour the 'enemy'
/// mostly movement
/// </summary>
public class EnemyBehavior : MonoBehaviour {
  // components
  Rigidbody2D RB;
  [SerializeField]
  EnemiesController enemiesController;

  // navigation
  [SerializeField]
  Waypoints waypoints;
  [SerializeField]
  Transform heading;
  [SerializeField]
  int headingIndex;
  
  [Range(1f, 20f)]
  public float waypointThreshold;
  float distanceToWaypoint;
  bool flightOrder;

  float eSpeed;
  
	void Start () {
    // grab components
    waypoints = GameObject.Find("Waypoints").GetComponent<Waypoints>();
    enemiesController = GameObject.Find("Enemies").GetComponent<EnemiesController>();
    RB = GetComponent<Rigidbody2D>();
    eSpeed = enemiesController.GetEnemySpeed();

    headingIndex = 0;
    waypointThreshold = 5f;

    Spawn();
  }

  /// <summary>
  /// because I am controlling the movement with the rigidbody,
  /// if needs to be done in the fixedupdate method.  However,
  /// input can be collected in update.
  /// </summary>
  void FixedUpdate () {
    UpdateMotion();
  }

  void Spawn() {
    flightOrder = enemiesController.FlightOrder();

    if (flightOrder) {
      heading = waypoints.GetWaypoint(headingIndex).transform;
    } else {
      heading = waypoints.GetRNGWaypoint().transform;
    }
    NewDirection();
  }

  // updates the position of the enemy
  private void UpdateMotion() {
    eSpeed = enemiesController.GetEnemySpeed();

    RB.MovePosition(transform.position + (transform.TransformDirection(Vector3.up) * eSpeed * Time.deltaTime));
    distanceToWaypoint = Vector3.Distance(heading.position, transform.position);
    flightOrder = enemiesController.FlightOrder();

    // if enemy reached waypoint, assign new waypoint
    if (distanceToWaypoint < waypointThreshold) {
      if (flightOrder) {
        // ordered flight pattern
        headingIndex++;

        // check overflow, reassign to 0 if at end
        if (headingIndex > waypoints.GetNumberOfWaypoints() - 1) {
          headingIndex = 0;
        }

        heading = waypoints.GetWaypoint(headingIndex).transform;
      } else {
        // random flight pattern
        heading = waypoints.GetRNGWaypoint().transform;
      }
      NewDirection();
    }
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    // hit by player
    if (collision.gameObject.tag == "Hero") {
      enemiesController.DestroyEnemy(this.gameObject);
    }
    if (collision.gameObject.tag == "Egg") {
      enemiesController.DestroyEnemy(this.gameObject);
    }
  }

  private void NewDirection() {
    Vector2 dir = new Vector2(heading.position.x - transform.position.x,
                              heading.position.y - transform.position.y);
    transform.up = dir;
  }
}
