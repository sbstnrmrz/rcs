using Raylib_cs;
using System.Numerics;

[Serializable]
public class Room {
    public Vector2 screenPos = Vector2.Zero;
    public int rows = 20;
    public int cols = 34;
    public int enemyCount = 0;
    public bool isRoomCleaned = false;
    public int[,] mat = new int[20, 34]; 

    public Room() {
        for (int i = 0; i < rows; i++) {
            mat[i, 0] = 1;
            mat[i, cols-1] = 1;
        }
        for (int i = 0; i < cols; i++) {
            mat[0, i] = 1;
            mat[rows-1, i] = 1;
        }
    }

    public void Init() {
        for (int i = 0; i < mat.GetLength(0); i++) {
            for (int j = 0; j < mat.GetLength(1); j++) {
                if (mat[i, j] > 3) {
                    if (mat[i, j] == (int)RoomManager.GridID.EnemySpellCaster) {
                        EnemyManager.Add(new EnemySpellcaster(GetEnemyPosFromMat(i, j)));
                        mat[i, j] = 0;
                        enemyCount++;
                    }
                    if (mat[i, j] == (int)RoomManager.GridID.EnemyMelee) {
                        EnemyManager.Add(new EnemyMelee(GetEnemyPosFromMat(i, j)));
                        mat[i, j] = 0;
                        enemyCount++;
                    }
                    if (mat[i, j] == (int)RoomManager.GridID.EnemyBouncer) {
                        EnemyManager.Add(new EnemyBouncer(GetEnemyPosFromMat(i, j)));
                        mat[i, j] = 0;
                        enemyCount++;
                    }
                    if (mat[i, j] == (int)RoomManager.GridID.EnemyTeleporter) {
                        EnemyManager.Add(new EnemyTeleporter(GetEnemyPosFromMat(i, j)));
                        mat[i, j] = 0;
                        enemyCount++;
                    }
                    if (mat[i, j] == (int)RoomManager.GridID.EnemyCharge) {
                        EnemyManager.Add(new EnemyCharger(GetEnemyPosFromMat(i, j)));
                        mat[i, j] = 0;
                        enemyCount++;
                    }
                } else {
                    continue;
                }
            }
        }
         
    }

    public void Update() {
//      Console.WriteLine(EnemyManager.enemies.Count);
    }

    public Vector2 GetEnemyPosFromMat(int i, int j) {
        // not adding 32 because of taking it directly from matrix that already takes into account the existence of the walls
        return new Vector2(RoomManager.roomScreenPos.X + j * 32, RoomManager.roomScreenPos.Y + i * 32);
    }
}
