using System;
using System.IO;
using MediaBrowser.Common;
using MediaBrowser.Common.Net;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Controller.Plugins;
using MediaBrowser.Model.Drawing;
using MediaBrowser.Model.Logging;

namespace Emby.Plugin.AppriseApi
{
    /// <summary>
    /// The plugin.
    /// </summary>
    public class Plugin : BasePluginSimpleUI<PluginOptions>, IHasThumbImage
    {
        /// <summary>The Plugin ID.</summary>
        private readonly Guid _id = new Guid("9f19e7af-0cd0-4370-967c-3a87fb63a7d2"); // << Generate one: Tools >> Create GUID

        private readonly ILogger _logger;

        /// <summary>Initializes a new instance of the <see cref="Plugin" /> class.</summary>
        /// <param name="applicationHost">The application host.</param>
        /// <param name="httpClient">A HTTP Client</param>
        /// <param name="logManager">The log manager.</param>
        public Plugin(
            IApplicationHost applicationHost,
            IHttpClient httpClient,
            ILogManager logManager) : base(applicationHost)
        {
            _logger = logManager.GetLogger(Name);
            _logger.Info("My plugin ({0}) is getting loaded", Name);
            HttpClient = httpClient;
        }

        /// <summary>Gets the description.</summary>
        /// <value>The description.</value>
        public override string Description => "Get notified about server events via an Apprise API instance";

        /// <summary>Gets the unique id.</summary>
        /// <value>The unique id.</value>
        public override Guid Id => _id;
        
        public static IHttpClient HttpClient { get; set; }

        /// <summary>Gets the name of the plugin</summary>
        /// <value>The name.</value>
        public sealed override string Name => "AppriseAPI Notifications";

        /// <summary>Gets the thumb image format.</summary>
        /// <value>The thumb image format.</value>
        public ImageFormat ThumbImageFormat => ImageFormat.Png;

        /// <summary>Gets the thumb image.</summary>
        /// <returns>An image stream.</returns>
        public Stream GetThumbImage()
        {
            var type = GetType();
            return type.Assembly.GetManifestResourceStream(type.Namespace + ".ThumbImage.png");
        }

        protected override void OnOptionsSaved(PluginOptions options)
        {
            _logger.Info("My plugin ({0}) options have been updated.", Name);
        }
    }
}
