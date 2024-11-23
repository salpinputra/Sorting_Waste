using UnityEngine;

public class ExitPanelHandler : MonoBehaviour
{
    public GameObject exitPanel; // Referensi ke panel keluar

    void Start()
    {
        // Pastikan panel keluar dalam keadaan tidak aktif saat aplikasi dimulai
        if (exitPanel != null)
        {
            exitPanel.SetActive(false);
        }
    }

    void Update()
    {
        // Deteksi tombol Kembali pada perangkat Android
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (exitPanel != null)
            {
                // Jika panel sudah aktif, keluar aplikasi
                if (exitPanel.activeSelf)
                {
                    Application.Quit();
                    Debug.Log("Aplikasi keluar");
                }
                else
                {
                    // Jika panel belum aktif, tampilkan panel keluar
                    exitPanel.SetActive(true);
                }
            }
        }
    }

    // Fungsi untuk menutup panel keluar (dari tombol "Batal")
    public void CloseExitPanel()
    {
        if (exitPanel != null)
        {
            exitPanel.SetActive(false);
        }
    }

    // Fungsi untuk keluar dari aplikasi (dari tombol "Keluar")
    public void QuitApplication()
    {
        Application.Quit();
        Debug.Log("Aplikasi keluar");
    }
}
