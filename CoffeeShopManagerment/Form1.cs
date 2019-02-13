using CoffeeShopManagerment_BUS;
using CoffeeShopManagerment_DAL.DTO_DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeShopManagerment
{
    

    public partial class Form1 : Form
    {
        int indexRow;
        coffeemanagerDBEntities db = new coffeemanagerDBEntities();
        DateTime todayDate = DateTime.Today;
        ToolTip ToolTip = new ToolTip();
        BUS bus = new BUS();
        float[] hourExtra;
        //this.dtpStartingDate.Value = twoWeeksAgo;
        public Form1()
        {
            InitializeComponent();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            clear.Enabled = false;
            ToolTip.InitialDelay = 0;
            ToolTip.ReshowDelay = 0;
            ToolTip.AutoPopDelay = 0;
            ToolTip.ForeColor = Color.Black;    
           
            
            timethisWeek(todayDate);
            loadDatagridView();
            gender.Items.Add("Nam");
            gender.Items.Add("Nữ");
            position.Items.Add("waiter");
            position.Items.Add("bartender");
            position.Items.Add("security");
        }
        public void loadDatagridView()
        {
            DateTime nn; 
            var data = (from d in db.Staffs select d);
            dataGridView.DataSource = data.ToList();
            dataGridView1.DataSource = data.ToList();
            this.dataGridView.Columns["Schedules"].Visible = false;
            this.dataGridView1.Columns["birthday"].Visible = false;
            this.dataGridView1.Columns["gender"].Visible = false;
            this.dataGridView1.Columns["position"].Visible = false;
            this.dataGridView1.Columns["hoursofMonth"].Visible = false;
            this.dataGridView1.Columns["coefficientPay"].Visible = false;
            this.dataGridView1.Columns["numberphone"].Visible = false;
            this.dataGridView1.Columns["Schedules"].Visible = false;
           
            var staffs = db.Staffs.ToList();
            bool check = bus.checkWorkedDate(dtpStartingDate.Value);
            //bool check =  db.Schedules.Any(o => o.dateID == dtpStartingDate.Value ) ;
            //bool check1 = db.Schedules.Any(o => o.staffID == staffs[1].staffID );

            if (check == false)
            {
                MessageBox.Show("Tuần này chưa được xếp lịch làm !!");
                for (int i = 0; i < staffs.Count; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        
                        Schedule ch = new Schedule();
                        ch.staffID = staffs[i].staffID;
                        if (j == 0)
                        {
                            ch.dateID = dtpStartingDate.Value;
                        }
                        else
                        if (j == 1)
                        {
                            ch.dateID = dtpStartingDate.Value.AddDays(1);
                        }
                        else
                        if (j == 2)
                        {
                            ch.dateID = dtpStartingDate.Value.AddDays(2);
                        }
                        else
                        if (j == 3)
                        {
                            ch.dateID = dtpStartingDate.Value.AddDays(3);
                        }
                        else
                        if (j == 4)
                        {
                            ch.dateID = dtpStartingDate.Value.AddDays(4);
                        }
                        else
                        if (j == 5)
                        {
                            ch.dateID = dtpStartingDate.Value.AddDays(5);
                        }
                        else
                        if (j == 6)
                        {
                            ch.dateID = dtpStartingDate.Value.AddDays(6);
                        }
                        dataGridView1.Rows[i].Cells[j].Value = "OFF";
                        
                        ch.shifts = "OFF";
                        ch.hoursPerShifts = 0;

                        db.Schedules.Add(ch);
                        
                        db.SaveChanges();
                       
                    }
                   
                }
            }
            else
            {
                
                for (int i = 0; i < staffs.Count; i++)
                {
                    for (int j = 0; j < 7; j++)

                    {
                        if(j == 0)
                        
                           nn = dtpStartingDate.Value;
                        
                        else
                        
                            nn = dtpStartingDate.Value.AddDays(j);
                        
                      
                       
                        int t = staffs[i].staffID;
                        int? h = bus.checkRow2(t, nn);
                        //int? h = db.Schedules.Where(x => x.staffID == t && x.dateID == nn).Select(o => o.scheduleID).FirstOrDefault();
                        var ch = db.Schedules.Find(h);
                        if (ch != null)
                        {

                          
                            // var ch = db.Schedules.Find(h);
                            //string l = ch.shifts.ToString();
                            dataGridView1.Rows[i].Cells[j].Value = ch.shifts;
                            //MessageBox.Show(ch.shifts.ToString());
                            //MessageBox.Show(dataGridView1.Rows[i].Cells[j].Value.ToString());



                        }
                        else
                        {
                            Schedule ch1 = new Schedule();
                            ch1.staffID = staffs[i].staffID;
                            if (j == 0)
                            {
                                ch1.dateID = dtpStartingDate.Value;
                            }
                            else
                            if (j == 1)
                            {
                                ch1.dateID = dtpStartingDate.Value.AddDays(1);
                            }
                            else
                            if (j == 2)
                            {
                                ch1.dateID = dtpStartingDate.Value.AddDays(2);
                            }
                            else
                            if (j == 3)
                            {
                                ch1.dateID = dtpStartingDate.Value.AddDays(3);
                            }
                            else
                            if (j == 4)
                            {
                                ch1.dateID = dtpStartingDate.Value.AddDays(4);
                            }
                            else
                            if (j == 5)
                            {
                                ch1.dateID = dtpStartingDate.Value.AddDays(5);
                            }
                            else
                            if (j == 6)
                            {
                                ch1.dateID = dtpStartingDate.Value.AddDays(6);
                            }
                            dataGridView1.Rows[i].Cells[j].Value = "OFF";
                           // dataGridView1.Rows[i].Cells[7].Value = "0";
                            ch1.shifts = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            ch1.hoursPerShifts = 0;



                            db.Schedules.Add(ch1);
                            db.SaveChanges();
                        }
                    }
                }
                

                
            }

            listShift();
        }
        public void countShifts()
        {
            int t2sa = 0, t3sa = 0, t4sa = 0, t5sa = 0, t6sa = 0, t7sa = 0, cnsa = 0, t2ch = 0, t3ch = 0, t4ch = 0, t5ch = 0, t6ch = 0, t7ch = 0, cnch = 0, t2to = 0, t3to = 0, t4to = 0, t5to = 0, t6to = 0, t7to = 0, cnto = 0;
            
           
            string t2sa_text = "";
            string t2ch_text = "";
            string t2to_text = "";
            string t3sa_text = "";
            string t3ch_text = "";
            string t3to_text = "";
            string t4sa_text = "";
            string t4ch_text = "";
            string t4to_text = "";
            string t5sa_text = "";
            string t5ch_text = "";
            string t5to_text = "";
            string t6sa_text = "";
            string t6ch_text = "";
            string t6to_text = "";
            string t7sa_text = "";
            string t7ch_text = "";
            string t7to_text = "";
            string cnsa_text = "";
            string cnch_text = "";
            string cnto_text = "";

            var staffs = db.Staffs.ToList();

            //chon nv trong ngay t2
            int saSecu = 0, saWait = 0, saBar = 0, chSecu = 0, chWait = 0, chBar = 0, toSecu = 0, toWait = 0, toBar = 0;
            int vtsaSecu = 0, vtsaWait = 0, vtsaBar = 0, vtchSecu = 0, vtchWait = 0, vtchBar = 0, vttoSecu = 0, vttoWait = 0, vttoBar = 0;
            string saSecutxt = "", saWaittxt = "", saBartxt = "", chSecutxt = "", chWaittxt = "", chBartxt = "", toSecutxt = "", toWaittxt = "", toBartxt = "";

            double hoursaSecu = 0, hoursaWait = 0, hoursaBar = 0, hourchSecu = 0, hourchWait = 0, hourchBar = 0, hourtoSecu = 0, hourtoWait = 0, hourtoBar = 0;
            int x = 0;
            string t2sa_textHide = "";
            string t2ch_textHide = ""; 
            string t2to_textHide = "";

           
            int[] manyShifts = new int[staffs.Count];

            //Tim so ca ma nhan vien dang ki trong tuan
            for (int i = 0; i < staffs.Count; i++)
                manyShifts[i] = 0;
            for(int i=0; i<staffs.Count; i++)
            {
                for(int j=0; j<7; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains("Sang") || dataGridView1.Rows[i].Cells[j].Value.ToString().Contains("Chieu") || dataGridView1.Rows[i].Cells[j].Value.ToString().Contains("Toi"))
                        manyShifts[i] += 1;
                    if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains("SangChieu") || dataGridView1.Rows[i].Cells[j].Value.ToString().Contains("ChieuToi") || dataGridView1.Rows[i].Cells[j].Value.ToString().Contains("SangToi"))
                        manyShifts[i] += 2;
                }
            }

            for (int j = 0; j < 7; j++)
            {
                t2sa = 0; t2ch = 0; t2to = 0;
                saSecu = 0; saWait = 0; saBar = 0; chSecu = 0; chWait = 0; chBar = 0; toSecu = 0; toWait = 0; toBar = 0;
                vtsaSecu = 0; vtsaWait = 0; vtsaBar = 0; vtchSecu = 0; vtchWait = 0; vtchBar = 0; vttoSecu = 0; vttoWait = 0; vttoBar = 0;
                saSecutxt = ""; saWaittxt = ""; saBartxt = ""; chSecutxt = ""; chWaittxt = ""; chBartxt = ""; toSecutxt = ""; toWaittxt = ""; toBartxt = "";
                hoursaSecu = 0; hoursaWait = 0; hoursaBar = 0; hourchSecu = 0; hourchWait = 0; hourchBar = 0; hourtoSecu = 0; hourtoWait = 0; hourtoBar = 0;
                //Chon nhan vien dang ki dau tien
                for (x = 0; x < staffs.Count; x++)
                {
                    //Chon nhan vien dang ki sang
                    if (dataGridView1.Rows[x].Cells[j].Value.ToString().Contains("Sang"))
                    {
                        if (staffs[x].position.ToString().Contains("security") && saSecu == 0)
                        {
                            vtsaSecu = x;
                            saSecutxt = staffs[x].staffName.ToString();
                            t2sa++;
                            saSecu++;
                            hoursaSecu =(double) staffs[x].hoursofMonth + 5;
                        }
                        if (staffs[x].position.ToString().Contains("waiter") && saWait == 0)
                        {
                            vtsaWait = x;
                            saWaittxt = staffs[x].staffName.ToString();
                            t2sa++;
                            saWait++;
                            hoursaWait = (double) staffs[x].hoursofMonth + 5;
                        }
                        if (staffs[x].position.ToString().Contains("bartender") && saBar == 0)
                        {
                            vtsaBar = x;
                            saBartxt = staffs[x].staffName.ToString();
                            t2sa++;
                            saBar++;
                            hoursaBar = (double) staffs[x].hoursofMonth + 5;
                        }
                    }
                    //Chon nhan vien dang ki chieu
                    if (dataGridView1.Rows[x].Cells[j].Value.ToString().Contains("Chieu"))
                    {
                        if (staffs[x].position.ToString().Contains("security") && chSecu == 0)
                        {
                            vtchSecu = x;
                            chSecutxt = staffs[x].staffName.ToString();
                            t2ch++;
                            chSecu++;
                            hourchSecu = (double) staffs[x].hoursofMonth + 5;
                        }
                        if (staffs[x].position.ToString().Contains("waiter") && chWait == 0)
                        {
                            vtchWait = x;
                            chWaittxt = staffs[x].staffName.ToString();
                            t2ch++;
                            chWait++;
                            hourchWait = (double) staffs[x].hoursofMonth + 5;
                        }
                        if (staffs[x].position.ToString().Contains("bartender") && chBar == 0)
                        {
                            vtchBar = x;
                            chBartxt = staffs[x].staffName.ToString();
                            t2ch++;
                            chBar++;
                            hourchBar = (double) staffs[x].hoursofMonth + 5;
                        }

                    }
                    //Chon nhan vien dang ki toi
                    if (dataGridView1.Rows[x].Cells[j].Value.ToString().Contains("Toi"))
                    {
                        if (staffs[x].position.ToString().Contains("security") && toSecu == 0)
                        {
                            vttoSecu = x;
                            toSecutxt = staffs[x].staffName.ToString();
                            t2to++;
                            toSecu++;
                            hourtoSecu = (double) staffs[x].hoursofMonth + 6;
                        }
                        if (staffs[x].position.ToString().Contains("waiter") && toWait == 0)
                        {
                            vttoWait = x;
                            toWaittxt = staffs[x].staffName.ToString();
                            t2to++;
                            toWait++;
                            hourtoWait = (double) staffs[x].hoursofMonth + 6;
                        }
                        if (staffs[x].position.ToString().Contains("bartender") && toBar == 0)
                        {
                            vttoBar = x;
                            toBartxt = staffs[x].staffName.ToString();
                            t2to++;
                            toBar++;
                            hourtoBar = (double) staffs[x].hoursofMonth + 6;
                        }

                    }
                    //Chon nhan vien dang ki Sang Chieu
                    if (dataGridView1.Rows[x].Cells[j].Value.ToString().Contains("SangChieu"))
                    {
                        if (staffs[x].position.ToString().Contains("security") && saSecu == 0)
                        {
                            vtsaSecu = x;
                            saSecutxt = staffs[x].staffName.ToString();
                            t2sa++;
                            saSecu++;
                            hoursaSecu = (double)staffs[x].hoursofMonth + 5;
                        }
                        if (staffs[x].position.ToString().Contains("waiter") && saWait == 0)
                        {
                            vtsaWait = x;
                            saWaittxt = staffs[x].staffName.ToString();
                            t2sa++;
                            saWait++;
                            hoursaWait = (double)staffs[x].hoursofMonth + 5;
                        }
                        if (staffs[x].position.ToString().Contains("bartender") && saBar == 0)
                        {
                            vtsaBar = x;
                            saBartxt = staffs[x].staffName.ToString();
                            t2sa++;
                            saBar++;
                            hoursaBar = (double)staffs[x].hoursofMonth + 5;
                        }
                        if (staffs[x].position.ToString().Contains("security") && chSecu == 0)
                        {
                            vtchSecu = x;
                            chSecutxt = staffs[x].staffName.ToString();
                            t2ch++;
                            chSecu++;
                            hourchSecu = (double)staffs[x].hoursofMonth + 5;
                        }
                        if (staffs[x].position.ToString().Contains("waiter") && chWait == 0)
                        {
                            vtchWait = x;
                            chWaittxt = staffs[x].staffName.ToString();
                            t2ch++;
                            chWait++;
                            hourchWait = (double)staffs[x].hoursofMonth + 5;
                        }
                        if (staffs[x].position.ToString().Contains("bartender") && chBar == 0)
                        {
                            vtchBar = x;
                            chBartxt = staffs[x].staffName.ToString();
                            t2ch++;
                            chBar++;
                            hourchBar = (double)staffs[x].hoursofMonth + 5;
                        }
                    }
                    //Chon nhan vien dang ki Sang Toi
                    if (dataGridView1.Rows[x].Cells[j].Value.ToString().Contains("SangToi"))
                    {
                        if (staffs[x].position.ToString().Contains("security") && saSecu == 0)
                        {
                            vtsaSecu = x;
                            saSecutxt = staffs[x].staffName.ToString();
                            t2sa++;
                            saSecu++;
                            hoursaSecu = (double)staffs[x].hoursofMonth + 5;
                        }
                        if (staffs[x].position.ToString().Contains("waiter") && saWait == 0)
                        {
                            vtsaWait = x;
                            saWaittxt = staffs[x].staffName.ToString();
                            t2sa++;
                            saWait++;
                            hoursaWait = (double)staffs[x].hoursofMonth + 5;
                        }
                        if (staffs[x].position.ToString().Contains("bartender") && saBar == 0)
                        {
                            vtsaBar = x;
                            saBartxt = staffs[x].staffName.ToString();
                            t2sa++;
                            saBar++;
                            hoursaBar = (double)staffs[x].hoursofMonth + 5;
                        }
                        if (staffs[x].position.ToString().Contains("security") && toSecu == 0)
                        {
                            vttoSecu = x;
                            toSecutxt = staffs[x].staffName.ToString();
                            t2to++;
                            toSecu++;
                            hourtoSecu = (double)staffs[x].hoursofMonth + 6;
                        }
                        if (staffs[x].position.ToString().Contains("waiter") && toWait == 0)
                        {
                            vttoWait = x;
                            toWaittxt = staffs[x].staffName.ToString();
                            t2to++;
                            toWait++;
                            hourtoWait = (double)staffs[x].hoursofMonth + 6;
                        }
                        if (staffs[x].position.ToString().Contains("bartender") && toBar == 0)
                        {
                            vttoBar = x;
                            toBartxt = staffs[x].staffName.ToString();
                            t2to++;
                            toBar++;
                            hourtoBar = (double)staffs[x].hoursofMonth + 6;
                        }
                    }
                    //Chon nhan vien dang ki Chieu Toi
                    if (dataGridView1.Rows[x].Cells[j].Value.ToString().Contains("ChieuToi"))
                    {
                        if (staffs[x].position.ToString().Contains("security") && chSecu == 0)
                        {
                            vtchSecu = x;
                            chSecutxt = staffs[x].staffName.ToString();
                            t2ch++;
                            chSecu++;
                            hourchSecu = (double)staffs[x].hoursofMonth + 5;
                        }
                        if (staffs[x].position.ToString().Contains("waiter") && chWait == 0)
                        {
                            vtchWait = x;
                            chWaittxt = staffs[x].staffName.ToString();
                            t2ch++;
                            chWait++;
                            hourchWait = (double)staffs[x].hoursofMonth + 5;
                        }
                        if (staffs[x].position.ToString().Contains("bartender") && chBar == 0)
                        {
                            vtchBar = x;
                            chBartxt = staffs[x].staffName.ToString();
                            t2ch++;
                            chBar++;
                            hourchBar = (double)staffs[x].hoursofMonth + 5;
                        }
                        if (staffs[x].position.ToString().Contains("security") && toSecu == 0)
                        {
                            vttoSecu = x;
                            toSecutxt = staffs[x].staffName.ToString();
                            t2to++;
                            toSecu++;
                            hourtoSecu = (double)staffs[x].hoursofMonth + 6;
                        }
                        if (staffs[x].position.ToString().Contains("waiter") && toWait == 0)
                        {
                            vttoWait = x;
                            toWaittxt = staffs[x].staffName.ToString();
                            t2to++;
                            toWait++;
                            hourtoWait = (double)staffs[x].hoursofMonth + 6;
                        }
                        if (staffs[x].position.ToString().Contains("bartender") && toBar == 0)
                        {
                            vttoBar = x;
                            toBartxt = staffs[x].staffName.ToString();
                            t2to++;
                            toBar++;
                            hourtoBar = (double)staffs[x].hoursofMonth + 6;
                        }
                    }
                }
                //Xu li khi co nhieu nhan vien dang ki chung ca
                for (int i = 0; i < staffs.Count; i++)
                {
                    //Xu li nhieu nhan vien dang ki buoi sang
                    if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains("Sang"))
                    {
                        if (staffs[i].position.ToString().Contains("security"))
                        {
                            if (manyShifts[i] < manyShifts[vtsaSecu])
                            {
                                saSecu++;
                                vtsaSecu = i;
                                saSecutxt = staffs[i].staffName.ToString();
                                manyShifts[i] += 1;
                            }
                        }
                        if (staffs[i].position.ToString().Contains("waiter"))
                        {
                            if (manyShifts[i] < manyShifts[vtsaWait])
                            {
                                saWait++;
                                vtsaWait = i;
                                saWaittxt = staffs[i].staffName.ToString();
                                manyShifts[i] += 1;
                            }
                        }
                        if (staffs[i].position.ToString().Contains("bartender"))
                        {
                            if (manyShifts[i] < manyShifts[vtsaBar])
                            {
                                saBar++;
                                vtsaBar = i;
                                saBartxt = staffs[i].staffName.ToString();
                                manyShifts[i] += 1;
                            }
                        }
                    }
                    //Xu li nhieu nhan vien dang ki buoi chieu
                    if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains("Chieu"))
                    {
                        if (staffs[i].position.ToString().Contains("security"))
                        {
                            if (manyShifts[i] < manyShifts[vtchSecu])
                            {
                                chSecu++;
                                vtchSecu = i;
                                chSecutxt = staffs[i].staffName.ToString();
                                manyShifts[i] += 1;
                            }
                        }
                        if (staffs[i].position.ToString().Contains("waiter"))
                        {
                            if (manyShifts[i] < manyShifts[vtchWait])
                            {
                                chWait++;
                                vtchWait = i;
                                chWaittxt = staffs[i].staffName.ToString();
                                manyShifts[i] += 1;
                            }
                        }
                        if (staffs[i].position.ToString().Contains("bartender"))
                        {
                            if (manyShifts[i] < manyShifts[vtchBar])
                            {
                                chBar++;
                                vtchBar = i;
                                chBartxt = staffs[i].staffName.ToString();
                                manyShifts[i] += 1;
                            }
                        }
                    }
                    //Xu li nhieu nhan vien dang ki buoi toi
                    if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains("Toi"))
                    {
                        if (staffs[i].position.ToString().Contains("security"))
                        {
                            if (manyShifts[i] < manyShifts[vttoSecu])
                            {
                                toSecu++;
                                vttoSecu = i;
                                toSecutxt = staffs[i].staffName.ToString();
                                manyShifts[i] += 1;
                            }
                        }
                        if (staffs[i].position.ToString().Contains("waiter"))
                        {
                            if (manyShifts[i] < manyShifts[vttoWait])
                            {
                                toWait++;
                                vttoWait = i;
                                toWaittxt = staffs[i].staffName.ToString();
                                manyShifts[i] += 1;
                            }
                        }
                        if (staffs[i].position.ToString().Contains("bartender"))
                        {
                            if (manyShifts[i] < manyShifts[vttoBar])
                            {
                                toBar++;
                                vttoBar = i;
                                toBartxt = staffs[i].staffName.ToString();
                                manyShifts[i] += 1;
                            }
                        }
                    }
                    //Xu li nhieu nhan vien dang ki Sang Chieu
                    if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains("SangChieu"))
                    {
                        if (staffs[i].position.ToString().Contains("security"))
                        {
                            if (manyShifts[i] < manyShifts[vtsaSecu])
                            {
                                saSecu++;
                                vtsaSecu = i;
                                saSecutxt = staffs[i].staffName.ToString();
                                manyShifts[i] += 1;
                            }
                        }
                        if (staffs[i].position.ToString().Contains("waiter"))
                        {
                            if (manyShifts[i] < manyShifts[vtsaWait])
                            {
                                saWait++;
                                vtsaWait = i;
                                saWaittxt = staffs[i].staffName.ToString();
                                manyShifts[i] += 1;
                            }
                        }
                        if (staffs[i].position.ToString().Contains("bartender"))
                        {
                            if (manyShifts[i] < manyShifts[vtsaBar])
                            {
                                saBar++;
                                vtsaBar = i;
                                saBartxt = staffs[i].staffName.ToString();
                                manyShifts[i] += 1;
                            }
                        }
                        if (staffs[i].position.ToString().Contains("security"))
                        {
                            if (manyShifts[i] < manyShifts[vtchSecu])
                            {
                                chSecu++;
                                vtchSecu = i;
                                chSecutxt = staffs[i].staffName.ToString();
                                manyShifts[i] += 1;
                            }
                        }
                        if (staffs[i].position.ToString().Contains("waiter"))
                        {
                            if (manyShifts[i] < manyShifts[vtchWait])
                            {
                                chWait++;
                                vtchWait = i;
                                chWaittxt = staffs[i].staffName.ToString();
                                manyShifts[i] += 1;
                            }
                        }
                        if (staffs[i].position.ToString().Contains("bartender"))
                        {
                            if (manyShifts[i] < manyShifts[vtchBar])
                            {
                                chBar++;
                                vtchBar = i;
                                chBartxt = staffs[i].staffName.ToString();
                                manyShifts[i] += 1;
                            }
                        }
                    }
                    //Xu li nhieu nhan vien dang ki Sang Toi
                    if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains("SangToi"))
                    {
                        if (staffs[i].position.ToString().Contains("security"))
                        {
                            if (manyShifts[i] < manyShifts[vtsaSecu])
                            {
                                saSecu++;
                                vtsaSecu = i;
                                saSecutxt = staffs[i].staffName.ToString();
                                manyShifts[i] += 1;
                            }
                        }
                        if (staffs[i].position.ToString().Contains("waiter"))
                        {
                            if (manyShifts[i] < manyShifts[vtsaWait])
                            {
                                saWait++;
                                vtsaWait = i;
                                saWaittxt = staffs[i].staffName.ToString();
                                manyShifts[i] += 1;
                            }
                        }
                        if (staffs[i].position.ToString().Contains("bartender"))
                        {
                            if (manyShifts[i] < manyShifts[vtsaBar])
                            {
                                saBar++;
                                vtsaBar = i;
                                saBartxt = staffs[i].staffName.ToString();
                                manyShifts[i] += 1;
                            }
                        }
                        if (staffs[i].position.ToString().Contains("security"))
                        {
                            if (manyShifts[i] < manyShifts[vttoSecu])
                            {
                                toSecu++;
                                vttoSecu = i;
                                toSecutxt = staffs[i].staffName.ToString();
                                manyShifts[i] += 1;
                            }
                        }
                        if (staffs[i].position.ToString().Contains("waiter"))
                        {
                            if (manyShifts[i] < manyShifts[vttoWait])
                            {
                                toWait++;
                                vttoWait = i;
                                toWaittxt = staffs[i].staffName.ToString();
                                manyShifts[i] += 1;
                            }
                        }
                        if (staffs[i].position.ToString().Contains("bartender"))
                        {
                            if (manyShifts[i] < manyShifts[vttoBar])
                            {
                                toBar++;
                                vttoBar = i;
                                toBartxt = staffs[i].staffName.ToString();
                                manyShifts[i] += 1;
                            }
                        }
                    }
                    //Xu li nhieu nhan vien dang ki Chieu Toi
                    if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains("ChieuToi"))
                    {
                        if (staffs[i].position.ToString().Contains("security"))
                        {
                            if (manyShifts[i] < manyShifts[vtchSecu])
                            {
                                chSecu++;
                                vtchSecu = i;
                                chSecutxt = staffs[i].staffName.ToString();
                                manyShifts[i] += 1;
                            }
                        }
                        if (staffs[i].position.ToString().Contains("waiter"))
                        {
                            if (manyShifts[i] < manyShifts[vtchWait])
                            {
                                chWait++;
                                vtchWait = i;
                                chWaittxt = staffs[i].staffName.ToString();
                                manyShifts[i] += 1;
                            }
                        }
                        if (staffs[i].position.ToString().Contains("bartender"))
                        {
                            if (manyShifts[i] < manyShifts[vtchBar])
                            {
                                chBar++;
                                vtchBar = i;
                                chBartxt = staffs[i].staffName.ToString();
                                manyShifts[i] += 1;
                            }
                        }
                        if (staffs[i].position.ToString().Contains("security"))
                        {
                            if (manyShifts[i] < manyShifts[vttoSecu])
                            {
                                toSecu++;
                                vttoSecu = i;
                                toSecutxt = staffs[i].staffName.ToString();
                                manyShifts[i] += 1;
                            }
                        }
                        if (staffs[i].position.ToString().Contains("waiter"))
                        {
                            if (manyShifts[i] < manyShifts[vttoWait])
                            {
                                toWait++;
                                vttoWait = i;
                                toWaittxt = staffs[i].staffName.ToString();
                                manyShifts[i] += 1;
                            }
                        }
                        if (staffs[i].position.ToString().Contains("bartender"))
                        {
                            if (manyShifts[i] < manyShifts[vttoBar])
                            {
                                toBar++;
                                vttoBar = i;
                                toBartxt = staffs[i].staffName.ToString();
                                manyShifts[i] += 1;
                            }
                        }
                    }
                }
                //Luu lai gio lam cua nhan vien duoc chon
                /*staffs[vtsaSecu].hoursofMonth += 5;
                staffs[vtsaWait].hoursofMonth += 5;
                staffs[vtsaBar].hoursofMonth += 5;
                staffs[vtchSecu].hoursofMonth += 5;
                staffs[vtchWait].hoursofMonth += 5;
                staffs[vtchBar].hoursofMonth += 5;
                staffs[vttoSecu].hoursofMonth += 6;
                staffs[vttoWait].hoursofMonth += 6;
                staffs[vttoBar].hoursofMonth += 6;*/

                //Luu lai thoi gian lam cua nhan vien theo tung ngay
                DateTime nn;
                for (int i = 0; i < staffs.Count; i++)
                {
                    if (j == 0)
                        nn = dtpStartingDate.Value;
                    else
                        nn = dtpStartingDate.Value.AddDays(j);
                    int t = staffs[i].staffID;
                    int h = bus.checkRow(t, nn);
                    var ch = db.Schedules.Find(h);
                    if (i == vtsaSecu || i == vtsaWait || i == vtsaBar || i == vtchSecu || i == vtchWait || i == vtchBar || i == vttoSecu || i == vttoWait || i == vttoBar)
                    {
                        string s = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        if (s.Trim() == "Sang" || s.Trim() == "Chieu")
                            ch.hoursPerShifts = 5;
                        if (s.Trim() == "Toi")
                            ch.hoursPerShifts = 6;
                        if (s.Trim() == "SangChieu")
                            ch.hoursPerShifts = 10;
                        if (s.Trim() == "SangToi" || s.Trim() == "ChieuToi")
                            ch.hoursPerShifts = 11;
                        ch.shifts = s;
                    }
                    else
                    {
                        ch.hoursPerShifts = 0;
                        ch.shifts = "OFF";
                    }
                    db.SaveChanges();
                }

                t2sa_text = saSecutxt + "\n" + saWaittxt + "\n" + saBartxt;
                t2sa_textHide = "Giữ Xe: " + saSecutxt + "\n" + "Phục Vụ: " + saWaittxt + "\n" +"Pha Chế: " + saBartxt;
                t2ch_text = chSecutxt + "\n" + chWaittxt + "\n" + chBartxt;
                t2ch_textHide = "Giữ Xe: " + chSecutxt + "\n" + "Phục Vụ: " + chWaittxt + "\n" + "Pha Chế: " + chBartxt;
                t2to_text = toSecutxt + "\n" + toWaittxt + "\n" + toBartxt;
                t2to_textHide = "Giữ Xe: " + toSecutxt + "\n" + "Phục Vụ: " + toWaittxt +  "\n" + "Pha Chế: " + toBartxt;
                if (j==0)
                {
                    ToolTip.SetToolTip(t2sa_tb, t2sa_textHide);
                    ToolTip.SetToolTip(t2ch_tb, t2ch_textHide);
                    ToolTip.SetToolTip(t2to_tb, t2to_textHide);
                    if (t2sa < 3)
                    {
                        t2sa_tb.BackColor = Color.Orange;
                        t2sa_tb.Text = t2sa_text;
                    }
                    else
                    {
                        t2sa_tb.BackColor = Color.Green;
                        t2sa_tb.Text = t2sa_text;
                    }
                    ////////////////////////////////////////////////////////////////////
                    if (t2ch < 3)
                    {
                        t2ch_tb.BackColor = Color.Orange;
                        t2ch_tb.Text = t2ch_text;

                    }
                    else
                    {
                        t2ch_tb.BackColor = Color.Green;
                        t2ch_tb.Text = t2ch_text;
                    }
                    /////////////////////////////////////////////////////////////////////
                    if (t2to < 3)
                    {
                        t2to_tb.BackColor = Color.Orange;
                        t2to_tb.Text = t2to_text;

                    }
                    else
                    {
                        t2to_tb.BackColor = Color.Green;
                        t2to_tb.Text = t2to_text;
                    }
                }

                if (j == 1)
                {
                    ToolTip.SetToolTip(t3sa_tb, t2sa_textHide);
                    ToolTip.SetToolTip(t3ch_tb, t2ch_textHide);
                    ToolTip.SetToolTip(t3to_tb, t2to_textHide);
                    if (t2sa < 3)
                    {
                        t3sa_tb.BackColor = Color.Orange;
                        t3sa_tb.Text = t2sa_text;
                    }
                    else
                    {
                        t3sa_tb.BackColor = Color.Green;
                        t3sa_tb.Text = t2sa_text;
                    }
                    ////////////////////////////////////////////////////////////////////
                    if (t2ch < 3)
                    {
                        t3ch_tb.BackColor = Color.Orange;
                        t3ch_tb.Text = t2ch_text;

                    }
                    else
                    {
                        t3ch_tb.BackColor = Color.Green;
                        t3ch_tb.Text = t2ch_text;
                    }
                    /////////////////////////////////////////////////////////////////////
                    if (t2to < 3)
                    {
                        t3to_tb.BackColor = Color.Orange;
                        t3to_tb.Text = t2to_text;

                    }
                    else
                    {
                        t3to_tb.BackColor = Color.Green;
                        t3to_tb.Text = t2to_text;
                    }
                }

                if (j == 2)
                {
                    ToolTip.SetToolTip(t4sa_tb, t2sa_textHide);
                    ToolTip.SetToolTip(t4ch_tb, t2ch_textHide);
                    ToolTip.SetToolTip(t4to_tb, t2to_textHide);
                    if (t2sa < 3)
                    {
                        t4sa_tb.BackColor = Color.Orange;
                        t4sa_tb.Text = t2sa_text;
                    }
                    else
                    {
                        t4sa_tb.BackColor = Color.Green;
                        t4sa_tb.Text = t2sa_text;
                    }
                    ////////////////////////////////////////////////////////////////////
                    if (t2ch < 3)
                    {
                        t4ch_tb.BackColor = Color.Orange;
                        t4ch_tb.Text = t2ch_text;

                    }
                    else
                    {
                        t4ch_tb.BackColor = Color.Green;
                        t4ch_tb.Text = t2ch_text;
                    }
                    /////////////////////////////////////////////////////////////////////
                    if (t2to < 3)
                    {
                        t4to_tb.BackColor = Color.Orange;
                        t4to_tb.Text = t2to_text;

                    }
                    else
                    {
                        t4to_tb.BackColor = Color.Green;
                        t4to_tb.Text = t2to_text;
                    }
                }

                if (j == 3)
                {
                    ToolTip.SetToolTip(t5sa_tb, t2sa_textHide);
                    ToolTip.SetToolTip(t5ch_tb, t2ch_textHide);
                    ToolTip.SetToolTip(t5to_tb, t2to_textHide);
                    if (t2sa < 3)
                    {
                        t5sa_tb.BackColor = Color.Orange;
                        t5sa_tb.Text = t2sa_text;
                    }
                    else
                    {
                        t5sa_tb.BackColor = Color.Green;
                        t5sa_tb.Text = t2sa_text;
                    }
                    ////////////////////////////////////////////////////////////////////
                    if (t2ch < 3)
                    {
                        t5ch_tb.BackColor = Color.Orange;
                        t5ch_tb.Text = t2ch_text;

                    }
                    else
                    {
                        t5ch_tb.BackColor = Color.Green;
                        t5ch_tb.Text = t2ch_text;
                    }
                    /////////////////////////////////////////////////////////////////////
                    if (t2to < 3)
                    {
                        t5to_tb.BackColor = Color.Orange;
                        t5to_tb.Text = t2to_text;

                    }
                    else
                    {
                        t5to_tb.BackColor = Color.Green;
                        t5to_tb.Text = t2to_text;
                    }
                }

                if (j == 4)
                {
                    ToolTip.SetToolTip(t6sa_tb, t2sa_textHide);
                    ToolTip.SetToolTip(t6ch_tb, t2ch_textHide);
                    ToolTip.SetToolTip(t6to_tb, t2to_textHide);
                    if (t2sa < 3)
                    {
                        t6sa_tb.BackColor = Color.Orange;
                        t6sa_tb.Text = t2sa_text;
                    }
                    else
                    {
                        t6sa_tb.BackColor = Color.Green;
                        t6sa_tb.Text = t2sa_text;
                    }
                    ////////////////////////////////////////////////////////////////////
                    if (t2ch < 3)
                    {
                        t6ch_tb.BackColor = Color.Orange;
                        t6ch_tb.Text = t2ch_text;

                    }
                    else
                    {
                        t6ch_tb.BackColor = Color.Green;
                        t6ch_tb.Text = t2ch_text;
                    }
                    /////////////////////////////////////////////////////////////////////
                    if (t2to < 3)
                    {
                        t6to_tb.BackColor = Color.Orange;
                        t6to_tb.Text = t2to_text;

                    }
                    else
                    {
                        t6to_tb.BackColor = Color.Green;
                        t6to_tb.Text = t2to_text;
                    }
                }

                if (j == 5)
                {
                    ToolTip.SetToolTip(t7sa_tb, t2sa_textHide);
                    ToolTip.SetToolTip(t7ch_tb, t2ch_textHide);
                    ToolTip.SetToolTip(t7to_tb, t2to_textHide);
                    if (t2sa < 3)
                    {
                        t7sa_tb.BackColor = Color.Orange;
                        t7sa_tb.Text = t2sa_text;
                    }
                    else
                    {
                        t7sa_tb.BackColor = Color.Green;
                        t7sa_tb.Text = t2sa_text;
                    }
                    ////////////////////////////////////////////////////////////////////
                    if (t2ch < 3)
                    {
                        t7ch_tb.BackColor = Color.Orange;
                        t7ch_tb.Text = t2ch_text;

                    }
                    else
                    {
                        t7ch_tb.BackColor = Color.Green;
                        t7ch_tb.Text = t2ch_text;
                    }
                    /////////////////////////////////////////////////////////////////////
                    if (t2to < 3)
                    {
                        t7to_tb.BackColor = Color.Orange;
                        t7to_tb.Text = t2to_text;

                    }
                    else
                    {
                        t7to_tb.BackColor = Color.Green;
                        t7to_tb.Text = t2to_text;
                    }
                }

                if (j == 6)
                {
                    ToolTip.SetToolTip(cnsa_tb, t2sa_textHide);
                    ToolTip.SetToolTip(cnch_tb, t2ch_textHide);
                    ToolTip.SetToolTip(cnto_tb, t2to_textHide);
                    if (t2sa < 3)
                    {
                        cnsa_tb.BackColor = Color.Orange;
                        cnsa_tb.Text = t2sa_text;
                    }
                    else
                    {
                        cnsa_tb.BackColor = Color.Green;
                        cnsa_tb.Text = t2sa_text;
                    }
                    ////////////////////////////////////////////////////////////////////
                    if (t2ch < 3)
                    {
                        cnch_tb.BackColor = Color.Orange;
                        cnch_tb.Text = t2ch_text;

                    }
                    else
                    {
                        cnch_tb.BackColor = Color.Green;
                        cnch_tb.Text = t2ch_text;
                    }
                    /////////////////////////////////////////////////////////////////////
                    if (t2to < 3)
                    {
                        cnto_tb.BackColor = Color.Orange;
                        cnto_tb.Text = t2to_text;

                    }
                    else
                    {
                        cnto_tb.BackColor = Color.Green;
                        cnto_tb.Text = t2to_text;
                    }
                }
            }
             
            
            // MessageBox.Show(t2sa.ToString());
        }
       

       





        private void btnAdd_Click_2(object sender, EventArgs e)
        {
            if(name.Text.Trim() == "" || gender.Text.Trim() == "" || position.Text.Trim() == "" || numberphone.Text.Trim() == "" || coefficientPay.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ dữ liệu trước khi THÊM !");
            }
            else
            {
                Staff s = new Staff();

                s.staffName = name.Text;
                s.birthday = birthday.Value;
                s.gender = gender.Text;
                s.position = position.Text;
                s.hoursofMonth = 0;
                s.numberphone = numberphone.Text;
                s.coefficientPay = Int32.Parse(coefficientPay.Text);
                db.Staffs.Add(s);

                db.SaveChanges();


                var data = (from d in db.Staffs select d);
                dataGridView.DataSource = data.ToList();
                MessageBox.Show("Thêm thành công");
            }
            
        }

        private void clear_Click_2(object sender, EventArgs e)
        {
            dataGridView.ClearSelection();
            id.Text = "";
            position.Text = "";
            gender.Text = "";
            shifts.ResetText();
            name.Text = "";
            numberphone.Text = "";
            coefficientPay.Text = "";
            birthday.Value = DateTime.Now;
            btnAdd.Enabled = true;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            var s = db.Staffs.Find(Int32.Parse(id.Text));
          
            s.staffName = name.Text;
            s.birthday = birthday.Value;
            
            if (gender.Text.Trim() != "Nam" && gender.Text.Trim() != "Nữ" )
            {
                MessageBox.Show("Nhập lại giới tính của nhân viên!");
            }
            else
            {
                s.gender = gender.Text;
            }
            if (position.Text.Trim() != "waiter" && position.Text.Trim() != "security" && position.Text.Trim()  != "bartender")
            {
                MessageBox.Show("Nhập lại chức vụ của nhân viên!");
            }
            else
            {
                s.position = position.Text;
            }
            
            s.numberphone = numberphone.Text;
            s.coefficientPay = Int32.Parse(coefficientPay.Text);

            db.SaveChanges();
            MessageBox.Show("Cập nhật thành công");
            var data = (from d in db.Staffs select d);
            dataGridView.DataSource = data.ToList();
        }

        private void btnDelete_Click_2(object sender, EventArgs e)
        {
            int ID = Int32.Parse(id.Text);
            var s = db.Staffs.Find(ID);
            db.Staffs.Remove(s);
            db.Schedules.RemoveRange(db.Schedules.Where(x => x.staffID == ID));
            
            db.SaveChanges();
            var data = (from d in db.Staffs select d);
            dataGridView.DataSource = data.ToList();
            MessageBox.Show("Xóa thành công");
        }

        private void dataGridView_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView.ReadOnly = true;
            indexRow = e.RowIndex;
            if(indexRow != -1)
            {
                DataGridViewRow row = dataGridView.Rows[indexRow];
                id.Text = row.Cells[0].Value.ToString();
                name.Text = row.Cells[1].Value.ToString();
                position.Text = row.Cells[4].Value.ToString();
                birthday.Value = Convert.ToDateTime(row.Cells[3].Value);
                gender.Text = row.Cells[2].Value.ToString();
                shifts.Text = row.Cells[5].Value.ToString();
                numberphone.Text = row.Cells[7].Value.ToString();
                coefficientPay.Text = row.Cells[6].Value.ToString();
                btnAdd.Enabled = false;
                btnDelete.Enabled = true;
                btnUpdate.Enabled = true;
                clear.Enabled = true;
                dataGridView.Refresh();
            }
            
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void staff_Click(object sender, EventArgs e)
        {
            loadDatagridView();
            panel2.BringToFront();
            panel3.SendToBack();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            
            panel3.BringToFront();
            panel2.SendToBack();
            panel1.SendToBack();
        }

     

        private void button1_Click(object sender, EventArgs e)
        {
            loadDatagridView();
            dataGridView1.Refresh();
            panel1.BringToFront();
            panel2.SendToBack();
            panel3.SendToBack();
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
          
            panel3.BringToFront();
            panel2.SendToBack();
            panel1.SendToBack();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void button1_Click_2(object sender, EventArgs e)
        {
                
            DateTime nn;
            var staffs = db.Staffs.ToList();
            hourExtra = new float[staffs.Count];
            for (int i = 0; i < staffs.Count; i++)
            {
                    if (dataGridView1.Rows[i].Cells[7].Value != null)
                    {
                        //staffs[i].hoursofMonth += Int32.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                        hourExtra[i] += float.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString(), CultureInfo.InvariantCulture);
                    }
                    else
                        hourExtra[i] = 0;
                    staffs[i].hoursofMonth += hourExtra[i];
                    
                    db.SaveChanges();
                    listShift();
                
                
            }
            
            MessageBox.Show("Đã Lưu");
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            timethisWeek(this.dtpEndDate.Value.AddDays(2));
            loadDatagridView();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timethisWeek(this.dtpStartingDate.Value.AddDays(-2));
            loadDatagridView();
        }

        private void coffeemanagerDBEntitiesBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime dteStart = this.dtpStartingDate.Value;

            // Find out if the user selected a day that is not Monday
            if (dteStart.DayOfWeek != DayOfWeek.Monday)
            {
                MessageBox.Show("Ngày bắt đầu phải là thứ 2 !");

                timethisWeek(dteStart);
                loadDatagridView();
            }
            else
            {
                timethisWeek(dteStart);
                loadDatagridView();
            }
           
            
            
        }
        public void timethisWeek(DateTime x)
        {
            DateTime dteStart;
            //DateTime dteEnd;
            //var s = db.dateTables.Find(todayDate);
            //var now = DateTime.Today;
            // x = todayDate;
            if (x.DayOfWeek == DayOfWeek.Monday)
            {

                dtpStartingDate.Value = x;
                dteStart = this.dtpStartingDate.Value;
                dtpEndDate.Value = x.AddDays(6);
            } else
            if (x.DayOfWeek == DayOfWeek.Sunday)
            {

                this.dtpEndDate.Value = x;

                dtpStartingDate.Value = x.AddDays(-6);
                dteStart = this.dtpStartingDate.Value;
            } else
            
            { 

                var firstDayOfWeek = Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
                if (x.DayOfWeek == DayOfWeek.Sunday && firstDayOfWeek != DayOfWeek.Sunday)
                    x = x.AddDays(-1);
                this.dtpStartingDate.Value = x.AddDays(-(x.DayOfWeek - DayOfWeek.Monday));
                while (x.DayOfWeek != DayOfWeek.Monday)
                    x = x.AddDays(-1);
                this.dtpEndDate.Value = x.AddDays(-(x.DayOfWeek - DayOfWeek.Sunday - 7));
                dteStart = this.dtpStartingDate.Value;
            }
            //Lable Day
            t2.Text = dteStart.Day + "/" + dteStart.Month;
            t3.Text = dteStart.AddDays(1).Day + "/" + dteStart.AddDays(1).Month;
            t4.Text = dteStart.AddDays(2).Day + "/" + dteStart.AddDays(2).Month;

            t5.Text = dteStart.AddDays(3).Day + "/" + dteStart.AddDays(3).Month;

            t6.Text = dteStart.AddDays(4).Day + "/" + dteStart.AddDays(4).Month;

            t7.Text = dteStart.AddDays(5).Day + "/" + dteStart.AddDays(5).Month;

            t8.Text = dteStart.AddDays(6).Day + "/" + dteStart.AddDays(6).Month;

           
        }
        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime dteStart = this.dtpStartingDate.Value;
            // Get the ending date
            DateTime dteEnd = this.dtpEndDate.Value;



            // Find the number of days that separate the start and end
            TimeSpan timeDifference = dteEnd.Subtract(dteStart);
            double sixDaysLater = timeDifference.Days;


            if ((dteEnd.DayOfWeek != DayOfWeek.Sunday) && (sixDaysLater != 6))
            {             
                timethisWeek(dteEnd);
                loadDatagridView();
            }
            else
            {
                timethisWeek(dteEnd);
                loadDatagridView();
            }

            

        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception.Message == "DataGridViewComboBoxCell value is not valid.")
            {
                object value = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (!((DataGridViewComboBoxColumn)dataGridView1.Columns[e.ColumnIndex]).Items.Contains(value))
                {
                    ((DataGridViewComboBoxColumn)dataGridView1.Columns[e.ColumnIndex]).Items.Add(value);
                    e.ThrowException = false;
                }
            }
           
        }

        private void countbtn_Click(object sender, EventArgs e)
        {
            countShifts();
            listShift();
        }
        public void listShift()
        {
            
            var query = from log in db.Schedules
                        join l in db.Staffs on log.staffID equals l.staffID
                        where log.dateID >= dtpStartingDate.Value && log.dateID <= dtpEndDate.Value && log.shifts != "OFF"
                        group log by l.staffName into grouping
                        orderby grouping.Count() descending
                        select new { ID = grouping.Key, SoNgayLam = grouping.Count() };

            var result = query.Take(100);
            dataGridView2.DataSource = result.ToList();
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

       

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel4.BringToFront();
            setTimeSalary();
            
        }
        public void setTimeSalary()
        {
            //DateTime date = DateTime.Today;
            DateTime firstDayOfMonth = new DateTime(todayDate.Year, todayDate.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            //MessageBox.Show(firstDayOfMonth.ToString() + "\n" + lastDayOfMonth.ToString());
            dtpFrom.Value = firstDayOfMonth;
            this.dtpTo.Value = lastDayOfMonth;
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            panel3.BringToFront();

        }
        public void calculateSalary()
        {
            dataGridView3.AutoResizeColumns();
            TimeSpan timeDifference = dtpTo.Value.Subtract(dtpFrom.Value);
            int daysLater = timeDifference.Days;
            int k;
            var staffs = db.Staffs.ToList();
            k = 2;
            for (DateTime i = dtpFrom.Value; i <= dtpTo.Value; i = i.AddDays(1))
            {
                    dataGridView3.Columns.Add(new DataGridViewColumn() { HeaderText = i.Day.ToString() + "/" + i.Month.ToString(), CellTemplate = new DataGridViewTextBoxCell(), Width = 40 });
                    for (int j = 0; j < staffs.Count; j++)
                    {
                        int a = staffs[j].staffID;
                        //var h1 = staffs[j].hoursofMonth;
                        var h1 = db.Schedules.Where(x => x.staffID == a && x.dateID == i).Select(o => o.hoursPerShifts).FirstOrDefault();
                        if(h1 == null) //Nho them vao
                        {
                            h1 = 0;
                        }
                        dataGridView3.Rows[j].Cells[k].Value = h1;
                    }
                    k++;
            }
            dataGridView3.Columns.Add(new DataGridViewColumn() { HeaderText = "Tổng Số Giờ ", CellTemplate = new DataGridViewTextBoxCell() });
            dataGridView3.Columns.Add(new DataGridViewColumn() { HeaderText = "Tổng Số Giờ Cộng Thêm", CellTemplate = new DataGridViewTextBoxCell() });
            dataGridView3.Columns.Add(new DataGridViewColumn() { HeaderText = "Tổng Tiền Lương", CellTemplate = new DataGridViewTextBoxCell() });
            int l;
            for (int i = 0; i < staffs.Count; i++)
            {
                l = 0;
                for (int j = 2; j < k; j++)
                {
                    l = l + Int32.Parse(dataGridView3.Rows[i].Cells[j].Value.ToString(), CultureInfo.InvariantCulture);
                }
                dataGridView3.Rows[i].Cells[k].Value = l;
                dataGridView3.Rows[i].Cells[k + 1].Value = staffs[i].hoursofMonth.ToString();
                var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
                dataGridView3.Rows[i].Cells[k + 2].Value = String.Format(info, "{0:c}", ( l + staffs[i].hoursofMonth) * staffs[i].coefficientPay * 1000).ToString();
            }         
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            // this.dataGridView3 = new DataGridView();
            
        }

        private void generate_Click(object sender, EventArgs e)
        {
            int result = DateTime.Compare(dtpFrom.Value, dtpTo.Value);
            if (result >= 0)
            {
                MessageBox.Show("Ngày cuối cùng phải đừng sau ngày bắt đầu tính lương !! Vui lòng sửa lại !!");
                dtpTo.Focus();
            }
            else
            {
                dataGridView3.Columns.Clear();
                
                dataGridView3.DataSource = db.Staffs.Select(o => new
                { ID = o.staffID, Name = o.staffName }).ToList();
                calculateSalary();
                dataGridView3.Refresh();
                
            }
            
            
        }

        private void newMonth_Click(object sender, EventArgs e)
        {
            var staffs = db.Staffs.ToList();
            
            if (MessageBox.Show("Tổng số giờ của nhân viên tháng trước sẽ trở về 0. Xuất ra file EXCEL trước khi nhấn YES !!","Important Question", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                for (int j = 0; j < staffs.Count; j++)
                {
                    staffs[j].hoursofMonth = 0;
                }
                MessageBox.Show("Đã xong.");
                db.SaveChanges();
            }
            else
            {
                // user clicked no
            }
            
        }
      

        private void exportExcel_Click(object sender, EventArgs e)
        {
            bus.exportExel(dataGridView3);
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void t3to_tb_Click(object sender, EventArgs e)
        {

        }

        private void t4to_tb_Click(object sender, EventArgs e)
        {

        }

        private void t5to_tb_Click(object sender, EventArgs e)
        {

        }

        private void t6to_tb_Click(object sender, EventArgs e)
        {

        }

        private void t7to_tb_Click(object sender, EventArgs e)
        {

        }

        private void cnto_tb_Click(object sender, EventArgs e)
        {

        }

        private void t2to_tb_Click(object sender, EventArgs e)
        {

        }

        private void t2ch_tb_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

       
    }

       
    
}
