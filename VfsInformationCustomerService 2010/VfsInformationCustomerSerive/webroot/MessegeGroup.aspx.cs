using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MessegeGroup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.Store1.DataSource = this.Jobs;
        //this.Store1.DataBind();
    }
    //private List<Job> Jobs
    //{
    //    get
    //    {
    //        List<Job> jobs = new List<Job>();

    //        for (int i = 1; i <= 50; i++)
    //        {
    //            jobs.Add(new Job(
    //                        i,
    //                        "Task" + i.ToString(),
    //                        DateTime.Today.AddDays(i),
    //                        DateTime.Today.AddDays(i + i),
    //                        (i % 3 == 0)));
    //        }

    //        return jobs;
    //    }
    //}
}
//public class Job
//{
//    public Job(int id, string name, DateTime start, DateTime end, bool completed)
//    {
//        this.ID = id;
//        this.Name = name;
//        this.Start = start;
//        this.End = end;
//        this.Completed = completed;
//    }

//    public int ID { get; set; }
//    public string Name { get; set; }
//    public DateTime Start { get; set; }
//    public DateTime End { get; set; }
//    public bool Completed { get; set; }
//}