using System;
using UnityEngine;
using UnityEngine.UI;

public class ExamineController : MonoBehaviour
{

    private bool canExamine = false;
    private bool examine = false;
    private Vector2 _lookInput;
    [SerializeField] GameObject target;
    [SerializeField] float rotationSpeed = 6.0f;
    [SerializeField] Button exitButton;

    void Start()
    {
        InputManager.Instance.UI.Click.started += ctx => canExamine = true;
        InputManager.Instance.UI.Click.canceled += ctx => canExamine = false;

        InputManager.Instance.UI.Examine.performed += ctx => _lookInput = ctx.ReadValue<Vector2>();
        InputManager.Instance.UI.Examine.canceled += ctx => _lookInput = Vector2.zero;

        exitButton.onClick.AddListener(ExitExamineState);
    }
    private void OnEnable()
    {
        EventsManager.AddListener<OnExamineObject>(Examine);
    }
    private void OnDisable()
    {
        EventsManager.RemoveListener<OnExamineObject>(Examine);
    }
    private void Examine(OnExamineObject evt)
    {
        examine = evt.StartExamination;
        target.GetComponent<MeshFilter>().mesh = evt.Target.GetComponent<MeshFilter>().mesh;
        target.GetComponent<Renderer>().material = evt.Target.GetComponent<Renderer>().material;
        target.SetActive(true);
        exitButton.gameObject.SetActive(true);
        GameManager.Instance.SetState(GameState.WinningScreen);
        
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
        GameManager.Instance.SetState(GameState.Gameplay);
        examine = false;
        target.SetActive(false);
        exitButton.gameObject.SetActive(false);

    }
}
