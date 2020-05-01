using UnityEngine;
using System.Collections;

/// <summary>
/// This script contols the behaviour the 'enemy'
/// mostly movement
/// </summary>
public class EnemyBehavior : MonoBehaviour {
  // UI
  [SerializeField]
  UIAPI uiapi;

  // navigation
  [SerializeField]
  Waypoints waypoints;
  [SerializeField]
  Transform heading;
  [SerializeField]
  int headingIndex;
  [SerializeField]
  float waypointThreshold;
  float distance;
  bool flightOrder;

  // enemy speed
  public float eSpeed = 25f;

  // components
  private Rigidbody2D RB;

	// Use this for initialization
	void Start () {
    // grab components
    waypoints = GameObject.Find("Waypoints").GetComponent<Waypoints>();
    uiapi = GameObject.Find("Canvas").GetComponent<UIAPI>();
    RB = GetComponent<Rigidbody2D>();

    headingIndex = 0;
    waypointThreshold = 5f;
    flightOrder = true;
    heading = waypoints.GetWaypoint(headingIndex).transform;
    NewDirection();
  }

  /// <summary>
  /// because I am controlling the movement with the rigidbody,
  /// if needs to be done in the fixedupdate method.  However,
  /// input can be collected in update.
  /// </summary>
  void FixedUpdate () {
    UpdateMotion();
  }

  private void Update() {
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

  // updates the position of the enemy
  private void UpdateMotion() {
    RB.MovePosition(transform.position + (transform.TransformDirection(Vector3.up) * eSpeed * Time.deltaTime));
    distance = Vector3.Distance(heading.position, transform.position);

    // if enemy reached waypoint, assign new waypoint
    if (distance < waypointThreshold) {
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
      Debug.Log("Hit by player");
      uiapi.IncEnemies();
    }
    
  }

  private void NewDirection() {
    Vector2 dir = new Vector2(heading.position.x - transform.position.x,
                              heading.position.y - transform.position.y);
    transform.up = dir;
  }
}
