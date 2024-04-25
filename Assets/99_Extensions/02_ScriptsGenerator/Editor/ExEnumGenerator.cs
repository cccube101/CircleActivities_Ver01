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
        // �����ȕ������Ǘ�����z��
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
        /// ����
        /// </summary>
        /// <param name="context"></param>
        public void Execute(GeneratorContext context)
        {
            context.OverrideFolderPath("Assets/00_GameData/Scripts/00_Base");

            //  �V�[��Enum
            var scenes = EditorBuildSettings.scenes
            .Select(x => Path.GetFileNameWithoutExtension(x.path))
            .Distinct()
            .Select(c => $"{RemoveInvalidChars(c)},");
            var sceneFields = string.Join("\r\n\t", scenes);

            //  �^�OEnum
            var tags = InternalEditorUtility.tags
            .Select(x => $"public const string {x.Replace(" ", string.Empty)} = \"{x}\";");
            var tagFields = string.Join("\r\n\t", tags);

            //  ���C���[Enum
            var layers = InternalEditorUtility.layers
            .Select(x => $"public const int {x.Replace(" ", string.Empty)} = {LayerMask.NameToLayer(x)};");
            var layerFields = string.Join("\r\n\t", layers);
            var maskTags = InternalEditorUtility.layers
                .Select(x => $"public const int {x.Replace(" ", string.Empty)}Mask = {1 << LayerMask.NameToLayer(x)};");
            var maskFields = string.Join("\r\n\t", maskTags);

            var code =
@$"/// <summary>
/// �V�[�������Ǘ����� Enum
/// </summary>
public enum SceneEnum
{{
    {sceneFields}
}}

/// <summary>
/// �^�O���𕶎���ŊǗ�����N���X
/// </summary>
public static class TagName
{{
    {tagFields}
}}

/// <summary>
/// ���C���[����萔�ŊǗ�����N���X
/// </summary>
public static class LayerName
{{
    {layerFields}

    {maskFields}
}}";
            context.AddCode("ExEnum.cs", code);
        }

        /// <summary>
        /// �����ȕ������폜���܂�
        /// </summary>
        private static string RemoveInvalidChars(string str)
        {
            Array.ForEach(INVALUD_CHARS, c => str = str.Replace(c, string.Empty));
            return str;
        }
    }
}