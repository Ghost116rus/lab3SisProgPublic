using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace lab1SisProg
{
    public class FSPass : Pass
    {
        readonly DataCheck dC = new DataCheck();

        //Проверка ТКО
        public bool CheckOperationCode(string[,] OCA)
        {
            int rows = OCA.GetLength(0);

            for (int i = 0; i < rows; i++)
            {

                if (OCA[i, 0] == "" || OCA[i, 1] == "" || OCA[i, 2] == "")
                {
                    errorText = $"В строке {i + 1} ошибка. Недопустима пустая ячейка в ТКО";
                    return false;
                }

                if (OCA[i, 0].Length > 6 || OCA[i, 1].Length > 2 || OCA[i, 2].Length > 1)
                {
                    errorText = $"В строке {i + 1} ошибка. Недопустимый размер строки в ТКО. Команда - от 1 до 6. Код - от 1 до 2. Длина - не более одного";
                    return false;
                }

                if (!dC.CheckLettersAndNumbers(OCA[i, 0]))
                {
                    errorText = $"В строке {i + 1} ошибка. Недопустимый символ в поле команды";
                    return false;
                }

                if (dC.CheckNumbers(OCA[i, 0]))
                {
                    errorText = $"В строке {i + 1} ошибка. Некорректный МКО";
                    return false;
                }

                if (!dC.CheckLettersAndNumbers(OCA[i, 0]))
                {
                    errorText = $"В строке {i + 1} ошибка. В поле команды недопустимый символ";
                }

                //Проверка на 16чные цифры
                if (dC.CheckAdress(OCA[i, 1]))
                {
                    if (dC.CheckRegisters(OCA[i, 0]) || dC.CheckDirective(OCA[i, 0]))
                    {
                        errorText = $"В строке {i + 1} ошибка. Код команды является зарезервированным словом";
                        return false;
                    }

                    if (Converter.ConvertHexToDec(OCA[i, 1]) > 63)
                    {
                        errorText = $"В строке {i + 1} ошибка. Код команды не должен превышать 3F";
                        return false;
                    }
                    else
                    {
                        if (OCA[i, 1].Length == 1)
                            OCA[i, 1] = Converter.ConvertToTwoChars(OCA[i, 1]);
                    }
                }
                else
                {
                    errorText = $"В строке {i + 1} ошибка. Недопустимые символы в поле кода";
                    return false;
                }

                if (dC.CheckNumbers(OCA[i, 2]))
                {
                    int res = int.Parse(OCA[i, 2]);

                    /*if(res <= 0 || res > 4)
                    {
                        errorText = $"В строке {i + 1} ошибка. Недопустимый размер команды. Должен быть от 1 до 4";
                        return false;
                    }*/
                    if (res <= 0 || res > 4 || res == 3)
                    {
                        errorText = $"В строке {i + 1} ошибка. Недопустимый размер команды. Должен быть 1, 2 или 4";
                        return false;
                    }
                }
                else
                {
                    errorText = $"В строке {i + 1} ошибка. Недопустимые символы в поле размера операции";
                    return false;
                }


                for (int k = i + 1; k < rows; k++)
                {
                    string str1 = OCA[i, 0];
                    string str2 = OCA[k, 0];
                    if (Equals(str1, str2))
                    {
                        errorText = $"В строке {i + 1} ошибка. В поле команда найдены совпадения";
                        return false;
                    }
                }


                for (int k = i + 1; k < rows; k++)
                {
                    string str1 = Converter.ConvertHexToDec(OCA[i, 1]).ToString();
                    string str2 = Converter.ConvertHexToDec(OCA[k, 1]).ToString();
                    if (Equals(str1, str2))
                    {
                        errorText = $"В строке {i + 1} ошибка. В поле кода операции найдены совпадения";
                        return false;
                    }
                }
            }

            return true;
        }

        //Первый проход
        public bool DoFirstPass(string[,] sourceCode, string[,] operationCode, DataGridView supportTableDG, DataGridView symbolTableDG)
        {
            startAddress = 0;
            endAddress = 0;
            countAddress = 0;

            string prevOC = "";

            List<string> sectionNames = new List<string>();
            List<string> externalDefNames = new List<string>();
            List<string> externalRefNames = new List<string>();

            symbolTable.Add(new List<string>());
            symbolTable.Add(new List<string>());
            symbolTable.Add(new List<string>());
            symbolTable.Add(new List<string>());

            supportTable.Add(new List<string>());
            supportTable.Add(new List<string>());
            supportTable.Add(new List<string>());
            supportTable.Add(new List<string>());

            int numberRows = sourceCode.GetLength(0);

            bool flagStart = false;
            bool flagEnd = false;

            int CSECTCount = 0;
            int countStart = 0;

            string currentCsectName = "";
            string OC = "";


            for (int i = 0; i < numberRows; i++)
            {
                if (flagStart)
                {
                    if (countAddress > memmoryMax)
                    {
                        errorText = $"В строке {i + 1} ошибка. Произошло переполнение";
                        return false;
                    }
                }

                if (flagEnd)
                    break;

                prevOC = OC;
                if (!dC.CheckRow(sourceCode, i, out string mark, out OC, out string OP1, out string OP2, nameProg))
                {
                    errorText = $"В строке {i + 1} синтаксическая ошибка.";
                    return false;
                }

                if (OC.ToUpper() != "EXTDEF" && OC.ToUpper() != "EXTREF")
                {
                    string strch = "";

                    for (int j = 0; j <= operationCode.GetUpperBound(0); j++)
                    {
                        if (Equals(mark.ToUpper(), operationCode[j, 0].ToUpper()))
                        {
                            errorText = $"В строке {i + 1} ошибка. Символическое имя не может совпадать с названием команды";
                            return false;
                        }
                    }

                    currentCsectName = nameProg;
                    int markRow = FindMarkInMarkTable(mark, ref strch, ref currentCsectName);
                    if (strch == "Er")
                    {
                        errorText = $"В строке {i + 1} ошибка. Ошибка в метке {mark}";
                        return false;
                    }
                    if (markRow != -1 && strch == "")
                    {
                        errorText = $"В строке {i + 1} ошибка. Найдена уже существующая метка {mark}";
                        return false;
                    }
                    else
                    {
                        if (strch == "mk")
                        {
                            if (mark != "" && flagStart && OC.ToUpper() != "CSECT")
                            {
                                if (symbolTable[2][markRow].ToUpper() != nameProg.ToUpper())
                                {
                                    errorText = $"В строке {i + 1} ошибка. Внешнее имя описано не в своей управляющей секции";
                                    return false;
                                }

                                symbolTable[1][markRow] = Converter.ConvertToSixChars(Converter.ConvertDecToHex(countAddress));
                                symbolTable[2][markRow] = nameProg.ToUpper();
                            }
                        }
                        if (strch == "")
                        {
                            if (mark != "" && flagStart && OC.ToUpper() != "CSECT")
                            {
                                symbolTable[0].Add(mark.ToUpper());
                                symbolTable[1].Add(Converter.ConvertToSixChars(Converter.ConvertDecToHex(countAddress)));
                                symbolTable[2].Add(nameProg);
                                symbolTable[3].Add("");
                            }
                        }
                    }
                }
                else
                {
                    if (mark != "" && (OC == "EXTDEF" || OC == "EXTREF"))
                    {
                        errorText = $"В строке {i + 1} ошибка. Программа не может обработать поле метки";
                        return false;
                    }
                }


                if (dC.CheckDirective(OC))
                {
                    switch (OC)
                    {
                        case "START":
                            {
                                countStart++;
                                if (i == 0 && !flagStart)
                                {
                                    flagStart = true;

                                    if (dC.CheckAdress(OP1) || OP1 == "")
                                    {
                                        OP1 = OP1.TrimStart('0');

                                        if (OP1 == "")
                                            OP1 = "0";

                                        countAddress = Converter.ConvertHexToDec(OP1);

                                        if (countAddress > 0)
                                        {
                                            errorText = $"В строке {i + 1} ошибка. Адрес начала программы должен быть равен нулю";
                                            return false;
                                        }

                                        startAddress = countAddress;

                                        if (countAddress > memmoryMax || countAddress < 0)
                                        {
                                            errorText = $"В строке {i + 1} ошибка. Неправильный адрес загрузки";
                                            return false;
                                        }

                                        if (mark == "")
                                        {
                                            errorText = $"В строке {i + 1} ошибка. Не задано имя программы";
                                            return false;
                                        }

                                        if (mark.Length > 10)
                                        {
                                            errorText = $"В строке {i + 1} ошибка. Первышена длина имени программы\n Имя программы должно быть не больше 10 символов";
                                            return false;
                                        }

                                        for (int j = 0; j <= operationCode.GetUpperBound(0); j++)
                                        {
                                            if (Equals(mark.ToUpper(), operationCode[j, 0].ToUpper()))
                                            {
                                                errorText = $"В строке {i + 1} ошибка. Имя программы не может совпадать с названием команды";
                                                return false;
                                            }
                                        }

                                        AddToSupportTable(mark, OC, Converter.ConvertToSixChars(OP1), "");
                                        nameProg = mark;
                                        sectionNames.Add(mark);

                                        if (OP2.Length > 0)
                                        {
                                            errorText = $"В строке {i + 1} второй операнд директивы START не рассматривается. Устраните и повторите заново.";
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Неверный адрес начала программы";
                                        return false;
                                    }
                                }
                                else
                                {
                                    if (countStart == 1)
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Директива START должна находится в 1-й строке";
                                        return false;
                                    }
                                    else
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Повторное использование директивы START";
                                        return false;
                                    }
                                }
                            }
                            break;

                        case "CSECT":
                            {
                                CSECTCount++;
                                if (!flagStart)
                                {
                                    errorText = $"В строке {i + 1} ошибка. Программа не может начинаться с управляющей секции";
                                    return false;
                                }

                                endSection.Add(Converter.ConvertToSixChars(Converter.ConvertDecToHex(countAddress)));
                                int odlAdressCount = countAddress;
                                countAddress = 0;
                                startAddress = countAddress;

                                if (dC.CheckLettersAndNumbers(OP1) || OP1 == "")
                                {
                                    if (OP1 == "")
                                        OP1 = "0";

                                    if (countAddress > 0)
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Адрес начала программы должен быть равен 0";
                                        return false;
                                    }

                                    if (countAddress > memmoryMax)
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Адресс программы выходит за диапазон памяти";
                                        return false;
                                    }

                                    if (mark == "")
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Не задано имя программы";
                                        return false;
                                    }

                                    if (mark.Length > 10)
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Превышена длина имени программы";
                                        return false;
                                    }

                                    for (int j = 0; j < externalDefNames.Count; j++)
                                    {
                                        if(Equals(mark.ToUpper(), externalDefNames[j].ToUpper()))
                                        {
                                            errorText = $"В строке {i + 1} ошибка. Имя секции не может совпадать с внешним именем";
                                            return false;
                                        }
                                    }

                                    for (int j = 0; j <= operationCode.GetUpperBound(0); j++)
                                    {
                                        if (Equals(mark.ToUpper(), operationCode[j, 0].ToUpper()))
                                        {
                                            errorText = $"В строке {i + 1} ошибка. Имя секции не может совпадать с названием команды";
                                            return false;
                                        }
                                    }

                                    foreach (string str in sectionNames)
                                    {
                                        if (str.Equals(mark))
                                        {
                                            errorText = $"В строке {i + 1} ошибка. Имя секции совпадает с системным именем";
                                            return false;
                                        }
                                    }

                                    if(dC.CheckDirective(OP1) || dC.CheckRegisters(OP1))
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Имя секции совпадает с системным именем";
                                        return false;
                                    }

                                    AddToSupportTable(Converter.ConvertToSixChars(Converter.ConvertDecToHex(odlAdressCount)), OC, mark, "");
                                    nameProg = mark;
                                    sectionNames.Add(mark);

                                    if (OP2.Length > 0)
                                    {
                                        errorText = $"В строке {i + 1} второй операнд директивы CSECT не рассматривается. Устраните и повторите заново.";
                                        return false;
                                    }
                                }
                                else
                                {
                                    errorText = $"В строке {i + 1} ошибка. Неверный адрес начала программы";
                                    return false;
                                }
                            }
                            break;

                        case "EXTREF":
                            {
                                if (prevOC == "EXTDEF" || prevOC == "EXTREF" || prevOC == "START" || prevOC == "CSECT")
                                {

                                    if (OP1.Length > 0)
                                    {
                                        if (!dC.CheckLettersAndNumbers(OP1))
                                        {
                                            errorText = $"В строке {i + 1} ошибка. Недопустимые символы в операнде";
                                            return false;
                                        }

                                        if (!dC.CheckLetters(OP1[0].ToString()))
                                        {
                                            errorText = $"В строке {i + 1} ошибка. Внешняя ссылка не должна начинаться с цифры";
                                            return false;
                                        }

                                        if (OP1.Length > 10)
                                        {
                                            errorText = $"В строке {i + 1} ошибка. Првышеная длина имени внешней ссылки";
                                            return false;
                                        }

                                        for (int j = 0; j <= operationCode.GetUpperBound(0); j++)
                                        {
                                            if (Equals(OP1, operationCode[j, 0]))
                                            {
                                                errorText = $"В строке {i + 1} ошибка. Имя внешней ссылки не может совпадать с названием команды";
                                                return false;
                                            }
                                        }

                                        foreach (var item in sectionNames)
                                        {
                                            if (item.Equals(OP1))
                                            {
                                                errorText = $"В строке {i + 1} ошибка. Имя внешенй ссылки совпадает с именем секции";
                                                return false;
                                            }
                                        }

                                        if (dC.CheckDirective(OP1) || dC.CheckRegisters(OP1))
                                        {
                                            errorText = $"В строке {i + 1} ошибка. Имя секции совпадает с системным именем";
                                            return false;
                                        }

                                        for (int j = 0; j < symbolTable[0].Count; j++)
                                        {
                                            if (Equals(nameProg, symbolTable[2][j]) && Equals(OP1.ToUpper(), symbolTable[0][j].ToUpper()))
                                            {
                                                errorText = $"В строке {i + 1} ошибка. Имя внешней ссылки не может совпадать с внешним именем, в одной и той же секции";
                                                return false;
                                            }
                                        }

                                        AddToSymbolTable(OP1, "", nameProg.ToUpper(), "ВС");

                                        AddToSupportTable(mark, OC, OP1, "");

                                        externalRefNames.Add(OP1);

                                        if (OP2.Length > 0)
                                        {
                                            errorText = $"В строке {i + 1} ошибка. Второй операнд директивы EXTREF не рассматривается. Устраните и повторите заново.";
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Не задана внешняя ссылка.";
                                        return false;
                                    }
                                }
                                else
                                {
                                    errorText = $"В строке {i + 1} ошибка. Неверная позиция EXTREF";
                                    return false;
                                }
                            }
                            break;
                        case "EXTDEF":
                            {
                                if (prevOC == "EXTDEF" || prevOC == "START" || prevOC == "CSECT")
                                {

                                    if (OP1.Length > 0)
                                    {
                                        if (!dC.CheckLettersAndNumbers(OP1))
                                        {
                                            errorText = $"В строке {i + 1} ошибка. Недопустимые символы в операнде";
                                            return false;
                                        }

                                        if (!dC.CheckLetters(OP1[0].ToString()))
                                        {
                                            errorText = $"В строке {i + 1} ошибка. Внешняя ссылка не должна начинаться с цифры";
                                            return false;
                                        }

                                        if (OP1.Length > 10)
                                        {
                                            errorText = $"В строке {i + 1} ошибка. Првышеная длина имени внешней ссылки";
                                            return false;
                                        }

                                        for (int j = 0; j <= operationCode.GetUpperBound(0); j++)
                                        {
                                            if (Equals(OP1, operationCode[j, 0]))
                                            {
                                                errorText = $"В строке {i + 1} ошибка. Имя внешней ссылки не может совпадать с названием команды";
                                                return false;
                                            }
                                        }

                                        foreach (var item in sectionNames)
                                        {
                                            if (item.Equals(OP1))
                                            {
                                                errorText = $"В строке {i + 1} ошибка. Имя внешенй ссылки совпадает с именем секции";
                                                return false;
                                            }
                                        }

                                        /*if (dC.CheckDirective(OP1) || dC.CheckRegisters(OP1))
                                        {
                                            errorText = $"В строке {i + 1} ошибка. Имя секции совпадает с системным именем";
                                            return false;
                                        }

                                        foreach (var item in externalDefNames)
                                        {
                                            if (item.Equals(OP1))
                                            {
                                                errorText = $"В строке {i + 1} ошибка. Повторение внешнего имени";
                                                return false;
                                            }

                                        }*/
                                        //symbolTableDataGrid

                                        /*symbolTable[0].Add(OP1);
                                        symbolTable[1].Add("");
                                        symbolTable[2].Add(nameProg);
                                        symbolTable[3].Add("BИ");*/

                                        AddToSymbolTable(OP1, "", nameProg.ToUpper(), "ВИ");

                                        AddToSupportTable(mark, OC, OP1, "");

                                        externalDefNames.Add(OP1);

                                        if (OP2.Length > 0)
                                        {
                                            errorText = $"В строке {i + 1} ошибка. Второй операнд директивы EXTDEF не рассматривается. Устраните и повторите заново.";
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Не задана внешняя ссылка.";
                                        return false;
                                    }
                                }
                                else
                                {
                                    errorText = $"В строке {i + 1} ошибка. Неверная позиция EXTDEF";
                                    return false;
                                }
                            }
                            break;

                        case "WORD":
                            {
                                if (int.TryParse(OP1, out int numb))
                                {
                                    if (numb >= 0 && numb <= memmoryMax)
                                    {
                                        if (!AddCheckError(i, 3, OC, Convert.ToString(numb), ""))
                                            return false;

                                        if (OP2.Length > 0)
                                        {
                                            errorText = $"В строке {i + 1} второй операнд директивы WORD не рассматривается. Устраните и повторите заново.";
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Отрицательное число либо превышено максимальное значение числа";
                                        return false;
                                    }
                                }
                                /*else
                                {
                                    if (OP1.Length == 1 && OP1 == "?")
                                    {
                                        if (!AddCheckError(i, 3, OC, Convert.ToString(numb), ""))
                                            return false;

                                        if (OP2.Length > 0)
                                            errorText = $"В строке {i + 1} второй операнд директивы WORD не рассматривается";
                                    }
                                    else
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Отрицательное число либо превышено максимальное значение числа";
                                        return false;
                                    }
                                }*/
                            }
                            break;

                        case "BYTE":
                            {

                                if (int.TryParse(OP1, out int numb))
                                {
                                    if (numb >= 0 && numb <= 255)
                                    {
                                        if (!AddCheckError(i, 1, OC, Convert.ToString(numb), ""))
                                            return false;

                                        if (OP2.Length > 0)
                                        {
                                            errorText = $"В строке {i + 1} второй операнд директивы BYTE не рассматривается. Устраните и повторите заново.";
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Отрицательное число либо превышено максимальное значение числа";
                                        return false;
                                    }
                                }
                                else
                                {
                                    string symb = dC.CheckAndGetString(OP1);

                                    if (symb != "")
                                    {
                                        if (!AddCheckError(i, symb.Length, OC, OP1, ""))
                                            return false;

                                        if (OP2.Length > 0)
                                        {
                                            errorText = $"В строке {i + 1} второй операнд директивы BYTE не рассматривается. Устраните и повторите заново.";
                                            return false;
                                        }

                                        continue;
                                    }

                                    symb = dC.CheckAndGetByteString(OP1);

                                    if (symb != "")
                                    {
                                        if (symb.Length % 2 == 0)
                                        {
                                            if (!AddCheckError(i, symb.Length / 2, OC, OP1, ""))
                                                return false;

                                            if (OP2.Length > 0)
                                            {
                                                errorText = $"В строке {i + 1} второй операнд директивы BYTE не рассматривается. Устраните и повторите заново.";
                                                return false;
                                            }

                                            continue;
                                        }
                                        else
                                        {
                                            errorText = $"В строке {i + 1} ошибка. Невозможно преобразовать BYTE нечетное количество символов";
                                            return false;
                                        }
                                    }

                                    /*if (OP1.Length == 1 && OP1 == "?")
                                    {
                                        if (!AddCheckError(i, 1, OC, OP1, ""))
                                            return false;

                                        if (OP2.Length > 0)
                                            errorText = $"В строке {i + 1} второй операнд директивы BYTE не рассматривается";

                                    }
                                    else
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Неверный формат строки {OP1}";
                                        return false;
                                    }*/

                                    errorText = $"В строке {i + 1} ошибка. Неверный формат строки {OP1}";
                                    return false;
                                }
                            }
                            break;

                        case "RESB":
                            {
                                if (int.TryParse(OP1, out int numb))
                                {
                                    if (numb > 0)
                                    {
                                        if (!AddCheckError(i, numb, OC, Convert.ToString(numb), ""))
                                            return false;

                                        if (OP2.Length > 0)
                                        {
                                            errorText = $"В строке {i + 1} второй операнд директивы RESB не рассматривается. Устраните и повторите заново.";
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Количество байт равно нулю или меньше нуля";
                                        return false;
                                    }
                                }
                                else
                                {
                                    errorText = $"В строке {i + 1} ошибка. Невозможно выполнить преобразование {OP1}";
                                    return false;
                                }
                            }
                            break;

                        case "RESW":
                            {
                                if (int.TryParse(OP1, out int numb))
                                {
                                    if (numb > 0)
                                    {
                                        if (!AddCheckError(i, numb * 3, OC, Convert.ToString(numb), ""))
                                            return false;

                                        if (OP2.Length > 0)
                                        {
                                            errorText = $"В строке {i + 1} второй операнд директивы RESW не рассматривается. Устраните и повторите заново.";
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Количество байт равно нулю или меньше нуля";
                                        return false;
                                    }
                                }
                                else
                                {
                                    errorText = $"В строке {i + 1} ошибка. Невозможно выполнить преобразование {OP1}";
                                    return false;
                                }
                            }
                            break;

                        case "END":
                            {
                                if (mark.Length > 0)
                                {
                                    errorText = $"В строке {i + 1} метка. Устраните и повторите заново.";
                                    return false;
                                }

                                if (flagStart && !flagEnd)
                                {
                                    flagEnd = true;
                                    if (OP1.Length == 0)
                                    {
                                        endAddress = startAddress;
                                        endSection.Add(Converter.ConvertToSixChars(Converter.ConvertDecToHex(countAddress)));
                                        AddToSupportTable(Converter.ConvertToSixChars(Converter.ConvertDecToHex(countAddress)), OC, Convert.ToString("0"), "");
                                    }
                                    else
                                    {
                                        if (dC.CheckAdress(OP1))
                                        {
                                            endAddress = Converter.ConvertHexToDec(OP1);
                                            if (endAddress >= startAddress && endAddress <= countAddress)
                                            {
                                                endSection.Add(Converter.ConvertToSixChars(Converter.ConvertDecToHex(countAddress)));
                                                AddToSupportTable(Converter.ConvertToSixChars(Converter.ConvertDecToHex(countAddress)), OC, Convert.ToString(OP1), "");
                                                break;
                                            }
                                            else
                                            {
                                                errorText = $"В строке {i + 1} ошибка. Неверный адрес входа в программу";
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            errorText = $"В строке {i + 1} ошибка. Неверный адрес входа в программу";
                                            return false;
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }
                else
                {
                    if (OC.Length > 0)
                    {
                        int numb = FindCode(OC, operationCode);
                        if (numb > -1)
                        {
                            if (operationCode[numb, 2] == "1")
                            {
                                if (!AddCheckError(i, 1, Converter.ConvertToTwoChars(Converter.ConvertDecToHex(Converter.ConvertHexToDec(operationCode[numb, 1]) * 4)), "", ""))
                                    return false;

                                if (OP1.Length > 0 || OP2.Length > 0)
                                {
                                    errorText = $"В строке {i + 1} операнды не рассматривается в команде {operationCode[numb, 0]}. Устраните и повторите заново.";
                                    return false;
                                }
                            }

                            else if (operationCode[numb, 2] == "2")
                            {
                                if (int.TryParse(OP1, out int number))
                                {

                                    if (number >= 0 && number <= 255)
                                    {
                                        if (!AddCheckError(i, 2, Converter.ConvertToTwoChars(Converter.ConvertDecToHex(Converter.ConvertHexToDec(operationCode[numb, 1]) * 4)), OP1, ""))
                                            return false;

                                        if (OP2.Length > 0)
                                        {
                                            errorText = $"В строке {i + 1} второй операнд не рассматривается в команде {operationCode[numb, 0]}. Устраните и повторите заново.";
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Отрицательное число либо превышено максимальное значение числа";
                                        return false;
                                    }
                                }
                                else
                                {
                                    if (dC.CheckRegisters(OP1) && dC.CheckRegisters(OP2))
                                    {
                                        if (!AddCheckError(i, 2, Converter.ConvertToTwoChars(Converter.ConvertDecToHex(Converter.ConvertHexToDec(operationCode[numb, 1]) * 4)), OP1, OP2))
                                            return false;
                                    }
                                    else
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Ошибка в команде {operationCode[numb, 0]}";
                                        return false;
                                    }
                                }
                            }
                            /*else if (operationCode[numb, 2] == "3")
                            {
                                if (!AddCheckError(i, 3, Converter.ConvertToTwoChars(Converter.ConvertDecToHex(Converter.ConvertHexToDec(operationCode[numb, 1]) * 4 + 1)), OP1, OP2))
                                    return false;

                                if (OP2.Length > 0)
                                    errorText = $"В строке {i + 1} второй операнд не рассматривается в команде {operationCode[numb, 0]}";
                            }*/
                            else if (operationCode[numb, 2] == "4")
                            {
                                int number;
                                if (OP1.Length > 0)
                                {
                                    if (OP1[0] == '[' && OP1[OP1.Length - 1] == ']')
                                        number = Converter.ConvertHexToDec(operationCode[numb, 1]) * 4 + 2;
                                    else
                                        number = Converter.ConvertHexToDec(operationCode[numb, 1]) * 4 + 1;
                                }
                                else
                                {
                                    errorText = $"В строке {i + 1} ошибка. Не найден операнд";
                                    return false;
                                }

                                if (!AddCheckError(i, 4, Converter.ConvertToTwoChars(Converter.ConvertDecToHex(number)), OP1, OP2))
                                    return false;

                                if (OP2.Length > 0)
                                {
                                    errorText = $"В строке {i + 1} второй операнд не рассматривается в команде {operationCode[numb, 0]}. Устраните и повторите заново.";
                                    return false;
                                }
                            }
                            else
                            {
                                errorText = $"В строке {i + 1} размер команды больше установленного";
                                return false;
                            }
                        }
                        else
                        {
                            errorText = $"В строке {i + 1} ошибка. В ТКО не найдено {OC}";
                            return false;
                        }
                    }
                    else
                    {
                        errorText = $"В строке {i + 1} ошибка. Синтаксическая ошибка";
                        return false;
                    }
                }

            }

            if (!dC.CheckExternalNames(symbolTable))
            {
                errorText = $"Найдено не определенное внешнее имя";
                return false;
            }

            if (!flagEnd)
            {
                errorText = $"Не найдена точка входа в программу";
                return false;
            }

            for (int i = 0; i < supportTable[0].Count; i++)
            {
                supportTableDG.Rows.Add();
                supportTableDG.Rows[i].Cells[0].Value = supportTable[0][i];
                supportTableDG.Rows[i].Cells[1].Value = supportTable[1][i];
                supportTableDG.Rows[i].Cells[2].Value = supportTable[2][i];
                supportTableDG.Rows[i].Cells[3].Value = supportTable[3][i];
            }

            for (int i = 0; i < symbolTable[1].Count; i++)
            {
                symbolTableDG.Rows.Add();
                symbolTableDG.Rows[i].Cells[0].Value = symbolTable[0][i];
                symbolTableDG.Rows[i].Cells[1].Value = symbolTable[1][i];
                symbolTableDG.Rows[i].Cells[2].Value = symbolTable[2][i];
                symbolTableDG.Rows[i].Cells[3].Value = symbolTable[3][i];
            }

            return true;
        }

        private bool AddCheckError(int i, int numbToAdd, string OC, string OP1, string OP2)
        {
            if (countAddress + numbToAdd > memmoryMax)
            {
                errorText = $"В строке {i + 1} ошибка. Произошло переполнение";
                return false;
            }

            AddToSupportTable(Converter.ConvertToSixChars(Converter.ConvertDecToHex(countAddress)), OC, OP1, OP2);

            countAddress += numbToAdd;

            if (!CheckMemmory())
                return false;

            return true;
        }

        public bool DoSecondPass(TextBox BC, DataGridView settingTableDG)
        {
            errorText = "";

            List<string> segmentEnd = new List<string>();
            List<string> extref = new List<string>();

            foreach (var item in endSection)
                segmentEnd.Add(item);

            List<List<string>> settingTable = new List<List<string>>();

            string currnetCSECTName = "";

            for (int i = 0; i < supportTable[0].Count; i++)
            {
                string address = supportTable[0][i];
                string OC = supportTable[1][i];
                string OP1 = supportTable[2][i];
                string OP2 = supportTable[3][i];

                if (OC == "START" || OC == "CSECT")
                {
                    if (OC == "START")
                    {
                        if (endSection.Count > 0)
                        {
                            extref.Clear();
                            for (int j = 0; j < settingTable.Count; j++)
                            {
                                if (settingTable[j][1] == currnetCSECTName.ToUpper())
                                    BC.Text += Converter.ConvertToBinaryCodeSetting(settingTable[j][0]) + "\r\n";
                            }

                            currnetCSECTName = supportTable[0][i];

                            BC.Text += Converter.ConvertToBinaryCodeSTART(supportTable[0][i], supportTable[2][0], endSection[0]) + "\r\n";
                            endSection.RemoveAt(0);
                        }
                        else
                        {
                            errorText = $"В сроке {i + 1} ошибка в START";
                            BC.Clear();
                            return false;
                        }
                    }

                    if (OC == "CSECT")
                    {
                        if (endSection.Count > 0)
                        {
                            extref.Clear();
                            for (int j = 0; j < settingTable.Count; j++)
                            {
                                if (settingTable[j][1].ToUpper() == currnetCSECTName.ToUpper())
                                    BC.Text += Converter.ConvertToBinaryCodeSetting(settingTable[j][0]) + "\r\n";
                            }

                            BC.Text += Converter.ConvertToBinaryCodeEND("000000") + "\r\n";
                            currnetCSECTName = supportTable[2][i];

                            BC.Text += Converter.ConvertToBinaryCodeSTART(supportTable[2][i], "000000", endSection[0]) + "\r\n";
                            endSection.RemoveAt(0);
                        }
                        else
                        {
                            errorText = $"В строке {i + 1} ошибка в CSECT";
                            BC.Clear();
                            return false;
                        }
                    }
                }
                else
                {
                    if (OC == "EXTDEF")
                    {
                        int find = -1;

                        for (int j = 0; j < symbolTable[0].Count; j++)
                        {
                            if (OP1 == symbolTable[0][j] && currnetCSECTName.ToUpper() == symbolTable[2][j] && symbolTable[3][j] == "ВИ")
                            {
                                find = j;
                                break;
                            }
                        }

                        if (find > -1)
                        {
                            BC.Text += Converter.ConvertToBinaryCodeD(Converter.ConvertToSixChars(symbolTable[1][find]), OP1) + "\r\n";
                            continue;
                        }
                        else
                        {
                            errorText = $"В строке {i + 1} ошибка в EXTDEF";
                            BC.Clear();
                            return false;
                        }
                    }

                    if (OC == "EXTREF")
                    {
                        extref.Add(OP1);
                        BC.Text += Converter.ConvertToBinaryCodeR(OP1) + "\r\n";
                        continue;
                    }

                    //ОТСЮДОВА НАЧИНАЙ
                    string res = CheckOP(OP1, out bool error, out bool flagMark, out string tuneAddress, i, currnetCSECTName);

                    if (error)
                    {
                        errorText = $"В строке {i + 1} ошибка. Ошибка при вычислении операндной части.";
                        BC.Clear();
                        break;
                    }

                    SettingTable(tuneAddress, currnetCSECTName, settingTable);

                    string ress = CheckOP(OP2, out error, out _, out tuneAddress, i, currnetCSECTName);

                    if (error)
                    {
                        errorText = $"В строке {i + 1} ошибка. Ошибка при вычислении операндной части.";
                        BC.Clear();
                        break;
                    }

                    SettingTable(tuneAddress, currnetCSECTName, settingTable);

                    if (dC.CheckDirective(OC))
                    {
                        if (OC == "RESB")
                        {
                            BC.Text += Converter.ConvertToBinaryCode(address, "", res, "", "") + "\r\n";
                            continue;
                        }
                        else if (OC == "RESW")
                        {
                            BC.Text += Converter.ConvertToBinaryCode(address, "", Converter.ConvertToTwoChars(Converter.ConvertDecToHex(Convert.ToInt32(OP1) * 3)), "", "") + "\r\n";
                            continue;
                        }
                        else if (OC == "BYTE")
                        {
                            BC.Text += Converter.ConvertToBinaryCode(address, "", Converter.ConvertToTwoChars(Converter.ConvertDecToHex(res.Length + ress.Length)), res, ress) + "\r\n";
                            continue;
                        }
                        else if (OC == "WORD")
                        {
                            BC.Text += Converter.ConvertToBinaryCode(address, "", Converter.ConvertToTwoChars(Converter.ConvertDecToHex(Converter.ConvertToSixChars(res).Length + ress.Length)), Converter.ConvertToSixChars(res), ress) + "\r\n";
                            continue;
                        }
                        /*else if (OC == "BYTE" && OP1 == "?")
                        {
                            BC.Text += Converter.ConvertToBinaryCode(address, "", Converter.ConvertToTwoChars(Converter.ConvertDecToHex(1)), "", "") + "\r\n";
                            continue;
                        }
                        else if (OC == "WORD" && OP1 == "?")
                        {
                            BC.Text += Converter.ConvertToBinaryCode(address, "", Converter.ConvertToTwoChars(Converter.ConvertDecToHex(3)), "", "") + "\r\n";
                            continue;
                        }*/
                    }
                    else
                    {
                        int type = (byte)Converter.ConvertHexToDec(OC) & 0x03;
                        if (type == 1)
                        {
                            if (!flagMark)
                            {
                                errorText = $"В строке {i + 1} ошибка. Для данного типа адресации операнд должен быть меткой";
                                BC.Clear();
                                return false;
                            }
                            if (ress != "")
                            {
                                errorText = $"В строке {i + 1} ошибка. Данный тип адрессации поддерживает один операнд";
                                BC.Clear();
                                return false;
                            }
                        }

                        BC.Text += Converter.ConvertToBinaryCode(address, OC,
                            Converter.ConvertToTwoChars(Converter.ConvertDecToHex(OC.Length + res.Length + ress.Length)), res, ress) + "\r\n";
                    }
                }
                if (settingTable.Count > 0)
                {
                    settingTableDG.Rows.Clear();
                    for (int j = 0; j < settingTable.Count; j++)
                        settingTableDG.Rows.Add(settingTable[j][0], settingTable[j][1]);
                }
            }

            for (int j = 0; j < settingTable.Count; j++)
            {
                if (settingTable[j][1].ToUpper() == currnetCSECTName.ToUpper())
                    BC.Text += Converter.ConvertToBinaryCodeSetting(settingTable[j][0]) + "\r\n";
            }

            BC.Text += Converter.ConvertToBinaryCodeEND(Converter.ConvertToSixChars(Converter.ConvertDecToHex(endAddress))) + "\r\n";

            if (errorText != "")
                BC.Clear();

            return true;
        }

        public string CheckOP(string OP, out bool er, out bool operandLabel, out string adress, int ind, string csectName)
        {
            er = false;
            operandLabel = false;
            adress = "";
            int find = 0;
            if (OP != "")
            {
                if (OP[0] == '[' && OP[OP.Length - 1] == ']')
                {
                    string temp = OP;
                    temp = temp.Substring(1, temp.Length - 2);
                    for (int i = 0; i < symbolTable[0].Count; i++)
                    {
                        if (temp == symbolTable[0][i] && csectName.ToUpper() == symbolTable[2][i].ToUpper() && symbolTable[3][i] != "ВИ")
                        {
                            find++;
                            if (symbolTable[3][i] == "ВС")
                            {
                                er = true;
                                return "000000";
                            }
                            if (symbolTable[3][i] == "")
                            {
                                operandLabel = true;
                                return Converter.ConvertSubHex(symbolTable[1][i], supportTable[0][ind + 1]); 
                            }
                        }
                    }
                    if(find == 0)
                    {
                        for (int i = 0; i < symbolTable[0].Count; i++)
                        {
                            if (temp == symbolTable[0][i] && csectName.ToUpper() == symbolTable[2][i].ToUpper() && symbolTable[3][i] == "ВИ")
                            {
                                operandLabel = true;
                                return Converter.ConvertSubHex(symbolTable[1][i], supportTable[0][ind + 1]);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < symbolTable[0].Count; i++)
                    {
                        if(OP == symbolTable[0][i] && csectName.ToUpper() == symbolTable[2][i].ToUpper() && symbolTable[3][i] != "ВИ")
                        {
                            find++;
                            if (symbolTable[3][i] == "ВС")
                            {
                                operandLabel = true;
                                adress = supportTable[0][ind] + " " + symbolTable[0][i];
                                return "000000";
                            }
                            if (symbolTable[3][i] == "")
                            {
                                operandLabel = true;
                                adress = supportTable[0][ind];
                                return symbolTable[1][i];
                            }
                        }
                    }
                    if(find == 0)
                    {
                        for (int i = 0; i < symbolTable[0].Count; i++)
                        {
                            if (OP == symbolTable[0][i] && csectName.ToUpper() == symbolTable[2][i].ToUpper() && symbolTable[3][i] == "ВИ")
                            {
                                operandLabel = true; 
                                adress = supportTable[0][ind];
                                return symbolTable[1][i];
                            }
                        }
                    }
                }
                int reg = dC.GetRegisters(OP);
                if (reg > -1)
                    return Converter.ConvertDecToHex(reg);
                else if (dC.CheckNumbers(OP))
                    return Converter.ConvertDecToHex(Convert.ToInt32(OP));
                else
                {
                    string str = dC.CheckAndGetString(OP);
                    if (str != "")
                        return Converter.ConvertASCII(str);

                    str = dC.CheckAndGetByteString(OP);

                    if (str != "")
                        return str;

                    er = true;
                }
            }
            return "";
        }

        public bool SettingTable(string adr, string currentName, List<List<string>> settingTale)
        {
            if (adr.Length > 0)
            {
                int i;
                for (i = 0; i < settingTale.Count; i++)
                {
                    if (settingTale[i][0] == adr)
                        return true;   
                }
                settingTale.Add(new List<string>());
                settingTale[i].Add(adr);
                settingTale[i].Add(currentName);
            }
            return false;
        }
    }
}
