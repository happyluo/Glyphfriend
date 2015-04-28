﻿using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.CSS.Editor.Completion;
using Microsoft.VisualStudio.Utilities;

namespace Glyphfriend.GlyphCompletionProviders
{
    [Export(typeof(ICssCompletionEntryGlyphProvider))]
    [Name("Glyphfriend Foundation")]
    class FoundationGlyphCompletionEntryGlyphProvider : ICssCompletionEntryGlyphProvider
    {
        // Store the default glyph for this particular library
        private static BitmapFrame _defaultGlyph = BitmapFrame.Create(new Uri("pack://application:,,,/Glyphfriend;component/Glyphs/Foundation/_default.png", UriKind.RelativeOrAbsolute));

        // Define a Regular Expression check for matches from this library
        private static Regex _regex = new Regex(@"^foundation-icons(\.min)?\.css$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public ImageSource GetCompletionGlyph(string entryName, Uri sourceUri, CssNameType nameType)
        {
            // If the source Uri for the image is null, ignore it
            if (sourceUri == null) { return null; }
            // Get the file path of the source being used
            string filename = Path.GetFileName(sourceUri.ToString()).Trim();
            // Determine if this matches our filename
            if (_regex.IsMatch(filename))
            {
                // If the glyph exists, serve it
                if (GlyphfriendPackage.Glyphs.ContainsKey(entryName))
                {
                    return GlyphfriendPackage.Glyphs[entryName];
                }
                // If one was not available, serve the default icon
                return _defaultGlyph;
            }

            return null;
        }
    }
}
