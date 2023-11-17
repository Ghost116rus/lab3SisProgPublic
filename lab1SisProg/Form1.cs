using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace lab1SisProg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            exampleComboBox.SelectedIndex = 0;
            ExampleTKO();
        }

        readonly int columnsCount = 4;
        bool firstPass = false;
        bool secondPass = false;
        int typeAdr = 0;
        FSPass fsp;
        DataCheck dC;

        private void FirstPassButton_Click(object sender, EventArgs e)
        {
            Clear();
            DeleteEmptyRows(operationCodeDataGrid);

            secondPass = false;

            fsp = new FSPass();
            dC = new DataCheck();

            // Словарь для хранения меток EXTREF для каждого управляющего сектора
            Dictionary<string, List<string>> extRefLabels = new Dictionary<string, List<string>>();
            string currentControlSection = "";

            List<string> marks = new List<string>();

            //помещаем ТКО в динамический массив
            string[,] operationCodeArray = new string[operationCodeDataGrid.RowCount - 1, operationCodeDataGrid.ColumnCount];

            for (int i = 0; i < operationCodeDataGrid.RowCount - 1; i++)
                for (int j = 0; j < operationCodeDataGrid.ColumnCount; j++)
                    operationCodeArray[i, j] = Convert.ToString(operationCodeDataGrid.Rows[i].Cells[j].Value).ToUpper();

            //помещаем исходный код в динамический массив
            string[] str = sourceCodeTextBox.Text.Split('\n');

            for (int i = 0; i < str.Length; i++)
                str[i] = Convert.ToString(str[i]).Replace("\r", "");

            str = str.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            string[,] sourceCodeArray = new string[str.Length, columnsCount];

            for (int i = 0; i < str.Length; i++)
                for (int j = 0; j < columnsCount; j++)
                    sourceCodeArray[i, j] = "";

            for (int i = 0; i < str.Length; i++)
            {
                str[i] = str[i].Trim();
                string[] temp = str[i].Split(' ');
                if (temp.Length >= 3)
                {
                    if (temp[1].IndexOf('"') == 1 && (temp[temp.Length - 1].LastIndexOf('"') == temp[temp.Length - 1].Length - 1))
                    {
                        for (int j = 2; j < temp.Length; j++)
                        {
                            temp[1] += " " + temp[j];
                            temp[j] = "";
                        }
                    }
                    else if (temp[2].IndexOf('"') == 1 && temp[temp.Length - 1].LastIndexOf('"') == temp[temp.Length - 1].Length - 1)
                    {
                        for (int j = 3; j < temp.Length; j++)
                        {
                            temp[2] += " " + temp[j];
                            temp[j] = "";
                        }
                    }
                }
                temp = temp.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                temp[temp.Length - 1] = temp[temp.Length - 1].Replace("\r", "");

                if (temp.Length <= 4)
                {
                    if (temp.Length == 1)
                    {
                        if (dC.CheckDirective(temp[0]) || fsp.FindCode(temp[0], operationCodeArray) != -1)
                            sourceCodeArray[i, 1] = temp[0];
                        else
                        {
                            for (int j = 0; j < temp.Length; j++)
                                sourceCodeArray[i, j] = temp[j];
                        }
                    }
                    else if (temp.Length == 2)
                    {

                        if ((temp[0] == "EXTREF") || (temp[0] == "EXTDEF"))
                        {
                            if (!extRefLabels.ContainsKey(currentControlSection))
                            {
                                extRefLabels[currentControlSection] = new List<string>();
                            }

                            if (extRefLabels[currentControlSection].Contains(temp[1]))
                            {
                                MessageBox.Show($"Метка EXTREF '{temp[1]}' уже определена!");
                                return;
                            }

                            extRefLabels[currentControlSection].Add(temp[1]);
                            marks.Add(temp[1]);
                        }

                        if (dC.CheckDirective(temp[0]) || fsp.FindCode(temp[0], operationCodeArray) != -1)
                        {
                            if (fsp.FindCode(temp[0], operationCodeArray) != -1 && typeAdr == 0 && (temp[1].Contains('[') || temp[1].Contains(']')) && !marks.Contains(temp[1]))
                            {
                                MessageBox.Show($"Относительная адресация в {i + 1} строке. Выбрана прямая адресация.", "Внимание!");
                                return;
                            }
                            if (fsp.FindCode(temp[0], operationCodeArray) != -1 && typeAdr == 1 && (!temp[1].Contains('[') || !temp[1].Contains(']')) && !marks.Contains(temp[1]) && !int.TryParse(temp[1], out int p))
                            {
                                MessageBox.Show($"Прямая адресация в {i + 1} строке. Выбрана относительная адресация.", "Внимание!");
                                return;
                            }
                            sourceCodeArray[i, 1] = temp[0];
                            sourceCodeArray[i, 2] = temp[1];
                        }
                        else if (dC.CheckDirective(temp[1]) || fsp.FindCode(temp[1], operationCodeArray) != -1)
                        {
                            sourceCodeArray[i, 0] = temp[0];
                            sourceCodeArray[i, 1] = temp[1];
                        }
                        else
                        {
                            for (int j = 0; j < temp.Length; j++)
                                sourceCodeArray[i, j] = temp[j];
                        }
                    }
                    else if (temp.Length == 3)
                    {
                        if (dC.CheckDirective(temp[0]) || fsp.FindCode(temp[0], operationCodeArray) != -1)
                        {
                            if (temp[2].IndexOf('"') == 1 && temp[2].IndexOf('"') == temp[2].Length - 1)
                            {
                                sourceCodeArray[i, 1] = temp[0];
                                sourceCodeArray[i, 2] = temp[1] + " " + temp[2];
                                sourceCodeArray[i, 3] = "";
                            }
                            else
                            {
                                sourceCodeArray[i, 1] = temp[0];
                                sourceCodeArray[i, 2] = temp[1];
                                sourceCodeArray[i, 3] = temp[2];
                            }
                        }
                        else if (dC.CheckDirective(temp[1]) || fsp.FindCode(temp[1], operationCodeArray) != -1)
                        {
                            if (fsp.FindCode(temp[1], operationCodeArray) != -1 && typeAdr == 0 && (temp[2].Contains('[') || temp[2].Contains(']')) && !marks.Contains(temp[2]))
                            {
                                MessageBox.Show($"Относительная адресация в {i + 1} строке. Выбрана прямая адресация.", "Внимание!");
                                return;
                            }
                            if (fsp.FindCode(temp[1], operationCodeArray) != -1 && typeAdr == 1 && (!temp[2].Contains('[') || !temp[2].Contains(']')) && !marks.Contains(temp[2]) && !int.TryParse(temp[2], out int p))
                            {
                                MessageBox.Show($"Прямая адресация в {i + 1} строке. Выбрана относительная адресация.", "Внимание!");
                                return;
                            }
                            sourceCodeArray[i, 0] = temp[0];
                            sourceCodeArray[i, 1] = temp[1];
                            sourceCodeArray[i, 2] = temp[2];
                        }
                        else
                        {
                            for (int j = 0; j < temp.Length; j++)
                                sourceCodeArray[i, j] = temp[j];
                        }
                    }
                    else if (temp.Length == 4)
                    {
                        if (dC.CheckDirective(temp[1]) || fsp.FindCode(temp[1], operationCodeArray) != -1)
                        {
                            if (fsp.FindCode(temp[1], operationCodeArray) != -1 && typeAdr == 0 && (temp[2].Contains('[') || temp[2].Contains(']')) && !marks.Contains(temp[2]))
                            {
                                MessageBox.Show($"Относительная адресация в {i + 1} строке. Выбрана прямая адресация.", "Внимание!");
                                return;
                            }
                            if (fsp.FindCode(temp[1], operationCodeArray) != -1 && typeAdr == 1 && (!temp[2].Contains('[') || !temp[2].Contains(']')) && !marks.Contains(temp[2]) && !int.TryParse(temp[2], out int p))
                            {
                                MessageBox.Show($"Прямая адресация в {i + 1} строке. Выбрана относительная адресация.", "Внимание!");
                                return;
                            }
                            if (temp[2].IndexOf('"') == 1 && temp[3].IndexOf('"') == temp[3].Length - 1)
                            {
                                sourceCodeArray[i, 0] = temp[0];
                                sourceCodeArray[i, 1] = temp[1];
                                sourceCodeArray[i, 2] = temp[2] + " " + temp[3];
                                sourceCodeArray[i, 3] = "";
                            }
                            else
                            {
                                sourceCodeArray[i, 0] = temp[0];
                                sourceCodeArray[i, 1] = temp[1];
                                sourceCodeArray[i, 2] = temp[2];
                                sourceCodeArray[i, 3] = temp[3];
                            }
                        }
                        else if (!(dC.CheckDirective(temp[1]) || fsp.FindCode(temp[1], operationCodeArray) != -1))
                        {
                            MessageBox.Show($"Синтаксическая ошибка в {i + 1} строке.", "Внимание!");
                            return;
                        }
                        else
                        {
                            for (int j = 0; j < temp.Length; j++)
                                sourceCodeArray[i, j] = temp[j];
                        }
                    }

                }
                else
                {
                    MessageBox.Show($"Синтаксическая ошибка в {i + 1} строке. Элементов в строке больше 4", "Внимание!");
                    return;
                }

                // Обновление текущего управляющего сектора при обнаружении ключевого слова "CSECT"
                if (temp.Length >= 2 && temp[1] == "CSECT")
                {
                    currentControlSection = temp[0];
                }
            }

            for (int i = 0; i < sourceCodeArray.GetLength(0); i++)
                sourceCodeArray[i, 1] = Convert.ToString(sourceCodeArray[i, 1]).ToUpper();

            for (int i = 0; i < str.Length; i++)
            {
                for (int j = 0; j < columnsCount; j++)
                    Console.Write(sourceCodeArray[i, j]);
                Console.WriteLine();
            }


            if (fsp.CheckOperationCode(operationCodeArray))
            {
                if (fsp.DoFirstPass(sourceCodeArray, operationCodeArray, supportTableDataGrid, symbolTableDataGrid))
                {
                    firstPass = true;
                    writeError(firstPassErrorTextBox, fsp.errorText);
                }
                else
                    writeError(firstPassErrorTextBox, fsp.errorText);
            }
            else
                writeError(firstPassErrorTextBox, fsp.errorText);


        }

        private void writeError(TextBox textBox, string str)
        {
            textBox.Text += str + "\r\n";
        }

        private void Clear()
        {
            supportTableDataGrid.Rows.Clear();
            symbolTableDataGrid.Rows.Clear();
            firstPassErrorTextBox.Clear();
            binaryCodeTextBox.Clear();
            secondPassErrorTextBox.Clear();
            settingTableDataGrid.Rows.Clear();
        }

        private void SourceCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            Clear();
            firstPass = false;
        }

        private void OperationCodeDataGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            Clear();
            firstPass = false;
        }

        private void SecondPassButton_Click(object sender, EventArgs e)
        {
            //binaryCodeTextBox.Clear();
            //secondPassErrorTextBox.Clear();

            if (firstPass)
            {
                if (!secondPass && fsp.DoSecondPass(binaryCodeTextBox, settingTableDataGrid) )
                {
                    secondPass = true;
                    writeError(secondPassErrorTextBox, fsp.errorText);
                }
                else
                {
                    writeError(secondPassErrorTextBox, fsp.errorText);
                }
            }
            else
            {
                MessageBox.Show($"Выполните первый проход!");
            }
        }

        public void DeleteEmptyRows(DataGridView DBGrid_source_code)
        {
            for (int i = 0; i < DBGrid_source_code.Rows.Count - 1; i++)
            {
                bool empty = true;
                for (int j = 0; j < DBGrid_source_code.Rows[i].Cells.Count; j++)
                    if ((DBGrid_source_code.Rows[i].Cells[j].Value != null) && (DBGrid_source_code.Rows[i].Cells[j].Value.ToString() != ""))
                        empty = false;

                if (empty)
                {
                    DBGrid_source_code.Rows.Remove(DBGrid_source_code.Rows[i]);
                }
            }
        }

        private void ExampleTKO()
        {
            operationCodeDataGrid.Rows.Add("JMP", "01", "4");
            operationCodeDataGrid.Rows.Add("LOADR1", "02", "4");
            operationCodeDataGrid.Rows.Add("LOADR2", "03", "4");
            operationCodeDataGrid.Rows.Add("ADD", "04", "2");
            operationCodeDataGrid.Rows.Add("SAVER1", "05", "4");
            operationCodeDataGrid.Rows.Add("NOP", "06", "1");
        }

        private void ExampleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
            if (exampleComboBox.SelectedIndex == 0)
            {
                sourceCodeTextBox.Text = "prog start 0\r\n" +
                   "EXTDEF D23\r\n" +
                   "EXTDEF D4\r\n" +
                   "EXTREF D2\r\n" +
                   "EXTREF D546\r\n" +
                   "D4 RESB 10\r\n" +
                   "D23 RESW 20\r\n" +
                   "JMP D2\r\n" +
                   "SAVER1 D546\r\n" +
                   "RESB 10\r\n" +
                   "A2 CSECT\r\n" +
                   "EXTDEF D42\r\n" +
                   "EXTREF D4\r\n" +
                   "D42 SAVER1 D4\r\n" +
                   "NOP\r\n" +
                   "END 0";

                sourceCodeTextBox.Text = "prog start 0\r\n" +
                    "EXTDEF D23\r\n" +
                    "EXTDEF D4\r\n" +
                    "EXTREF D2\r\n" +
                    "EXTREF D546\r\n" +
                    "T1 RESB 10\r\n" +
                    "D23 RESW 10\r\n" +
                    "D4 SAVER1 D546\r\n" +
                    "D42 LOADR1 T1\r\n" +
                    "RESB 10\r\n" +
                    "A2 CSECT\r\n" +
                    "EXTDEF D2\r\n" +
                    "EXTREF D4\r\n" +
                    "EXTREF D58\r\n" +
                    "D2 SAVER1 D2\r\n" +
                    "B2 BYTE X\"2F4C008A\"\r\n" +
                    "B3 BYTE C\"Hello!\"\r\n" +
                    "B4 BYTE 128\r\n" +
                    "LOADR1 B2\r\n" +
                    "LOADR2 B4\r\n" +
                    "LOADR1 D2\r\n" +
                    "T3 NOP\r\n" +
                    "END 0";
                typeAdr = 0;
            }
            if (exampleComboBox.SelectedIndex == 1)
            {
                typeAdr = 1;
                sourceCodeTextBox.Text = 
                    "prog start 0\r\n" +
                    "EXTDEF D23\r\n" +
                    "EXTDEF D4\r\n" +
                    "EXTREF D2\r\n" +
                    "EXTREF D546\r\n" +
                    "D4 RESB 10\r\n" +
                    "D23 RESB 10\r\n" +
                    "T1 jmp D2\r\n" +
                    "SAVER1 D546\r\n" +
                    "D42 LOADR2 [T1]\r\n" +
                    "RESB 10\r\n" + 
                    "A2 CSECT\r\n" +
                    "EXTDEF D2\r\n" +
                    "EXTREF D4\r\n" +
                    "EXTREF D58\r\n" +
                    "D2 SAVER1 [D2]\r\n" +
                    "LOADR1 [D2]\r\n" +
                    "T3 NOP\r\n" +
                    "END 0";

                sourceCodeTextBox.Text = "prog start 0\r\n" +
                    "EXTDEF D23\r\n" +
                    "EXTDEF D4\r\n" +
                    "EXTREF D2\r\n" +
                    "EXTREF D546\r\n" +
                    "T1 RESB 10\r\n" +
                    "D23 RESW 10\r\n" +
                    "D4 SAVER1 D546\r\n" +
                    "D42 LOADR1 [T1]\r\n" +
                    "RESB 10\r\n" +
                    "A2 CSECT\r\n" +
                    "EXTDEF D2\r\n" +
                    "EXTREF D4\r\n" +
                    "EXTREF D58\r\n" +
                    "D2 SAVER1 [D2]\r\n" +
                    "B2 BYTE X\"2F4C008A\"\r\n" +
                    "B3 BYTE C\"Hello!\"\r\n" +
                    "B4 BYTE 128\r\n" +
                    "LOADR1 [B2]\r\n" +
                    "LOADR2 [B4]\r\n" +
                    "LOADR1 [D2]\r\n" +
                    "T3 NOP\r\n" +
                    "END 0";
            }
            if (exampleComboBox.SelectedIndex == 2)
            {
                typeAdr = 2;
                sourceCodeTextBox.Text = 
                    "prog start 0\r\n" +
                    "EXTDEF D23\r\n" +
                    "EXTDEF D4\r\n" +
                    "EXTREF D2\r\n" +
                    "EXTREF D546\r\n" +
                    "D4 RESB 10\r\n" +
                    "D23 RESB 10\r\n" +
                    "T1 jmp D2\r\n" +
                    "SAVER1 D546\r\n" +
                    "D42 LOADR2 [T1]\r\n" +
                    "RESB 10\r\n" +
                    "A2 CSECT\r\n" +
                    "EXTDEF D2\r\n" +
                    "EXTREF D4\r\n" +
                    "D2 SAVER1 [D2]\r\n" +
                    "LOADR1 [D2]\r\n" +
                    "T2 NOP\r\n" +
                    "END 0";

                sourceCodeTextBox.Text = "prog start 0\r\n" +
                   "EXTDEF D23\r\n" +
                   "EXTDEF D4\r\n" +
                   "EXTREF D2\r\n" +
                   "EXTREF D546\r\n" +
                   "T1 RESB 10\r\n" +
                   "D23 RESW 10\r\n" +
                   "D4 SAVER1 D546\r\n" +
                   "D42 LOADR1 [T1]\r\n" +
                   "RESB 10\r\n" +
                   "A2 CSECT\r\n" +
                   "EXTDEF D2\r\n" +
                   "EXTREF D4\r\n" +
                   "EXTREF D58\r\n" +
                   "D2 SAVER1 [D2]\r\n" +
                   "B2 BYTE X\"2F4C008A\"\r\n" +
                   "B3 BYTE C\"Hello!\"\r\n" +
                   "B4 BYTE 128\r\n" +
                   "LOADR1 B2\r\n" +
                   "LOADR2 [B4]\r\n" +
                   "LOADR1 D2\r\n" +
                   "T3 NOP\r\n" +
                   "END 0";
            }
        }
    }
}
