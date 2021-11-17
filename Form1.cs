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
                    //nếu chỉ số hàng lớn hơn 4 thì quay về hàng 0
                    result += keyMatrix[0, firstCharInPair[1]] + keyMatrix[0, secondCharInPair[1]];
                }
                else if (secondCharInPair[0] + 1 > 4)
                {
                    result += keyMatrix[firstCharInPair[0] + 1, firstCharInPair[1]] + keyMatrix[0, secondCharInPair[1]];
                }
                else if (firstCharInPair[0] + 1 > 4)
                {                    
                    result += keyMatrix[0, firstCharInPair[1]] + keyMatrix[secondCharInPair[0] + 1, secondCharInPair[1]];
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
                    result += keyMatrix[firstCharInPair[0], firstCharInPair[1] - rowCount] + keyMatrix[secondCharInPair[0], secondCharInPair[1] + rowCount];
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
        //sau hàm SearchCharacter thì ta được một mảng 
        void Decryption(string[,] keyMatrix,string keyword,int length)
        {
            keyMatrix = new string[5, 5];
            //length là độ dài của mảng gòm các kí tự mà mình thu được
            int[] newCharacter = new int[4];
            //duyệt một lần 2 kí tự
            for(int i =0;i<length;i+=2)
            {
                //gọi lại hàm search
                string t1 = keyword[i].ToString();
                string t2 = keyword[i + 1].ToString();
                SearchCharacter(keyMatrix, t1, t2, newCharacter);   
                if(newCharacter[0] == newCharacter[2])//cùng 1 hàng => dịch trái
                {
                    t1 = keyMatrix[newCharacter[0], (newCharacter[1] - 1) % 5];
                    t2 = keyMatrix[newCharacter[0], (newCharacter[3] - 1) % 5];
                }    
                else if(newCharacter[1]==newCharacter[3])//cùng 1 cột
                {
                    t1 = keyMatrix[(newCharacter[0] - 1) % 5, newCharacter[1]];
                    t2 = keyMatrix[(newCharacter[2] - 1) % 5, newCharacter[1]];
                }    
                else
                {
                    t1 = keyMatrix[newCharacter[0], newCharacter[3]];
                    t2 = keyMatrix[newCharacter[1], newCharacter[2]];
                }    
            }    
        }
        void DecryptCipherText(string keyword,string CipherText)
        {
           string[,] keyMatrix = new string[5, 5];
            int length = CipherText.Length;
            Decryption(keyMatrix, keyword, length);
        }
         private void buttonDecrypt_Click(object sender, EventArgs e)
         {
            //conver to UPCase and display on matrix
            takeKey();
            string key = textBoxKey.Text;
            string mess = textBoxMsg.Text;
            DecryptCipherText(key,mess);
         }



}
}       /*
            key00.Text = key[0][0]; key01.Text = key[0][1]; key02.Text = key[0][2]; key03.Text = key[0][3]; key04.Text = key[0][4];
            key10.Text = key[1][0]; key11.Text = key[1][1]; key12.Text = key[1][2]; key13.Text = key[1][3]; key14.Text = key[1][4];
            key20.Text = key[2][0]; key21.Text = key[2][1]; key22.Text = key[2][2]; key23.Text = key[2][3]; key24.Text = key[2][4];
            key30.Text = key[3][0]; key31.Text = key[3][1]; key32.Text = key[3][2]; key33.Text = key[3][3]; key34.Text = key[3][4];
            key40.Text = key[4][0]; key41.Text = key[4][1]; key42.Text = key[4][2]; key43.Text = key[4][3]; key44.Text = key[4][4];
         */
