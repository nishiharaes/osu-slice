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

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        List<string> hitObjects = OsuParser.GetHitObjects(
            "D:/osu!/Songs/Kardashev - Torchpassing/Kardashev - Torchpassing (nishihara) [the torch passes].osu");
        List<string> hitObjectTimes = OsuParser.GetHitObjectTimes(hitObjects, 78864, 88151);
        
        OsuParser.CreateNewDifficulty("D:/osu!/Songs/Kardashev - Torchpassing/Kardashev - Torchpassing (nishihara) [the torch passes].osu", "D:/osu!/Songs/Kardashev - Torchpassing/Kardashev - Torchpassing (nishihara) [the torch passesv2].osu", hitObjectTimes, "test");
    }
}