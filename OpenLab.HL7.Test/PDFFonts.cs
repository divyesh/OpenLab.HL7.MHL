using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;

namespace OpenLab.HL7.Test
{
    public static class PDFFonts
    {
        public static Font Times(int fontSize)
        {
            return FontFactory.GetFont(FontFactory.TIMES_ROMAN, fontSize);
        }

        public static Font Helvetica(int fontSize, int style)
        {
            return FontFactory.GetFont(FontFactory.HELVETICA, fontSize, style);
        }
        public static Font Courier(int fontSize, int style)
        {
            return FontFactory.GetFont(FontFactory.COURIER, fontSize, style);
        }
        public static Font Times(int fontSize, int style)
        {
            return FontFactory.GetFont(FontFactory.TIMES, fontSize, style);
        }
        public static Font TimesRoman(int fontSize, int style)
        {
            return FontFactory.GetFont(FontFactory.TIMES_ROMAN, fontSize, style);
        }
        public static Font Symbol(int fontSize, int style)
        {
            return FontFactory.GetFont(FontFactory.SYMBOL, fontSize, style);
        }

        public static Font ZAPFDINGBATS(int fontSize, int style)
        {
            return FontFactory.GetFont(FontFactory.ZAPFDINGBATS, fontSize, style);
        }
        public static Font Wingdings(int fontSize, int style)
        {
            return FontFactory.GetFont("Wingdings", fontSize, style);
        }
    }
}
