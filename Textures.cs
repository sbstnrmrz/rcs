using Raylib_cs;
using System.Numerics;
using System.Text.Json;

public class TextureJSON {
    Dictionary<string, object>? dict;
    public TextureJSON(string jsonFile) {
        string textureInfoJSON = File.ReadAllText(jsonFile);
        dict = JsonSerializer.Deserialize<Dictionary<string, object>>(textureInfoJSON);
        if (dict == null) {
            return;
        }

    }

    public TextureInfo GetTextureInfo(string textureID) {
        if (dict.TryGetValue(textureID, out object? value)) {
            if (dict == null) {
                Console.WriteLine("error getting value: " + textureID);
                return new TextureInfo(0, 0, 0, 0, 0);
            }
            return JsonSerializer.Deserialize<TextureInfo>(value.ToString());
        }

        return new TextureInfo(0, 0, 0, 0, 0);
    }
}

public class TextureAtlas {

    public TextureAtlas() {

    }
}

public class TextureInfo {
    public string id = "";
    public int x {get; set;}
    public int y {get; set;}
    public int rotation {get; set;}
    public int width {get; set;}
    public int height {get; set;}

    public TextureInfo(int x, int y, int rotation, int width, int height) {
        this.x = x;
        this.y = y;
        this.rotation = rotation;
        this.width = width;
        this.height = height;
    }
}

public static class Textures {
    public static Texture2D arrow = Raylib.LoadTexture("arrow.png");
    public static Texture2D fireball = Raylib.LoadTexture("assets/fireball.png");
    public static Texture2D waterball = Raylib.LoadTexture("assets/waterball.png");
    public static Texture2D iceshard = Raylib.LoadTexture("assets/iceshard.png");
    public static Texture2D pointers = Raylib.LoadTexture("assets/pointers.png");
    public static TextureJSON pointersInfo = new TextureJSON("assets/pointers.json");
}
