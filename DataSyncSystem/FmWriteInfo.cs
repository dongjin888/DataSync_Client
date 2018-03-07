using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataSyncSystem.Utils;
using DataSyncSystem.SelfView;
using DataSyncSystem.Dao;

namespace DataSyncSystem
{
    public partial class FmWriteInfo : Form
    {
        bool isEngMode = false;
        DataService service = null;
        TrialInfo trialInfo = null;

        List<string> teamList = new List<string>();
        List<string> teamUserList = new List<string>();
        List<string> pltfmList = new List<string>();
        List<string> pdctList = new List<string>();

        List<Control> infos = new List<Control>();

        public FmWriteInfo()
        {
            InitializeComponent();
        }

        public FmWriteInfo(bool engMode,DataService service,ref TrialInfo trial)
        {

            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            infos.Add(combActivator);
            infos.Add(combOperator);
            infos.Add(combPltfm);
            infos.Add(combPdct);
            infos.Add(txtInfo);
            infos.Add(txtOther);

            isEngMode = engMode;
            this.service = service;
            trialInfo = trial;

            combActivator.Text = trial.Activator;
            combOperator.Text = Cache.userId;
            combPltfm.Text = trial.Pltfm;
            combPdct.Text = trial.Pdct;
            txtInfo.Text = trial.Info;
            txtOther.Text = trial.Other;

            teamList = service.getTeamList();
            if(teamList != null)
            {
                foreach (string team in teamList)
                {
                    combTeam.Items.Add(team);
                }
                if (combTeam.Text.Equals("") && teamList.Count >= 1)
                {
                    combTeam.SelectedIndex = 0;
                    combTeam.Refresh();
                }
            }

            teamUserList = service.getUserNmLstByTeam(teamList[0]);
            if(teamUserList != null)
            {
                foreach (string user in teamUserList)
                {
                    combActivator.Items.Add(user);
                }
                if (combActivator.Text.Equals("") && teamUserList.Count >= 1)
                {
                    combActivator.SelectedIndex = 0;
                    combActivator.Refresh();
                }
            }

            pltfmList = service.getPltfmNames();
            if(pltfmList != null)
            {
                foreach(string pltfm in pltfmList)
                {
                    combPltfm.Items.Add(pltfm);
                }
                if(combPltfm.Text.Equals("") && pltfmList.Count >= 1)
                {
                    combPltfm.SelectedIndex = 0;
                    combPltfm.Refresh();
                }
            }

            pdctList = service.getPdctNamesByPltfm(pltfmList[0]);
            if(pdctList != null)
            {
                foreach(string pdct in pdctList)
                {
                    combPdct.Items.Add(pdct);
                }
                if(combPdct.Text.Equals("") && pdctList.Count >= 1)
                {
                    combPdct.Text = pdctList[0];
                }
            }

        }

        private void combTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectTeam = (string)combTeam.SelectedItem;
            if(selectTeam != null)
            {
                teamUserList = service.getUserNmLstByTeam(selectTeam);
                if(teamUserList != null && teamUserList.Count > 0)
                {
                    combActivator.Items.Clear();
                    foreach (string user in teamUserList)
                    {
                        combActivator.Items.Add(user);
                    }
                    combActivator.SelectedIndex = 0;
                    combActivator.Refresh();
                }
                else
                {
                    combActivator.Items.Clear();
                    combActivator.Text = "";
                }
            }
        }

        private void combPltfm_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectPltfm = (string)combPltfm.SelectedItem;
            if(selectPltfm != null)
            {
                pdctList = service.getPdctNamesByPltfm(selectPltfm);
                if(pdctList != null && pdctList.Count > 0)
                {
                    combPdct.Items.Clear();
                    foreach(string pdct in pdctList)
                    {
                        combPdct.Items.Add(pdct);
                    }
                    combPdct.SelectedIndex = 0;
                    combPdct.Refresh();
                }
                else
                {
                    combPdct.Items.Clear();
                    combPdct.Text = "";
                }
            }
        }

        private void combActivator_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectActivator = (string)combActivator.SelectedItem;
            if(selectActivator != null)
            {
                txtInfo.Text = selectActivator + "'s code test!";
            }
        }

        private void btSure_Click(object sender, EventArgs e)
        {
            foreach(Control con in infos)
            {
                if (con.Text.Equals(""))
                {
                    MessageBox.Show("Trail info not complete!", "error");
                    return;
                }
            }
            trialInfo.Activator = combActivator.Text;
            trialInfo.Operator = combOperator.Text;
            trialInfo.Pltfm = combPltfm.Text;
            trialInfo.Pdct = combPdct.Text;
            trialInfo.Info = txtInfo.Text;
            trialInfo.Other = txtOther.Text;

            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }
    }
}
