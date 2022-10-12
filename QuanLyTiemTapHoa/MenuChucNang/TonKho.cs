﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace QuanLyTiemTapHoa.MenuChucNang
{
    public partial class TonKho : Form
    {
        #region Global Varibles
        SqlConnection cnn = KetNoiCoSoDuLieu.cnn;
        SqlCommand command;
        string str = @"Data Source=LAPTOP-FAMD6FDU\PHAMHAO;Initial Catalog=QLCuaHangTapHoa;Integrated Security=True";
       // string str = @"Data Source=DESKTOP-O2TB88K\SQLEXPRESS;Initial Catalog=QLCuaHangTapHoa;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        public TonKho()
        {
            InitializeComponent();
        }
        #endregion
        #region Methods
        void LoadData()
        {
            command = cnn.CreateCommand();
            command.CommandText = "select MaSP,TenSP,SoLuong,TenDV,GiaNhap,GiaBan,TenNCC from NhapKho n, NhaCungCap c,DonViSP d where n.MaNCC = c.MaNCC and n.MaDV= d.MaDV ";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dtgvTonKho.DataSource = table;
        }
        void AddCmbDonVi()
        {
            cnn = new SqlConnection(str);
            cnn.Open();
            string sql = "select TenDV from DonViSP";
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmbDonVi.DisplayMember = "TenDV";
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            cmbDonVi.DataSource = dt;
        }
        void AddCmbNcc()
        {
            cnn = new SqlConnection(str);
            cnn.Open();
            string sql = "select TenNCC from NhaCungCap";
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmbNCC.DisplayMember = "TenNCC";
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            cmbNCC.DataSource = dt;
        }
        void Add()
        {
            string ma = txtMaSP.Text;
            string tensp = txtTenSp.Text;
            string sl = txtSL.Text;
            string gn = txtGiaNhap.Text;
            string gb =txtGiaBan.Text;
            int dv = cmbDonVi.SelectedIndex + 1;
            int ncc = cmbNCC.SelectedIndex + 1;
            command = cnn.CreateCommand();
            command.CommandText = "Insert into TonKho values('" + ma + "',N'" + tensp + "','" + sl + "','" + dv + "','" + gn + "','" + gb + "','" + ncc + "')";
            command.ExecuteNonQuery();
            LoadData();
        }
        void Delete()
        {
            command = cnn.CreateCommand();
            command.CommandText = "delete from NhapKho where MaSP = '" + txtMaSP.Text + "'";
            command.ExecuteNonQuery();
            LoadData();
        }
        void Update()
        {

            string ma = txtMaSP.Text;
            string tensp = txtTenSp.Text;
            string sl = txtSL.Text;
            decimal gn = decimal.Parse(txtGiaNhap.Text);
            decimal gb = decimal.Parse(txtGiaBan.Text);
            int dv = cmbDonVi.SelectedIndex + 1;
            int ncc = cmbNCC.SelectedIndex + 1;
            command = cnn.CreateCommand();
            command.CommandText = "update NhapKho set TenSP = N'" + tensp + "',SoLuong = '" + sl + "',MaDV = '" + dv + "',GiaNhap = '" + gn + "',GiaBan = '" + gb + "',MaNCC = '" + ncc + "' where MaSP = '" + ma + "'";
            command.ExecuteNonQuery();
            LoadData();
        }
        void Search()
        {
            command = cnn.CreateCommand();
            command.CommandText = "select MaSP,TenSP,SoLuong,TenDV,GiaNhap,GiaBan,TenNCC from NhapKho n, NhaCungCap c,DonViSP d where n.MaNCC = c.MaNCC and n.MaDV= d.MaDV and MaSP = '" + txtSearch.Text + "' ";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dtgvTonKho.DataSource = table;
        }
        #endregion
        #region Events

        private void TonKho_Load_1(object sender, EventArgs e)
        {
            LoadData();
            AddCmbDonVi();
            AddCmbNcc();
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            Add();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            Update();
        }

        private void btnDel_Click_1(object sender, EventArgs e)
        {
            Delete();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            Search();
        }

        private void dtgvTonKho_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            txtMaSP.Text = dtgvTonKho.Rows[e.RowIndex].Cells[0].Value?.ToString();
            txtTenSp.Text = dtgvTonKho.Rows[e.RowIndex].Cells[1].Value?.ToString();
            txtSL.Text = dtgvTonKho.Rows[e.RowIndex].Cells[2].Value?.ToString();
            cmbDonVi.Text = dtgvTonKho.Rows[e.RowIndex].Cells[3].Value?.ToString();
            txtGiaNhap.Text = dtgvTonKho.Rows[e.RowIndex].Cells[4].Value?.ToString();
            txtGiaBan.Text = dtgvTonKho.Rows[e.RowIndex].Cells[5].Value?.ToString();
            cmbNCC.Text = dtgvTonKho.Rows[e.RowIndex].Cells[6].Value?.ToString();
            txtSearch.Text = dtgvTonKho.Rows[e.RowIndex].Cells[0].Value?.ToString();
        }


        private void btnMenu_Click_1(object sender, EventArgs e)
        {
            frmMenu frmMenu = new frmMenu();
            this.Hide();
            frmMenu.ShowDialog();
        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
            NhapKho frmnk = new NhapKho();
            frmnk.Show();
        }
        #endregion
    }
}