using UnityEditor;

public class Build
{
    [MenuItem("File/Build All")]
    public static void BuildAll()
    {
        BuildWindows();
        // BuildWeb();
    }

    [MenuItem("File/Build Windows")]
    public static void BuildWindows()
    {
        BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, "./Builds/Windows/TowerDefense2D.exe", BuildTarget.StandaloneWindows, BuildOptions.None);
    }

    //[MenuItem("File/Build Web")]
    //public static void BuildWeb()
    //{
    //    BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, "./Builds/Web", BuildTarget.WebGL, BuildOptions.None);
    //}
}
