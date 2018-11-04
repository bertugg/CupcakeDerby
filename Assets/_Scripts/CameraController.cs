using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Camera))]
public class CameraController : MonoBehaviour {
	
	public float cameraZoomInOutPercent;
	public float cameraZoomTime;
	public float shakeAmount;

	private Vector3 originalPos;
	private bool isShaking;
	private float originalCameraZoomValue;
	private Camera _camera;

	private void Awake() {
		_camera = GetComponent<Camera> ();
	}

	private void OnEnable() {
		originalPos = transform.localPosition;
		originalCameraZoomValue =  _camera.orthographicSize;
	    _camera.orthographicSize = (_camera.orthographicSize + (originalCameraZoomValue * (cameraZoomInOutPercent / 100)));
		StartCoroutine(ZoomIn(cameraZoomTime)); // Zoom-in on game start
    }
    

    public IEnumerator OnPause()
    {
        float elapsedTime = 0;
        while (elapsedTime < 1)
        {
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, 13f, elapsedTime);
            elapsedTime += Time.unscaledDeltaTime; // unscaled time for evade pause or any other time scale effect
		      
            yield return new WaitForEndOfFrame();
        }
    }
    

    public IEnumerator OnResume()
    {
        float elapsedTime = 0;
        while (elapsedTime < 1)
        {
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, 11.5f, 0.3f);
            elapsedTime += Time.unscaledDeltaTime; // unscaled time for evade pause or any other time scale effect

            yield return new WaitForSeconds(.2f);
        }
    }

    private void Update() {
		if (isShaking) {
				transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			} 
		else 
		{
			transform.localPosition = originalPos;
			isShaking = false;
		}			
	}

	public void ShakeCamera() {
		isShaking = true;
	}

	// Change orthographic size of the camera.
	// ZoomPercent: Positive for zooming in / Negative for zooming out
	public IEnumerator ZoomCamera(float zoomPercent, float time)
	{
		float elapsedTime = 0;
		float zoomedValue = (_camera.orthographicSize + (originalCameraZoomValue * -zoomPercent / 100)); // For a fixed zooming in/out effect, percentage taken from originalCameraZoomValue. ((%80 x) %120 != x)
        while (elapsedTime < time)
		{
			_camera.orthographicSize = Mathf.Lerp (_camera.orthographicSize, zoomedValue, elapsedTime / time);
			elapsedTime += Time.unscaledDeltaTime; // unscaled time for evade pause or any other time scale effect
			yield return new WaitForEndOfFrame ();
		}
	}

	public IEnumerator ZoomIn(float time = -1) {
		if (time < 0)
			return ZoomCamera (cameraZoomInOutPercent, 1f);
		return ZoomCamera (cameraZoomInOutPercent, time);
	}

	public IEnumerator ZoomOut(float time = -1) {
		if (time < 0)
			return ZoomCamera (-cameraZoomInOutPercent, 1f);
		return ZoomCamera (-cameraZoomInOutPercent, time);
	}
}