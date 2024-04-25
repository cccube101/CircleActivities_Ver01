using System.Linq;
using UnityEditor;
using System.IO;
using System;
using UnityEditorInternal;
using UnityEngine;

namespace UnityCodeGen
{
    [Generator]
    public class ExEnumGenerator : ICodeGenerator
    {
        // 無効な文字を管理する配列
        private static readonly string[] INVALUD_CHARS =
        {
            " ", "!", "\"", "#", "$",
            "%", "&", "\'", "(", ")",
            "-", "=", "^",  "~", "\\",
            "|", "[", "{",  "@", "`",
            "]", "}", ":",  "*", ";",
            "+", "/", "?",  ".", ">",
            "<", ",",
        };

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="context"></param>
        public void Execute(GeneratorContext context)
        {
            context.OverrideFolderPath("Assets/00_GameData/Scripts/00_Base");

            //  シーンEnum
            var scenes = EditorBuildSettings.scenes
            .Select(x => Path.GetFileNameWithoutExtension(x.path))
            .Distinct()
            .Select(c => $"{RemoveInvalidChars(c)},");
            var sceneFields = string.Join("\r\n\t", scenes);

            //  タグEnum
            var tags = InternalEditorUtility.tags
            .Select(x => $"public const string {x.Replace(" ", string.Empty)} = \"{x}\";");
            var tagFields = string.Join("\r\n\t", tags);

            //  レイヤーEnum
            var layers = InternalEditorUtility.layers
            .Select(x => $"public const int {x.Replace(" ", string.Empty)} = {LayerMask.NameToLayer(x)};");
            var layerFields = string.Join("\r\n\t", layers);
            var maskTags = InternalEditorUtility.layers
                .Select(x => $"public const int {x.Replace(" ", string.Empty)}Mask = {1 << LayerMask.NameToLayer(x)};");
            var maskFields = string.Join("\r\n\t", maskTags);

            var code =
@$"/// <summary>
/// シーン名を管理する Enum
/// </summary>
public enum SceneEnum
{{
    {sceneFields}
}}

/// <summary>
/// タグ名を文字列で管理するクラス
/// </summary>
public static class TagName
{{
    {tagFields}
}}

/// <summary>
/// レイヤー名を定数で管理するクラス
/// </summary>
public static class LayerName
{{
    {layerFields}

    {maskFields}
}}";
            context.AddCode("ExEnum.cs", code);
        }

        /// <summary>
        /// 無効な文字を削除します
        /// </summary>
        private static string RemoveInvalidChars(string str)
        {
            Array.ForEach(INVALUD_CHARS, c => str = str.Replace(c, string.Empty));
            return str;
        }
    }
}