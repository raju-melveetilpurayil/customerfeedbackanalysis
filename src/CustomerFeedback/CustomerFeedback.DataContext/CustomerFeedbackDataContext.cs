using CustomerFeedback.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerFeedback.DataContext
{
    public class CustomerFeedbackDataContext:DbContext
    {
        public CustomerFeedbackDataContext(DbContextOptions<CustomerFeedbackDataContext> options)
            : base(options)
        {

        }
        public DbSet<Feedback> Feedbacks { get; set; }
    }
}
