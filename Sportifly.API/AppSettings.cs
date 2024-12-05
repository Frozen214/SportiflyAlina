namespace Sportifly.API;


/// <summary>
/// Набор настроек из appSetting.json
/// </summary>
public class AppSettings
{
    /// <summary>
    /// Строки подключения
    /// </summary>
    public ConnectionStrings ConnectionStrings { get; set; }

}
public class ConnectionStrings
{
    /// <summary>
    /// Подключение к MS SQL
    /// </summary>
    public string MsSqlConnection { get; set; }
}
