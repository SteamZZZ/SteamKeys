namespace SteamKeysApp.Services;

public class CurrencyService
{
    public double UmToRub(double rub) => rub * 60.02; 
    public double TrToRub(double rub) => rub * 3.28;  
    public double KzToRub(double rub) => rub * 0.1257;
}
