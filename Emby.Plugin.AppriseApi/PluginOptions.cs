// ReSharper disable InconsistentNaming

using System;
using System.ComponentModel;
using Emby.Web.GenericEdit;
using Emby.Web.GenericEdit.Validation;
using MediaBrowser.Model.Attributes;
using MediaBrowser.Model.Logging;

namespace Emby.Plugin.AppriseApi
{
    public class PluginOptions : EditableOptionsBase
    {
        public override string EditorTitle => "Plugin Options";

        public override string EditorDescription => "Get notified about server events via an Apprise API instance.\n"
                                                    + "The options below are just a few examples for creating UI elements.";

        [DisplayName("Output Folder")]
        [Description("Please choose a folder for plugin output")]
        [EditFolderPicker]
        public string TargetFolder { get; set; }

        [Description("The log level determines how messages will be logged")]
        public LogSeverity LogLevel { get; set; }

        [Description("Apprise server URL, including http(s):// and port if needed")]
        [Required]
        public string AppriseServerURL { get; set; }
        
        [Description("Configuration Key from Apprise API Configuration Manager")]
        public string AppriseConfigurationKey { get; set; }

        protected override void Validate(ValidationContext context)
        {
            var validateUrl = Uri.TryCreate(AppriseServerURL, UriKind.Absolute, out var uriResult);
            if (!validateUrl)
            {
                context.AddValidationError(nameof(AppriseServerURL), "Provided URL is not valid");
            }

            if (uriResult.Scheme != Uri.UriSchemeHttp || uriResult.Scheme != Uri.UriSchemeHttps)
            {
                context.AddValidationError(nameof(AppriseServerURL), "URL scheme is not valid. Must be HTTP or HTTPS");
            }
        }
    }
}
