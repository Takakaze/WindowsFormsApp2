﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Permissions;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        List<files> list1 = new List<files>();
        private int saveres = 1;

        public class FileBinaryConvertHelper
        {
            /// <summary>
            /// 将文件转换为byte数组
            /// </summary>
            /// <param name="path">文件地址</param>
            /// <returns>转换后的byte数组</returns>
            public static async Task<byte[]> File2Bytes(string path)
            {
                if (!System.IO.File.Exists(path))
                {
                    return new byte[0];
                }

                FileInfo fi = new FileInfo(path);
                FileStream fs = fi.OpenRead();
                byte[] buff = new byte[fs.Length];
                //FileStream fs = fi.OpenRead();
                // fs.Read(buff, 0, Convert.ToInt32(fs.Length));
                await Task.Run(() => 
                {
                    fs.Read(buff, 0, Convert.ToInt32(fs.Length));
                    fs.Close();
                });
                // fs.Close();

                return buff;
            }

            void fs1(byte[] buff,FileStream fs)
            {
                fs.Read(buff, 0, Convert.ToInt32(fs.Length));
            }

            /// <summary>
            /// 将byte数组转换为文件并保存到指定地址
            /// </summary>
            /// <param name="buff">byte数组</param>
            /// <param name="savepath">保存地址</param>
            /// 
            public static void Bytes2File(byte[] buff, string savepath)
            {
                if (System.IO.File.Exists(savepath))
                {
                    System.IO.File.Delete(savepath);
                }

                FileStream fs = new FileStream(savepath, FileMode.CreateNew);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(buff, 0, buff.Length);
                bw.Close();
                fs.Close();
            }
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            if (saveres == 0)
            {
                MessageBoxResult mbr = MessageBox.Show("you have not saved the file, are you sure to create a new one before you save?", "WARNING", MessageBoxButton.YesNoCancel);

                if (mbr == MessageBoxResult.Yes)
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = @"文本文档|*.txt|批处理文件|*.bat|C/C++源文件|*.c;*.cpp|python文件|*.py;|PHP文件|*.php;|javascript文件|*.js;|网页文件|*.html;*.htm|所有文件|*.*";
                    Nullable<bool> result = ofd.ShowDialog();
                    if (result == true)
                    {
                        StreamReader sr = new StreamReader(ofd.FileName, Encoding.Default);
                        string str = sr.ReadToEnd();
                        textbox1.Text = str;
                        UTF8Encoding utf8 = new UTF8Encoding();
                        byte[] buff = await FileBinaryConvertHelper.File2Bytes(ofd.FileName);
                        //byte[] buffer = utf8.GetBytes(str);
                        if (list1.Count == 0)
                        {
                            list1.Add(new files
                            {
                                content = buff,
                                filename = ofd.FileName
                            });
                        }
                        list1[0].filename = ofd.FileName;
                        list1[0].content = new byte[buff.Length];
                        list1[0].content = buff;
                        string msg = "";
                        await Task.Run(() =>
                        {
                            foreach (var item in buff)
                            {
                                msg += ($"{item:X2} ");
                            }
                        });

                        //  string msg = string.Format("{0}",buffer);
                        textbox2.Text = msg;
                        saveres = 0;
                        sr.Close();
                    }
                }
                else if (mbr == MessageBoxResult.No)
                {
                    button3_Click(sender, e);
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = @"文本文档|*.txt|批处理文件|*.bat|C/C++源文件|*.c;*.cpp|python文件|*.py;|PHP文件|*.php;|javascript文件|*.js;|网页文件|*.html;*.htm|所有文件|*.*";
                    Nullable<bool> result = ofd.ShowDialog();
                    if (result == true)
                    {
                        StreamReader sr = new StreamReader(ofd.FileName, Encoding.Default);
                        string str = sr.ReadToEnd();
                        textbox1.Text = str;
                        UTF8Encoding utf8 = new UTF8Encoding();
                        byte[] buff = await FileBinaryConvertHelper.File2Bytes(ofd.FileName);
                        //byte[] buffer = utf8.GetBytes(str);
                        if (list1.Count == 0)
                        {
                            list1.Add(new files
                            {
                                content = buff,
                                filename = ofd.FileName
                            });
                        }
                        list1[0].filename = ofd.FileName;
                        list1[0].content = new byte[buff.Length];
                        list1[0].content = buff;
                        string msg = "";
                        await Task.Run(() =>
                        {
                            foreach (var item in buff)
                            {
                                msg += ($"{item:X2} ");
                            }
                        });

                        //  string msg = string.Format("{0}",buffer);
                        textbox2.Text = msg;
                        saveres = 0;
                        sr.Close();
                    }
                }
                else if (mbr == MessageBoxResult.Cancel)
                {
                }
            }
            else
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = @"文本文档|*.txt|批处理文件|*.bat|C/C++源文件|*.c;*.cpp|python文件|*.py;|PHP文件|*.php;|javascript文件|*.js;|网页文件|*.html;*.htm|所有文件|*.*";
                Nullable<bool> result = ofd.ShowDialog();
                if (result == true)
                {
                    StreamReader sr = new StreamReader(ofd.FileName, Encoding.Default);
                    string str = sr.ReadToEnd();
                    textbox1.Text = str;
                    UTF8Encoding utf8 = new UTF8Encoding();
                    byte[] buff = await FileBinaryConvertHelper.File2Bytes(ofd.FileName);
                    //byte[] buffer = utf8.GetBytes(str);
                    if (list1.Count == 0)
                    {
                        list1.Add(new files
                        {
                            content = buff,
                            filename = ofd.FileName
                        });
                    }
                    list1[0].filename = ofd.FileName;
                    list1[0].content = new byte[buff.Length];
                    list1[0].content = buff;
                    string msg = "";
                    await Task.Run(() =>
                    {
                        foreach (var item in buff)
                        {
                            msg += ($"{item:X2} ");
                        }
                    });

                    //  string msg = string.Format("{0}",buffer);
                    textbox2.Text = msg;
                    saveres = 0;
                    sr.Close();
                }
            }
           
        }

        /// <summary>
        /// 去BOM头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, RoutedEventArgs e)
        {
        try
            {
                byte[] bombuffer = new byte[] { 0xef, 0xbb, 0xbf };
                byte[] buffer = new byte[] { };
                if (list1[0].content[0] == bombuffer[0] &&
                    list1[0].content[1] == bombuffer[1] &&
                    list1[0].content[2] == bombuffer[2])
                {
                    string msg = "";
                    byte[] buff = new byte[list1[0].content.Length - 3];
                    int len = 0;
                    for (int i = 3; i <= list1[0].content.Length - 1; i++)
                    {
                        len++;
                        buff[i - 3] = list1[0].content[i];
                    }
                    list1[0].content = new byte[len];
                    for (int i = 0; i <= len - 1; i++)
                    {
                        list1[0].content[i] = buff[i];
                        msg += ($"{list1[0].content[i]:X2} ");
                    }
                    textbox2.Text = msg;
                    string str = Encoding.UTF8.GetString(list1[0].content);
                    textbox1.Text = str;
                }
            }
            catch
            {
                textbox2.Text = "ERROR X001:在未打开文件的情况下去BOM头！";
            }
        }

        /// <summary>
        /// 输出文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = @"文本文档|*.txt|批处理文件|*.bat|C/C++源文件|*.c;*.cpp|python文件|*.py;|PHP文件|*.php;|javascript文件|*.js;|网页文件|*.html;*.htm|所有文件|*.*";
                bool? result = sfd.ShowDialog();
                if (result == true)
                {
                    using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        using (StreamWriter wt = new StreamWriter(fs, new UTF8Encoding(false)))
                        {
                            string str = Encoding.UTF8.GetString(list1[0].content);
                            wt.Write(str);
                            saveres = 1;
                            wt.Close();
                        }
                    }
                }
            }
            catch
            {
                //textbox2.Text = "ERROR X002:不能保存空白！";
            }
        }

        /// <summary>
        /// 显示文件信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileInfo fi = new FileInfo(list1[0].filename);
                string str = "";
                string msg = "";
                byte[] buff = new byte[textbox2.MaxLength];
                str += ($"最后修改日期：{fi.LastWriteTime}\n");
                str += ($"最后访问时间：{fi.LastAccessTime}\n");
                str += ($"文件创建时间：{fi.CreationTime}\n");
                str += ($"文件大小：{fi.Length}byte\n");
                str += ($"文件属性：{fi.Attributes}\n");
                str += ($"文件路径：{list1[0].filename}");
                foreach (var item in list1[0].content)
                {
                    msg += ($"{item:X2} ");
                }
                if (textbox2.Text == str)
                {
                    textbox2.Text = msg;
                }
                else
                {
                    textbox2.Text = str;
                }
            }
            catch
            {
                textbox2.Text = "ERROR X002:未打开文件无法查看文件信息！";
            }
        }

        /// <summary>
        /// UTF-8自动翻译
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textbox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            saveres = 0;
            byte[] buff = new byte[textbox2.MaxLength];
            buff = Encoding.UTF8.GetBytes(textbox1.Text);
            string msg = "";
            if (list1.Count == 0)
            {
                list1.Add(new files{content = buff,filename = null});
            }
            list1[0].content = new byte[buff.Length];
            foreach (var item in buff)
            {
                msg += ($"{item:X2} ");
            }
            for (int i = 0; i <= buff.Length - 1; i++)
            {
                list1[0].content[i] = buff[i];
            }
            textbox2.Text = msg;     
        }

        /// <summary>
        /// UTF-8自动翻译为字符串（尝试中，未完成）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textbox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            string str;
            byte[] buff = new byte[textbox2.MaxLength];
            str = textbox2.Text;
            str = str.Replace(" ", "%");
            string[] st = str.Split('%');
        }

        /// <summary>
        /// 更换背景图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = @"图片文件|*.jpg;*.png;*.bmp";
            Nullable<bool> result = ofd.ShowDialog();
            if (result == true)
            {
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = new BitmapImage(new Uri(ofd.FileName, UriKind.Absolute));
                imageBrush.Stretch = Stretch.Fill;//设置图像的显示格式  
                this.Background = imageBrush;
            }
        }

        private void EXIT(object sender, RoutedEventArgs e)
        {
            object obj = this;
            if (saveres == 0)
            {
                MessageBoxResult mbr = MessageBox.Show("you have not saved the file, are you sure to exit?", "WARNING", MessageBoxButton.YesNoCancel);

                if (mbr == MessageBoxResult.Yes)
                {
                    MainWindow mw = (MainWindow)obj;
                    this.Close();
                }
                else if (mbr == MessageBoxResult.No)
                {
                    button3_Click(sender, e);
                    this.Close();
                }
                else if (mbr == MessageBoxResult.Cancel)
                {
                }
            }
            else
            {
                this.Close();
            }
        }

        private void Info(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show("Copyright Takakaze\n Alpha ver0.53.1.19.12.00","About",MessageBoxButton.OK);
        }

        private void NEW(object sender, RoutedEventArgs e)
        {
            if (saveres == 0)
            {
                MessageBoxResult mbr = MessageBox.Show("you have not saved the file, are you sure to create a new one?", "WARNING", MessageBoxButton.YesNoCancel);

                if (mbr == MessageBoxResult.Yes)
                {
                    textbox1.Text = null;
                    textbox2.Text = null;
                }
                else if (mbr == MessageBoxResult.No)
                {
                    button3_Click(sender, e);
                    textbox1.Text = null;
                    textbox2.Text = null;
                }
                else if (mbr == MessageBoxResult.Cancel)
                {
                }
            }
            else
            {
                textbox1.Text = null;
                textbox2.Text = null;
            }
            saveres = 1;
        }
    }
}
