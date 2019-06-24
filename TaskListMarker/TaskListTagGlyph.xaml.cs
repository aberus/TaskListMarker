using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualStudio.Shell;

namespace Aberus.TaskListMarker
{
    /// <summary>
    /// Interaction logic for TodoGlyph.xaml
    /// </summary>
    public partial class TaskListTagGlyph : UserControl
    {
        public TaskListTagGlyph()
        {
            InitializeComponent();
        }
    }

    public static class VsBrushes
    {
        public static object SideBarBackgroundKey
        {
            get
            {
                return Microsoft.VisualStudio.Shell.VsBrushes.SideBarBackgroundKey;
            }
        }
    }
}
