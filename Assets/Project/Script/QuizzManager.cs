using UnityEngine;
using UnityEngine.UI;

public class QuizzManager : MonoBehaviour
{
    public GameObject playPanel;
    public GameObject levelPanel;
    public GameObject quizPanel;
    public Text questionText;
    public Button[] answerButtons;
    public GameObject resultPanel;
    public Text resultText;
    public AudioSource correctSound;
    public AudioSource wrongSound;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public Button level2Button;
    public Button level3Button;
    public Button level4Button;
    public Button level5Button;
    public Button level6Button;
    public Button level7Button;
    public Button level8Button;
    public Button level9Button;
    public Button level10Button;

    public Button backButton;
    public Button backButton2;
    public Button backButton3;

    private int currentQuestionIndex = 0;
    private int score = 0;
    private const int pointsPerQuestion = 10;

    [System.Serializable]
    public class Question
    {
        public string questionText;
        public string[] answers;
        public int correctAnswerIndex;
    }

    public Question[] level1Questions;
    public Question[] level2Questions;
    public Question[] level3Questions;
    public Question[] level4Questions;
    public Question[] level5Questions;
    public Question[] level6Questions;
    public Question[] level7Questions;
    public Question[] level8Questions;
    public Question[] level9Questions;
    public Question[] level10Questions;

    private Question[] currentQuestions;

    private bool[] levelCompleted = new bool[10];
    public Text errorMessageText;

    void Start()
    {
        playPanel.SetActive(true);
        levelPanel.SetActive(false);
        quizPanel.SetActive(false);
        resultPanel.SetActive(false);
        errorMessageText.gameObject.SetActive(false);

        // Muat status level dari PlayerPrefs
        for (int i = 0; i < levelCompleted.Length; i++)
        {
            levelCompleted[i] = PlayerPrefs.GetInt("Level" + (i + 1) + "Completed", 0) == 1;
        }

        UpdateLevelButtons();

        backButton.onClick.AddListener(OnBackButtonClicked);
        backButton2.onClick.AddListener(OnBackButtonClicked2);
        backButton3.onClick.AddListener(OnBackButtonClicked3);
    }

    public void OnPlayButtonClicked()
    {
        playPanel.SetActive(false);
        levelPanel.SetActive(true);
    }

    public void OnLevelSelected(int level)
    {
        // Cek jika level adalah level 1 atau level sebelumnya sudah selesai
        if (level == 1 || levelCompleted[level - 2])
        {
            levelPanel.SetActive(false);
            quizPanel.SetActive(true);

            switch (level)
            {
                case 1: currentQuestions = level1Questions; break;
                case 2: currentQuestions = level2Questions; break;
                case 3: currentQuestions = level3Questions; break;
                case 4: currentQuestions = level4Questions; break;
                case 5: currentQuestions = level5Questions; break;
                case 6: currentQuestions = level6Questions; break;
                case 7: currentQuestions = level7Questions; break;
                case 8: currentQuestions = level8Questions; break;
                case 9: currentQuestions = level9Questions; break;
                case 10: currentQuestions = level10Questions; break;
            }

            currentQuestionIndex = 0;
            score = 0;
            ShowQuestion();
        }
        else
        {
            ShowErrorMessage("Selesaikan level " + (level - 1) + " dengan skor 70 untuk membuka level " + level + ".");
        }
    }

    void ShowErrorMessage(string message)
    {
        errorMessageText.text = message;
        errorMessageText.gameObject.SetActive(true);
    }

    public void OnAnswerSelected(int index)
    {
        if (index == currentQuestions[currentQuestionIndex].correctAnswerIndex)
        {
            correctSound.Play();
            score += pointsPerQuestion;
        }
        else
        {
            wrongSound.Play();
        }

        currentQuestionIndex++;

        if (currentQuestionIndex < currentQuestions.Length)
        {
            ShowQuestion();
        }
        else
        {
            ShowResult();
        }
    }

    void ShowQuestion()
    {
        questionText.text = currentQuestions[currentQuestionIndex].questionText;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = currentQuestions[currentQuestionIndex].answers[i];
            answerButtons[i].interactable = true;
        }
    }

        void ShowResult()
    {
        quizPanel.SetActive(false);
        resultPanel.SetActive(true);
        resultText.text = "Score Anda : " + score.ToString();

        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);

        if (score >= 80)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
        }
        else if (score >= 60)
        {
            star1.SetActive(true);
            star2.SetActive(true);
        }
        else if (score >= 30)
        {
            star1.SetActive(true);
        }

        int levelIndex = System.Array.IndexOf(new[] { level1Questions, level2Questions, level3Questions, level4Questions, level5Questions, level6Questions, level7Questions, level8Questions, level9Questions, level10Questions }, currentQuestions);

        // Pastikan hanya membuka level berikutnya jika level belum pernah terbuka sebelumnya
        if (score >= 70 && !levelCompleted[levelIndex] && PlayerPrefs.GetInt("Level" + (levelIndex + 2) + "Completed", 0) == 0)
        {
            levelCompleted[levelIndex] = true;
            PlayerPrefs.SetInt("Level" + (levelIndex + 1) + "Completed", 1);  // Menyimpan level saat ini sebagai selesai
            PlayerPrefs.SetInt("Level" + (levelIndex + 2) + "Completed", 1);  // Membuka level berikutnya
            resultText.text += "\nLuar biasa! Sekarang Anda bisa melanjutkan ke Level " + (levelIndex + 2) + ".";
        }
        else if (score < 70)
        {
            resultText.text += "\nScore Anda kurang dari 70. Tidak bisa melanjutkan ke level berikutnya.";
        }

        UpdateLevelButtons();
    }



    void UpdateLevelButtons()
    {
        Button[] levelButtons = { level2Button, level3Button, level4Button, level5Button, level6Button, level7Button, level8Button, level9Button, level10Button };

        for (int i = 0; i < levelButtons.Length; i++)
        {
            bool isUnlocked = levelCompleted[i];

            // Mengatur warna tombol berdasarkan status terbuka/tidaknya level
            ColorBlock cb = levelButtons[i].colors;
            
            if (isUnlocked)
            {
                cb.normalColor = Color.white;      // Warna untuk level yang sudah terbuka
                cb.highlightedColor = Color.cyan;   // Warna sorotan untuk level yang sudah terbuka
                levelButtons[i].interactable = true;
            }
            else
            {
                cb.normalColor = Color.grey;       // Warna untuk level yang masih terkunci
                cb.highlightedColor = Color.grey;  // Warna sorotan untuk level yang masih terkunci
                levelButtons[i].interactable = false;
            }
            
            levelButtons[i].colors = cb;
        }
    }


    void OnBackButtonClicked()
    {
        resultPanel.SetActive(false);
        levelPanel.SetActive(true);
        errorMessageText.gameObject.SetActive(false);
    }

    void OnBackButtonClicked2()
    {
        levelPanel.SetActive(false);
        playPanel.SetActive(true);
        errorMessageText.gameObject.SetActive(false);
    }

    void OnBackButtonClicked3()
    {
        quizPanel.SetActive(false);
        levelPanel.SetActive(true);
        errorMessageText.gameObject.SetActive(false);
    }
}
