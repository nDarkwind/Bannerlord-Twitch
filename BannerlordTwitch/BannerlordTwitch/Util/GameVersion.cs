﻿using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;
using MonoMod.Utils;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace BannerlordTwitch.Util
{
    public static class GameVersion 
    {
        public static string GameVersionString { get; private set; }

        public static bool IsVersion(string version) => GameVersionString?.Contains(version) ?? false;

        static GameVersion()
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(VirtualFolders.GetFileContent(Path.Combine(BasePath.Name, "Parameters", "Version.xml")));
            GameVersionString = xmlDocument.SelectSingleNode("Version")?.SelectSingleNode("Singleplayer")?.Attributes?["Value"]?.InnerText;
        }
    }
}