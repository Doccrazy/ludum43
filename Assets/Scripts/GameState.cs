using System;

public static class GameState {
    public static int Level = 1;
    public static int Loot;
    public static int Score;
    public static int Lives = 2;

    public static void ResetLevel() {
        Loot = Loot/2;
    }

    public static void ResetAll() {
        ResetLevel();
        Level = 1;
        Lives = 2;
        Score = 0;
    }

    public static int GetTotalScore() {
        return Score + Loot * 1000;
    }

    public static void AddScore(int amount) {
        Score = Math.Max(Score + amount, 0);
    }
}
