using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ImaginationOverflow.UniversalDeepLinking.Storage;
#if UNITY_STANDALONE_WIN 
using Microsoft.Win32;
#endif
using UnityEngine;

namespace ImaginationOverflow.UniversalDeepLinking.Providers
{
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR

    public class WindowsLinkProvider : ILinkProvider
    {
        private readonly bool _steamBuild;
        private string _scheme;

        public WindowsLinkProvider(bool steamBuild)
        {
            _steamBuild = steamBuild;
        }

        public bool Initialize()
        {
            var config = ConfigurationStorage.Load();

            var protocol = config.GetPlatformDeepLinkingProtocols(SupportedPlatforms.Windows,true).FirstOrDefault();
            if (protocol == null)
                return false;

            _scheme = protocol.Scheme;
            var activationProtocol = protocol.Scheme;
            var fromSteam = _steamBuild;
            var steamAppId = config.SteamId;

            try
            {

                var key = Registry.CurrentUser.OpenSubKey("Software", true);
                var classes = key.OpenSubKey("Classes", true);

                if (classes == null)
                    return false;

                var appkey = classes.OpenSubKey(activationProtocol);

                if (appkey != null)
                {
                    classes.DeleteSubKeyTree(activationProtocol);
                    appkey = null;
                }

                appkey = classes.CreateSubKey(activationProtocol);
                appkey.SetValue("", string.Format("URL:{0} Protocol", activationProtocol));
                appkey.SetValue("URL Protocol", "");

                string args;
                var executingExe = GetExe(fromSteam, steamAppId, out args);
                appkey
                    .CreateSubKey("shell")
                    .CreateSubKey("open")
                    .CreateSubKey("command")
                    .SetValue("", string.Format("\"{0}\" {1} \"%1\"", executingExe, args));


                var defaultIcon = appkey.CreateSubKey("DefaultIcon");
                defaultIcon.SetValue("", string.Format("\"{0}\",-1", ProviderHelpers.GetExecutingPath()));

            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return false;
            }
            return true;
        }

        private string GetExe(bool fromSteam, string steamAppId, out string args)
        {
            args = string.Empty;

           

            try
            {
                if (fromSteam && string.IsNullOrEmpty(steamAppId) == false)
                {
                    var steamExe = Registry.CurrentUser.OpenSubKey("Software", false)
                        .OpenSubKey("Classes")
                        .OpenSubKey("Steam")
                        .OpenSubKey("shell")
                        .OpenSubKey("open")
                        .OpenSubKey("command")
                        .GetValue("").ToString();
                    steamExe = steamExe.Replace(" \"%1\"", string.Empty).Replace("\"", String.Empty);
                    args = " -applaunch " + steamAppId;
                    return steamExe;
                }
            }
            catch (Exception)
            {


            }

            if (string.IsNullOrEmpty(LinkProviderFactory.DeferredExePath) == false)
            {
                return LinkProviderFactory.DeferredExePath;
            }

            return ProviderHelpers.GetExecutingPath();

        }

        private event Action<string> _linkReceived;
        public event Action<string> LinkReceived
        {
            add
            {
                _linkReceived += value;
                CheckArguments();
            }
            remove { _linkReceived -= value; }
        }

        private void CheckArguments()
        {
            if (string.IsNullOrEmpty(_scheme))
                return;

          
            var arg = Environment.GetCommandLineArgs().FirstOrDefault(a => a.StartsWith(_scheme));

            if (string.IsNullOrEmpty(arg) == false)
                OnLinkReceived(arg);
        }

        public void PollInfoAfterPause()
        {

        }

        protected virtual void OnLinkReceived(string obj)
        {
            var handler = _linkReceived;
            if (handler != null) handler(obj);
        }

        
    }

#endif
}
