using System;
using UnityEngine;
using UnityEngine.UI;

public class ExamineController : MonoBehaviour
{

    private bool canExamine = false;
    private bool examine = false;
    private Vector2 _lookInput;
    [SerializeField] GameObject target;
    [SerializeField] GameObject inspectingText;
    [SerializeField] float rotationSpeed = 6.0f;
    [SerializeField] Button exitButton;
    [SerializeField] float targetSize = 1.0f;

    void Start()
    {
        InputManager.Instance.UI.Click.started += ctx => canExamine = true;
        InputManager.Instance.UI.Click.canceled += ctx => canExamine = false;

        InputManager.Instance.UI.Examine.performed += ctx => _lookInput = ctx.ReadValue<Vector2>();
        InputManager.Instance.UI.Examine.canceled += ctx => _lookInput = Vector2.zero;

        inspectingText.SetActive(false);

        exitButton.onClick.AddListener(ExitExamineState);
    }
    private void OnEnable()
    {
        EventsManager.AddListener<OnExamineObject>(Examine);
        EventsManager.AddListener<OnGameStateChanged>(UpdateOnGameStateChanged);

    }
    private void OnDisable()
    {
        EventsManager.RemoveListener<OnExamineObject>(Examine);
        EventsManager.RemoveListener<OnGameStateChanged>(UpdateOnGameStateChanged);

    }

    private void UpdateOnGameStateChanged(OnGameStateChanged evt)
    {
        if (evt.NewState != GameState.Inspecting)
        {
            examine = false;
            target.SetActive(false);
            exitButton.gameObject.SetActive(false);
            inspectingText.SetActive(false);
        }
    }

    private void Examine(OnExamineObject evt)
    {
        examine = evt.StartExamination;
        if (evt.Target == null)
            return;
        //target.transform.Rotate() I need to reset rotation everytime the object is inspected
        inspectingText.SetActive(true);
        target.GetComponent<MeshFilter>().mesh = evt.Target.GetComponent<MeshFilter>().mesh;
        target.GetComponent<Renderer>().materials = evt.Target.GetComponent<Renderer>().materials;
        target.SetActive(true);
        exitButton.gameObject.SetActive(true);
        GameManager.Instance.SetState(GameState.Inspecting);

        NormalizeScale(evt.Target);

    }
    private void NormalizeScale(GameObject source)
    {
        Mesh mesh = source.GetComponent<MeshFilter>().mesh;
        Vector3 meshSize = mesh.bounds.size; // unscaled, local space
        float largestAxis = Mathf.Max(meshSize.x, meshSize.y, meshSize.z);
        float scaleFactor = targetSize / largestAxis;
        target.transform.localScale = Vector3.one * scaleFactor;
    }
    private void Update()
    {
        if (!examine)
            return;
        if (!canExamine)
            return;

        target.transform.Rotate(Vector3.up, -_lookInput.x * rotationSpeed * Time.deltaTime, Space.World);
        target.transform.Rotate(Vector3.right,   _lookInput.y* rotationSpeed *Time.deltaTime, Space.World);
    }
    public void ExitExamineState()
    {
        examine = false;
        target.SetActive(false);
        exitButton.gameObject.SetActive(false);
        inspectingText.SetActive(false);
        EventsManager.Broadcast(new OnExamineObject { StartExamination = false });
        GameManager.Instance.SetState(GameState.Gameplay);

    }
}
