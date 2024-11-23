using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class swipe_menu : MonoBehaviour
{
    public GameObject scrollbar; // Scrollbar UI
    private float scroll_pos = 0f; // Posisi scroll saat ini
    private float[] pos; // Posisi target setiap item
    public float smoothTime = 0.2f; // Waktu untuk membuat transisi lebih smooth
    private float velocity = 0f; // Kecepatan untuk perhitungan Lerp

    // Update is called once per frame
    void Update()
    {
        // Menghitung posisi untuk setiap child
        int childCount = transform.childCount;
        pos = new float[childCount];
        float distance = 1f / (childCount - 1f);

        for (int i = 0; i < childCount; i++)
        {
            pos[i] = distance * i;
        }

        // Deteksi input
        if (Input.GetMouseButton(0))
        {
            // Mengambil nilai scroll saat ini
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            // Lerp ke posisi terdekat
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    float targetValue = Mathf.SmoothDamp(scrollbar.GetComponent<Scrollbar>().value, pos[i], ref velocity, smoothTime);
                    scrollbar.GetComponent<Scrollbar>().value = targetValue;
                }
            }
        }

        // Mengatur skala elemen untuk memberikan efek zoom
        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                // Perbesar elemen yang sedang aktif
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), Time.deltaTime * 10f);

                // Kecilkan elemen lainnya
                for (int a = 0; a < pos.Length; a++)
                {
                    if (a != i)
                    {
                        transform.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(a).localScale, new Vector2(0.8f, 0.8f), Time.deltaTime * 10f);
                    }
                }
            }
        }
    }
}
