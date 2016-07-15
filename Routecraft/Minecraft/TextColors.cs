using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Routecraft.Minecraft
{
    public static class TextColors
    {
        public static Color GetForegroundColor(char id)
        {
            switch (id)
            {
                case '0':
                    return Color.Black;
                case '1':
                    return Color.FromArgb(0, 0, 191);
                case '2':
                    return Color.FromArgb(0, 191, 0);
                case '3':
                    return Color.FromArgb(0, 191, 191);
                case '4':
                    return Color.FromArgb(191, 0, 0);
                case '5':
                    return Color.FromArgb(191, 0, 191);
                case '6':
                    return Color.FromArgb(191, 191, 0);
                case '7':
                    return Color.FromArgb(191, 191, 191);
                case '8':
                    return Color.FromArgb(64, 64, 64);
                case '9':
                    return Color.FromArgb(64, 64, 255);
                case 'a':
                case 'A':
                    return Color.FromArgb(64, 255, 64);
                case 'b':
                case 'B':
                    return Color.FromArgb(64, 255, 255);
                case 'c':
                case 'C':
                    return Color.FromArgb(255, 64, 64);
                case 'd':
                case 'D':
                    return Color.FromArgb(255, 64, 255);
                case 'e':
                case 'E':
                    return Color.FromArgb(255, 255, 64);
                case 'f':
                case 'F':
                    return Color.FromArgb(255, 255, 255);
            }
            return Color.Black;
        }
    }
}
