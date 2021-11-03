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
        public Form1()
        {
            InitializeComponent();
        }
        void displayKeyMatrix(string[,] key) // đừng đụng vào
        {
            key00.Text = key[0, 0]; key01.Text = key[0, 1]; key02.Text = key[0, 2]; key03.Text = key[0, 3]; key04.Text = key[0, 4];
            key10.Text = key[1, 0]; key11.Text = key[1, 1]; key12.Text = key[1, 2]; key13.Text = key[1, 3]; key14.Text = key[1, 4];
            key20.Text = key[2, 0]; key21.Text = key[2, 1]; key22.Text = key[2, 2]; key23.Text = key[2, 3]; key24.Text = key[2, 4];
            key30.Text = key[3, 0]; key31.Text = key[3, 1]; key32.Text = key[3, 2]; key33.Text = key[3, 3]; key34.Text = key[3, 4];
            key40.Text = key[4, 0]; key41.Text = key[4, 1]; key42.Text = key[4, 2]; key43.Text = key[4, 3]; key44.Text = key[4, 4];
        }
        void separateMsg(string msg, string[] separatedMsg)
        {
            string newMsg = msg;
            if (msg.Length % 2 != 0) // nếu thông điệp có số ký tự lẻ thì thêm X rồi tách đôi
            {
                newMsg = msg + "X";
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
            textBoxAnswer.Clear();
            string[] PairCharacter = new string[30];
            string msg = "";
            msg = textBoxMsg.Text.Trim().ToUpper();

            separateMsg(msg, PairCharacter);

            string answer = string.Join(" ", PairCharacter);
            textBoxAnswer.Text = answer;
        }

        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            string[,] keymatrix = new string[5, 5];
            for(int i = 0; i<5; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    keymatrix[i, j] = "B";
                }    
            }
            displayKeyMatrix(keymatrix);
            takeMesage();
        }
    }
}       /*
            key00.Text = key[0][0]; key01.Text = key[0][1]; key02.Text = key[0][2]; key03.Text = key[0][3]; key04.Text = key[0][4];
            key10.Text = key[1][0]; key11.Text = key[1][1]; key12.Text = key[1][2]; key13.Text = key[1][3]; key14.Text = key[1][4];
            key20.Text = key[2][0]; key21.Text = key[2][1]; key22.Text = key[2][2]; key23.Text = key[2][3]; key24.Text = key[2][4];
            key30.Text = key[3][0]; key31.Text = key[3][1]; key32.Text = key[3][2]; key33.Text = key[3][3]; key34.Text = key[3][4];
            key40.Text = key[4][0]; key41.Text = key[4][1]; key42.Text = key[4][2]; key43.Text = key[4][3]; key44.Text = key[4][4];
         */
