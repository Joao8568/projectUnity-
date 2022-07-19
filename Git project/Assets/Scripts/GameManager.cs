using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <sumary>
///  Classe usada oara gerenciar o jogo
/// </sumary>
public class GameManager : MonoBehaviour
{
    [SerializeField] 
    private string guiName; // nome da fase de interface

    [SerializeField] 
    private string levelName; // nome da fase do jogo

    [SerializeField] 
    private GameObject playerAndCameraPrefab; // referencia jogador + camera
    
    
    // Start is called before the first frame update
    void Start()
    {
        //impede que o objeto indicado entre parenteses seja destuido 
        DontDestroyOnLoad(this.gameObject); // referência por objeto que contem o GameManager 
        // 1 - carregar o cena da interface do jogo 
        SceneManager.LoadScene(guiName);

        SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive).completed += operation =>
        {
            //inicializa a variavel para guardar a cena do level com o valor padrão (default)
            Scene levelScene = default;

            // econtrar cena de level que estar carregada 
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                // se o nome da cena na posição i do array for igual ao nome do level
                if (SceneManager.GetSceneAt(i).name == levelName)
                {
                    // associa a cena na posição i do arrauy na variavel 
                    levelScene = SceneManager.GetSceneAt(i);
                    break;
                }
            }

            if (levelScene != default) SceneManager.SetActiveScene(levelScene);
            // 2 - Precisa instanciar o jogador na cena 
            Vector3 playerStartPosition = GameObject.Find("PlayerSart").transform.position;

            Instantiate(original: playerAndCameraPrefab, playerStartPosition, Quaternion.identity);

        };


        // 3 - Começar a partida 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
