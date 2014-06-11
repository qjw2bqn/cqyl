using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TableToTable
{
    public partial class Form1 : Form
    {
        private static  CDKCYOAEntities entitiesCDKCY = new CDKCYOAEntities();
        private static  chmisdbEntities entitiesChmis = new chmisdbEntities();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //UserInfoToUser();
            UserField();
        }

        #region .表处理

        /// <summary>
        /// 将UserInfo表复制到User表中
        /// </summary>
        private void UserInfoToUser()
        {
            foreach (var t in entitiesChmis.userinfoes)
            {
                try
                {
                    if (t.us_name != "曹向健" && t.us_user != "admin")
                    {
                        User user = new User
                        {
                            UserName = t.us_user,
                            UserPwd = t.us_password,
                            UserUsbhd = t.us_usbkeyhd,
                            UserIsUsbKey = (t.us_isusbkey == "是" ? 1 : 0).ToString(),
                            StaffName = t.us_name,
                            StaffDepartmentID = int.Parse(t.de_code),
                            UserSpdeCode = t.us_spdecode,
                            UserRole = int.Parse(t.ro_code),
                            StaffSex = t.us_sex == "男" ? 1 : 0,
                            //StaffBirthDay = DateTime.Parse(t.us_birthday),
                            //StaffPositionID = int.Parse(t.po_code),
                            StaffEmail = t.us_email,
                            StaffHousePhone = t.us_phone,
                            StaffPhone = t.us_mobile,
                            UserStatus = t.us_state == "正常" ? 1 : 0,
                            UserCreateTime = DateTime.Parse(t.us_createtime),
                            StaffNation = t.us_nationality,
                            StaffNativePlace = t.us_nativeplace,
                            StaffBirthPlace = t.us_homeplace,
                            StaffPoliticalStatus = t.us_publicist,
                            StaffMarryStatus = t.us_marriage == "否" ? 0 : 1,
                            StaffHighestEducation = t.us_lastlydegree,
                            StaffGraduate = t.us_graduateschool,
                            //StaffGraducationTime = DateTime.Parse(t.us_graduatetime),
                            //StaffComeUnitTime = DateTime.Parse(t.us_comeunittime),
                            StaffFileAddr = t.us_archives,
                            StaffHouseholdAddr = t.us_census,
                            StaffIndentyCardID = t.us_identity,
                            StaffProfession = t.us_speciality,
                            StaffTitle = t.us_competence,
                            StaffAddr = t.us_homeaddress,
                            StaffPageSize = t.us_pagesize,
                            StaffIds = t.no_ids,
                            StaffComment = t.us_remarks
                        };
                        entitiesCDKCY.Users.Add(user);
                        entitiesCDKCY.SaveChanges();
                        System.Threading.Thread.Sleep(100);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(t.us_user + ";" + t.us_name);
                    continue;
                }
            }    
        }


        private void UserField()
        {        
            foreach (var u in entitiesCDKCY.Users.Where(t => t.UserPwd != string.Empty).ToList().Take(1))
            {
                //if (u.UserPwd != string.Empty)
                //{

                //    string id = (u.StaffDepartmentID > 1000 || (u.StaffDepartmentID >= 10 && u.StaffDepartmentID <= 100))
                //                    ? u.StaffDepartmentID.ToString()
                //                    : string.Format("0{0}", u.StaffDepartmentID);
                //    department d = GetDepartment(id);
                //    if (d == null) continue;
                //    u.StaffDepartmentName = d.de_name;
                //    Departmente d12 = entitiesCDKCY.Departmentes.Where(t => t.name == d.de_name).FirstOrDefault();
                //    if (d12 == null) continue;
                //    u.StaffDepartmentID = d12.id;
                //    try
                //    {
                //        u.StaffDepartmentName = "地理信息分院";
                //        entitiesCDKCY.SaveChanges();
                //    }
                //    catch (Exception ex)
                //    {
                //        Console.WriteLine(ex.Message);
                //    }
                //}
            }         
        }


        private department GetDepartment(string de_code)
        {
            try
            {
                if (string.IsNullOrEmpty(de_code))
                    return null;
                else
                    return entitiesChmis.departments.Where(t => t.de_code == de_code).FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private User GetUser(string key)
        {
            return entitiesCDKCY.Users.Where(t => t.UserName == key).FirstOrDefault();
        }

        #endregion
    }
}
