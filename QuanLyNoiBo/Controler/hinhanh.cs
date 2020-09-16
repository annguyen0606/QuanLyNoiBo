using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNoiBo.Controler
{
    class hinhanh
    {
        public static string LayDuongDanHinhAnh()
        {
            String linkPic = "";
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "PNG Files (.png)|*.png|JPG Files (.jpg)|*.jpg|GIF Files (.jpeg)|*.jpeg|All Files (.*)|*.*";
                dialog.Title = "Chọn Hình Ảnh";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    linkPic = dialog.FileName.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return linkPic;
        }

        public static byte[] ReadBytePic(String linkImage)
        {
            byte[] dataAfterRead = null;
            FileStream fs = new FileStream(linkImage, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            dataAfterRead = br.ReadBytes((Int32)fs.Length);
            return dataAfterRead;
        }
    }
}
