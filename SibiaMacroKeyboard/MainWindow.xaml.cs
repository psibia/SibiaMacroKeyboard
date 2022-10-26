using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Ports;
using System.IO;
using System.Collections.Specialized;
using System.Diagnostics;
using WindowsInput;
using WindowsInput.Native;
using System.ComponentModel;
using Microsoft.Win32;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SibiaMacroKeyboard
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();
        InputSimulator simulator = new InputSimulator();
        SerialPort sp = new SerialPort();
        string[] ports = SerialPort.GetPortNames();
        byte keyPadSelected = 0;
        bool initialization = true;
        Point cursorBuffer = default;

        public MainWindow()
        {
            InitializeComponent();
            this.Left = Properties.Settings.Default.LeftScreen;
            this.Top = Properties.Settings.Default.TopScreen;
            this.Topmost = Properties.Settings.Default.TopMost;
            COM.ItemsSource = ports;
            KeyPadSelectedChanged();
            sp.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
            ComboBoxInit();
        }

        void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                keyPadSelected = byte.Parse(sp.ReadExisting());
                GetCommand();
            });
        }
        private void Grid_DragLeave(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
            Properties.Settings.Default.LeftScreen = this.Left;
            Properties.Settings.Default.TopScreen = this.Top;
            Properties.Settings.Default.Save();
        }

        private void Bar_Click(object sender, RoutedEventArgs e)
        {
            switch (sender)
            {
                case Button btn when btn == BtnHelp:
                    try
                    {
                        ProcessStartInfo infoStartProcess = new ProcessStartInfo();
                        infoStartProcess.FileName = AppDomain.CurrentDomain.BaseDirectory + "readme.txt";
                        Process.Start(infoStartProcess);
                    } catch (Exception ex) { MessageBox.Show(ex.Message); MessageBox.Show(ex.Source); }
                    break;

                case Button btn when btn == BtnSave:
                    try 
                    {
                        Properties.Settings.Default.configMacros[keyPadSelected] = ConfigText.Text;
                        Properties.Settings.Default.Save();
                        MessageBox.Show("Макрос сохранен");
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); MessageBox.Show(ex.Source); }
                    break;

                case Button btn when btn == BtnTray:
                    this.Hide();
                    break;

                case Button btn when btn == BtnClose:
                    this.Close();
                    break;

                case Button btn when btn == BtnSettings:
                    try 
                    {
                        if (ConfigText.Visibility == Visibility.Visible)
                        {
                            ConfigText.Visibility = Visibility.Hidden;
                            ports = SerialPort.GetPortNames();
                            COM.ItemsSource = ports;
                        }
                        else ConfigText.Visibility = Visibility.Visible;
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    break;
            }             
        }


        private void KeyPad_Click(object sender, RoutedEventArgs e)
        {
            switch (sender)
            {
                case Button btn when btn == Btn0:
                    keyPadSelected = 0;
                    Grid.SetColumn(SelectButton, 0);
                    Grid.SetRow(SelectButton, 0);
                    KeyPadSelectedChanged();
                    if (Properties.Settings.Default.TopMost) // Если активирован режим работы без внешней клавиатуры и окно поверх других
                        GetCommand();
                    break;
                case Button btn when btn == Btn1:
                    keyPadSelected = 1;
                    Grid.SetColumn(SelectButton, 0);
                    Grid.SetRow(SelectButton, 1);
                    KeyPadSelectedChanged();
                    if (Properties.Settings.Default.TopMost) // Если активирован режим работы без внешней клавиатуры и окно поверх других
                        GetCommand();
                    break;
                case Button btn when btn == Btn2:
                    keyPadSelected = 2;
                    Grid.SetColumn(SelectButton, 0);
                    Grid.SetRow(SelectButton, 2);
                    KeyPadSelectedChanged();
                    if (Properties.Settings.Default.TopMost) // Если активирован режим работы без внешней клавиатуры и окно поверх других
                        GetCommand();
                    break;
                case Button btn when btn == Btn3:
                    keyPadSelected = 3;
                    Grid.SetColumn(SelectButton, 1);
                    Grid.SetRow(SelectButton, 0);
                    KeyPadSelectedChanged();
                    if (Properties.Settings.Default.TopMost) // Если активирован режим работы без внешней клавиатуры и окно поверх других
                        GetCommand();
                    break;
                case Button btn when btn == Btn4:
                    keyPadSelected = 4;
                    Grid.SetColumn(SelectButton, 1);
                    Grid.SetRow(SelectButton, 1);
                    KeyPadSelectedChanged();
                    if (Properties.Settings.Default.TopMost) // Если активирован режим работы без внешней клавиатуры и окно поверх других
                        GetCommand();
                    break;
                case Button btn when btn == Btn5:
                    keyPadSelected = 5;
                    Grid.SetColumn(SelectButton, 1);
                    Grid.SetRow(SelectButton, 2);
                    KeyPadSelectedChanged();
                    if (Properties.Settings.Default.TopMost) // Если активирован режим работы без внешней клавиатуры и окно поверх других
                        GetCommand();
                    break;
                case Button btn when btn == Btn6:
                    keyPadSelected = 6;
                    Grid.SetColumn(SelectButton, 2);
                    Grid.SetRow(SelectButton, 0);
                    KeyPadSelectedChanged();
                    if (Properties.Settings.Default.TopMost) // Если активирован режим работы без внешней клавиатуры и окно поверх других
                        GetCommand();
                    break;
                case Button btn when btn == Btn7:
                    keyPadSelected = 7;
                    Grid.SetColumn(SelectButton, 2);
                    Grid.SetRow(SelectButton, 1);
                    KeyPadSelectedChanged();
                    if (Properties.Settings.Default.TopMost) // Если активирован режим работы без внешней клавиатуры и окно поверх других
                        GetCommand();
                    break;
                case Button btn when btn == Btn8:
                    keyPadSelected = 8;
                    Grid.SetColumn(SelectButton, 2);
                    Grid.SetRow(SelectButton, 2);
                    KeyPadSelectedChanged();
                    if (Properties.Settings.Default.TopMost) // Если активирован режим работы без внешней клавиатуры и окно поверх других
                        GetCommand();
                    break;
                case Button btn when btn == Btn9:
                    keyPadSelected = 9;
                    Grid.SetColumn(SelectButton, 3);
                    Grid.SetRow(SelectButton, 0);
                    KeyPadSelectedChanged();
                    if (Properties.Settings.Default.TopMost) // Если активирован режим работы без внешней клавиатуры и окно поверх других
                        GetCommand();
                    break;
                case Button btn when btn == Btn10:
                    keyPadSelected = 10;
                    Grid.SetColumn(SelectButton, 3);
                    Grid.SetRow(SelectButton, 1);
                    KeyPadSelectedChanged();
                    if (Properties.Settings.Default.TopMost) // Если активирован режим работы без внешней клавиатуры и окно поверх других
                        GetCommand();
                    break;
                case Button btn when btn == Btn11:
                    keyPadSelected = 11;
                    Grid.SetColumn(SelectButton, 3);
                    Grid.SetRow(SelectButton, 2);
                    KeyPadSelectedChanged();
                    if (Properties.Settings.Default.TopMost) // Если активирован режим работы без внешней клавиатуры и окно поверх других
                        GetCommand();
                    break;
            }
        } // Обработка действий при нажатии на квадратную кнопку слева (которых 12 штук)
        void KeyPadSelectedChanged() // Отрисовка текста макроса для соответствующей кнопки
        {
            ConfigText.Text = Properties.Settings.Default.configMacros[keyPadSelected];
        }

        async private void GetCommand()
        {
            try { 
            string commandList = Properties.Settings.Default.configMacros[keyPadSelected];
            string[] subCommand = commandList.Split('\n');
                for (int i = 0; i < subCommand.Length; i++)
                {
                    string targetCommand = subCommand[i];
                    // MessageBox.Show($"Substring: {targetCommand}");
                    switch (targetCommand)
                    {
                        case string command when command.StartsWith("start ", StringComparison.OrdinalIgnoreCase):
                            targetCommand = targetCommand.Remove(0, 6);
                            ProcessStartInfo infoStartProcess = new ProcessStartInfo();
                            // Костыль, который удаляет знак переноса каретки, если запускаемых процессов больше, чем 1 или после адреса на файл стоит знак переноса картеки  
                            while (targetCommand[targetCommand.Length - 1] == '\r')
                                targetCommand = targetCommand.Remove(targetCommand.Length - 1);
                            infoStartProcess.FileName = targetCommand;
                            Process.Start(infoStartProcess);
                            break;

                        case string command when command.StartsWith("sound ", StringComparison.OrdinalIgnoreCase):
                            targetCommand = targetCommand.Remove(0, 6);
                            while (targetCommand[targetCommand.Length - 1] == '\r')
                                targetCommand = targetCommand.Remove(targetCommand.Length - 1);
                            player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"\Sounds\" + targetCommand;
                            player.Play();
                            break;

                        case string command when command.StartsWith("url ", StringComparison.OrdinalIgnoreCase):
                            targetCommand = targetCommand.Remove(0, 4);
                            while (targetCommand[targetCommand.Length - 1] == '\r')
                                targetCommand = targetCommand.Remove(targetCommand.Length - 1);
                            Process.Start(targetCommand);
                            break;

                        case string command when command.StartsWith("key ", StringComparison.OrdinalIgnoreCase):
                            targetCommand = targetCommand.Remove(0, 4);
                            while (targetCommand[targetCommand.Length - 1] == '\r')
                                targetCommand = targetCommand.Remove(targetCommand.Length - 1);
                            if (targetCommand.Length == 1)
                                targetCommand = "VK_" + targetCommand;
                            simulator.Keyboard.KeyPress(ConverterStringToEnum.ToEnum<VirtualKeyCode>(targetCommand));
                            break;

                        case string command when command.StartsWith("keySeries ", StringComparison.OrdinalIgnoreCase):
                            targetCommand = targetCommand.Remove(0, 10);
                            while (targetCommand[targetCommand.Length - 1] == '\r')
                                targetCommand = targetCommand.Remove(targetCommand.Length - 1);
                            bool result = int.TryParse(targetCommand.Substring(0, 2), out int j);
                            targetCommand = targetCommand.Remove(0, 3);
                            if (targetCommand.Length == 1)
                                targetCommand = "VK_" + targetCommand;
                            if (result)
                            {
                                for (int k = 0; k < j; k++)
                                    simulator.Keyboard.KeyPress(ConverterStringToEnum.ToEnum<VirtualKeyCode>(targetCommand));
                            }
                            break;

                        case string command when command.StartsWith("keyDown ", StringComparison.OrdinalIgnoreCase):
                            targetCommand = targetCommand.Remove(0, 8);
                            while (targetCommand[targetCommand.Length - 1] == '\r')
                                targetCommand = targetCommand.Remove(targetCommand.Length - 1);
                            if (targetCommand.Length == 1)
                                targetCommand = "VK_" + targetCommand;
                            simulator.Keyboard.KeyDown(ConverterStringToEnum.ToEnum<VirtualKeyCode>(targetCommand));
                            break;

                        case string command when command.StartsWith("keyUp ", StringComparison.OrdinalIgnoreCase):
                            targetCommand = targetCommand.Remove(0, 6);
                            while (targetCommand[targetCommand.Length - 1] == '\r')
                                targetCommand = targetCommand.Remove(targetCommand.Length - 1);
                            if (targetCommand.Length == 1)
                                targetCommand = "VK_" + targetCommand;
                            simulator.Keyboard.KeyUp(ConverterStringToEnum.ToEnum<VirtualKeyCode>(targetCommand));
                            break;

                        case string command when command.StartsWith("text ", StringComparison.OrdinalIgnoreCase):
                            targetCommand = targetCommand.Remove(0, 5);
                            if (targetCommand[targetCommand.Length - 1] == '\r')
                                targetCommand = targetCommand.Remove(targetCommand.Length - 1);
                            simulator.Keyboard.TextEntry(targetCommand);
                            break;

                        case string command when command.StartsWith("delay "):
                            targetCommand = targetCommand.Remove(0, 6);
                            while (targetCommand[targetCommand.Length - 1] == '\r')
                                targetCommand = targetCommand.Remove(targetCommand.Length - 1);
                            if (int.TryParse(targetCommand, out int value))
                            {
                                await Task.Delay(value);
                                // MessageBox.Show("Completed");
                            }
                            break;

                        case string command when command.StartsWith("cursor "):
                            targetCommand = targetCommand.Remove(0, 7);
                            while (targetCommand[targetCommand.Length - 1] == '\r')
                                targetCommand = targetCommand.Remove(targetCommand.Length - 1);
                            string[] sub = targetCommand.Split(' ');
                            var X = int.Parse(sub[0]) * 65535 / SystemParameters.PrimaryScreenWidth;
                            var Y = int.Parse(sub[1]) * 65535 / SystemParameters.PrimaryScreenHeight;
                            simulator.Mouse.MoveMouseToPositionOnVirtualDesktop(Convert.ToDouble(X), Convert.ToDouble(Y));
                            break;

                        case string command when command.StartsWith("cursorCopy", StringComparison.OrdinalIgnoreCase):
                            cursorBuffer = GetCursorPosition();
                            break;

                        case string command when command.StartsWith("cursorPaste", StringComparison.OrdinalIgnoreCase):
                            simulator.Mouse.MoveMouseToPositionOnVirtualDesktop(cursorBuffer.X * 65535 / SystemParameters.PrimaryScreenWidth,
                                cursorBuffer.Y * 65535 / SystemParameters.PrimaryScreenHeight);
                            break;

                        case string command when command.StartsWith("mouseDownLeft"):
                            simulator.Mouse.LeftButtonDown();
                            break;

                        case string command when command.StartsWith("mouseUpLeft"):
                            simulator.Mouse.LeftButtonUp();
                            break;

                        case string command when command.StartsWith("mouseClickLeft"):
                            simulator.Mouse.LeftButtonClick();
                            break;

                        case string command when command.StartsWith("mouseDoubleClickLeft"):
                            simulator.Mouse.LeftButtonDoubleClick();
                            break;

                        case string command when command.StartsWith("mouseDownRight"):
                            simulator.Mouse.RightButtonDown();
                            break;

                        case string command when command.StartsWith("mouseUpRight"):
                            simulator.Mouse.RightButtonUp();
                            break;

                        case string command when command.StartsWith("mouseClickRight"):
                            simulator.Mouse.RightButtonClick();
                            break;

                        case string command when command.StartsWith("mouseDoubleClickRight"):
                            simulator.Mouse.RightButtonDoubleClick();
                            break;

                        case string command when command.StartsWith("vertScroll "):
                            targetCommand = targetCommand.Remove(0, 11);
                            while (targetCommand[targetCommand.Length - 1] == '\r')
                                targetCommand = targetCommand.Remove(targetCommand.Length - 1);
                            simulator.Mouse.VerticalScroll(int.Parse(targetCommand));
                            break;

                        case string command when command.StartsWith("horScroll "):
                            targetCommand = targetCommand.Remove(0, 10);
                            while (targetCommand[targetCommand.Length - 1] == '\r')
                                targetCommand = targetCommand.Remove(targetCommand.Length - 1);
                            simulator.Mouse.VerticalScroll(int.Parse(targetCommand));
                            break;
                    }
                } 
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); MessageBox.Show(ex.StackTrace); }
        } // Макрос-команды

        private void COM_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (sender == COM)
                {
                    if (sp.IsOpen)
                        sp.Close();
                    sp.PortName = COM.SelectedItem as string;
                    sp.BaudRate = 9600;
                    sp.Open();
                    if (Properties.Settings.Default.AutoConnect == 1)
                    {
                        Properties.Settings.Default.COM = sp.PortName;
                        Properties.Settings.Default.Save();
                    }
                }
                else if (sender == AutoConnect && initialization == false)
                {
                    if (AutoConnect.SelectedIndex == 0)
                    {
                        Properties.Settings.Default.AutoConnect = 0;
                        Properties.Settings.Default.Save();
                        MessageBox.Show("Автоматическое подключение к COM-порту отключено. При повторном запуске приложения COM-порт необходимо выбрать вручную");
                    }
                    else if (AutoConnect.SelectedIndex == 1)
                    {
                        Properties.Settings.Default.AutoConnect = 1;
                        Properties.Settings.Default.Save();
                        MessageBox.Show("При следующем запуске приложения произойдет автоматическое подключение к последнему выбранному COM-порту");
                    }
                }
                else if (sender == AppStartup && initialization == false)
                {
                    switch (AppStartup.SelectedIndex)
                    {
                        case 0:
                            SetAutorun(false, Assembly.GetExecutingAssembly().Location);
                            Properties.Settings.Default.AppStartup = 0;
                            Properties.Settings.Default.Save();
                            MessageBox.Show("Приложение удалено из автозагрузки");
                            break;
                        case 1:
                            SetAutorun(true, Assembly.GetExecutingAssembly().Location);
                            Properties.Settings.Default.AppStartup = 1;
                            Properties.Settings.Default.Save();
                            MessageBox.Show("Приложение добавлено в автозагрузку. При запуске Windows будет открыто окно");
                            break;
                        case 2:
                            SetAutorun(true, Assembly.GetExecutingAssembly().Location);
                            Properties.Settings.Default.AppStartup = 2;
                            Properties.Settings.Default.Save();
                            MessageBox.Show("Приложение добавлено в автозагрузку. При запуске Windows оно будет запущено в фоновом режиме");
                            break;
                    }
                }
                else if (sender == TopmostBox && initialization == false)
                {
                    if (TopmostBox.SelectedIndex == 0)
                    {
                        this.Topmost = false;
                        Properties.Settings.Default.TopMost = false;
                        Properties.Settings.Default.Save();
                        MessageBox.Show("Выбран режим работы с внешней вклавиатурой\nОкно НЕ будет отображаться поверх других, а нажатие ЛКМ на кнопке в приложении НЕ вызывает срабатывание макроса");
                    }
                    else if (TopmostBox.SelectedIndex == 1)
                    {
                        this.Topmost = true;
                        Properties.Settings.Default.TopMost = true;
                        Properties.Settings.Default.Save();
                        MessageBox.Show("Выбран режим работы с виртуальной вклавиатурой\nОкно будет отображаться поверх других, а нажатие ЛКМ на кнопке в приложении вызывает срабатывание записанного для неё макроса");
                    }
                }
            } catch (Exception ex) { MessageBox.Show(ex.Message); MessageBox.Show(ex.StackTrace); }
        }
        private void ComboBoxInit() // Здесь мы инициализируем все комбо-боксы из настроек (только при старте приложения)
        {
            try
            {
                AutoConnect.SelectedIndex = Properties.Settings.Default.AutoConnect;
                AppStartup.SelectedIndex = Properties.Settings.Default.AppStartup;
                if (Properties.Settings.Default.TopMost)
                    TopmostBox.SelectedIndex = 1;
                else TopmostBox.SelectedIndex = 0;
                if (Properties.Settings.Default.AutoConnect == 1)
                    COM.SelectedItem = Properties.Settings.Default.COM as string;
                if ((Directory.GetCurrentDirectory() != AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.Length - 1)) && Properties.Settings.Default.AppStartup == 2)  // Нужно, чтобы сразу после запуска отправлять приложение в фоновый режим работы, если оно было запущено через автозапуск
                    this.Hide();
                
                initialization = false;
            } catch (Exception ex) { MessageBox.Show(ex.Message); MessageBox.Show(ex.StackTrace); }
        }

        public bool SetAutorun(bool autorun, string path)
        {
            const string name = "Application";
            string ExePath = path;
            RegistryKey reg;
            reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            try
            {
                if (autorun)
                {
                    reg.SetValue(name, ExePath);
                }
                else
                {
                    reg.DeleteValue(name);
                }
                reg.Flush();
                reg.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        // Taskbar icon 
        private void TaskbarIcon_TrayLeftMouseDown(object sender, RoutedEventArgs e)
        {
            this.Show();
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #region // Определение текущего положения курсора относительно экрана
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);
        public static Point GetCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);
            // Если нужна обработка ошибок:
            // bool success = GetCursorPos(out lpPoint);
            // if (!success)
            return lpPoint;
        }
        private void ConfigText_KeyDown(object sender, KeyEventArgs e)
        {
            if (simulator.InputDeviceState.IsKeyDown(VirtualKeyCode.F1))
            {
                Point point = GetCursorPosition();
                ConfigText.Text += $"cursor {point.X.ToString()} {point.Y.ToString()}\n";
                ConfigText.SelectionStart = ConfigText.Text.Length;
            }
        }
    }
    #endregion
}


