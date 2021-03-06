﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;

namespace FrameworkProfiles
{
    public static class NugetTargets
    {
        // https://github.com/NuGet/NuGet3/blob/dev/src/NuGet.Frameworks/FrameworkConstants.cs
        // https://github.com/NuGet/NuGet3/blob/dev/src/NuGet.Frameworks/DefaultFrameworkMappings.cs

        private static readonly Dictionary<FrameworkName, string> KnownNugetTargets = new Dictionary<FrameworkName, string>
        {
            { new FrameworkName("Silverlight,Version=v4.0"), "sl4" },
            { new FrameworkName("Silverlight,Version=v5.0"), "sl5" },
            { new FrameworkName("Silverlight,Version=v4.0,Profile=WindowsPhone*"), "wp7" },
            { new FrameworkName("Silverlight,Version=v4.0,Profile=WindowsPhone7*"), "wp71" },

            // Prefer "win" TFMs to reduce confusion with the new .NET Core (which has nothing to do with the netcore TFM).
            { new FrameworkName(".NETCore,Version=v4.5,Profile=*"), "win8" },
            { new FrameworkName(".NETCore,Version=v4.5.1,Profile=*"), "win81" },
        };

        private static readonly Dictionary<string, string> KnownNugetPlatforms = new Dictionary<string, string>
        {
            { ".NETFramework", "net" },
            { "WindowsPhone", "wp" },
            { "WindowsPhoneApp", "wpa" },
            { "MonoAndroid", "monoandroid" },
            { "MonoTouch", "monotouch" },
            { "Xamarin.iOS", "xamarinios" },
        };

        public static string GetNugetTarget(FrameworkProfile profile)
        {
            string result;
            if (KnownNugetTargets.TryGetValue(profile.Name, out result))
                return result;
            string platform;
            if (!KnownNugetPlatforms.TryGetValue(profile.Name.Identifier, out platform))
                return string.Empty;
            var version = profile.Name.Version.ToString();
            while (version.EndsWith(".0"))
                version = version.Substring(0, version.Length - 2);

            // This is dangerous if any version number goes >= 10, but hey, that's NuGet's problem...
            return platform + version.Replace(".", string.Empty);
        }
    }
}
