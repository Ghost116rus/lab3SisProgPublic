using System;
using System.Collections.Generic;

namespace lab1SisProg
{
    public class DataCheck
    {
        //-------------------------------------------------------------------------
        //simple checks
        public bool CheckLettersAndNumbers(string str)
        {
            if (str != null)
                str = str.ToUpper();
            else
                return false;

            char[] chars = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            return CycleFor(str, chars);
        }

        public bool CheckNumbers(string str)
        {
            char[] chars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            return CycleFor(str, chars);
        }

        public bool CheckLetters(string str)
        {
            if (str != null)
                str = str.ToUpper();
            else
                return false;

            char[] chars = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

            return CycleFor(str, chars);
        }


        public bool CheckAdress(string str)
        {
            if (str.Length > 0)
            {
                str = str.ToUpper();
                char[] chars = { 'A', 'B', 'C', 'D', 'E', 'F', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

                return CycleFor(str, chars);
            }
            else
                return false;
        }

        public bool CheckRegisters(string str)
        {
            if (str != null)
                str = str.ToUpper();
            else
                return false;

            string[] registers = { "R0", "R1", "R2", "R3", "R4", "R5", "R6", "R7", "R8", "R9", "R10", "R11", "R12", "R13", "R14", "R15" };

            for (int i = 0; i < registers.Length; i++)
                if (Array.IndexOf(registers, str) == -1)
                    return false;

            return true;
        }

        public int GetRegisters(string str)
        {
            str = str.ToUpper();

            string[] registers = { "R0", "R1", "R2", "R3", "R4", "R5", "R6", "R7", "R8", "R9", "R10", "R11", "R12", "R13", "R14", "R15" };

            return Array.IndexOf(registers, str);
        }

        public bool CheckDirective(string str)
        {
            if (str != null)
                str = str.ToUpper();
            else
                return false;

            string[] directives = { "START", "END", "BYTE", "WORD", "RESB", "RESW", "CSECT", "EXTREF", "EXTDEF" };

            if (Array.IndexOf(directives, str) == -1)
                return false;

            return true;
        }

        private bool CycleFor(string str, char[] chars)
        {
            for (int i = 0; i < str.Length; i++)
                if (Array.IndexOf(chars, str[i]) == -1)
                    return false;

            return true;
        }

        public string CheckAndGetString(string OP1)
        {
            if ((OP1.Length > 3) && (OP1[0] == 'C') && (OP1[1] == '"') && (OP1[OP1.Length - 1] == '"'))
                return OP1.Substring(2, OP1.Length - 3);

            return "";
        }

        public string CheckAndGetByteString(string OP1)
        {
            if ((OP1.Length > 3) && (OP1[0] == 'X') && (OP1[1] == '"') && (OP1[OP1.Length - 1] == '"'))
            {
                string text = OP1.Substring(2, OP1.Length - 3);
                if (!CheckAdress(text))
                    return "";
                return text;
            }
            return "";
        }
        //--------------------------------------------------------------------------------------

        public bool CheckRow(string[,] sc, int numb, out string mark, out string OC, out string OP1, out string OP2, string nameProg)
        {
            mark = sc[numb, 0];
            OC = sc[numb, 1];
            OP1 = sc[numb, 2];
            OP2 = sc[numb, 3];

            if (CheckDirective(mark) || CheckRegisters(mark))
                return false;

            if (numb > 0 && nameProg == mark.ToUpper())
                return false;

            if ((CheckLettersAndNumbers(mark) || mark.Length == 0) && CheckLettersAndNumbers(OC) && (CheckLettersAndNumbers(OP2) || OP2.Length == 0))
            {
                if (mark.Length > 0)
                {
                    if (CheckLetters(Convert.ToString(mark[0])))
                        return true;
                    else
                        return false;
                }

                return true;
            }
            else
                return false;

        }
        //--------------------------------------------------------------------------------------

        public bool CheckExternalNames(List<List<string>> tt)
        {
            for (int i = 0; i < tt[0].Count; i++)
            {
                if (tt[1][i].ToString() == "" && tt[3][i].ToString() == "ВИ")
                    return false;
            }
            return true;
        }
    }
}
