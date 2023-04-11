using System.Text.Json;

public class Program
{
    private static void Main(String[] args)
    {

    }
}

public class BankTransferConfig
{
    public bank bank { get; set; }
    public BankTransferConfig() { }

    public BankTransferConfig(bank bank)
    {
        this.bank = bank;
    }

    public void readJSON()
    {
        String txt = File.ReadAllText(@"./bank_transfer_config.json");
        bank = JsonSerializer.Deserialize<bank>(txt);
    }

    public void setDefault()
    {
        List<String> methods = new List<string> { "RTO (real-time)", "SKN", "RTGS", "BI FAST" };
        bank = new bank("en", 2500000, 6500, 15000, methods, "yes", "ya");
    }

    public void writeConfig()
    {
        var option = new JsonSerializerOptions { WriteIndented = true };
        String jsonstr = JsonSerializer.Serialize(bank, option);
        File.WriteAllText(@"./bank_transfer_config.json", jsonstr);
    }

    public void changeLang()
    {
        if(bank.lang == "en")
        {
            bank.lang = "id";
            writeConfig();
            return;
        }
        bank.lang = "en";
        writeConfig();
        return;
    }
}

public class bank
{
    public bank() { }
    public string lang { get; set; }
    public transfer transfer { get; set; }
    public List <String> methods { get; set; }
    public confirmation confirmation { get; set; }

    public bank (string lang, int treshold, int low_fee, int high_fee, List<String> methods, string en, string id)
    {
        this.lang = lang;
        transfer = new transfer(treshold, low_fee, high_fee);
        this.methods = methods;
        confirmation = new confirmation (en, id);
    }
}

public class transfer
{
    public int treshold { get; set; }
    public int low_fee {get; set; }
    public int high_fee { get; set; }

    public transfer(int treshold, int low_fee, int high_fee)
    {
        this.treshold = treshold;
        this.low_fee = low_fee;
        this.high_fee = high_fee;
    }
}

public class methods
{
    public List<methods> Methods { get; set; }
}

public class confirmation
{
    public string en { get; set; }
    public string id { get; set; }

    public confirmation(string en, string id)
    {
        this.en = en;
        this.id = id;
    }
}
