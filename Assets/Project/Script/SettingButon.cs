using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class SettingsToggle : MonoBehaviour
{
    public GameObject settingsPanel; // Panel untuk pengaturan
    public GameObject settingButton; // Tombol Setting yang tidak memengaruhi panel
    private Animator settingsAnimator; // Animator untuk panel
    private bool isVisible = false; // Status panel

    void Start()
    {
        settingsAnimator = settingsPanel.GetComponent<Animator>();
        settingsPanel.SetActive(false); // Panel dimulai tersembunyi
    }

    public void ToggleSettings()
    {
        isVisible = !isVisible; // Ubah status
        if (isVisible)
        {
            settingsPanel.SetActive(true); // Aktifkan panel sebelum animasi
        }
        settingsAnimator.SetBool("IsVisible", isVisible); // Atur animasi

        // Jika panel akan disembunyikan, jalankan coroutine untuk menonaktifkan setelah animasi selesai
        if (!isVisible)
        {
            StartCoroutine(DeactivateAfterAnimation());
        }
    }

    public void CheckClick()
    {
        // Dapatkan objek yang diklik
        GameObject clickedObject = EventSystem.current.currentSelectedGameObject;

        // Jika tidak ada objek yang diklik atau itu adalah tombol Setting, abaikan
        if (clickedObject == null || clickedObject == settingButton)
            return;

        // Jika tombol lain diklik, tutup panel
        if (isVisible)
        {
            isVisible = false;
            settingsAnimator.SetBool("IsVisible", isVisible);
            StartCoroutine(DeactivateAfterAnimation());
        }
    }

    private IEnumerator DeactivateAfterAnimation()
    {
        yield return new WaitForSeconds(1.2f); // Sesuaikan durasi animasi
        settingsPanel.SetActive(false); // Sembunyikan panel
    }
}
