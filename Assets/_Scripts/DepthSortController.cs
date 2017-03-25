using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]
public class DepthSortController : MonoBehaviour {

	[Tooltip("Distance of object from the floor")]
	public int HeightOffset = 0;

	private const int IsometricRangePerYUnit = 100;

	void Update()
	{
		SpriteRenderer renderer = GetComponent<SpriteRenderer>();
		renderer.sortingOrder = -(int)((transform.position.y - transform.localScale.y) * IsometricRangePerYUnit) + HeightOffset;
	}
}