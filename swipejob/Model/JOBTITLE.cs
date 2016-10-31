using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwipeJob.Model
{
    public class JOBTITLE
    {
        /// <summary>
        /// job title id key
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string JOB_TITLE_NAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int JOB_TITLE_PARENT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte JOB_TITLE_LEVEL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string USERID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CREATE_DATE { get; set; }

 //       JOB_TITLE_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
 //   JOB_TITLE_NAME NVARCHAR(255) NULL,
	//JOB_TITLE_PARENT INT NULL,
	//JOB_TITLE_LEVEL TINYINT NULL,
	//USERID VARCHAR(255),
	//CREATE_DATE[datetime2](7) NOT NULL DEFAULT(GETDATE())

        
    }
}
