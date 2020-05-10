﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WeChatRobotCSharp.Messages;

namespace WeChatRobotCSharp
{
    public partial class SendMessageForm : Form
    {
        private readonly string _wxid;

        public SendMessageForm(string wxid, string displayName, bool reply = false)
        {
            InitializeComponent();
            if (reply)
            {
                this.Text = "回复消息";
                this.btn_send.Text = "回复";
            }
                
            _wxid = wxid;
            lb_target.Text = $"接收者：{(displayName ?? wxid)}";
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            if(tb_content.TextLength == 0)
            {
                MessageBox.Show("请输入内容后进行发送!");
                return;
            }

            int size = Marshal.SizeOf<SendMessageInfo>();
            IntPtr pAddress = Marshal.AllocHGlobal(size);
            var message = new SendMessageInfo();
            message.wxid = _wxid;
            message.content = tb_content.Text;
            Marshal.StructureToPtr(message, pAddress, false);
            SendWechatHelper(WM_SendTextMessage, pAddress, size);
            Marshal.FreeHGlobal(pAddress);
            tb_content.Text = string.Empty;
        }
    }
}
