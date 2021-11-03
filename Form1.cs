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
        void takeInput()
        {
            textBoxAnswer.Clear();
            string[] PairCharacter = new string[30];
            string msg = "";
            msg = textBoxMsg.Text.Trim().ToUpper();

            string keyWord = "";
            keyWord = textBoxKey.Text.Trim().ToUpper();

            separateMsg(msg, PairCharacter);

            string answer = string.Join(" ", PairCharacter);
            textBoxAnswer.Text = answer;
        }

        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            takeInput();
        }
    }
}
