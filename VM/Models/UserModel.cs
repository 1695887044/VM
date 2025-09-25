

namespace VM.Start.Models
{
    public class UserModel:ModelBase
    {
        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 用户密码
        /// </summary>
        private string userPwd;

        public string UserPwd
        {
            get { return userPwd; }
            set { userPwd = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 用户角色-权限
        /// </summary>
        public int Role {  get; set; }
    }
}
