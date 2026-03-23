using System.Buffers.Text;
using System.Diagnostics;
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
namespace WpfApp1;
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        var beatmaps = OsuParser.LoadBeatmaps("D:/osu!/Songs");

        foreach (var sets in beatmaps)
        {
            Console.WriteLine($"{sets.Artist} - {sets.Title}");

            foreach (var diff in sets.Difficulties)
            {
                Console.WriteLine($"   [{diff.Version}]");
            }
        }
        
        
    }
    
    
    
    
}