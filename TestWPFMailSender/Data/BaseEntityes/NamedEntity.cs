namespace MailSender.Data.BaseEntityes
{
    /// <summary>
    /// Именованная сущность
    /// </summary>
    public abstract class NamedEntity : Entity
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Коменатрий (описание)
        /// </summary>
        public string Comment { get; set; }
    }
}
