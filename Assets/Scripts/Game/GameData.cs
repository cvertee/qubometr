using UnityEngine.SceneManagement;

public static class GameData
{
    private static SaveData data;

    public static SaveData Data
    {
        get
        {
            if (data == null)
            {
                data = new SaveData
                {
                    coins = 0,
                    hp = 50,
                    sceneName = SceneManager.GetActiveScene().name
                };
            }

            return data;
        }

        set { data = value; }
    }
}