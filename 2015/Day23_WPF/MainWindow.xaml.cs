using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Xml;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;

namespace Day23_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();

            using (XmlTextReader reader =new XmlTextReader(GetType().Assembly.GetManifestResourceStream("Day23_WPF.res.syntax.xsd")))
            {
                AvalonTextEditor.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
            }

            AvalonTextEditor.Text = _initialCode;
            RegisterA = 1;
        }

        #region GUI ========================================================================================================
        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine("keydown");
            if(e.Key == Key.Enter)
            {
                Debug.WriteLine("f10");
                Step();
            }
        }

        private void Compile_OnClick(object sender, RoutedEventArgs e)
        {
            Reset();
            try
            {
                var result = ParseInput(AvalonTextEditor.Text);
                foreach (var i in result)
                {
                    Instructions.Add(i);
                }

                Instructions[0].Background = Brushes.Red;
            }
            catch(Exception exception)
            {
                Debug.WriteLine(exception.Message + " " + exception.StackTrace);
            }
        }

        private async void Run_OnClick(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                while (ProgramCounter >= 0 && ProgramCounter < Instructions.Count)
                {
                    Step();
                }
            });
        }
        #endregion

        #region Properties ========================================================================================================

        private void Reset()
        {
            ProgramCounter = 0;
            Cycles = 0;
            Finished = false;
            Instructions.Clear();
        }

        public bool Finished
        {
            get => _finished;
            set => SetField(ref _finished, value);
        }
        private bool _finished;
        
        public ObservableCollection<Instruction> Instructions { get; set; } = new ObservableCollection<Instruction>();

        public long Cycles
        {
            get => _cycles;
            set => SetField(ref _cycles, value);
        }
        private long _cycles;

        public long RegisterA
        {
            get => _registerA;
            set => SetField(ref _registerA, value);
        }
        private long _registerA;

        public long RegisterB
        {
            get => _registerB;
            set => SetField(ref _registerB, value);
        }
        private long _registerB;

        public long ProgramCounter
        {
            get => _programCounter;
            set => SetField(ref _programCounter, value);
        }
        private long _programCounter;
        

        private string _initialCode = @"jio a, +16
inc a
inc a
tpl a
tpl a
tpl a
inc a
inc a
tpl a
inc a
inc a
tpl a
tpl a
tpl a
inc a
jmp +23
tpl a
inc a
inc a
tpl a
inc a
inc a
tpl a
tpl a
inc a
inc a
tpl a
inc a
tpl a
inc a
tpl a
inc a
inc a
tpl a
inc a
tpl a
tpl a
inc a
jio a, +8
inc b
jie a, +4
tpl a
inc a
jmp +2
hlf a
jmp -7";
        #endregion

        #region virual machine ========================================================================================================
        private long GetRegisterValue(Register register)
        {
            if (register == Register.a)
            {
                return RegisterA;
            }
            else
            {
                return RegisterB;
            }
        }

        private void SetRegisterValue(Register register, long value)
        {
            if (register == Register.a)
            {
                RegisterA = value;
            }
            else
            {
                RegisterB = value;
            }
        }
        
        private void RunInstruction(Instruction instruction)
        {
            long value;
            switch (instruction.Type)
            {
                case InstructionType.hlf:
                    value = GetRegisterValue(instruction.Register);
                    SetRegisterValue(instruction.Register, value / 2);
                    ProgramCounter++;
                    break;
                case InstructionType.tpl:
                    value = GetRegisterValue(instruction.Register);
                    SetRegisterValue(instruction.Register, value * 3);
                    ProgramCounter++;
                    break;
                case InstructionType.inc:
                    value = GetRegisterValue(instruction.Register);
                    SetRegisterValue(instruction.Register, value + 1);
                    ProgramCounter++;
                    break;
                case InstructionType.jmp:
                    ProgramCounter += instruction.Offset;
                    break;
                case InstructionType.jie:
                    if (GetRegisterValue(instruction.Register) % 2 == 0)
                    {
                        ProgramCounter += instruction.Offset;
                    }
                    else
                    {
                        ProgramCounter++;
                    }
                    break;
                case InstructionType.jio:
                    if (GetRegisterValue(instruction.Register) == 1)
                    {
                        ProgramCounter += instruction.Offset;
                    }
                    else
                    {
                        ProgramCounter++;
                    }
                    break;
            }
        }

        private void Step()
        {
            if (ProgramCounter >= 0 && ProgramCounter < Instructions.Count)
            {
                Instructions[(int)ProgramCounter].Background = Brushes.Transparent;
                RunInstruction(Instructions[(int)ProgramCounter]);


                if (ProgramCounter >= 0 && ProgramCounter < Instructions.Count)
                {
                    Instructions[(int)ProgramCounter].Background = Brushes.Red;
                }
                Cycles++;
            }
        }
        #endregion

        #region parsing ========================================================================================================
        private List<Instruction> ParseInput(string input)
        {
            var instructions = new List<Instruction>();
            var lines = input.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                var bits = line.Replace(",", "").Split(' ');
                if (!Enum.TryParse(bits[0], out InstructionType type))
                {
                    throw new Exception();
                }

                Instruction instruction = new Instruction();
                instruction.Type = type;

                List<InstructionType> registerFirstArgument = new List<InstructionType>()
                {
                    InstructionType.hlf,
                    InstructionType.tpl,
                    InstructionType.inc,
                    InstructionType.jie,
                    InstructionType.jio,
                };

                List<InstructionType> offsetFirstArgument = new List<InstructionType>()
                {
                    InstructionType.jmp,
                };

                List<InstructionType> offsetSecondArgument = new List<InstructionType>()
                {
                    InstructionType.jie,
                    InstructionType.jio,
                };

                //Instructions with the register as second argument
                if (registerFirstArgument.Contains(type))
                {
                    if (!Enum.TryParse(bits[1], out Register register))
                    {
                        throw new Exception();
                    }
                    instruction.Register = register;
                }

                //Instructions with the offset as first argument
                if (offsetFirstArgument.Contains(type))
                {
                    instruction.Offset = int.Parse(bits[1]);
                }

                //instructions with the offset as 2nd argument
                if (offsetSecondArgument.Contains(type))
                {
                    instruction.Offset = int.Parse(bits[2]);
                }
                instructions.Add(instruction);
            }

            return instructions;
        }
        #endregion

        #region INotifyPropertyChanged ========================================================================================================
        [field: NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;

        protected void InvokePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (this != null && !(this is INotifyPropertyChanged))
            {
                throw new Exception("Forgot to inherit INotifyPropertyChanged");
            }
            if (propertyName == null)
            {
                throw new ArgumentException($"{nameof(propertyName)} is null. PropertyChangedEventHandler will not be fired correctly.");
            }
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }
        #endregion

        
    }

    public enum InstructionType
    {
        hlf,
        tpl,
        inc,
        jmp,
        jie,
        jio
    }

    public enum Register
    {
        None,
        a,
        b,
    }

    public class Instruction : INotifyPropertyChanged
    {
        public InstructionType Type;
        public int Offset;
        public Register Register;

        public override string ToString()
        {
            string toString = Type.ToString();
            toString += Register == Register.None ? "" : " " + Register.ToString();
            toString += Offset == 0 ? "" : " " + Offset.ToString();
            return toString;
        }
        
        public Brush Background
        {
            get => _background;
            set => SetField(ref _background, value);
        }
        private Brush _background;

        

        #region INotifyPropertyChanged ========================================================================================================
        [field: NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;

        protected void InvokePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (this != null && !(this is INotifyPropertyChanged))
            {
                throw new Exception("Forgot to inherit INotifyPropertyChanged");
            }
            if (propertyName == null)
            {
                throw new ArgumentException($"{nameof(propertyName)} is null. PropertyChangedEventHandler will not be fired correctly.");
            }
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }
        #endregion
    }
}