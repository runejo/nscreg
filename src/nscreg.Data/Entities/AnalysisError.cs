namespace nscreg.Data.Entities
{
    /// <summary>
    ///  Класс сущность анализ ошибок
    /// </summary>
    public abstract class AnalysisError
    {
        public int Id { get; set; }
        public int AnalysisLogId { get; set; }
        public string ErrorKey { get; set; }
        public string ErrorValue { get; set; }
        public virtual AnalysisLog AnalysisLog { get; set; }
    }
}