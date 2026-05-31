using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    [Header("References")]
    [Space]
    [SerializeField] Image backgroundImage;
    [SerializeField] TextMeshProUGUI categoryTextHolder;
    [SerializeField] TextMeshProUGUI contentTextHolder;
    public void InitializeNotification(NotificationInfo info)
    {
        backgroundImage.color = info.Color;
        categoryTextHolder.text = info.Categoy.ToString();
        contentTextHolder.text = info.textToDisplay;
       
        StartCoroutine(nameof(NotificationRoutine));

    }
    IEnumerator NotificationRoutine()
    {

        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}
[Serializable]
public struct NotificationInfo
{
    public NotificationCategory Categoy;
    public Color Color;
    [HideInInspector] public string textToDisplay;
    public AudioClip audioClip;


}
