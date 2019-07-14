using System.ComponentModel;

namespace MailSender.Data.BaseEntityes
{
    public enum SendStatusEnum : byte
    {
        [Description("Статус: Не определён")]
        Unknown = 0,
        [Description("Статус: Выполняется")]
        Processing = 1,
        [Description("Статус: Отправлено")]
        Ok = 2,
        [Description("Статус: Ошибка")]
        Error = 3,
        [Description("Статус: Частично отправлено")]
        Partialy = 4,
        [Description("Статус: Отмена")]
        Canceled = 5
    }
}
