using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Reporting;
using System;
using System.Collections;
using System.Collections.Generic;


class Builder {
    static string[] SCENES = FindEnabledEditorScenes();
    static string APP_NAME = "android_build_lab";

    [MenuItem ("Custom/CI/Build Android")]
    static void PerformAndroidBuild ()
    {
         string target_filename = APP_NAME + ".apk";
         GenericBuild(SCENES, target_filename, BuildTarget.Android ,BuildOptions.None);
    }

 private static string[] FindEnabledEditorScenes() {
  List<string> EditorScenes = new List<string>();
  foreach(EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
   if (!scene.enabled) continue;
   EditorScenes.Add(scene.path);
  }
  return EditorScenes.ToArray();
 }

    static void GenericBuild(string[] scenes, string target_filename, BuildTarget build_target, BuildOptions build_options)
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(build_target);
        BuildReport report = BuildPipeline.BuildPlayer(scenes, target_filename, build_target, build_options);
        BuildSummary summary = report.summary;
        if (summary.result == BuildResult.Failed) {
            throw new Exception("BuildPlayer failure");
        }
    }
}
