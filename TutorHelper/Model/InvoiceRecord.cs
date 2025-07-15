namespace TutorHelper.Model
{
    public class InvoiceRecord
    {
        public StudentLessonLink StudentLessonLink { get; set; }
        public TimeOnly LessonTime { get; set; }
        public DateOnly LessonDate { get; set; }
        public DateOnly InvoiceDate { get; set; }

        public InvoiceRecord()
        {
        }

        public InvoiceRecord (StudentLessonLink sllink, TimeOnly lessonTime, DateOnly lessonDate, DateOnly invoiceDate)
        {
            StudentLessonLink = sllink;
            LessonTime = lessonTime;
            LessonDate = lessonDate;
            InvoiceDate = invoiceDate;
        }

        public string CustomizeInvoiceFileName()
        {
            return $"invoice_{StudentLessonLink.Student.StudentName}_{LessonDate.ToString("ddMMyyyy")}";
        }
    }
}
