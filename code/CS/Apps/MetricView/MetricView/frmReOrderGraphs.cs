using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Teleflora.Operations.MetricView
{
    public partial class frmReOrderGraphs : Form
    {
        private MetricViewConfiguration _config;
        public MetricViewConfiguration Config
        {
            get { return _config; }
        }

        private string[] items = new string[1];

        public frmReOrderGraphs(MetricViewConfiguration Configuration)
        {
            InitializeComponent();
            _config = Configuration;
            btnUpdate.Enabled = false;
            btnMoveUp.Enabled = false;
            btnMoveDown.Enabled = false;
            LoadItemArray();
            LoadListBox();
        }

        private void LoadItemArray()
        {
            items = new string[_config.MetricGraphs.Count];
            int itemCount = 0;

            foreach (KeyValuePair<string, MetricGraphConfiguration> listEntry in _config.MetricGraphs)
                items[itemCount++] = GeneralUtility.StripSeqFromName(listEntry.Value.GraphName);
        }

        private void LoadListBox()
        {
            lbGraphs.Items.Clear();
            foreach (string s in items)
                lbGraphs.Items.Add(s);
        }




        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateConfig();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void UpdateConfig()
        {
            MetricGraphConfiguration[] graphConfigs = new MetricGraphConfiguration[_config.MetricGraphs.Count];
            string[] graphNames = new string[_config.MetricGraphs.Count];

            int i = 0;
            foreach (KeyValuePair<string, MetricGraphConfiguration> listEntry in _config.MetricGraphs)
            {
                graphNames[i] = GeneralUtility.StripSeqFromName(listEntry.Value.GraphName);
                graphConfigs[i++] = listEntry.Value;
            }

            _config.MetricGraphs.Clear();

            for (int j = 0; j < items.Length; j++)
            {
                string name = items[j];
                for (int k = 0; k < graphNames.Length; k++)
                {
                    if (name == graphNames[k])
                    {
                        string seqName = "~[" + j.ToString("0000") + "]" + name;
                        graphConfigs[k].GraphName = seqName;
                        _config.MetricGraphs.Add(seqName, graphConfigs[k]);
                    }
                }
            }
        }

        private void lbGraphs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbGraphs.SelectedItems.Count == 0)
            {
                btnMoveUp.Enabled = false;
                btnMoveDown.Enabled = false;
                return;
            }

            if (lbGraphs.SelectedIndices[0] == 0)
            {
                btnMoveUp.Enabled = false;
                if(lbGraphs.SelectedIndices[0] != lbGraphs.Items.Count - 1)
                    btnMoveDown.Enabled = true;

                return;
            }

            if (lbGraphs.SelectedIndices[0] == lbGraphs.Items.Count - 1)
            {
                btnMoveDown.Enabled = false;
                if (lbGraphs.SelectedIndices[0] != 0)
                    btnMoveUp.Enabled = true;

                return;
            }

            btnMoveUp.Enabled = true;
            btnMoveDown.Enabled = true;
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            UpdateItemArray(-1);
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            UpdateItemArray(1);
        }


        private void UpdateItemArray(int offset)
        {
            int index = lbGraphs.SelectedIndices[0];
            btnUpdate.Enabled = true;
            int newIndex = index + offset;
            string movedItem = items[index];
            string displacedItem = items[newIndex];
            items[index] = displacedItem;
            items[newIndex] = movedItem;
            LoadListBox();

            for (int i = 0; i < lbGraphs.Items.Count; i++)
            {
                if (lbGraphs.Items[i].ToString() == movedItem)
                    lbGraphs.SetSelected(i, true);
            }
        }


    }
}