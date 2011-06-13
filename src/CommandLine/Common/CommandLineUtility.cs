using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NuGet.Common;

namespace NuGet {
    internal static class CommandLineUtility {
        public readonly static string ApiKeysSectionName = "apikeys";
        
        public static string GetApiKey(IPackageSourceProvider sourceProvider, ISettings settings, string source, bool throwIfNotFound = true) {
            var value = settings.GetDecryptedValue(CommandLineUtility.ApiKeysSectionName, source);
            if (String.IsNullOrEmpty(value) && throwIfNotFound) {
                throw new CommandLineException(NuGetResources.NoApiKeyFound, sourceProvider.GetDisplayName(source));
            }
            return value;
        }

        public static void ValidateSource(string source) {
            if (!PathValidator.IsValidUrl(source)) {
                throw new CommandLineException(NuGetResources.InvalidSource, source);
            }
        }

        public static string GetUnambiguousFile(string searchPattern) {
            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), searchPattern);
            if (files.Length == 1) {
                return files[0];
            }

            return null;
        }

#if DEBUG
        internal static void WaitForDebugger() {
            System.Console.WriteLine("Waiting for debugger");
            while (!System.Diagnostics.Debugger.IsAttached) {

            }
        }
#endif
    }
}