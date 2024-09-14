using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitQuestions : MonoBehaviour
{


    /* public variables that hold references assigned in the Editor */
    /****************************************************************/
    public TextMeshProUGUI questionsText;   // reference to QuestionsText
    public TMP_InputField inputField;       // reference to InputField
    
    public GameObject correctAnswerPrompt;  // reference to CorrectAnswerPrompt
    public GameObject nextQuestionButton;   // reference to NextQuestionButton
    public GameObject seeResultsButton;     // reference to SeeResultsButton
    public GameObject wrongAnswerPrompt;    // reference to WrongAnswerPrompt
    public GameObject instruction;          // reference to Instruction
    public GameObject submitButton;         // reference to SubmitButton

    public GameObject[] diagrams;           // array of references to the QuestionDiagrams
    public GameObject[] hints;              // array of references to the HintDiagrams
    /****************************************************************/

    // index referring to the currentQuestion
    public int currentQuestion; 
    
    // array holding all of Unit One's questions
    private Question[] questions = new Question[3];

    // int variable holding the unit of the gameobject that called UnitQuestions
    private int unit;

    /* Class definition of Question */
    private class Question
    {
        // three attributes: the question, its answer, and the number of tries the user took
        public string question;
        public string answer;
        public int tries;

        // Question constructor
        public Question(string que, string ans)
        {
            question = que;
            answer = ans;

            // Start tries at 1
            tries = 1;
        }
        
    }

    // Start(): Is called as soon as the first lesson begins
    void Start()
    {
        // Determine the current unit
        switch (gameObject.name)
        {
            case "Unit1Questions":
                unit = 1;
                break;
            case "Unit2Questions":
                unit = 2;
                break;
            case "Unit3Questions":
                unit = 3;
                break;
            case "Unit4Questions":
                unit = 4;
                break;
            case "Unit5Questions":
                unit = 5;
                break;
            default:
                Debug.Log("Cannot determine the unit");
                break;
        }
            

        // Check if UnitXQuestions is active in the scene
        if (gameObject.activeSelf == true)
        {
            // Activate all of UnitX's attributes
            gameObject.transform.Find("QuestionText").gameObject.SetActive(true);
            gameObject.transform.Find("QuestionDiagrams").gameObject.SetActive(true);
            gameObject.transform.Find("HelpButton").gameObject.SetActive(true);
            gameObject.transform.Find("ReturnButton").gameObject.SetActive(true);
            gameObject.transform.Find("AnswerField").gameObject.SetActive(true);
            gameObject.transform.Find("SubmitButton").gameObject.SetActive(true);
            gameObject.transform.Find("Instruction").gameObject.SetActive(true);
            gameObject.transform.Find("HintDiagrams").gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("UnitXQuestions not active in the scene");
        }
        

        // Deactivate WrongAnswerPrompt
        wrongAnswerPrompt.SetActive(false);

        // Fill questions[] with the (question, answer) pairs
        switch (unit)
        {
            case 1:
                questions[0] = new Question("What is the value of a?", "8");
                questions[1] = new Question("What is the value of b?", "15");
                questions[2] = new Question("What is the value of x?", "11");
                break;
            case 2:
                questions[0] = new Question("Enter the missing percentage.", "25");
                questions[1] = new Question("Enter the missing fraction, in its simplest form", "3/4");
                questions[2] = new Question("Enter the missing decimal", "0.04");
                break;
            case 3:
                questions[0] = new Question("Complete the equation.", "5");
                questions[1] = new Question("Complete the equation.", "7");
                questions[2] = new Question("Complete the equation.", "2");
                break;
            case 4:
                questions[0] = new Question("Solve for I.", "3");
                questions[1] = new Question("Solve for C.", "2");
                questions[2] = new Question("Solve for I.", "6");
                break;
            case 5:
                questions[0] = new Question("What is the slope, m, of the following line?", "7");
                questions[1] = new Question("What is the y-intercept, b, of the following line?", "5");
                questions[2] = new Question("What is the slope of a line with the following two points?", "3");
                break;
            default:
                Debug.Log("Cannot load questions");
                break;
        }
        

        // Set the current question to the first one
        currentQuestion = 0;

        // Call DisplayQuestionText()
        DisplayQuestionText();
        
    }

    // DisplayQuestionText(): displays the question text at the top of the screen
    void DisplayQuestionText()
    {
        // Set the question text to "QX: question", where X is 1, 2, or 3
        questionsText.text = "Q" + (currentQuestion + 1) + ": " + (questions[currentQuestion].question);
    }

    // OnClickHelp(): determines what hint to display when HelpButton is clicked
    public void OnClickHelp()
    {
        // disable question elements
        gameObject.transform.Find("QuestionText").gameObject.SetActive(false);
        gameObject.transform.Find("QuestionDiagrams").gameObject.SetActive(false);
        gameObject.transform.Find("HelpButton").gameObject.SetActive(false);
        gameObject.transform.Find("ReturnButton").gameObject.SetActive(false);
        gameObject.transform.Find("AnswerField").gameObject.SetActive(false);
        gameObject.transform.Find("SubmitButton").gameObject.SetActive(false);
        gameObject.transform.Find("Instruction").gameObject.SetActive(false);

        //Display the appropriate hint image, text, and button
        hints[currentQuestion].SetActive(true);
        gameObject.transform.Find("HintText").gameObject.SetActive(true);
        gameObject.transform.Find("ReturnToQuestionButton").gameObject.SetActive(true);
    }

    // OnReturnToQuestion(): bring the user back to the current question
    public void OnReturnToQuestion()
    {
        // enable question elements
        gameObject.transform.Find("QuestionText").gameObject.SetActive(true);
        gameObject.transform.Find("QuestionDiagrams").gameObject.SetActive(true);
        gameObject.transform.Find("HelpButton").gameObject.SetActive(true);
        gameObject.transform.Find("ReturnButton").gameObject.SetActive(true);
        gameObject.transform.Find("AnswerField").gameObject.SetActive(true);
        gameObject.transform.Find("SubmitButton").gameObject.SetActive(true);
        gameObject.transform.Find("Instruction").gameObject.SetActive(true);

        // disable the appropriate hint image, text, and button
        hints[currentQuestion].SetActive(false);
        gameObject.transform.Find("HintText").gameObject.SetActive(false);
        gameObject.transform.Find("ReturnToQuestionButton").gameObject.SetActive(false);
    }
    

    // OnSubmit(): determines which action to take when the user clicks SubmitButton
    public void OnSubmit()
    {
        // If the user's answer is correct
        if (inputField.text.Equals(questions[currentQuestion].answer))
        {
            // If *not* on the final question
            if (currentQuestion < (questions.Length - 1))
            {
                // Deactivate WrongAnswerPrompt, SubmitButton, and Instruction
                wrongAnswerPrompt.SetActive(false);
                submitButton.SetActive(false);
                instruction.SetActive(false);

                // Activate CorrectAnswerPrompt and NextQuestionButton
                correctAnswerPrompt.SetActive(true);
                nextQuestionButton.SetActive(true);
                
            }
            // If on the final question
            else
            {
                // Deactivate WrongAnswerPrompt, SubmitButton, and Instruction
                wrongAnswerPrompt.SetActive(false);
                submitButton.SetActive(false);
                instruction.SetActive(false);

                // Activate CorrectAnswerPrompt and SeeResultsButton
                correctAnswerPrompt.SetActive(true);
                seeResultsButton.SetActive(true);
                
            }
        }
        // If the user's answer is *not* correct
        else
        {
            // Activate WrongAnswerPrompt
            wrongAnswerPrompt.SetActive(true);

            // Increment the number of tries
            questions[currentQuestion].tries++;
        }
            
    }

    // OnContinue(): determines what to do when the user clicks NextQuestionButton
    public void OnContinue()
    {
        // Verify that the user is not on the final question
        if (currentQuestion < (questions.Length-1))
        { 
            // Deactivate CorrectAnswerPrompt and NextQuestionButton
            correctAnswerPrompt.SetActive(false);
            nextQuestionButton.SetActive(false);

            // Activate SubmitButton and Instruction
            submitButton.SetActive(true);
            instruction.SetActive(true);

            // Update the diagram to the next question
            diagrams[currentQuestion].SetActive(false);
            currentQuestion++;
            diagrams[currentQuestion].SetActive(true); 

            // Clear InputField
            inputField.text = "";

            // Call DisplayQuestionText
            DisplayQuestionText();
        }
        // Otherwise, log an error in the Console
        else
        {
            Debug.Log("NextQuestionButton clicked on the final question");
        }
    }

    // ViewResults: View the user's results upon clicking SeeResultsButton
    public void ViewResults()
    {
        GameObject results = GameObject.Find("Unit"+unit+"Results");

        // Check if is active in the scene
        if (results.activeSelf == true)
        {
            // Get references to QXResults ( X = {1, 2, 3} )
            TextMeshProUGUI q1Results = results.transform.Find("Q1Results").gameObject.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI q2Results = results.transform.Find("Q2Results").gameObject.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI q3Results = results.transform.Find("Q3Results").gameObject.GetComponent<TextMeshProUGUI>();

            // Display the number of attempts it took the user to answer correctly
            q1Results.text = "Q1: " + questions[0].tries + " attempt(s)";
            q2Results.text = "Q2: " + questions[1].tries + " attempt(s)";
            q3Results.text = "Q3: " + questions[2].tries + " attempt(s)";

        }
        else
        {
            Debug.Log("UnitXResults is not active in the scene");
        }
    }

    
}
