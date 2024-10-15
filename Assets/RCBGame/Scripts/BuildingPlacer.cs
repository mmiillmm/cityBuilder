using UnityEngine.EventSystems;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    public static BuildingPlacer instance; // Singleton pattern

    public LayerMask terrainLayerMask; // Use this instead of groundLayerMask

    protected GameObject _buildingPrefab;
    protected GameObject _toBuild;//asd

    protected Camera _mainCamera;

    protected Ray _ray;
    protected RaycastHit _hit;

    private void Awake()
    {
        instance = this;
        _mainCamera = Camera.main;
        _buildingPrefab = null;
    }

    private void Update()
    {
        if (_buildingPrefab != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Destroy(_toBuild);
                _toBuild = null;
                _buildingPrefab = null;
                return;
            }

            if (EventSystem.current.IsPointerOverGameObject())
            {
                if (_toBuild.activeSelf) _toBuild.SetActive(false);
                return;
            }
            else if (!_toBuild.activeSelf) _toBuild.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _toBuild.transform.Rotate(Vector3.up, 90);
            }

            _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit, 1000f, terrainLayerMask)) // Use terrainLayerMask here
            {
                if (!_toBuild.activeSelf) _toBuild.SetActive(true);
                _toBuild.transform.position = _hit.point;

                if (Input.GetMouseButtonDown(0))
                {
                    BuildingManager m = _toBuild.GetComponent<BuildingManager>();
                    if (m.hasValidPlacement)
                    {
                        m.SetPlacementMode(PlacementMode.Fixed);

                        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                        {
                            _toBuild = null; // To avoid destruction
                            _PrepareBuilding();
                        }
                        else
                        {
                            _buildingPrefab = null;
                            _toBuild = null;
                        }
                    }
                }

            }
            else if (_toBuild.activeSelf) _toBuild.SetActive(false);
        }
    }

    public void SetBuildingPrefab(GameObject prefab)
    {
        _buildingPrefab = prefab;
        _PrepareBuilding();
        EventSystem.current.SetSelectedGameObject(null);
    }

    protected virtual void _PrepareBuilding()
    {
        if (_toBuild) Destroy(_toBuild);

        _toBuild = Instantiate(_buildingPrefab);
        _toBuild.SetActive(false);

        BuildingManager m = _toBuild.GetComponent<BuildingManager>();
        m.isFixed = false;
        m.SetPlacementMode(PlacementMode.Valid);
    }
}
