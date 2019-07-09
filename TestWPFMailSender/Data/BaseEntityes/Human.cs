namespace MailSender.Data.BaseEntityes
{
    /// <summary>
    /// Сущность-персоналия
    /// </summary>
    public abstract class Human : NamedEntity
    {
        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        public string Address { get; set; }
    }
}
