using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CCTVControl1 : MonoBehaviour
{
    public float turnSpeed = 20f; // ī�޶� ȸ�� �ӵ�
    public Button toggleButton; // UI Button
    public RawImage rawImage; // RawImage

    private bool isEnabled = false; // ��ũ��Ʈ Ȱ��ȭ ����

    void Start()
    {
        // UI Button�� Ŭ�� �̺�Ʈ�� ToggleScript �Լ� ����
        toggleButton.onClick.AddListener(ToggleScript);
        // �ʱ⿡�� rawImage�� ��Ȱ��ȭ
        rawImage.gameObject.SetActive(false);
    }

    void Update()
    {
        // ��ũ��Ʈ�� Ȱ��ȭ�Ǿ��ִ� ��쿡�� ȸ�� ���� ����
        if (isEnabled)
        {
            // ����Ű �Է� �ޱ�
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // ī�޶� ȸ�� ���
            Vector3 rotation = transform.rotation.eulerAngles;
            rotation.y += horizontalInput * turnSpeed * Time.deltaTime;
            rotation.x -= verticalInput * turnSpeed * Time.deltaTime; // ���� ȸ�� �߰�
            transform.rotation = Quaternion.Euler(rotation);
        }
    }

    // ��ũ��Ʈ Ȱ��ȭ �Ǵ� ��Ȱ��ȭ
    void ToggleScript()
    {
        isEnabled = !isEnabled;
        // ��ư�� ������ ������ ���� rawImage�� Ȱ��ȭ
        rawImage.gameObject.SetActive(isEnabled);
    }
}