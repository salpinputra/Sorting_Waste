using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsMenuController : MonoBehaviour
{
    public GameObject settingsPanel; // Panel pengaturan yang berisi pilihan tombol
    public Button settingsButton; // Tombol untuk membuka pengaturan
    public Button backButton; // Tombol untuk kembali ke menu sebelumnya
    private Animator panelAnimator; // Animator untuk panel pengaturan

    void Start()
    {
        // Panel pengaturan disembunyikan pada awalnya
        settingsPanel.SetActive(false); // Panel awalnya tidak aktif

        // Ambil komponen Animator dari settingsPanel
        panelAnimator = settingsPanel.GetComponent<Animator>();

        // Menambahkan fungsi pada tombol Settings untuk membuka pengaturan
        settingsButton.onClick.AddListener(OpenSettings);

        // Menambahkan fungsi pada tombol Back untuk menutup pengaturan
        backButton.onClick.AddListener(CloseSettings);
    }

    // Fungsi untuk membuka panel pengaturan
    void OpenSettings()
    {
        settingsPanel.SetActive(true); // Aktifkan panel pengaturan
        panelAnimator.SetTrigger("open"); // Memulai animasi masuk (FadeIn)
    }

    // Fungsi untuk menutup panel pengaturan
    void CloseSettings()
    {
        panelAnimator.SetTrigger("close"); // Memulai animasi keluar (FadeOut)
        StartCoroutine(DisablePanelAfterAnimation()); // Menunggu animasi selesai sebelum menyembunyikan panel
    }

    // Fungsi untuk menunggu animasi selesai dan menyembunyikan panel
    private IEnumerator DisablePanelAfterAnimation()
    {
        // Menunggu selama durasi animasi FadeOut
        yield return new WaitForSeconds(panelAnimator.GetCurrentAnimatorStateInfo(0).length);
        settingsPanel.SetActive(false); // Menyembunyikan panel setelah animasi selesai
    }
}
