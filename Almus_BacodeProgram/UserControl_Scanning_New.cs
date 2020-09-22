using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using BarcodeSqliteLib;
using System.Data.SQLite;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Almus_BacodeProgram
{
    public partial class UserControl_Scanning_New : UserControl, IUserControl
    {
        ErrorAlarmForm errorAlarmForm;

        BarcodeTestResult_CRUD testResult_CRUD;
        BarcodeTestResultInfo testResultInfo = new BarcodeTestResultInfo();

        BarcodeReferenceCode_CRUD code_CRUD;
        SQLiteConnection conn = null;
        string connStr = null;

        DataSet master_ds = new DataSet(); //unit code
        DataSet middle_ds = new DataSet(); //middle code
        DataSet carton_ds = new DataSet(); //carton code

        FunctionSetting function = new FunctionSetting();
        string jsonFunctionSettingFilePath = Path.Combine(Environment.CurrentDirectory, "functionSetting.json");
        string jsonFilePath = Path.Combine(Environment.CurrentDirectory, "testResultConf.json");

        StringBuilder scannedCode = new StringBuilder();
        string _masterCode = "";
        int _masterIndex = 0;
        string _middleCode = "";
        int _middleIndex = 0;
        string _cartonCode = "";
        int _cartonIndex = 0;

        int _unit_1Carton = 0; //DB
        int _middle_1Carton = 0; //DB
        int _unit_1Middle = 0; //DB

        int currentTestCount = 0; //DB
        int currentLotNumber = 1; //TEST
        int currentSeqQty = 0;

        int sumOfPassCount = 0; // DISP
        int sumOfLot = 1; //DB
        int sumOfTestCount = 0; //
        int passCount = 0; //TEST
        int failCount = 0;

        bool _statustestSkuCode = false;

        public UserControl_Scanning_New()
        {
            InitializeComponent();
            InitializeDatabaseConfig();
        }

        private void InitializeDatabaseConfig()
        {
            string currentFolder = Environment.CurrentDirectory;
            string path = Path.Combine(currentFolder, "BarcodeDatabase.sqlite");
            connStr = "Data Source=" + path + ";" + "version=3;";

            using (conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                code_CRUD = new BarcodeReferenceCode_CRUD(conn);
                master_ds = code_CRUD.SelectAll_Dataset(BarcodeReferenceCode_CRUD.Table.MASTER_CODE);
                middle_ds = code_CRUD.SelectAll_Dataset(BarcodeReferenceCode_CRUD.Table.BOX_CODE);
                carton_ds = code_CRUD.SelectAll_Dataset(BarcodeReferenceCode_CRUD.Table.LOT_CODE);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "START")
            {
                _masterCode = cbbMaster.Text;
                _middleCode = cbbMiddle.Text;
                _cartonCode = cbbCarton.Text;

                _masterIndex = cbbMaster.SelectedIndex;
                _middleIndex = cbbMiddle.SelectedIndex;
                _cartonIndex = cbbCarton.SelectedIndex;

                try
                {
                    this._middle_1Carton = Int32.Parse(txtMiddlePerCarton.Text);
                    this._unit_1Middle = Int32.Parse(txtUnitPerMiddle.Text);
                }
                catch
                {
                    if (function.BOX_LOT_CODE == false && function.COUNT_SETUP == false)
                    {
                        MessageBox.Show(this, "Must write down \"Box Qty\" and \"Lot Qty\" Number!", "[Error]", MessageBoxButtons.OK);
                        return;
                    }
                }

                this._unit_1Carton = this._middle_1Carton * this._unit_1Middle;
                txtUnitPerCarton.Text = this._unit_1Carton.ToString();

                currentLotNumber = 1;
                sumOfLot = 1;
                currentSeqQty = 0;
                currentTestCount = 0;
                failCount = 0;
                passCount = 0;
                sumOfPassCount = 0;

                if (function.COUNT_SETUP == false)
                {
                    this.lblMiddleBoxNumber.Text = this.sumOfLot.ToString();
                    this.lblTestCount.Text = this.currentSeqQty.ToString();
                }
                else
                {
                    this.lblMiddleBoxNumber.Text = "";
                    this.lblTestCount.Text = "";
                }

                this.btnPassFail.Text = "PASS (" + (this.passCount).ToString() + ") / FAIL (" + (this.failCount).ToString() + ")";

                cbbMaster.DropDownStyle = ComboBoxStyle.Simple;

                if (function.BOX_LOT_CODE == false)
                {
                    cbbMiddle.Enabled = true;
                    cbbCarton.Enabled = true;
                    cbbMiddle.DropDownStyle = ComboBoxStyle.Simple;
                    cbbCarton.DropDownStyle = ComboBoxStyle.Simple;
                }
                else
                {
                    cbbMiddle.Text = "";
                    cbbCarton.Text = "";
                    cbbMiddle.Enabled = false;
                    cbbCarton.Enabled = false;
                }

                btnStart.BackColor = Color.Tomato;
                btnStart.Text = "STOP";

                txtSkuScanData.Text = "";
                this.ActiveControl = txtSkuScanData;
                //FocusTimer.Enabled = true;
                //FocusTimer.Start();
            }
            else
            {
                _masterCode = "";
                _middleCode = "";
                _cartonCode = "";

                _masterIndex = 0;
                _middleIndex = 0;
                _cartonIndex = 0;

                currentLotNumber = 1;
                sumOfLot = 1;
                currentSeqQty = 0;

                txtUnitPerCarton.Text = "";
                txtMiddlePerCarton.Text = "";
                txtUnitPerMiddle.Text = "";
                this.lblMiddleBoxNumber.Text = "0";

                cbbMaster.DropDownStyle = ComboBoxStyle.DropDown;
                cbbMiddle.DropDownStyle = ComboBoxStyle.DropDown;
                cbbCarton.DropDownStyle = ComboBoxStyle.DropDown;

                btnStart.BackColor = Color.LimeGreen;
                btnStart.Text = "START";

                //FocusTimer.Enabled = false;
                //FocusTimer.Stop();
            }
        }

        private void UserControl_Scanning_New_Load(object sender, EventArgs e)
        {
            InitializeControlAddedEvent();
        }

        private void UserControl_Scanning_New_VisibleChanged(object sender, EventArgs e)
        {
            InitializeControlAddedEvent();
        }

        private void InitializeControlAddedEvent()
        {
            FileInfo existFunctionFileCheck = new FileInfo(this.jsonFunctionSettingFilePath);

            if (existFunctionFileCheck.Exists == true)
            {
                using (FileStream fs = new FileStream(this.jsonFunctionSettingFilePath, FileMode.Open))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        string jsonDeSerializedData = br.ReadString();
                        this.function = JsonConvert.DeserializeObject<FunctionSetting>(jsonDeSerializedData);
                        br.Close();
                    }
                    fs.Close();
                }
            }

            FileInfo existCheck = new FileInfo(this.jsonFilePath);

            if (existCheck.Exists == true)
            {
                using (FileStream fs = new FileStream(this.jsonFilePath, FileMode.Open))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        string jsondeSerializedData = br.ReadString();
                        this.testResultInfo = JsonConvert.DeserializeObject<BarcodeTestResultInfo>(jsondeSerializedData);
                        br.Close();
                    }
                    fs.Close();
                }

                if (this.testResultInfo.Factory_Name == "" || this.testResultInfo.Worker_Name == "")
                {
                    this.testResultInfo.Factory_Name = "None";
                    this.testResultInfo.Worker_Name = "None";
                    this.testResultInfo.Line_Num = 0;
                }
            }
            else
            {
                this.testResultInfo.Factory_Name = "None";
                this.testResultInfo.Worker_Name = "None";
                this.testResultInfo.Line_Num = 0;
            }

            using (conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                code_CRUD = new BarcodeReferenceCode_CRUD(conn);
                master_ds = code_CRUD.SelectAll_Dataset(BarcodeReferenceCode_CRUD.Table.MASTER_CODE);
                middle_ds = code_CRUD.SelectAll_Dataset(BarcodeReferenceCode_CRUD.Table.BOX_CODE);
                carton_ds = code_CRUD.SelectAll_Dataset(BarcodeReferenceCode_CRUD.Table.LOT_CODE);
            }

            cbbMaster.DataSource = master_ds.Tables["MasterCodeTable"];
            cbbMaster.DisplayMember = "MasterCode";
            try
            {
                cbbMaster.SelectedIndex = _masterIndex;
            }
            catch
            {

            }

            cbbMiddle.DataSource = middle_ds.Tables["BoxCodeTable"];
            cbbMiddle.DisplayMember = "BoxCode";

            try
            {
                if (function.BOX_LOT_CODE == false)
                {
                    cbbMiddle.Enabled = true;
                    cbbMiddle.SelectedIndex = _middleIndex;
                }
                else
                {
                    cbbMiddle.Enabled = false;
                    cbbMiddle.Text = "";
                }

            }
            catch
            {

            }

            cbbCarton.DataSource = carton_ds.Tables["LotCodeTable"];
            cbbCarton.DisplayMember = "LotCode";

            try
            {
                if (function.BOX_LOT_CODE == false)
                {
                    cbbCarton.Enabled = true;
                    cbbCarton.SelectedIndex = _cartonIndex;
                }
                else
                {
                    cbbCarton.Enabled = false;
                    cbbCarton.Text = "";
                }
            }
            catch
            {

            }
        }

        private void txtSkuScanData_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.BeginInvoke(new Action(delegate ()
                {
                    txtSkuScanData.Text = scannedCode.ToString();
                    txtUnitBox.Text = scannedCode.ToString();
                    scannedCode.Clear();

                    if (function.BOX_LOT_CODE == false && function.COUNT_SETUP == false)
                    {
                        if (this.currentLotNumber > this._middle_1Carton)
                        {
                            if (txtSkuScanData.Text == _middleCode)
                            {
                                txtSkuScanData.Text = "Complete Box";

                                this.sumOfPassCount = 0;
                                this.failCount = 0;
                                this.sumOfLot++;
                                this.currentLotNumber = 1;
                                this.currentSeqQty = 0;

                                this.btnPassFail.Text = "PASS (" + (this.sumOfPassCount).ToString() + ") / FAIL (" + (this.failCount).ToString() + ")";
                                this.lblMiddleBoxNumber.Text = this.currentLotNumber.ToString();
                                this.lblTestCount.Text = this.currentSeqQty.ToString();
                                return;
                            }
                            else
                            {
                                // Phoung want
                                errorAlarmForm = new ErrorAlarmForm(testResultInfo, false);
                                errorAlarmForm.Show(this);
                                txtSkuScanData.Text = "Scan Box Code !";
                                return;
                            }
                        }

                        if (this.passCount >= this._unit_1Middle) // lot + 1
                        {
                            // CHECK LOT CODE
                            if (txtSkuScanData.Text == _cartonCode)
                            {
                                passCount = 0;
                                this.currentLotNumber++;
                                this.sumOfLot++;
                                txtSkuScanData.Text = "";
                                this.lblMiddleBoxNumber.Text = this.currentLotNumber.ToString();

                                if (this.currentLotNumber > this._middle_1Carton)
                                {
                                    this.sumOfLot--;
                                    this.lblMiddleBoxNumber.Text = (this._middle_1Carton).ToString();
                                    txtSkuScanData.Text = "Scan Box Code !";
                                }
                                return;
                            }
                            else
                            {
                                // Phoung want
                                errorAlarmForm = new ErrorAlarmForm(testResultInfo, false);
                                errorAlarmForm.Show(this);
                                txtSkuScanData.Text = "Scan Lot Code !";
                                return;
                            }
                        }
                    }

                    this.currentSeqQty++;
                    this.currentTestCount++;

                    if (this.function.COUNT_SETUP == false)
                    {
                        this.lblTestCount.Text = this.currentSeqQty.ToString();
                    }


                    testResultInfo.Time = DateTime.Now;
                    testResultInfo.Origin_Item = _masterCode;
                    testResultInfo.Compared_Item = txtSkuScanData.Text;
                    testResultInfo.Sample_1Box = this._unit_1Carton;
                    testResultInfo.Sample_Tray = this._unit_1Middle;
                    testResultInfo.TrayNumber = this.sumOfLot;
                    testResultInfo.TestCount = this.currentTestCount;

                    if (txtSkuScanData.Text == _masterCode)
                    {
                        testResultInfo.Result = true;
                        testResultInfo.Reason = "TEST OK";

                        btnPassFail.Text = "OK";
                        sumOfPassCount++;
                        passCount++;
                        _statustestSkuCode = true;

                        using (conn = new SQLiteConnection(connStr))
                        {
                            conn.Open();
                            testResult_CRUD = new BarcodeTestResult_CRUD(conn);
                            testResult_CRUD.Insert(testResultInfo);
                        }
                    }
                    else
                    {
                        testResultInfo.Result = false;
                        errorAlarmForm = new ErrorAlarmForm(testResultInfo, true);
                        errorAlarmForm.Show(this);
                        failCount++;
                        _statustestSkuCode = false;
                        btnPassFail.Text = "NG";
                    }
                }));
                this.ActiveControl = txtSerialScanData;
            }
            else
            {
                scannedCode.Append(KeyToUni.KeyCodeToUnicode(e.KeyCode).ToUpper());
            }
            
        }

        private void txtSerialScanData_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSerialScanData.Text = scannedCode.ToString();
                if (txtSerialScanData.Text.CompareTo("") != 1)
                {
                    this.BeginInvoke(new Action(delegate ()
                    {
                        this.btnPassFail.Text = "PASS (" + (this.sumOfPassCount).ToString() + ") / FAIL (" + (this.failCount).ToString() + ")";

                        if (function.BOX_LOT_CODE == false && function.COUNT_SETUP == false)
                        {
                            if (this.passCount >= this._unit_1Middle)
                            {
                                txtSkuScanData.Text = "Scan Lot Code !";
                            }
                        }
                    }));
                }

                this.txtSkuScanData.KeyUp -= this.txtSkuScanData_KeyUp;
                Stopwatch delay = new Stopwatch();
                delay.Start();

                while (true)
                {
                    if (delay.ElapsedMilliseconds > 900)
                    {
                        delay.Stop();
                        break;
                    }
                }
                this.ActiveControl = txtSkuScanData;
                this.txtSkuScanData.KeyUp += this.txtSkuScanData_KeyUp;
            }
        }
    }
}
