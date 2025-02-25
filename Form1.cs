﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Playfair
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
        void displayKeyMatrix(string[,] key) 
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
            string defaultChar = "ABCDEFGHIKLMNOPQRSTUVWXYZ"; // 25 chữ cái
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

        }
        void takeMesage()
        {
            PairCharacter = new string[30]; // 
            string msg = textBoxMsg.Text.Trim().ToUpper(); // Lấy chuỗi playpair được nhập

            separateMsg(msg, PairCharacter);

            //đổi answer thành processedMsg
            processedMsg = string.Join(" ", PairCharacter);  
        }
        void takeKey()
        {
            string keyword = textBoxKey.Text.Trim().ToUpper(); 
            keyMatrix = new string[5, 5];
  
            addStringTo2DMatrix(ProcessKey(keyword), keyMatrix);
            displayKeyMatrix(keyMatrix);
        }
        int[] charPositionInKeyMatrix(char temp) // temp = 'T'
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

        string encryption(int[] firstCharInPair, int[] secondCharInPair) 
        {
            string result = "";
            if(firstCharInPair[0] == secondCharInPair[0]) // cùng hàng 
            {
                result += keyMatrix[firstCharInPair[0], (firstCharInPair[1] + 1)% 5]
                           + keyMatrix[secondCharInPair[0], (secondCharInPair[1] + 1) % 5];

            }
            else if(firstCharInPair[1] == secondCharInPair[1])
            {
                result = keyMatrix[(firstCharInPair[0] + 1) % 5, firstCharInPair[1]]
                            + keyMatrix[(secondCharInPair[0] + 1) % 5, secondCharInPair[1]];
            }
            else
            {
                //trường hợp khác cột và hàng thì chỉ cần đếm số cột cách giữa 2 ký tự rồi trừ qua cộng lại chỉ số :v (I think so)
                int rowCount = firstCharInPair[1] - secondCharInPair[1];
                result += keyMatrix[firstCharInPair[0], firstCharInPair[1] - rowCount] + keyMatrix[secondCharInPair[0], secondCharInPair[1] + rowCount] ;
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
            foreach (char c in processedMsg) // cho từng kí tự c trong "th an hh uy"
            {
                if (Char.IsWhiteSpace(processedMsg, charCount)) // kiểm tra kí tự tại vị trí charCount trong chuổi processMsg có phải là whitespace hay ko
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
                        firstCharInPair = new int[2] { 10, 10 };
                        secondCharInPair = new int[2] { 10, 10 };
                    }
                }
                else
                {
                    spaceCount = 0;
                    if (firstCharInPair[0] == 10)
                    {
                        firstCharInPair = charPositionInKeyMatrix(c); // return int[hang, cot]
                    }
                    else
                    {
                        secondCharInPair = charPositionInKeyMatrix(c);
                    }
                }
                charCount++;

            }
            textBoxAnswer.Text = result;
        }

        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            takeKey();
            takeMesage(); // biến đổi "thanh huy" => "th an hh uy" 
            takeAction();
        }
        //Xóa dữ liệu sau khi thực hiện 1 trong 2 
        private void btn_clear_Click(object sender, EventArgs e)
        {
            textBoxMsg.Text = "";
            textBoxKey.Text = "";
            textBoxAnswer.Text = "";
            keyMatrix = new string[5, 5];
            for(int i=0;i<5;i++)
            {
                for(int j=0;j<5;j++)
                {
                    keyMatrix[i, j] = "";
                }    
            }
            displayKeyMatrix(keyMatrix);
        }


        ///DECRYPTION
        ///Tìm vị trí và trả về vị trí
        void SearchCharacter(string[,] keyMatrix,string a,string b,int[] arrayPos)
        {
            keyMatrix = new string[5,5];
            //kiểm tra nếu là I thì đổi thành J(ngược lại)
            if(a=="j")
            {
                a = "i";
            }    
            else if (b=="j")
            {
                b="i";
            }    
            //tiến hành tìm và trả về vị trí
            for(int row =0;row < 5;row++)
            {
                for(int col = 0;col < 5;col++)
                {
                    //nếu phần tử trong ma trận = kí tự đầu tiên
                    if(keyMatrix[row,col] == a)
                    {
                        arrayPos[0] = row;
                        arrayPos[1] = col;
                    }    
                    //còn nếu mà bằng kí tự thứ 2
                    else if(keyMatrix[row,col] == b)
                    {
                        arrayPos[2] = row;
                        arrayPos[3] = col;
                    }    
                }    
            }    
        }
        void DecryptCipherText(string keyword,string CipherText)
        {
           string[,] keyMatrix = new string[5, 5];
            int length = CipherText.Length;
           // Decryption(keyMatrix, keyword, length);
        }


        // Chỗ này là decryption nè 

         private void buttonDecrypt_Click(object sender, EventArgs e)
         {
            //conver to UPCase and display on matrix
            takeKey();
            takeEncrptMessage();
            takeActionDecrypt();
         }

        void separateEncryptMessage(string msg, string[] separatedMsg)
        {
            string newMsg = msg.Replace(" ", "");
            
            if (newMsg.Length % 2 != 0) // Ktra mã hóa có hợp lệ ko
            {
                MessageBox.Show("định dạng cipher text không đúng", "Lỗi", MessageBoxButtons.OK);
                return;
            }

            // nếu không thì loop
            for (int i = 0; i < newMsg.Length; i += 2) //  tách đôi
            {
                string characterPair = newMsg[i].ToString() + newMsg[i + 1].ToString();
                separatedMsg[i] = characterPair; 
            }

        }

        void takeEncrptMessage()
        {
            PairCharacter = new string[30]; // 
            string msg = textBoxMsg.Text.Trim().ToUpper(); // Lấy chuỗi playpair được nhập
                      
            separateEncryptMessage(msg, PairCharacter);

            //đổi answer thành processedMsg
            processedMsg = string.Join(" ", PairCharacter);  // "th an hh uy"
            //textBoxAnswer.Text = answer;     // test purpose   


        }


        void takeActionDecrypt()
        {
            string result = "";
            int[] firstCharInPair = new int[2] { 10, 10 };
            int[] secondCharInPair = new int[2] { 10, 10 };

            int spaceCount = 0;
            int charCount = 0;
            foreach (char c in processedMsg) // cho từng kí tự c trong "th an hh uy"
            {
                if (Char.IsWhiteSpace(processedMsg, charCount)) // kiểm tra kí tự tại vị trí charCount trong chuổi processMsg có phải là whitespace hay ko
                {
                    spaceCount++;
                    if (spaceCount == 1)
                    {
                        //Xử lý 2 ký tự liền nhau trước khi khoảng trắng
                        string temp = Decryption(firstCharInPair, secondCharInPair);
                        result += temp;
                    }
                    else
                    {
                        firstCharInPair = new int[2] { 10, 10 };
                        secondCharInPair = new int[2] { 10, 10 };
                    }
                }
                else
                {
                    spaceCount = 0;
                    if (firstCharInPair[0] == 10)
                    {
                        firstCharInPair = charPositionInKeyMatrix(c); // return int[hang, cot]
                    }
                    else
                    {
                        secondCharInPair = charPositionInKeyMatrix(c);
                    }
                }
                charCount++;

            }
            textBoxAnswer.Text = result;
        }


        string Decryption(int[] firstCharInPair, int[] secondCharInPair) 
        {
            string result = "";
            if (firstCharInPair[0] == secondCharInPair[0]) // cùng hàng 
            {
                if (firstCharInPair[1] - 1 < 0 && secondCharInPair[1] - 1 < 0)
                {
                    result += keyMatrix[firstCharInPair[0], 4] + keyMatrix[secondCharInPair[0], 4];
                }
                else if (firstCharInPair[1] - 1 < 0)
                {
                    result += keyMatrix[firstCharInPair[0], 4] + keyMatrix[secondCharInPair[0], secondCharInPair[1] - 1];
                }
                else if (secondCharInPair[1] - 1 < 0)
                {
                    result += keyMatrix[firstCharInPair[0], firstCharInPair[1] -1] + keyMatrix[secondCharInPair[0], 4];
                } else
                {
                    result += keyMatrix[firstCharInPair[0], firstCharInPair[1] - 1]
                               + keyMatrix[secondCharInPair[0], secondCharInPair[1] - 1];
                }
              
            }
            else if (firstCharInPair[1] == secondCharInPair[1]) // cùng cột
            {

                if (firstCharInPair[0] - 1 < 0 && secondCharInPair[0] - 1 < 0)
                {
                    result += keyMatrix[4, firstCharInPair[1]] + keyMatrix[4, secondCharInPair[1]];
                }
                else if (firstCharInPair[0] - 1 < 0)
                {
                    result += keyMatrix[4, firstCharInPair[1]] + keyMatrix[secondCharInPair[0] -1, secondCharInPair[1]];
                }
                else if (secondCharInPair[0] - 1 < 0)
                {
                    result += keyMatrix[firstCharInPair[0] -1, firstCharInPair[1]] + keyMatrix[4, secondCharInPair[1]];
                }
                else
                {
                    result += keyMatrix[firstCharInPair[0] -1 , firstCharInPair[1]]
                               + keyMatrix[secondCharInPair[0] -1, secondCharInPair[1]];
                }

            }
            else
            {
                //trường hợp khác cột và hàng thì chỉ cần đếm số cột cách giữa 2 ký tự rồi trừ qua cộng lại chỉ số :v (I think so)
                int rowCount = firstCharInPair[1] - secondCharInPair[1];
                if (rowCount > 0)//trường hợp này là ký tự thứ 1 nằm bên tay phải so với ký tự thứ 2 trong ma trận  
                {
                    result += keyMatrix[firstCharInPair[0], firstCharInPair[1] - rowCount] + keyMatrix[secondCharInPair[0], secondCharInPair[1] + rowCount];
                }
                else //trường hợp còn lại là ký tự thứ 1 nằm bên tay trái so với ký tự thứ 2 trong ma trận
                {
                    result += keyMatrix[firstCharInPair[0], firstCharInPair[1] - rowCount] + keyMatrix[secondCharInPair[0], secondCharInPair[1] + rowCount];
                }
            }
            return result;
        }
    }
}
