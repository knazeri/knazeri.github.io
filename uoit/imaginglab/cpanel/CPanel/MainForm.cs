﻿using ImagingLab.CPanel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImagingLab.CPanel
{
    public partial class MainForm : Form
    {
        #region Class methods

        public MainForm()
        {
            InitializeComponent();
            RenderForm();
        }

        void RenderForm()
        {
            RenderPeople();
            RenderPublication();
            RenderTeching();
        }

        void InitGrid(DataGridView grid, object datasource)
        {
            grid.DataSource = datasource;
            grid.RowEnter += (s, e) => RenderForm();
            grid.UserDeletingRow += (s, e) => e.Cancel = !ConfirmDelete();
            grid.UserDeletedRow += (s, e) => RenderForm();
        }

        void SaveData()
        {
            CPanelApp.Current.Data.title = txt_title.Text;
            CPanelApp.Current.Data.updated = DateTime.Now.ToString("MMMM yyyy");
            CPanelApp.Current.SaveData();
        }

        bool ConfirmDelete()
        {
            DialogResult res = MessageBox.Show("Are you sure to delete the selected item?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                return true;
            }

            return false;
        }

        void DeleteRow(DataGridView grid)
        {
            bool res = ConfirmDelete();
            if (res)
            {
                DataGridViewRow selected = grid.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
                if (selected != null)
                {
                    int index = Math.Min(selected.Index, grid.Rows.Count - 2);
                    grid.Rows.Remove(selected);
                    if (index >= 0) grid.Rows[index].Selected = true;
                    RenderForm();
                }
            };
        }

        #endregion

        #region Home

        void MainForm_Load(object sender, EventArgs e)
        {
            txt_title.Text = CPanelApp.Current.Data.title;
            label_update.Text = CPanelApp.Current.Data.updated;

            InitGrid(grd_teaching, CPanelApp.Current.Data.teachings);
            InitGrid(grd_people, CPanelApp.Current.Data.people);
            InitGrid(grd_publications, CPanelApp.Current.Data.publications);
        }

        void button_publish_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        void button_save_Click(object sender, EventArgs e)
        {
            SaveData();
            MessageBox.Show("Data is saved successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void tab_cpanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            RenderForm();
        }

        #endregion

        #region People

        void button_addPeople_Click(object sender, EventArgs e)
        {

        }

        void button_editPeople_Click(object sender, EventArgs e)
        {

        }

        void button_deletePeople_Click(object sender, EventArgs e)
        {
            DeleteRow(grd_people);
        }

        void RenderPeople()
        {
            bool selected = grd_people.SelectedRows.Count > 0;
            button_editPeople.Enabled = selected;
            button_deletePeople.Enabled = selected;
        }

        #endregion

        #region Publications

        void button_addPublication_Click(object sender, EventArgs e)
        {

        }

        void button_editPublication_Click(object sender, EventArgs e)
        {

        }

        void button_deletePublication_Click(object sender, EventArgs e)
        {
            DeleteRow(grd_publications);
        }

        void RenderPublication()
        {
            bool selected = grd_publications.SelectedRows.Count > 0;
            button_editPublication.Enabled = selected;
            button_deletePublication.Enabled = selected;
        }

        #endregion

        #region Teachings

        void button_addTeaching_Click(object sender, EventArgs e)
        {

        }

        void button_editTeaching_Click(object sender, EventArgs e)
        {

        }

        void button_deleteTeaching_Click(object sender, EventArgs e)
        {
            DeleteRow(grd_teaching);
        }

        void RenderTeching()
        {
            bool selected = grd_teaching.SelectedRows.Count > 0;
            button_editTeaching.Enabled = selected;
            button_deleteTeaching.Enabled = selected;
        }

        #endregion
   }
}
