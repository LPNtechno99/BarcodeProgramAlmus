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
        Stopwatch delay = new Stopwatch();

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
        int currentMiddleNumber = 1; //TEST
        int currentSeqQty = 0;

        int sumOfPassCount = 0; // DISP
        int sumOfLot = 1; //DB
        int sumOfTestCount = 0; //
        int passCount = 0; //TEST
        int failCount = 0;

        bool _scanMiddleCode = false;
        bool _scanCartonCode = false;
        bool _iFlag;

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
                if (!chbOnlyMaster.Checked)
                {
                    Properties.Settings.Default.Middle = int.Parse(txtUnitPerMiddle.Text.Trim());
                    Properties.Settings.Default.Carton = int.Parse(txtMiddlePerCarton.Text.Trim());
                    Properties.Settings.Default.Save();
                }

                _masterCode = cbbMaster.Text;
                _middleCode = cbbMiddle.Text;
                _cartonCode = cbbCarton.Text;

                _masterIndex = cbbMaster.SelectedIndex;
                _middleIndex = cbbMiddle.SelectedIndex;
                _cartonIndex = cbbCarton.SelectedIndex;

                if (!chbOnlyMaster.Checked)
                {
                    try
                    {
                        this._middle_1Carton = Int32.Parse(txtMiddlePerCarton.Text.Trim());
                        this._unit_1Middle = Int32.Parse(txtUnitPerMiddle.Text.Trim());

                        this._unit_1Carton = this._middle_1Carton * this._unit_1Middle;
                        txtUnitPerCarton.Text = this._unit_1Carton.ToString();
                        timerFocus.Enabled = false;
                    }
                    catch
                    {
                        if (function.BOX_LOT_CODE == false && function.COUNT_SETUP == false)
                        {
                            MessageBox.Show(this, "Must write down \"Box Qty\" and \"Lot Qty\" Number!", "[Error]", MessageBoxButtons.OK);
                            return;
                        }
                    }
                }
                else
                {
                    txtUnitPerCarton.Text = "";
                    timerFocus.Enabled = true;
                }

                currentMiddleNumber = 0;
                sumOfLot = 0;
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

                //cbbMaster.DropDownStyle = ComboBoxStyle.Simple;

                //if (function.BOX_LOT_CODE == false)
                //{
                //    cbbMiddle.Enabled = true;
                //    cbbCarton.Enabled = true;
                //    cbbMaster.Enabled = true;
                //    cbbMiddle.DropDownStyle = ComboBoxStyle.Simple;
                //    cbbCarton.DropDownStyle = ComboBoxStyle.Simple;
                //    cbbMaster.DropDownStyle = ComboBoxStyle.Simple;
                //}
                //else
                //{
                //    cbbMiddle.Text = "";
                //    cbbCarton.Text = "";
                //    cbbMaster.Text = "";
                //    cbbMiddle.Enabled = false;
                //    cbbCarton.Enabled = false;
                //    cbbMaster.Enabled = false;
                //}

                btnStart.BackColor = Color.Tomato;
                btnStart.Text = "STOP";

                txtSkuScanData.Text = "";
                if (!chbSerial.Checked)
                    this.ActiveControl = txtSkuScanData;
                else
                    this.ActiveControl = txtSerialScanData;
            }
            else
            {
                _masterCode = "";
                _middleCode = "";
                _cartonCode = "";

                _masterIndex = 0;
                _middleIndex = 0;
                _cartonIndex = 0;

                currentMiddleNumber = 0;
                sumOfLot = 1;
                currentSeqQty = 0;

                txtUnitPerCarton.Text = "";
                txtMiddlePerCarton.Text = "";
                txtUnitPerMiddle.Text = "";
                txtUnitBox.Text = "";
                txtSerialScanData.Text = "";
                txtSkuScanData.Text = "";
                this.lblMiddleBoxNumber.Text = "0";

                cbbMaster.DropDownStyle = ComboBoxStyle.DropDown;
                cbbMiddle.DropDownStyle = ComboBoxStyle.DropDown;
                cbbCarton.DropDownStyle = ComboBoxStyle.DropDown;

                btnStart.BackColor = Color.LimeGreen;
                btnStart.Text = "START";

                txtUnitPerMiddle.Text = Properties.Settings.Default.Middle.ToString();
                txtMiddlePerCarton.Text = Properties.Settings.Default.Carton.ToString();

                if (!chbSerial.Checked)
                    this.ActiveControl = txtSkuScanData;
                else
                    this.ActiveControl = txtSerialScanData;
                timerFocus.Enabled = false;
            }
        }

        private void UserControl_Scanning_New_Load(object sender, EventArgs e)
        {
            InitializeControlAddedEvent();

            txtUnitPerMiddle.Text = Properties.Settings.Default.Middle.ToString();
            txtMiddlePerCarton.Text = Properties.Settings.Default.Carton.ToString();
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

                if (function.BOX_LOT_CODE == false)
                {
                    cbbMaster.Enabled = true;
                    cbbMaster.SelectedIndex = _masterIndex;
                    chbOnlyMaster.Checked = false;
                }
                else
                {
                    if (chbOnlyMaster.Checked)
                    {
                        cbbMaster.Enabled = true;
                        cbbMaster.SelectedIndex = _masterIndex;
                    }
                    else
                    {
                        cbbMaster.Enabled = false;
                        cbbMaster.Text = "";
                    }
                }
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
                    chbSelectMiddle.Checked = false;
                }
                else
                {

                    if (chbSelectMiddle.Checked)
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
                    chbSelectCarton.Checked = false;
                }
                else
                {
                    if (chbSelectCarton.Checked)
                    {
                        cbbCarton.Enabled = true;
                        cbbCarton.SelectedIndex = _middleIndex;
                    }
                    else
                    {
                        cbbCarton.Enabled = false;
                        cbbCarton.Text = "";
                    }
                }
            }
            catch
            {

            }

            if (function.BOX_LOT_CODE == false)
            {
                chbSerial.Checked = false;
            }
            else
            {
                chbSerial.Checked = true;
            }

        }

        private void txtSkuScanData_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.BeginInvoke(new Action(delegate ()
                {
                    if (!chbOnlyMaster.Checked)
                    {
                        txtSkuScanData.Text = scannedCode.ToString();
                        txtUnitBox.Text = scannedCode.ToString();
                        scannedCode.Clear();

                        if (function.BOX_LOT_CODE == false && function.COUNT_SETUP == false)
                        {
                            if (this.currentMiddleNumber >= this._middle_1Carton)
                            {
                                if (txtSkuScanData.Text.Trim() == _cartonCode)
                                {
                                    txtSerialScanData.Text = "Complete Carton";

                                    this.sumOfPassCount = 0;
                                    this.failCount = 0;
                                    this.sumOfLot++;
                                    this.currentMiddleNumber = 0;
                                    this.currentSeqQty = 0;

                                    txtSkuScanData.Text = "";
                                    _scanCartonCode = false;
                                    timerBlink.Enabled = false;
                                    this.cbbCarton.BackColor = Color.LightSkyBlue;
                                    this.ActiveControl = txtSkuScanData;
                                    return;
                                }
                                else
                                {
                                    errorAlarmForm = new ErrorAlarmForm(testResultInfo, false);
                                    errorAlarmForm.Show(this);
                                    txtSkuScanData.Text = "";
                                    this.ActiveControl = txtSkuScanData;
                                    txtSerialScanData.Text = "Scan Carton Code !";
                                    return;
                                }

                            }
                            if (this.passCount >= this._unit_1Middle)
                            {
                                // CHECK MIDDLE CODE
                                if (txtSkuScanData.Text.Trim() == _middleCode)
                                {
                                    passCount = 0;
                                    this.currentMiddleNumber++;
                                    this.sumOfLot++;
                                    txtSkuScanData.Text = "";
                                    txtSerialScanData.Text = "";
                                    this.lblMiddleBoxNumber.Text = this.currentMiddleNumber.ToString();
                                    _scanMiddleCode = false;
                                    timerBlink.Enabled = false;
                                    this.cbbMiddle.BackColor = Color.Khaki;
                                    if (this.currentMiddleNumber >= this._middle_1Carton)
                                    {
                                        this.lblMiddleBoxNumber.Text = (this._middle_1Carton).ToString();
                                        txtSerialScanData.Text = "Scan Carton Code !";
                                        this.ActiveControl = txtSkuScanData;
                                        _scanCartonCode = true;
                                        timerBlink.Enabled = true;
                                    }
                                    this.ActiveControl = txtSkuScanData;
                                    return;
                                }
                                else
                                {
                                    errorAlarmForm = new ErrorAlarmForm(testResultInfo, false);
                                    errorAlarmForm.Show(this);
                                    txtSerialScanData.Text = "Scan Middle Code !";
                                    txtSkuScanData.Text = "";
                                    this.ActiveControl = txtSkuScanData;
                                    return;
                                }

                            }
                        }

                        this.currentSeqQty++;
                        this.currentTestCount++;

                        testResultInfo.Time = DateTime.Now;
                        testResultInfo.Origin_Item = _masterCode;
                        testResultInfo.Compared_Item = txtSkuScanData.Text;
                        testResultInfo.Sample_1Box = this._unit_1Carton;
                        testResultInfo.Sample_Tray = this._unit_1Middle;
                        testResultInfo.TrayNumber = this.sumOfLot;
                        testResultInfo.TestCount = this.currentTestCount;

                        if (txtSkuScanData.Text.Trim() == _masterCode)
                        {
                            btnPassFail.Text = "OK";
                        }
                        else
                        {
                            testResultInfo.Result = false;
                            errorAlarmForm = new ErrorAlarmForm(testResultInfo, true);
                            errorAlarmForm.Show(this);
                            failCount++;
                            this.ActiveControl = txtSkuScanData;
                            btnPassFail.Text = "NG";
                        }
                    }
                    else
                    {
                        txtSkuScanData.Text = scannedCode.ToString();
                        txtUnitBox.Text = scannedCode.ToString();
                        scannedCode.Clear();

                        if (txtSkuScanData.Text.Trim() == _masterCode)
                        {
                            btnPassFail.Text = "OK";
                            testResultInfo.Result = true;
                            testResultInfo.Reason = "TEST OK";

                            this.currentSeqQty++;
                            this.currentTestCount++;
                            this.sumOfPassCount++;

                            delay.Start();
                            while (true)
                            {
                                if (delay.ElapsedMilliseconds > 200)
                                {
                                    delay.Stop();
                                    break;
                                }
                            }
                            this.btnPassFail.Text = "PASS (" + (this.sumOfPassCount).ToString() + ") / FAIL (" + (this.failCount).ToString() + ")";
                            this.lblTestCount.Text = this.currentTestCount.ToString();

                            testResultInfo.Time = DateTime.Now;
                            testResultInfo.Origin_Item = _masterCode;
                            testResultInfo.Compared_Item = txtSkuScanData.Text;
                            testResultInfo.Sample_1Box = this._unit_1Carton;
                            testResultInfo.Sample_Tray = this._unit_1Middle;

                            testResultInfo.TrayNumber = this.currentSeqQty; // cái này khác

                            testResultInfo.TestCount = this.currentTestCount;

                            try
                            {
                                using (conn = new SQLiteConnection(connStr))
                                {
                                    conn.Open();
                                    testResult_CRUD = new BarcodeTestResult_CRUD(conn);
                                    testResult_CRUD.Insert(testResultInfo);
                                }
                            }
                            catch (Exception ex)
                            { MessageBox.Show(ex.Message); }

                            this.txtSkuScanData.Text = "";
                            //this.ActiveControl = txtSkuScanData;
                            return;
                        }
                        else
                        {

                            failCount++;
                            currentTestCount++;
                            btnPassFail.Text = "NG";

                            delay.Start();
                            while (true)
                            {
                                if (delay.ElapsedMilliseconds > 200)
                                {
                                    delay.Stop();
                                    break;
                                }
                            }
                            testResultInfo.Time = DateTime.Now;
                            testResultInfo.Origin_Item = _masterCode;
                            testResultInfo.Compared_Item = txtSkuScanData.Text;
                            testResultInfo.Sample_1Box = this._unit_1Carton;
                            testResultInfo.Sample_Tray = this._unit_1Middle;
                            testResultInfo.TrayNumber = this.currentSeqQty;
                            testResultInfo.TestCount = this.currentTestCount;
                            testResultInfo.Result = false;

                            errorAlarmForm = new ErrorAlarmForm(testResultInfo, true);
                            errorAlarmForm.Show(this);

                            this.btnPassFail.Text = "PASS (" + (this.sumOfPassCount).ToString() + ") / FAIL (" + (this.failCount).ToString() + ")";
                            this.lblTestCount.Text = this.currentSeqQty.ToString();

                            this.txtSkuScanData.Text = "";
                            this.ActiveControl = txtSkuScanData;
                            return;
                        }
                    }
                }));

                this.txtSkuScanData.KeyUp -= this.txtSkuScanData_KeyUp;
                delay.Start();
                while (true)
                {
                    if (delay.ElapsedMilliseconds > 200)
                    {
                        delay.Stop();
                        break;
                    }
                }
                this.ActiveControl = txtSerialScanData;
                this.txtSkuScanData.KeyUp += this.txtSkuScanData_KeyUp;
            }
            else
            {
                scannedCode.Append(KeyToUni.KeyCodeToUnicode(e.KeyCode).ToUpper()); //Khong duoc bam phim nao tren ban phim, bam la se sai ngay
            }
        }
        /// <summary>
        /// Kiểm tra trạng thái của các checkbox
        /// </summary>
        void CheckSelection()
        {
            InitializeControlAddedEvent();
            if (!chbSelectMiddle.Checked)
            {
                chbSelectMiddle.Text = "SELECT";
                chbSelectMiddle.BackColor = Color.DarkGreen;
            }
            else
            {
                chbSelectMiddle.Text = "UNSELECT";
                chbSelectMiddle.BackColor = Color.Tomato;
            }
            if (!chbSelectCarton.Checked)
            {
                chbSelectCarton.Text = "SELECT";
                chbSelectCarton.BackColor = Color.DarkGreen;
            }
            else
            {
                chbSelectCarton.Text = "UNSELECT";
                chbSelectCarton.BackColor = Color.Tomato;
            }
            if(chbSelectMiddle.Checked && chbSelectCarton.Checked)
            {
                chbOnlyMaster.Checked = false;
            }
            if (!chbOnlyMaster.Checked)
            {
                chbOnlyMaster.Text = "ONLY MASTER";
                chbOnlyMaster.BackColor = Color.DarkGreen;
                //cbbMiddle.Enabled = true;
                //cbbCarton.Enabled = true;
                //cbbMiddle.DropDownStyle = ComboBoxStyle.Simple;
                //cbbCarton.DropDownStyle = ComboBoxStyle.Simple;
                txtSerialScanData.Text = "";
                txtSerialScanData.Enabled = true;
                txtUnitPerMiddle.Text = Properties.Settings.Default.Middle.ToString();
                txtMiddlePerCarton.Text = Properties.Settings.Default.Carton.ToString();
                txtUnitPerMiddle.Enabled = true;
                txtMiddlePerCarton.Enabled = true;
            }
            else
            {
                chbOnlyMaster.Text = "UNSELECT";
                chbOnlyMaster.BackColor = Color.Tomato;
                //cbbMiddle.Text = "";
                //cbbCarton.Text = "";
                //cbbMiddle.Enabled = false;
                //cbbCarton.Enabled = false;
                txtSerialScanData.Text = "";
                txtSerialScanData.Enabled = false;
                txtUnitPerMiddle.Text = "";
                txtMiddlePerCarton.Text = "";
                txtUnitPerMiddle.Enabled = false;
                txtMiddlePerCarton.Enabled = false;
            }
        }
        private void txtSerialScanData_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                txtSerialScanData.Text = scannedCode.ToString();
                scannedCode.Clear();

                if (txtSerialScanData.Text != "")
                {
                    if (txtSerialScanData.Text != txtSkuScanData.Text)
                    {
                        this.BeginInvoke(new Action(delegate ()
                        {
                            if (!chbSerial.Checked)
                            {
                                sumOfPassCount++;
                                passCount++;

                                testResultInfo.Time = DateTime.Now;
                                testResultInfo.Result = true;
                                testResultInfo.Reason = "TEST OK";
                                testResultInfo.Compared_Item += ";" + txtSerialScanData.Text.Trim();

                                this.btnPassFail.Text = "PASS (" + (this.sumOfPassCount).ToString() + ") / FAIL (" + (this.failCount).ToString() + ")";
                                this.lblTestCount.Text = this.currentSeqQty.ToString();
                                try
                                {
                                    using (conn = new SQLiteConnection(connStr))
                                    {
                                        conn.Open();
                                        testResult_CRUD = new BarcodeTestResult_CRUD(conn);
                                        testResult_CRUD.Insert(testResultInfo);
                                    }
                                }
                                catch (Exception ex)
                                { MessageBox.Show(ex.Message); }

                                if (function.BOX_LOT_CODE == false && function.COUNT_SETUP == false)
                                {
                                    if (this.passCount >= this._unit_1Middle)
                                    {
                                        txtSerialScanData.Text = "Scan Middle Code !";
                                        txtSkuScanData.Text = "";
                                        _scanMiddleCode = true;
                                        timerBlink.Enabled = true;
                                        this.ActiveControl = txtSkuScanData;
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                btnPassFail.Text = "OK";
                                sumOfPassCount++;
                                passCount++;

                                this.currentSeqQty++;
                                this.currentTestCount++;

                                testResultInfo.Time = DateTime.Now;
                                testResultInfo.Origin_Item = txtSerialScanData.Text.Trim();
                                testResultInfo.Compared_Item = txtSerialScanData.Text.Trim();
                                testResultInfo.Sample_1Box = this._unit_1Carton;
                                testResultInfo.Sample_Tray = this._unit_1Middle;
                                testResultInfo.TrayNumber = this.sumOfLot;
                                testResultInfo.TestCount = this.currentTestCount;
                                testResultInfo.Result = true;
                                testResultInfo.Reason = "TEST OK";

                                this.btnPassFail.Text = "PASS (" + (this.sumOfPassCount).ToString() + ") / FAIL (" + (this.failCount).ToString() + ")";
                                this.lblTestCount.Text = this.currentSeqQty.ToString();

                                try
                                {
                                    using (conn = new SQLiteConnection(connStr))
                                    {
                                        conn.Open();
                                        testResult_CRUD = new BarcodeTestResult_CRUD(conn);
                                        testResult_CRUD.Insert(testResultInfo);
                                    }
                                }
                                catch (Exception ex)
                                { MessageBox.Show(ex.Message); }


                                if (function.BOX_LOT_CODE == true && chbSelectMiddle.Checked && chbSelectCarton.Checked)
                                {
                                    if (this.currentMiddleNumber >= this._middle_1Carton)
                                    {
                                        if (txtSerialScanData.Text.Trim() == _cartonCode)
                                        {
                                            txtSkuScanData.Text = "Complete Carton";

                                            this.sumOfPassCount = 0;
                                            this.failCount = 0;
                                            this.sumOfLot++;
                                            this.currentMiddleNumber = 0;
                                            this.currentSeqQty = 0;
                                            this.passCount = 0;

                                            //this.btnPassFail.Text = "PASS (" + (this.sumOfPassCount).ToString() + ") / FAIL (" + (this.failCount).ToString() + ")";
                                            //this.lblMiddleBoxNumber.Text = this.currentMiddleNumber.ToString();
                                            txtSerialScanData.Text = "";

                                            _scanCartonCode = false;
                                            timerBlink.Enabled = false;
                                            this.cbbCarton.BackColor = Color.LightSkyBlue;

                                            this.ActiveControl = txtSerialScanData;
                                            //this.lblTestCount.Text = this.currentSeqQty.ToString();
                                            return;
                                        }
                                        else
                                        {
                                            //Phoung want
                                            errorAlarmForm = new ErrorAlarmForm(testResultInfo, false);
                                            errorAlarmForm.Show(this);
                                            txtSkuScanData.Text = "Scan Carton Code !";
                                            txtSerialScanData.Clear();
                                            this.ActiveControl = txtSerialScanData;
                                            return;
                                        }

                                    }
                                    if (this.passCount >= this._unit_1Middle) // lot + 1
                                    {
                                        // CHECK MIDDLE CODE
                                        if (this.passCount == this._unit_1Middle)
                                        {
                                            txtSkuScanData.Text = "Scan Middle Code !";
                                            txtSerialScanData.Clear();
                                            _scanMiddleCode = true;
                                            timerBlink.Enabled = true;
                                            this.ActiveControl = txtSerialScanData;
                                            return;
                                        }

                                        if (txtSerialScanData.Text.Trim() == _middleCode && passCount > _unit_1Middle)
                                        {
                                            passCount = 0;
                                            this.currentMiddleNumber++;
                                            this.sumOfLot++;
                                            txtSerialScanData.Text = "";
                                            txtSkuScanData.Text = "";
                                            this.lblMiddleBoxNumber.Text = this.currentMiddleNumber.ToString();

                                            _scanMiddleCode = false;
                                            timerBlink.Enabled = false;
                                            this.cbbMiddle.BackColor = Color.Khaki;

                                            if (this.currentMiddleNumber >= this._middle_1Carton)
                                            {
                                                //this.sumOfLot--;
                                                this.lblMiddleBoxNumber.Text = (this._middle_1Carton).ToString();
                                                txtSkuScanData.Text = "Scan Carton Code !";
                                                this.ActiveControl = txtSerialScanData;
                                                _scanCartonCode = true;
                                                timerBlink.Enabled = true;
                                            }
                                            this.ActiveControl = txtSerialScanData;
                                            return;
                                        }
                                        else
                                        {
                                            //Phoung want
                                            errorAlarmForm = new ErrorAlarmForm(testResultInfo, false);
                                            errorAlarmForm.Show(this);
                                            txtSkuScanData.Text = "Scan Middle Code !";
                                            txtSerialScanData.Text = "";

                                            this.ActiveControl = txtSerialScanData;
                                            return;
                                        }
                                    }
                                }
                            }

                            txtSkuScanData.Text = "";
                            txtSerialScanData.Text = "";

                        }));
                    }
                    else
                    {
                        this.BeginInvoke(new Action(delegate ()
                        {
                            errorAlarmForm = new ErrorAlarmForm(testResultInfo, false);
                            errorAlarmForm.Show(this);
                            txtSerialScanData.Text = "";
                            failCount++;
                            this.btnPassFail.Text = "PASS (" + (this.sumOfPassCount).ToString() + ") / FAIL (" + (this.failCount).ToString() + ")";
                            this.lblTestCount.Text = this.currentSeqQty.ToString();
                            this.ActiveControl = txtSerialScanData;
                        }));
                    }

                }
                //this.txtSerialScanData.KeyUp -= this.txtSerialScanData_KeyUp;

                delay.Start();
                while (true)
                {
                    if (delay.ElapsedMilliseconds > 200)
                    {
                        delay.Stop();
                        break;
                    }
                }

                if (!chbSerial.Checked)
                    this.ActiveControl = txtSkuScanData;
                else
                    this.ActiveControl = txtSerialScanData;
                //this.txtSerialScanData.KeyUp += this.txtSerialScanData_KeyUp;
            }
            else
            {
                scannedCode.Append(KeyToUni.KeyCodeToUnicode(e.KeyCode).ToUpper()); //Khong duoc bam phim nao tren ban phim
            }
        }

        private void chbSelectMiddle_CheckedChanged(object sender, EventArgs e)
        {
            CheckSelection();
        }

        private void chbSelectCarton_CheckedChanged(object sender, EventArgs e)
        {
            CheckSelection();
        }
        private void chbOnlyMaster_CheckedChanged(object sender, EventArgs e)
        {
            CheckSelection();
        }
        private void timerBlink_Tick(object sender, EventArgs e)
        {
            if (_scanMiddleCode)
            {
                if (_iFlag != true)
                {
                    cbbMiddle.BackColor = Color.Khaki;
                    _iFlag = !_iFlag;
                }
                else
                {
                    cbbMiddle.BackColor = Color.MediumVioletRed;
                    _iFlag = !_iFlag;
                }
            }
            if (_scanCartonCode)
            {
                if (_iFlag != true)
                {
                    cbbCarton.BackColor = Color.MediumVioletRed;
                    _iFlag = !_iFlag;
                }
                else
                {
                    cbbCarton.BackColor = Color.LightSkyBlue;
                    _iFlag = !_iFlag;
                }
            }
        }

        private void timerFocus_Tick(object sender, EventArgs e)
        {
            this.ActiveControl = txtSkuScanData;
        }

        private void txtSkuScanData_KeyDown(object sender, KeyEventArgs e)
        {
            //txtSkuScanData.Text = "";
        }
    }
}
