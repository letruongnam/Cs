using AppQLSV.DAL;
using AppQLSV.GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppQLSV
{
    public partial class frmMain : Form
    {
        
        public frmMain()
        {
            InitializeComponent();
            gridLopHoc.AutoGenerateColumns = false;
            dataGridView2.AutoGenerateColumns = false;
            DanhSachLopHoc();
        }

        private void btnThemLop_Click(object sender, EventArgs e)
        {
            var f = new frmLopChiTiet();
            if(f.ShowDialog()== DialogResult.OK)
            {
                DanhSachLopHoc();
            }
        }

        private void DanhSachLopHoc()
        {
            AppQLSVDBContext db = new AppQLSVDBContext();
           var ls =  db.Classrooms.OrderBy(e=>e.Name).ToList();
         
            BDSLopHoc.DataSource = ls;
            gridLopHoc.DataSource = BDSLopHoc;
          
           
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var LopDangChon = BDSLopHoc.Current as Classroom;
            if (LopDangChon != null)
            {
               var rs =   MessageBox.Show("Bạn có thực sự muốn xóa không? ",
                                            "chú ý",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Warning);
                if(rs== DialogResult.OK)
                {
                    //Xóa lớp đang chọn
                    AppQLSVDBContext db = new AppQLSVDBContext();
                    var lop = db.Classrooms.Where(t => t.ID == LopDangChon.ID).FirstOrDefault();
                    if (lop != null)
                    {
                        db.Classrooms.Remove(lop);
                        db.SaveChanges();
                        DanhSachLopHoc();
                    }
                }

            }

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BDSLopHoc_CurrentChanged(object sender, EventArgs e)
        {
            var LopDangChon = BDSLopHoc.Current as Classroom;
            if (LopDangChon != null)
            {
                var db = new AppQLSVDBContext();
                var dsSv = db.Students.Where(h => h.IDClassroom == LopDangChon.ID).ToList();
                bdsSinhVien.DataSource = dsSv;
                dataGridView2.DataSource = bdsSinhVien;
            }
        }

        private void btnSuaLop_Click(object sender, EventArgs e)
        {
            var lopDangChon = BDSLopHoc.Current as Classroom;
            if (lopDangChon != null)
            {
                var f = new frmLopChiTiet(lopDangChon);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    DanhSachLopHoc();
                }
            }
          
        }
    }
}
