// adds EdgeCollider2D colliders to screen edges
// only works with orthographic camera

using UnityEngine;
using System.Collections;

namespace UnityLibrary {
  public class ScreenEdgeColliders : MonoBehaviour {
    [Range(1f, 50f)]
    public float edgePadding = 5f;
    [SerializeField]
    Vector2 screenBounds;
    void Start() {
      AddCollider();
    }

    void AddCollider() {
      if (Camera.main == null) { Debug.LogError("Camera.main not found, failed to create edge colliders"); return; }

      var cam = Camera.main;
      if (!cam.orthographic) { Debug.LogError("Camera.main is not Orthographic, failed to create edge colliders"); return; }

      screenBounds = new Vector2(cam.pixelWidth - edgePadding, cam.pixelHeight - 100);

      var bottomLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(edgePadding, edgePadding, cam.nearClipPlane));
      var topLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(edgePadding, cam.pixelHeight - 100, cam.nearClipPlane));
      var topRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth - edgePadding, cam.pixelHeight - 100, cam.nearClipPlane));
      var bottomRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth - edgePadding, edgePadding, cam.nearClipPlane));

      // add or use existing EdgeCollider2D
      var edge = GetComponent<EdgeCollider2D>() == null ? gameObject.AddComponent<EdgeCollider2D>() : GetComponent<EdgeCollider2D>();

      var edgePoints = new[] { bottomLeft, topLeft, topRight, bottomRight, bottomLeft };
      edge.points = edgePoints;
    }
  }
}