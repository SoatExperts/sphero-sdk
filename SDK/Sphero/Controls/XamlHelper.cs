using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Sphero.Controls
{
    internal static class XamlHelper
    {
        internal static bool IsControlChildOf(DependencyObject element, DependencyObject root)
        {
            DependencyObject parent = element;
            while ((parent != root) && (parent != null))
                parent = VisualTreeHelper.GetParent(parent);
            if (parent == root)
                return true;
            else
                return false;
        }
    }
}
