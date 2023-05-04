using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditorInternal;
using UnityEditor;

namespace NReflect.Unity
{
    public static class NReflectUnityTools
    {
        [MenuItem("NReflect/Export Selected Assembly To Json")]
        public static void ExportAssemblyDefinitions()
        {
            var selectedAssembly = GetSelectedAssembly();
            var assemblies = selectedAssembly.Select(assemblyDefinitionAsset =>
                    AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(asm => asm.GetName().Name == assemblyDefinitionAsset.name))
                .Where(x => x != null);

            var reflector = new Reflector();
            foreach (var assembly in assemblies)
            {
                var nrAssembly = reflector.Reflect(assembly);
                var fileName = "Assets/" + assembly.GetName().Name + ".json";
                File.WriteAllText(fileName, JsonConvert.SerializeObject(nrAssembly));
                Debug.Log($"NReflect: Assembly '{assembly.GetName().FullName}' exported to '{fileName}'");
            }
        }

        private static IEnumerable<AssemblyDefinitionAsset> GetSelectedAssembly() =>
            Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets).OfType<AssemblyDefinitionAsset>();
    }
}