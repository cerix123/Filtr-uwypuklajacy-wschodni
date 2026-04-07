using System.Runtime.InteropServices;

namespace JA_filtr
{
    internal static class Program
    {
        [DllImport(@"C:\Users\Acer\Desktop\JA_projekt\JA_filtr\x64\Debug\filtrcpp.dll")]
        static extern int dodaj(int a, int b);
        
        static void Main()
        {
            int a = 2, b = 3;
            int c = dodaj(a, b);
            ApplicationConfiguration.Initialize();
            MessageBox.Show($"wynik {c}", "xd", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Run(new Form1());
        }
    }
}