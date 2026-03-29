

namespace VM.Start.Models
{
    public class UserAccount:ModelBase
    {
        /// <summary>
        /// 用户账号
        /// </summary>
        public string AccountId { get; set; }
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
        private string _password;

        public string PasswordHash
        {
            get { return _password; }
            set { _password = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 用户角色-权限
        /// </summary>
        public int Role {  get; set; }
    }
}
