using System;
using AltTester.AltTesterUnitySDK;
using AltTester.AltTesterUnitySDK.Editor;
using AltTester.AltTesterUnitySDK.Editor.Logging;
using UnityEditor;
using UnityEditor.Build.Reporting;

public class BuildAltTicTacToe
{
    private static readonly NLog.Logger logger = EditorLogManager.Instance.GetCurrentClassLogger();


    [MenuItem("Build/Mac")]
    protected static void MacBuildFromCommandLine()
    {
        try
        {
            SetCommonSettings(BuildTargetGroup.Standalone);

            PlayerSettings.fullScreenMode = UnityEngine.FullScreenMode.Windowed;
            PlayerSettings.defaultScreenHeight = 1080;
            PlayerSettings.defaultScreenWidth = 1920;

            logger.Debug("Starting Mac build..." + PlayerSettings.productName + " : " + PlayerSettings.bundleVersion);
            var buildPlayerOptions = GetBuildPlayerOptions("altTicTacToe", BuildTarget.StandaloneOSX);
            buildGame(buildPlayerOptions, BuildTargetGroup.Standalone);

        }
        catch (Exception exception)
        {
            logger.Error(exception);
        }

    }



    [MenuItem("Build/Android")]
    protected static void AndroidBuildFromCommandLine()
    {
        try
        {
            SetCommonSettings(BuildTargetGroup.Android);

            PlayerSettings.Android.bundleVersionCode = int.Parse(PlayerSettings.bundleVersion);
            PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel23;
#if UNITY_2018_1_OR_NEWER
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7;
#endif
            logger.Debug("Starting Android build..." + PlayerSettings.productName + " : " + PlayerSettings.bundleVersion);
            var buildPlayerOptions = GetBuildPlayerOptions("altTicTacToe.apk", BuildTarget.Android, false);

            buildGame(buildPlayerOptions, BuildTargetGroup.Android);

        }
        catch (Exception exception)
        {
            logger.Error(exception);
        }

    }

    private static AltInstrumentationSettings getInstrumentationSettings()
    {
        var instrumentationSettings = new AltInstrumentationSettings();

        var host = System.Environment.GetEnvironmentVariable("ALTSERVER_HOST");
        if (!string.IsNullOrEmpty(host))
        {
            instrumentationSettings.AltServerHost = host;
        }

        var port = System.Environment.GetEnvironmentVariable("ALTSERVER_PORT");
        if (!string.IsNullOrEmpty(port))
        {
            instrumentationSettings.AltServerPort = int.Parse(port);
        }
        else
        {
            instrumentationSettings.AltServerPort = 13010;
        }
        
        return instrumentationSettings;
    }
    public static string[] GetScenes()
    {
        return new string[]
                {
                    "Assets/Scenes/StartScene.unity",
                    "Assets/Scenes/PlayScene.unity"
                };
    }
    private static void buildGame(BuildPlayerOptions buildPlayerOptions, BuildTargetGroup targetGroup)
    {
        var instrumentationSettings = getInstrumentationSettings();

        AltBuilder.InsertAltInScene(buildPlayerOptions.scenes[0], instrumentationSettings);

        var results = BuildPipeline.BuildPlayer(buildPlayerOptions);
        AltBuilder.RemoveAltTesterFromScriptingDefineSymbols(targetGroup);
        HandleResults(results);
    }

    private static void HandleResults(BuildReport results)
    {
#if UNITY_2017
        if (results.Equals(""))
        {
            logger.Info("Build succeeded!");
            // EditorApplication.Exit(0);

        }
        else
            {
                logger.Error("Build failed!");
                // EditorApplication.Exit(1);
            }

#else
        if (results.summary.totalErrors == 0)
        {
            logger.Info("Build succeeded!");
        }
        else
        {
            logger.Error("Total Errors: " + results.summary.totalErrors);
            logger.Error("Build failed! " + results.steps + "\n Result: " + results.summary.result + "\n Stripping info: " + results.strippingInfo);
            // EditorApplication.Exit(1);
        }

#endif

        logger.Info("Finished. " + PlayerSettings.productName + " : " + PlayerSettings.bundleVersion);
        // EditorApplication.Exit(0);
    }
    public static void SetCommonSettings(BuildTargetGroup targetGroup)
    {
        string versionNumber = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
        PlayerSettings.companyName = "AltTester";
        PlayerSettings.productName = "AltTicTacToe";
        PlayerSettings.bundleVersion = versionNumber;
        PlayerSettings.SetApplicationIdentifier(targetGroup, "com.alttester.altTickTacToe");
        PlayerSettings.SetApiCompatibilityLevel(targetGroup, ApiCompatibilityLevel.NET_4_6);
        AltBuilder.AddAltTesterInScriptingDefineSymbolsGroup(targetGroup);

    }
    public static BuildPlayerOptions GetBuildPlayerOptions(string locationPathName, BuildTarget target, bool autorun = true)
    {
        return new BuildPlayerOptions
        {
            scenes = GetScenes(),

            locationPathName = locationPathName,
            target = target,
            options = BuildOptions.Development | BuildOptions.IncludeTestAssemblies | (autorun ? BuildOptions.AutoRunPlayer : BuildOptions.None)
        };
    }
}