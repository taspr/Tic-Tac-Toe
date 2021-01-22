using UnityEngine;

public class GamePlay : MonoBehaviour
{
    private UI ui;

    private enum Player
    {
        X = 0,
        O = 1
    }

    private int[,] gameState = new int[3, 3];

    private Player player = Player.X;
    private Player winner;
    private int moves = 9;

    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.Find("Canvas").GetComponent<UI>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                if (player == Player.X)
                {
                    hit.collider.transform.GetChild(1).gameObject.SetActive(true);
                    gameState[(int)hit.collider.transform.rotation.eulerAngles.x, (int)hit.collider.transform.rotation.eulerAngles.y] = 1;
                    hit.collider.GetComponent<BoxCollider2D>().enabled = false;
                    GetComponent<AudioSource>().Play();
                    CheckState();
                    player = Player.O;
                    moves--;
                }
                else
                {
                    hit.collider.transform.GetChild(0).gameObject.SetActive(true);
                    gameState[(int)hit.collider.transform.rotation.eulerAngles.x, (int)hit.collider.transform.rotation.eulerAngles.y] = 2;
                    hit.collider.GetComponent<BoxCollider2D>().enabled = false;
                    GetComponent<AudioSource>().Play();
                    CheckState();
                    player = Player.X;
                    moves--;
                }
            }

            if (moves <= 0)
            {
                ui.ShowWinner("Draw");
                ui.ShowUI();
            }
        }
    }

    private void CheckState()
    {
        for (int i = 0; i < gameState.GetLength(0); i++)
        {
            // vertical check |
            if (gameState[i, 0] != 0 && gameState[i, 1] != 0 && gameState[i, 2] != 0)
            {
                if (gameState[i, 0] == gameState[i, 1] && gameState[i, 1] == gameState[i, 2])
                {
                    DisplayWinner();
                }
            }

            // horizontal check --
            if (gameState[0, i] != 0 && gameState[1, i] != 0 && gameState[2, i] != 0)
            {
                if (gameState[0, i] == gameState[1, i] && gameState[1, i] == gameState[2, i])
                {
                    DisplayWinner();
                }
            }
        }

        // diagonal check \
        if (gameState[0, 0] != 0 && gameState[1, 1] != 0 && gameState[2, 2] != 0)
        {
            if (gameState[0, 0] == gameState[1, 1] && gameState[1, 1] == gameState[2, 2])
            {
                DisplayWinner();
            }
        }

        // diagonal check /
        if (gameState[0, 2] != 0 && gameState[1, 1] != 0 && gameState[2, 0] != 0)
        {
            if (gameState[0, 2] == gameState[1, 1] && gameState[1, 1] == gameState[2, 0])
            {
                DisplayWinner();
            }
        }
    }

    private void DisplayWinner()
    {
        winner = player;
        ui.ShowWinner($"Player {player.ToString()} won");
        ui.ShowUI();
        Destroy(this);
    }
}