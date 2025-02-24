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
                    if (mat[i, j] == (int)RoomManager.GridID.EnemySpellcaster) {
                        EnemyManager.Add(new EnemyRanger(GetEnemyPosFromMat(i, j)));
                        mat[i, j] = 0;
                        enemyCount++;
                    }
                    if (mat[i, j] == (int)RoomManager.GridID.EnemyMelee) {
                        EnemyManager.Add(new EnemyMelee(GetEnemyPosFromMat(i, j)));
                        mat[i, j] = 0;
                        enemyCount++;
                    }
                    if (mat[i, j] == (int)RoomManager.GridID.EnemyBouncer) {
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

    }

    public void Draw() {
//      Console.WriteLine(String.Format("rows: {0}, cols: {1}", mat.GetLength(0), mat.GetLength(1)));
        int counter = 0;
        for (int i = 0; i < mat.GetLength(0); i++) {
            for (int j = 0; j < mat.GetLength(1); j++) {
                Color color = Color.Blue;
                if (mat[i, j] == (int)RoomManager.GridID.Wall) {
                    color = Color.Gold;
                }
                if (mat[i, j] == (int)RoomManager.GridID.Door) {
                    color = Color.Gray;
                }
                if (mat[i, j] == (int)RoomManager.GridID.Rock) {
                    color = Color.Black;
                }
                Rectangle grid = new Rectangle(((Raylib.GetScreenWidth() - 32 * cols)/2) + j * 32,
                                               ((Raylib.GetScreenHeight() - 32 * rows)/2) + i * 32,
                                               32, 32);
                Raylib.DrawRectangleRec(grid, color);
                Raylib.DrawRectangleLinesEx(grid, 1f, Color.Black);
                Raylib.DrawText(String.Format("{0}", counter), 
                                ((Raylib.GetScreenWidth() - 32 * cols)/2) + j * 32,
                                ((Raylib.GetScreenHeight() - 32 * rows)/2) + i * 32,
                                15,
                                Color.Black);
                counter++;
            }
        }
//      for (int i = 0; i < 21; i++) {
//          Raylib.DrawRectangleRec(new Rectangle(50, 50 + i * 32, 32, 32), Color.Gold);
//          Raylib.DrawRectangleRec(new Rectangle(50 + 32 * 32, 50 + i * 32, 32, 32), Color.Gold);
//      }
//      for (int i = 0; i < 31; i++) {
//          Raylib.DrawRectangleRec(new Rectangle(50 + 32 + i * 32, 50, 32, 32), Color.Gold);
//          Raylib.DrawRectangleRec(new Rectangle(50 + 32 + i * 32, 50 + 32 * 20, 32, 32), Color.Gold);
//      }
    }

    public Vector2 GetEnemyPosFromMat(int i, int j) {
        return new Vector2(screenPos.X + j * 32, screenPos.Y + i * 32);
    }
}
