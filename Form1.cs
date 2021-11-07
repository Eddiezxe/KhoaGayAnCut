using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhoaGayAnCut
{
    public partial class Form1 : Form
    {
        private string[,] keyMatrix;
        private string processedMsg;
        private string[] PairCharacter;
        public Form1()
        {
            InitializeComponent();
        }
        void displayKeyMatrix(string[,] key) // đừng đụng vào, hong thích, thích đụng vào được hong
        {
            key00.Text = key[0, 0]; key01.Text = key[0, 1]; key02.Text = key[0, 2]; key03.Text = key[0, 3]; key04.Text = key[0, 4];
            key10.Text = key[1, 0]; key11.Text = key[1, 1]; key12.Text = key[1, 2]; key13.Text = key[1, 3]; key14.Text = key[1, 4];
            key20.Text = key[2, 0]; key21.Text = key[2, 1]; key22.Text = key[2, 2]; key23.Text = key[2, 3]; key24.Text = key[2, 4];
            key30.Text = key[3, 0]; key31.Text = key[3, 1]; key32.Text = key[3, 2]; key33.Text = key[3, 3]; key34.Text = key[3, 4];
            key40.Text = key[4, 0]; key41.Text = key[4, 1]; key42.Text = key[4, 2]; key43.Text = key[4, 3]; key44.Text = key[4, 4];
        }
        string ProcessKey( string keyword)
        {
            //c2
            //add key vào đầu chuỗi default
            //remove duplicate
            // thêm chuỗi mới vào ma trận
            string defaultChar = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
            string temp = keyword + defaultChar;
            string result = string.Empty;
            for(int i = 0; i< temp.Length; i++)
            {
                if (!result.Contains(temp[i]))
                    result += temp[i];
            }
            return result;
        }
        void addStringTo2DMatrix(string keyword, string[,] keyMatrix)
        {
            int count = 0;
            for (int hang = 0; hang < 5; hang++)
            {
                for (int cot = 0; cot < 5; cot++)
                {
                        keyMatrix[hang, cot] = keyword[count].ToString();
                        count++;
                }       

            }    
        }
        void separateMsg(string msg, string[] separatedMsg)
        {
            string newMsg = msg.Replace(" ", "");
            //
            //string newMsg = msg;
            if (newMsg.Length % 2 != 0) // nếu thông điệp có số ký tự lẻ thì thêm X rồi tách đôi
            {
                newMsg += "X";
            }
            
            // nếu không thì loop
            for (int i = 0; i < newMsg.Length; i += 2) //  tách đôi
            {
                string characterPair = newMsg[i].ToString() + newMsg[i + 1].ToString();
                separatedMsg[i] = characterPair;
            }

            string temp = "";
            for(int i = 0; i < separatedMsg.Length; i++)
            {
                temp += separatedMsg[i];
            }


        }
        void takeMesage()
        {
            PairCharacter = new string[30];
            string msg = textBoxMsg.Text.Trim().ToUpper(); 

            separateMsg(msg, PairCharacter);

            //đổi answer thành processedMsg
            processedMsg = string.Join(" ", PairCharacter);
            //textBoxAnswer.Text = answer;     // test purpose   
        }
        void takeKey()
        {
            string keyword = textBoxKey.Text.Trim().ToUpper();
            keyMatrix = new string[5, 5];
  
            addStringTo2DMatrix(ProcessKey(keyword), keyMatrix);
            displayKeyMatrix(keyMatrix);
            //textBoxAnswer.Text = ProcessKey(keyword); //test purpose


        }
        int[] charPositionInKeyMatrix(char temp)
        {
            int[] positionArray = new int[2];
            for (int hang = 0; hang < 5; hang++)
            {
                for (int cot = 0; cot < 5; cot++)
                {
                    if (temp.Equals(keyMatrix[hang, cot].ToCharArray()[0]))
                    {
                        positionArray[0] = hang;
                        positionArray[1] = cot;
                        break;
                    }
                }
            }
            return positionArray;
        }
        string encryption(int[] firstCharInPair, int[] secondCharInPair) //hack não vl 
        {
            string result = "";
            if(firstCharInPair[0] == secondCharInPair[0])
            {
                if (firstCharInPair[1] + 1 > 4 && secondCharInPair[1] + 1 > 4)//nếu chỉ số hàng giống nhau thì sẽ lấy ký tự bên tay phải
                {
                    //nếu chỉ số cột lớn hơn 5 thì quay về cột 0
                    result += keyMatrix[firstCharInPair[0], 0] + keyMatrix[secondCharInPair[0], 0];
                }
                else if(secondCharInPair[1] + 1 > 4)
                {
                    result += keyMatrix[firstCharInPair[0], firstCharInPair[1] + 1] + keyMatrix[secondCharInPair[0], 0]; 
                }
                else if (firstCharInPair[1] + 1 > 4)
                {
                    result += keyMatrix[firstCharInPair[0], 0] + keyMatrix[secondCharInPair[0], secondCharInPair[1] + 1];
                }
                else
                {
                    result += keyMatrix[firstCharInPair[0], firstCharInPair[1] + 1] + keyMatrix[secondCharInPair[0], secondCharInPair[1] + 1];
                }

            }
            else if(firstCharInPair[1] == secondCharInPair[1])
            {
                if (firstCharInPair[0] + 1 > 4 && secondCharInPair[0] + 1 > 4)//nếu chỉ số cột giống nhau thì sẽ lấy ký tự ngay bên dưới
                {
                    //nếu chỉ số hàng lớn hơn 5 thì quay về hàng 0
                    result += keyMatrix[0, firstCharInPair[1]] + keyMatrix[secondCharInPair[0] + 1, secondCharInPair[1]];
                }
                else if (secondCharInPair[0] + 1 > 4)
                {
                    result += keyMatrix[firstCharInPair[0] + 1, firstCharInPair[1]] + keyMatrix[0, secondCharInPair[1]];
                }
                else if (firstCharInPair[0] + 1 > 4)
                {
                    result += keyMatrix[0, firstCharInPair[1]] + keyMatrix[0, secondCharInPair[1]];
                }
                else
                {
                    result += keyMatrix[firstCharInPair[0] + 1, firstCharInPair[1]] + keyMatrix[secondCharInPair[0] + 1, secondCharInPair[1]];
                }

            }
            else
            {
                //trường hợp khác cột và hàng thì chỉ cần đếm số cột cách giữa 2 ký tự rồi trừ qua cộng lại chỉ số :v (I think so)
                int rowCount = firstCharInPair[1] - secondCharInPair[1];
                if(rowCount > 0)//trường hợp này là ký tự thứ 1 nằm bên tay phải so với ký tự thứ 2 trong ma trận  
                {
                    result += keyMatrix[firstCharInPair[0], firstCharInPair[1] - rowCount] + keyMatrix[secondCharInPair[0], secondCharInPair[1] + rowCount] ;
                }
                else //trường hợp còn lại là ký tự thứ 1 nằm bên tay trái so với ký tự thứ 2 trong ma trận
                {
                    result += keyMatrix[firstCharInPair[0] + rowCount, firstCharInPair[1]] + keyMatrix[secondCharInPair[0] - rowCount, secondCharInPair[1]];
                }
            }

            return result;
        }
        void takeAction()
        {
            string result = "";
            int[] firstCharInPair = new int[2] {10, 10};
            int[] secondCharInPair = new int[2] {10, 10};
            
            int spaceCount = 0;
            int charCount = 0;
            foreach (char c in processedMsg)
            {
                if (Char.IsWhiteSpace(processedMsg, charCount))
                {
                    spaceCount++;
                    if (spaceCount == 1)
                    {
                        //Xử lý 2 ký tự liền nhau trước khi khoảng trắng
                        string temp = encryption(firstCharInPair, secondCharInPair);
                        result += temp;
                    }
                    else
                    {
                        /*Array.Clear(firstCharInPair, 3, 2);
                        Array.Clear(secondCharInPair, 3, 2);*/
                        firstCharInPair = new int[2] { 10, 10 };
                        secondCharInPair = new int[2] { 10, 10 };
                    }
                }
                else
                {
                    spaceCount = 0;
                    if (firstCharInPair[0] == 10)
                    {
                        firstCharInPair = charPositionInKeyMatrix(c);
                    }
                    else
                    {
                        secondCharInPair = charPositionInKeyMatrix(c);
                    }
                }
                charCount++;

            }
            //textBoxAnswer.Text = processedMsg;
            textBoxAnswer.Text = result;
        }
        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            takeKey();
            takeMesage();
            takeAction();
        }
    }
}       /*
            key00.Text = key[0][0]; key01.Text = key[0][1]; key02.Text = key[0][2]; key03.Text = key[0][3]; key04.Text = key[0][4];
            key10.Text = key[1][0]; key11.Text = key[1][1]; key12.Text = key[1][2]; key13.Text = key[1][3]; key14.Text = key[1][4];
            key20.Text = key[2][0]; key21.Text = key[2][1]; key22.Text = key[2][2]; key23.Text = key[2][3]; key24.Text = key[2][4];
            key30.Text = key[3][0]; key31.Text = key[3][1]; key32.Text = key[3][2]; key33.Text = key[3][3]; key34.Text = key[3][4];
            key40.Text = key[4][0]; key41.Text = key[4][1]; key42.Text = key[4][2]; key43.Text = key[4][3]; key44.Text = key[4][4];
         */
