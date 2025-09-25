

namespace VM.Start.Models
{
    public class ModelBase:BindableBase
    {
		private long id;

		public long Id
		{
			get { return id; }
			set { id = value; }
		}
        private Guid token;

        public Guid Token
        {
            get { return token; }
            set { token = value; }
        }
		private DateTime createTime;

		public DateTime CreateTime
		{
			get { return createTime; }
			set { createTime = value; }
		}
		private DateTime updateTime;

		public DateTime Updatetime
		{
			get { return updateTime; }
			set { updateTime = value; }
		}


	}
}
