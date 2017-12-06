using System;
using System.Collections.Generic;

namespace Analyzer {

    class Analyzer {

        List<List<int>> rules = new List<List<int>>();


        List<token> identifiers = new List<token>();
        List<token> operations = new List<token>();
        List<token> keyWords = new List<token>();
        List<token> DataTypes = new List<token>();
        List<token> values = new List<token>();

        private void loadTransitionTable(string path) {
            string text = System.IO.File.ReadAllText(path);
            if (text.Length < 2) {
                throw new Exception();
            }
            foreach (var item in text.Split('\n')) {
                var temp = new List<int>();
                foreach (var itm in item.Trim().Split(' ')) {
                    temp.Add(Convert.ToInt32(itm));
                }
                rules.Add(temp);
            }
        }
        private int getNextState(int iState, char cChar) {
            if (char.IsLetter(cChar))
                return rules[iState][1];
            else if (char.IsDigit(cChar))
                return rules[iState][2];
            else if (cChar == '_')
                return rules[iState][3];
            else if (cChar == '"')
                return rules[iState][4];
            else if (cChar == '\'')
                return rules[iState][5];
            else if (cChar == '.')
                return rules[iState][6];
            else if (cChar == 'e')
                return rules[iState][7];
            else if (cChar == 'E')
                return rules[iState][8];
            else if (cChar == '+')
                return rules[iState][9];
            else if (cChar == '-')
                return rules[iState][10];
            else if (cChar == '*')
                return rules[iState][11];
            else if (cChar == '/')
                return rules[iState][12];
            else if (cChar == '%')
                return rules[iState][13];
            else if (cChar == '=')
                return rules[iState][14];
            else if (cChar == '!')
                return rules[iState][15];
            else if (cChar == '~')
                return rules[iState][16];
            else if (cChar == '&')
                return rules[iState][17];
            else if (cChar == '|')
                return rules[iState][18];
            else if (cChar == '^')
                return rules[iState][19];
            else if (cChar == '<')
                return rules[iState][20];
            else if (cChar == '>')
                return rules[iState][21];
            else if (cChar == '\\')
                return rules[iState][22];
            return rules[iState][0];
        }
        private bool isKeyword(string sToken) {
            if ((sToken).Length > 16 || (sToken).Length == 0)
                return false;
            var sKeywords = new List<string>(){
                "include","NULL","stdio","stdlib","system","include","bool","break","case","catch","class","const",
                "continue","default","delete","do","else","enum","explicit",
                "export","false","for","goto","if",
                "main","mutable","namespace","new","operator","private","protected","public",
                "register","reinterpret_cast","return","signed","sizeof","static",
                "static_cast","struct","switch","template","this","throw","true","try","typedef",
                "typeid","typename","union","unsigned","using","while"};
            return sKeywords.Exists(element => sToken == element);
        }
        private bool isDataType(string sToken)
        {
            if ((sToken).Length > 16 || (sToken).Length == 0)
                return false;
            var sDataType = new List<string>(){
                "bool", "char","short","float","int","FILE","int32","double","long","void"};
            return sDataType.Exists(element => sToken == element);
        }
        //public string Result(string txt, string tt = @"matrix.txt")
        //{
        //    try
        //    {
        //        loadTransitionTable(tt);
        //    }
        //    catch (Exception)
        //    {
        //        System.Windows.Forms.MessageBox.Show("Unable to open the input file.\nPress any key to exit.\n");
        //        return "";
        //    }
        //    if (txt.Length == 0)
        //        return "";
        //    var result = "";
        //    int txtIndex = 0, iState = 0;
        //    char cTemp = txt[txtIndex], cChar = ' ';
        //    string sToken = "";
        //    bool flag = true;
        //    txt += " ";
        //    while (txtIndex != txt.Length)
        //    {
        //        //System.Windows.Forms.MessageBox.Show(txt[txtIndex].ToString());
        //        if (flag)
        //        {
        //            cChar = cTemp;
        //            if (txt.Length - 1 == txtIndex)
        //            { cTemp = ' '; ++txtIndex; }
        //            else cTemp = txt[++txtIndex];
        //        }
        //        else
        //            flag = true;
        //        iState = getNextState(iState, cChar);
        //        switch (iState)
        //        {
        //            case 0:
        //                result += cChar;
        //                iState = 0;
        //                sToken = "";
        //                break;
        //            case 1:
        //            case 3:
        //            case 4:
        //            case 5:
        //            case 6:
        //            case 9:
        //            case 10:
        //            case 12:
        //            case 13:
        //            case 14:
        //            case 16:
        //            case 18:
        //            case 19:
        //            case 21:
        //            case 25:
        //            case 29:
        //            case 34:
        //            case 37:
        //            case 40:
        //            case 44:
        //            case 48:
        //            case 52:
        //            case 55:
        //            case 56:
        //            case 61:
        //            case 62:
        //            case 69:
        //            case 70:
        //            case 72:
        //                sToken += cChar;
        //                break;
        //            case 2:
        //                if (isKeyword(sToken))
        //                    token.addOne(sToken, keyWords);
        //                else if (isDataType(sToken))
        //                    token.addOne(sToken, DataTypes);
        //                else
        //                {
        //                    token.addOne(sToken, identifiers);
        //                }
        //                iState = 0;
        //                flag = false;
        //                sToken = "";
        //                break;
        //            case 7:
        //                token.addOne("FLOAT", values);
        //                iState = 0;
        //                flag = false;
        //                sToken = "";
        //                break;
        //            case 8:
        //                token.addOne("INT", values);
        //                iState = 0;
        //                flag = false;
        //                sToken = "";
        //                break;
        //            case 11:
        //                token.addOne(sToken, operations);
        //                iState = 0;
        //                sToken = "";
        //                break;
        //            case 71:
        //            case 68:
        //                token.addOne("CHAR", values);
        //                iState = 0;
        //                sToken = "";
        //                break;
        //            case 15:
        //                token.addOne("STR", values);
        //                iState = 0;
        //                sToken = "";
        //                break;
        //            case 20:
        //                iState = 0;
        //                sToken = "";
        //                break;
        //            case 43:
        //                token.addOne(sToken, operations);
        //                iState = 0;
        //                sToken = "";
        //                break;
        //            case 67:
        //                flag = false;
        //                result += "<ERR,\'" + sToken + "\'>";
        //                iState = 0;
        //                sToken = "";
        //                break;
        //            case 22:
        //            case 23:
        //            case 24:
        //                if (cChar != '+' && cChar != '=')
        //                { flag = false; token.addOne(sToken, operations); }
        //                else
        //                { sToken += cChar; token.addOne(sToken, operations); }
        //                iState = 0;
        //                sToken = "";
        //                break;
        //            case 26:
        //            case 27:
        //            case 28:
        //            case 73:
        //                if (cChar != '-' && cChar != '='&& cChar != '>')
        //                { flag = false; token.addOne(sToken, operations); }
        //                else
        //                { sToken += cChar; token.addOne(sToken, operations); }
        //                iState = 0;
        //                sToken = "";
        //                break;
        //            case 30:
        //            case 31:
        //            case 32:
        //            case 33:
        //            case 35:
        //            case 36:
        //            case 38:
        //            case 39:
        //            case 41:
        //            case 42:
        //            case 53:
        //            case 54:
        //            case 57:
        //            case 58:
        //            case 63:
        //            case 64:
        //            case 65:
        //            case 66:
        //                if (cChar != '=')
        //                { flag = false; token.addOne(sToken, operations); }
        //                else
        //                { sToken += cChar; token.addOne(sToken, operations); }
        //                iState = 0;
        //                sToken = "";
        //                break;
        //            case 17:
        //                while ((cTemp != '\n') && (txt.Length - 1 != txtIndex))
        //                { cTemp = txt[++txtIndex]; }
        //                iState = 0;
        //                sToken = "";
        //                break;
        //            case 45:
        //            case 46:
        //            case 47:
        //                if (cChar != '&' && cChar != '=')
        //                { flag = false; token.addOne(sToken, operations); }
        //                else
        //                { sToken += cChar; token.addOne(sToken, operations); }
        //                iState = 0;
        //                sToken = "";
        //                break;
        //            case 49:
        //            case 50:
        //            case 51:
        //                if (cChar != '|' && cChar != '=')
        //                { flag = false; token.addOne(sToken, operations); }
        //                else
        //                { sToken += cChar; token.addOne(sToken, operations); }
        //                iState = 0;
        //                sToken = "";
        //                break;
        //            case 59:
        //            case 60:
        //                if (cChar != '+' && cChar != '-' && cChar != '/'
        //                    && cChar != '>' && cChar != '<' && cChar != '=')
        //                { flag = false; token.addOne(sToken, operations); }
        //                else
        //                { sToken += cChar; token.addOne(sToken, operations); }
        //                iState = 0;
        //                sToken = "";
        //                break;
        //        }
        //    }
        //    String str = "identifiers:\n";
        //    //foreach (token i in identifiers)
        //    //    str += i.GetLexeme() + " " + i.GetCount().ToString() + "\n";
        //    str += "# if identifiers is : " + identifiers.Count.ToString() + "\n";
        //    //System.Windows.Forms.MessageBox.Show(str);
        //    //str = "";
        //    foreach (token i in operations)
        //        str += i.getLexeme() + " " + i.getCount().ToString() + "\n";
        //    str += "# if operations is : " + operations.Count.ToString() + "\n";
        //    //System.Windows.Forms.MessageBox.Show(str);
        //    //str = "";
        //    foreach (token i in DataTypes)
        //        str += i.getLexeme() + " " + i.getCount().ToString() + "\n";
        //    str += "# if DataTypes is : " + DataTypes.Count.ToString() + "\n";

        //    //System.Windows.Forms.MessageBox.Show(str);
        //    //str = "";
        //    foreach (token i in keyWords)
        //        str += i.getLexeme() + " " + i.getCount().ToString() + "\n";
        //    str += "# if keyWords is : " + keyWords.Count.ToString() + "\n";

        //    //System.Windows.Forms.MessageBox.Show(str);
        //    //str = "";
        //    foreach (token i in values)
        //        str += i.getLexeme() + " " + i.getCount().ToString() + "\n";
        //    str += "# if values is : " + values.Count.ToString() + "\n";

        //    return str;
        //}
        public List<token> Result2(string txt, string tt = @"matrix.txt")
        {
            List<token> resultlist = new List<token>();
            try
            {
                loadTransitionTable(tt);
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Unable to open the input file.\nPress any key to exit.\n");
                return resultlist;
            }
            if (txt.Length == 0)
                return resultlist;
            var result = "";
            int txtIndex = 0, iState = 0;
            char cTemp = txt[txtIndex], cChar = ' ';
            string sToken = "";
            bool flag = true;
            txt += " ";
            while (txtIndex != txt.Length)
            {
                //System.Windows.Forms.MessageBox.Show(txt[txtIndex].ToString());
                if (flag)
                {
                    cChar = cTemp;
                    if (txt.Length - 1 == txtIndex)
                    { cTemp = ' '; ++txtIndex; }
                    else cTemp = txt[++txtIndex];
                }
                else
                    flag = true;
                iState = getNextState(iState, cChar);
                switch (iState)
                {
                    case 0:
                        if ((cChar != '\n') && (cChar != '\t') && (cChar != ' '))
                            resultlist.Add(new token(cChar.ToString()));
                        result += cChar;
                        iState = 0;
                        sToken = "";
                        break;
                    case 1:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 9:
                    case 10:
                    case 12:
                    case 13:
                    case 14:
                    case 16:
                    case 18:
                    case 19:
                    case 21:
                    case 25:
                    case 29:
                    case 34:
                    case 37:
                    case 40:
                    case 44:
                    case 48:
                    case 52:
                    case 55:
                    case 56:
                    case 61:
                    case 62:
                    case 69:
                    case 70:
                    case 72:
                        sToken += cChar;
                        break;
                    case 2:
                        if (isKeyword(sToken))
                            resultlist.Add(new token(sToken, "keyword"));
                        else if (isDataType(sToken))
                            resultlist.Add(new token(sToken, "datatype"));
                        else
                        {
                            resultlist.Add(new token(sToken, "identifier"));
                        }
                        iState = 0;
                        flag = false;
                        sToken = "";
                        break;
                    case 7:
                        resultlist.Add(new token(sToken, "vfloat"));
                        iState = 0;
                        flag = false;
                        sToken = "";
                        break;
                    case 8:
                        resultlist.Add(new token(sToken, "vint"));
                        iState = 0;
                        flag = false;
                        sToken = "";
                        break;
                    case 11:
                        resultlist.Add(new token( sToken, "op"));
                        flag = false;
                        iState = 0;
                        sToken = "";
                        break;
                    case 71:
                    case 68:
                        resultlist.Add(new token( sToken, "vchar"));
                        iState = 0;
                        sToken = "";
                        break;
                    case 15:
                        sToken += cChar;
                        resultlist.Add(new token( sToken, "vstring"));
                        iState = 0;
                        sToken = "";
                        break;
                    case 20:
                        iState = 0;
                        sToken = "";
                        break;
                    case 43:
                        resultlist.Add(new token( sToken, "op"));
                        iState = 0;
                        sToken = "";
                        break;
                    case 67:
                        flag = false;
                        result += "<ERR,\'" + sToken + "\'>";
                        iState = 0;
                        sToken = "";
                        break;
                    case 22:
                    case 23:
                    case 24:
                        if (cChar != '+' && cChar != '=')
                        { flag = false; resultlist.Add(new token( sToken, "op"));
                        }
                        else
                        { sToken += cChar; resultlist.Add(new token( sToken, "op"));
                        }
                        iState = 0;
                        sToken = "";
                        break;
                    case 26:
                    case 27:
                    case 28:
                    case 73:
                        if (cChar != '-' && cChar != '=' && cChar != '>')
                        { flag = false; resultlist.Add(new token( sToken, "op"));
                        }
                        else
                        { sToken += cChar; resultlist.Add(new token( sToken, "op"));
                        }
                        iState = 0;
                        sToken = "";
                        break;
                    case 30:
                    case 31:
                    case 32:
                    case 33:
                    case 35:
                    case 36:
                    case 38:
                    case 39:
                    case 41:
                    case 42:
                    case 53:
                    case 54:
                    case 57:
                    case 58:
                    case 63:
                    case 64:
                    case 65:
                    case 66:
                        if (cChar != '=')
                        { flag = false; resultlist.Add(new token( sToken, "op"));
                        }
                        else
                        { sToken += cChar; resultlist.Add(new token( sToken, "op"));
                        }
                        iState = 0;
                        sToken = "";
                        break;
                    case 17:
                        while ((cTemp != '\n') && (txt.Length - 1 != txtIndex))
                        { cTemp = txt[++txtIndex]; }
                        iState = 0;
                        sToken = "";
                        break;
                    case 45:
                    case 46:
                    case 47:
                        if (cChar != '&' && cChar != '=')
                        { flag = false; resultlist.Add(new token( sToken, "op"));
                        }
                        else
                        { sToken += cChar; resultlist.Add(new token( sToken, "op"));
                        }
                        iState = 0;
                        sToken = "";
                        break;
                    case 49:
                    case 50:
                    case 51:
                        if (cChar != '|' && cChar != '=')
                        { flag = false; resultlist.Add(new token( sToken, "op"));
                        }
                        else
                        { sToken += cChar; resultlist.Add(new token( sToken, "op"));

                        }
                        iState = 0;
                        sToken = "";
                        break;
                    case 59:
                    case 60:
                        if (cChar != '+' && cChar != '-' && cChar != '/'
                            && cChar != '>' && cChar != '<' && cChar != '=')
                        { flag = false; resultlist.Add(new token( sToken, "op"));
                        }
                        else
                        { sToken += cChar; resultlist.Add(new token( sToken, "op"));
                        }
                        iState = 0;
                        sToken = "";
                        break;
                }
            }
            return resultlist;
        }
        public static void structAsDatatype(List<token> result, List<token> result2, List<string> strcts, List<string> typedefNames)
        {
            for (int i = 0; i < result.Count - 1; i++)
            {
                bool structPtr = (result[i].getLexeme() == "struct") && (strcts.Contains(result[i + 1].getLexeme()));
                bool structInitFromTypedefNames = typedefNames.Contains(result[i].getLexeme());
                if (structPtr)
                {
                    string temp = result[i].getLexeme() + " " + result[i + 1].getLexeme();
                    result[i + 1].setType("datatype");
                    result[i + 1].setLexeme(temp);
                    
                    for (int ii = 0; ii < result2.Count; ii++)
                        if (result2[ii].id == result[i].id)
                        {
                            result2[ii + 1].setType("datatype");
                            result2[ii + 1].setLexeme(temp);
                            result2.Remove(result2[ii]);
                        }
                    token t = result[i];
                    result.Remove(t);
                    

                }
                if (structInitFromTypedefNames)
                {
                    result[i].setType("datatype");
                    result[i].setLexeme(result[i].getLexeme());
                    for (int ii = 0; ii < result2.Count; ii++)
                        if (result2[ii].id == result[i].id)
                        {
                            result2[ii].setType("datatype");
                            result2[ii].setLexeme(result[ii].getLexeme());
                        }
                }
            
            }
        }

    }
}
